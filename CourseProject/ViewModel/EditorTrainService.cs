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

namespace CourseProject.ViewModel
{
    public class EditorTrainService : IEditorTrain
    {
        private User currentUser;
        private IUnitOfWork db;
        public EditorTrainService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void GetUser(User user) => this.currentUser = user;
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
        ObservableCollection<ModelForEditingSchedule> ModelForEditingScheduleCollection = new ObservableCollection<ModelForEditingSchedule>();
        public ObservableCollection<ModelForEditingSchedule> StationTrainScheduleModels
        {
            get => ModelForEditingScheduleCollection;
        }
        ObservableCollection<DateTimeModel> dateTimesForDeparture = new ObservableCollection<DateTimeModel>();
        public ObservableCollection<DateTimeModel> DateTimesForDeparture
        {
            get => dateTimesForDeparture;
        }
        private TrainModel currentTrainModel = new TrainModel() { LoadedInDB = false };
        public void EditTrain(TrainModel trainModel)
        {
            currentTrainModel = trainModel;
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
                if (currentTrainModel.LoadedInDB)
                {

                }
                else
                {
                    Train TrainForAdd = currentTrainModel.GetTrain();
                    TrainForAdd.IdUserCreator = currentUser.Id;
                    db.Train.Create(TrainForAdd);
                    vans.ToList().ForEach(van => TrainForAdd.Van.Add(van.GetVan()));
                    Track track = new DAL.Entities.Track();
                    TrainForAdd.Track.Add(track);
                    ModelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                    {
                        TrainForAdd.StationTrainSchedule.Add(modelForEditingSchedule.StationTrainScheduleModel.GetStationTrainSchedule());
                        track.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime, DepartureTime = modelForEditingSchedule.DepartureTime });
                    });
                    dateTimesForDeparture.ToList().ForEach(dateTimeModel =>
                    {
                        TrainForAdd.Track.Add(track = new Track());
                        TimeSpan dif = dateTimeModel.DateTime - ModelForEditingScheduleCollection[0].DepartureTime;
                        ModelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                        {
                            track.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime + dif, DepartureTime = modelForEditingSchedule.DepartureTime + dif });
                        });

                    }
                    );

                    db.Save();
                }
            });
            		

        }
        public ICommand AddStationInSchedule
        {
            get => new RelayCommand(obj =>
            {
                ModelForEditingScheduleCollection.Add(new ModelForEditingSchedule()
                {
                    PreviousModel = ModelForEditingScheduleCollection.LastOrDefault() ?? new ModelForEditingSchedule() { DepartureTime=DateTime.Now},
                    StationTrainScheduleModel = new StationTrainScheduleModel() { NumberInTrip = ModelForEditingScheduleCollection.Count + 1, IdStation = -1 }
                });
                ModelForEditingScheduleCollection[ModelForEditingScheduleCollection.Count - 1].ArrivalTime = ModelForEditingScheduleCollection[ModelForEditingScheduleCollection.Count - 1].DepartureTime =
                ModelForEditingScheduleCollection[ModelForEditingScheduleCollection.Count - 1].PreviousModel.DepartureTime;
            //vans.Add(new Van());
        });
        }
        public ICommand RemoveStationFromSchedule
        {
            get => new RelayCommand(obj =>
            {
                if(obj is ModelForEditingSchedule modelForEditingSchedule)
                {
                    ModelForEditingScheduleCollection.Remove(modelForEditingSchedule);
                    for (int i = modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1; i < ModelForEditingScheduleCollection.Count; i++)
                        ModelForEditingScheduleCollection[i].StationTrainScheduleModel.NumberInTrip--;
                    if (modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1 >= 0 && modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1 < ModelForEditingScheduleCollection.Count)
                        ModelForEditingScheduleCollection[modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1].PreviousModel = modelForEditingSchedule.PreviousModel;
                }
            });
        }
    }
}
