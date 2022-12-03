using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class EditorTrainService : IEditorTrain
    {
        private IUnitOfWork db;
        public EditorTrainService(IUnitOfWork db)
        {
            this.db = db;
        }
        List<TypeOfVanModel> typeOfVanModels = null;
        public List<TypeOfVanModel> TypeOfVanModels
        {
            get => typeOfVanModels ?? (typeOfVanModels = db.TypeOfVan.GetList().Select(i => new TypeOfVanModel(i)).ToList());
        }
        ObservableCollection<VanModel> vans = new ObservableCollection<VanModel>();
        public ObservableCollection<VanModel> Vans
        {
            get => vans;
        }
        List<StationModel> stationModels = null;
        public List<StationModel> StationModels
        {
            get => stationModels ?? (stationModels = db.Station.GetList().Select(i => new StationModel(i)).ToList());
        }
        ObservableCollection<ModelForEditingSchedule> stationTrainSchedule = new ObservableCollection<ModelForEditingSchedule>();
        public ObservableCollection<ModelForEditingSchedule> StationTrainScheduleModels
        {
            get => stationTrainSchedule;
        }
        ObservableCollection<DateTimeModel> dateTimesForDeparture = new ObservableCollection<DateTimeModel>();
        public ObservableCollection<DateTimeModel> DateTimesForDeparture
        {
            get => dateTimesForDeparture;
        }
        public ICommand AddStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                dateTimesForDeparture.Add(new DateTimeModel() { DateTime = DateTime.Now });
            });
        }
        public ICommand RemoveStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                if (obj is DateTimeModel dateTime)
                    dateTimesForDeparture.Remove(dateTime);
            });
        }
        public ICommand AddVan
        {
            get => new RelayCommand(obj =>
            {
                vans.Add(new VanModel() { NumberInTrain = ((vans.LastOrDefault()?.NumberInTrain) ?? 0) + 1 });
            });
        }
        public ICommand RemoveVan
        {
            get => new RelayCommand(obj =>
            {
                if (obj is VanModel vanModel)
                {
                    vans.Remove(vanModel);
                    for (int i = vanModel.NumberInTrain - 1; i < vans.Count; i++)
                        vans[i].NumberInTrain--;
                }
            });
        }
        public ICommand AddTrain
        {
            get => new RelayCommand(obj =>
            {
                //vans.Add(new VanModel());
            });
            		

        }
        public ICommand AddStationInSchedule
        {
            get => new RelayCommand(obj =>
            {
                stationTrainSchedule.Add(new ModelForEditingSchedule()
                {
                    PreviousModel = stationTrainSchedule.LastOrDefault() ?? new ModelForEditingSchedule() { DepartureTime=DateTime.Now},
                    StationTrainSchedule = new StationTrainScheduleModel() { NumberInTrip = stationTrainSchedule.Count + 1, IdStation = -1 }
                });
                stationTrainSchedule[stationTrainSchedule.Count - 1].ArrivalTime = stationTrainSchedule[stationTrainSchedule.Count - 1].DepartureTime =
                stationTrainSchedule[stationTrainSchedule.Count - 1].PreviousModel.DepartureTime;
            //vans.Add(new Van());
        });
        }
        public ICommand RemoveStationFromSchedule
        {
            get => new RelayCommand(obj =>
            {
                if(obj is ModelForEditingSchedule modelForEditingSchedule)
                {
                    stationTrainSchedule.Remove(modelForEditingSchedule);
                    for (int i = modelForEditingSchedule.StationTrainSchedule.NumberInTrip - 1; i < stationTrainSchedule.Count; i++)
                        stationTrainSchedule[i].StationTrainSchedule.NumberInTrip--;
                    if (modelForEditingSchedule.StationTrainSchedule.NumberInTrip - 1 >= 0 && modelForEditingSchedule.StationTrainSchedule.NumberInTrip - 1 < stationTrainSchedule.Count)
                        stationTrainSchedule[modelForEditingSchedule.StationTrainSchedule.NumberInTrip - 1].PreviousModel = modelForEditingSchedule.PreviousModel;
                }
            });
        }
    }
}
