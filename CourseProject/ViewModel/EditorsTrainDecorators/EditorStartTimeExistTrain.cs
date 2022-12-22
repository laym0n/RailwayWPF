using CourseProject.Model.StaticModelsForPassInfo;
using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DAL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.EditorsTrainDecorators
{
    public class EditorStartTimeExistTrain : IProcesserDoUndo<Train>
    {
        IUnitOfWork db;
        IProcesserDoUndo<Train> processerDoUndo;
        public event Action<Train> ProcessComplete;
        public static event Action StartProcessVans;
        ObservableCollection<TimesForStationModel> startTimes = new ObservableCollection<TimesForStationModel>();

        
        Train trainForEdit;
        bool active = false;
        public EditorStartTimeExistTrain(IUnitOfWork db, IProcesserDoUndo<Train> processerDoUndo)
        {
            this.db = db;
            SetProcesserDoUndo(processerDoUndo);
        }
        void SetProcesserDoUndo(IProcesserDoUndo<Train> processerDoUndo)
        {
            if (processerDoUndo != null)
            {
                this.processerDoUndo = processerDoUndo;
                this.processerDoUndo.ProcessComplete += SetStartDataAndStartProcess;
            }
        }
        void SetStartDataAndStartProcess(Train trainForEdit)
        {
            SetStartData(trainForEdit);
            StartProcessVans?.Invoke();
        }
        void SetStartData(Train trainForEdit)
        {
            this.trainForEdit = trainForEdit;
            startTimes = new ObservableCollection<TimesForStationModel>();
            (from tracks in trainForEdit.Track
             join times in db.TimesForStation.GetList()
             on tracks.Id equals times.TrackId
             group times by tracks.Id into ways
             from tickets in db.Ticket.GetList()
             where ways.FirstOrDefault(i => i.Id == tickets.IdTimesForStationSource) == null
             select ways).ToList().ForEach(i => 
             {
                 TimesForStation time = i.Where(j => j.DepartureTime == i.Min(k => k.DepartureTime)).First();
                 startTimes.Add(new TimesForStationModel(time) { loadedInDb = true });
             });
            active = true;
        }
        public void CancelCurrentProcess()
        {
            if (active)
            {
                active = false;
                ClearData();
            }
            else if (processerDoUndo != null)
                processerDoUndo.CancelCurrentProcess();
        }
        void ClearData()
        {
            startTimes.Clear();
            trainForEdit = null;
        }
        public void StartProcess(Train train)
        {
            if (processerDoUndo != null)
            {
                active = false;
                processerDoUndo.StartProcess(train);
            }
            else
                SetStartDataAndStartProcess(train);
        }
        public object InfoForView
        {
            get
            {
                return !active && processerDoUndo != null ? processerDoUndo.InfoForView : (new InfoForPassToEditStartTimesPage()
                {
                    timesForStationModels = startTimes
                });
            }
        }
        public ICommand DoProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.DoProcess : new RelayCommand((obj) =>
            {
                startTimes.Add(new TimesForStationModel() { ArrivalTime = DateTime.Now, DepartureTime = DateTime.Now });
            });
        }
        public ICommand UndoProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.UndoProcess : new RelayCommand((obj) =>
            {
                if (obj is TimesForStationModel model)
                {
                    startTimes.Remove(model);
                }
            });
        }
        public ICommand СompleteProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.СompleteProcess : new RelayCommand((obj) =>
            {
                List<Track> tracksForRemove = new List<Track>();
                trainForEdit.Track.ToList().ForEach(i =>
                {
                    if(startTimes.FirstOrDefault(j=> j.loadedInDb && j.TrackId == i.Id) == null)
                        tracksForRemove.Add(i);
                });
                tracksForRemove.ForEach(i => trainForEdit.Track.Remove(i));
                TimesForStation firstTime = trainForEdit.Track.First().TimesForStation.First();
                List<TimesForStation> times = trainForEdit.Track.First().TimesForStation.ToList().Where(i => i.TrackId == firstTime.TrackId).OrderBy(i => i.DepartureTime).ToList();
                startTimes.ToList().ForEach(i =>
                {
                    if (i.loadedInDb)
                        return;
                    Track addedTrack = new Track();
                    trainForEdit.Track.Add(addedTrack);
                    var sub = times[0].DepartureTime - i.DepartureTime;
                    for (int j = 0; j < times.Count; j++)
                        addedTrack.TimesForStation.Add(new TimesForStation()
                        {
                            ArrivalTime = times[j].ArrivalTime - sub,
                            DepartureTime = times[j].DepartureTime - sub
                        });
                });
                ProcessComplete?.Invoke(trainForEdit);
            });
        }
    }
}
