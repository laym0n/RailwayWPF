using CourseProject.Model.StaticModelsForPassInfo;
using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using DAL.Entities;

namespace CourseProject.ViewModel.EditorsTrainDecorators
{
    public class EditorStationTrainSchedule : IProcesserDoUndo<Train>
    {
        IUnitOfWork db;
        IProcesserDoUndo<Train> processerDoUndo;
        public event Action<Train> ProcessComplete;
        public static event Action StartProcessVans;
        ObservableCollection<ModelForEditingSchedule> schedules = new ObservableCollection<ModelForEditingSchedule>();

        List<StationModel> stationModels;
        Train trainForEdit;
        bool active = false;
        public EditorStationTrainSchedule(IUnitOfWork db, IProcesserDoUndo<Train> processerDoUndo)
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
            schedules = new ObservableCollection<ModelForEditingSchedule>();
            stationModels = db.Station.GetList().Select(i => new StationModel(i)).ToList();
            active = true;
        }
        void ClearData()
        {
            schedules.Clear();
            stationModels.Clear();
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
                return !active && processerDoUndo != null ? processerDoUndo.InfoForView : (new InfoForPassToEditStationTrainSchedulePage()
                {
                    StationModels = stationModels,
                    collection = schedules
                });
            }
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
        public ICommand DoProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.DoProcess : new RelayCommand((obj) =>
            {
                ModelForEditingSchedule vanModelForAdd = new ModelForEditingSchedule()
                {
                    ArrivalTime = DateTime.Now,
                    DepartureTime = DateTime.Now,
                    StationTrainScheduleModel = new StationTrainScheduleModel()
                    {
                        NumberInTrip = schedules.Count + 1
                    },
                    PreviousModel = schedules.Count != 0? schedules.Last() : new ModelForEditingSchedule() { DepartureTime = DateTime.Now},
                };
                schedules.Add(vanModelForAdd);
            });
        }
        public ICommand UndoProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.UndoProcess : new RelayCommand((obj) =>
            {
                if (obj is ModelForEditingSchedule model)
                {
                    schedules.Remove(model);
                    if (schedules.Count >= model.StationTrainScheduleModel.NumberInTrip)
                        schedules[model.StationTrainScheduleModel.NumberInTrip - 1].PreviousModel = model.PreviousModel;
                    for (int i = model.StationTrainScheduleModel.NumberInTrip - 1; i < schedules.Count; i++)
                        schedules[i].StationTrainScheduleModel.NumberInTrip--;
                }
            });
        }
        public ICommand СompleteProcess
        {
            get => !active && processerDoUndo != null ? processerDoUndo.СompleteProcess : new RelayCommand((obj) =>
            {
                Track addedTrack = new Track();
                trainForEdit.Track.Add(addedTrack);
                schedules.ToList().ForEach(i =>
                {
                    trainForEdit.StationTrainSchedule.Add(i.StationTrainScheduleModel.GetStationTrainSchedule());
                    addedTrack.TimesForStation.Add(new TimesForStation()
                    {
                        ArrivalTime = i.ArrivalTime,
                        DepartureTime = i.DepartureTime
                    });
                });
                ProcessComplete?.Invoke(trainForEdit);
            }, (obj) =>
            {
                if (schedules.Count <= 1 || schedules.ToList().Any(i => stationModels.FirstOrDefault(j => j.Id == i.StationTrainScheduleModel.IdStation) == null
                || i.ArrivalTime <= i.PreviousModel.DepartureTime))
                    return false;
                return true;
            });
        }
    }
}
