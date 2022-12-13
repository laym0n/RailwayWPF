using CourseProject.Model;
using CourseProject.Model.ModelsForEditingViewStyle;
using CourseProject.View;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DAL.Entities;
using DLL.Interfaces;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class EditorTrainService : IEditorTrain
    {
        private User currentUser;
        public event Action TrainSaved;
        public event Action<TypeOfVanModel> VanChoosen;
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
        ObservableCollection<VanModel> vans = null;
        public ObservableCollection<VanModel> Vans
        {
            get => vans ?? (vans = new ObservableCollection<VanModel>());
        }
        List<StationModel> stationModels = null;
        public List<StationModel> StationModels
        {
            get => stationModels ?? (stationModels = db.Station.GetList().Select(i => new StationModel(i)).ToList());
        }
        ObservableCollection<ModelForEditingSchedule> modelForEditingScheduleCollection = null;
        public ObservableCollection<ModelForEditingSchedule> ModelForEditingScheduleCollection
        {
            get => modelForEditingScheduleCollection ?? (modelForEditingScheduleCollection = new ObservableCollection<ModelForEditingSchedule>());
        }
        ObservableCollection<TimesForStationModel> dateTimesForDeparture = null;
        public ObservableCollection<TimesForStationModel> DateTimesForDeparture
        {
            get => dateTimesForDeparture ?? (dateTimesForDeparture = new ObservableCollection<TimesForStationModel>());
        }
        private TrainModel currentTrainModel = null;
        private ButtonInfoTrainEditPage buttonInfo = new ButtonInfoTrainEditPage();
        public ButtonInfoTrainEditPage ButtonInfo { get; }

        public void EditTrain(TrainModel trainModel)
        {
            InfoButtonsOnTrainEditPage.Instance.IsEnable = false;
            currentTrainModel = trainModel;
            (
                from van in db.Van.GetList()
                where van.TrainId == trainModel.Id
                orderby van.NumberInTrain
                select new VanModel(van)).ToList().ForEach(i => vans.Add(i));
            (from StationTrainSchedule in db.StationTrainSchedule.GetList()
            where StationTrainSchedule.IdTrain == trainModel.Id
            orderby StationTrainSchedule.NumberInTrip
            select new StationTrainScheduleModel(StationTrainSchedule)).ToList().ForEach(i=> 
            modelForEditingScheduleCollection.Add(new ModelForEditingSchedule() { StationTrainScheduleModel = i }));
            (from track in db.Track.GetList()
             join timesForStation in db.TimesForStation.GetList()
             on track.Id equals timesForStation.TrackId
             where track.TrainId == trainModel.Id
             orderby timesForStation.DepartureTime
             group new TimesForStationModel(timesForStation) by track.Id).ToList()
             .Where(i => i.FirstOrDefault().DepartureTime > DateTime.Now).ToList().ForEach(i => dateTimesForDeparture.Add(i.FirstOrDefault()));
            if (dateTimesForDeparture.Count == 0)
            {
                dateTimesForDeparture.Add((from track in db.Track.GetList()
                                           join timesForStation in db.TimesForStation.GetList()
                                           on track.Id equals timesForStation.TrackId
                                           where track.TrainId == trainModel.Id
                                           orderby timesForStation.DepartureTime
                                           group new TimesForStationModel(timesForStation) by track.Id).FirstOrDefault().FirstOrDefault());
            }
            List<KeyValuePair<DateTime, DateTime>> times = 
                (from Times in db.TimesForStation.GetList()
             where Times.TrackId == dateTimesForDeparture.FirstOrDefault()?.TrackId
             orderby Times.DepartureTime
             select new KeyValuePair<DateTime, DateTime>(Times.ArrivalTime, Times.DepartureTime)).ToList();
            for (int i = 0; i < times.Count; i++)
            {
                modelForEditingScheduleCollection[i].ArrivalTime = times[i].Key;
                modelForEditingScheduleCollection[i].DepartureTime = times[i].Value;
            }

        }
        public void RemoveTrain(TrainModel trainModel)
        {
            Train train = trainModel.GetTrain();
            (from track in db.Track.GetList()
             where track.TrainId != trainModel.Id
             join times in db.TimesForStation.GetList()
             on track.Id equals times.TrackId
             orderby times.DepartureTime
             group times by track.Id).ToList()
             .ForEach(i =>
             {
                 if (i.FirstOrDefault().DepartureTime > DateTime.Now)
                 {
                     int idTrack = i.FirstOrDefault().TrackId;
                     i.ToList().ForEach(j => db.TimesForStation.Delete(j.Id));
                     db.Track.Delete(idTrack);
                 }
             });
            if(db.Track.GetList().Where(i=>i.TrainId == train.Id).Count() == 0)
            {
                db.Train.Delete(train.Id);
            }
        }
        public void SetDataWhenUserEnterPage(Page page)
        {
            if (page is TrainEditPage)
            {
                InfoButtonsOnTrainEditPage.Instance.IsEnable = true;
                currentTrainModel = new TrainModel() { LoadedInDB = false };
                buttonInfo.IsEnabledButtonForAddVan = buttonInfo.IsEnabledButtonForAddStationInSchedule = true;
            }
        }
        public void SetDataWhenUserLeavePage(Page page)
        {
            if(page is TrainEditPage)
            {
                vans.Clear();
                modelForEditingScheduleCollection.Clear();
                dateTimesForDeparture.Clear();
            }
        }
        public ICommand AddStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                dateTimesForDeparture.Add(new TimesForStationModel() { DepartureTime = DateTime.Now });
            });
        }
        public ICommand RemoveStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                if (obj is TimesForStationModel dateTime)
                {
                    if (dateTime != dateTimeModelForFirstStationTrainSchedule)
                        dateTimesForDeparture.Remove(dateTime);
                    else
                    {
                        TimesForStationModel NewTime = dateTimesForDeparture.FirstOrDefault(i => i != dateTimeModelForFirstStationTrainSchedule);
                        if(NewTime != null)
                        {
                            dateTimeModelForFirstStationTrainSchedule.DepartureTime = NewTime.DepartureTime;
                            dateTimesForDeparture.Remove(NewTime);
                        }
                        else
                        {
                            MessageBox.Show(currentTrainModel.LoadedInDB ? "Поезд можно удалить только из вашего профиля!" : "Чтобы удалить это время отправления создайте другое время отправления или удалите все станции!");
                        }
                    }

                }
            });
        }
        public ICommand AddVan
        {
            get => new RelayCommand(obj =>
            {
                vans.Add(new VanModel() { NumberInTrain = ((vans.LastOrDefault()?.NumberInTrain) ?? 0) + 1 });
                vans.Last().PropertyChanged += ChangeViewOfVan;
            });
        }
        void ChangeViewOfVan(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (changingTime && propertyChangedEventArgs.PropertyName == "TypeOfVanId")
            {
                VanChoosen?.Invoke(new TypeOfVanModel() { Id = (e as VanModel).TypeOfVanId });
            }
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
        bool ValidateDateBeforeAddTrain()
        {
            bool ok = true;
            if (!(ok = vans.Count > 0))
            {
                MessageBox.Show("Добвьте вагоны в поезд!");
                return ok;
            }
            if (!(ok = modelForEditingScheduleCollection.Count > 1))
            {
                MessageBox.Show("Добавьте больше станций в расписание остановок поезда!");
                return ok;
            }
            vans.ToList().ForEach(i =>
            {
                if (typeOfVanModels.FirstOrDefault(j => j.Id == i.TypeOfVanId) == null)
                    ok = false;
            });
            if (!ok)
            {
                MessageBox.Show("Запоните все типы вагонов, добавленных в поезд!");
                return ok;
            }
            ModelForEditingScheduleCollection.ToList().ForEach(i =>
            {
                if (stationModels.FirstOrDefault(j => j.Id == i.StationTrainScheduleModel.IdStation) == null)
                    ok = false;
            });
            if (!ok)
            {
                MessageBox.Show("Запоните все станции, добавленные в расписание поезда!");
                return ok;
            }
            ModelForEditingScheduleCollection.ToList().ForEach(i =>
            {
                if (ok && ModelForEditingScheduleCollection.Count(j => j.StationTrainScheduleModel.IdStation == i.StationTrainScheduleModel.IdStation) != 1)
                {
                    MessageBox.Show($"Станция {stationModels.First(j => j.Id == i.StationTrainScheduleModel.IdStation).Name} встречается в расписании несколько раз!");
                    ok = false;
                }
                if (ok && i.ArrivalTime < i.PreviousModel.DepartureTime)
                {
                    MessageBox.Show($"Время приезда на станцию {stationModels.First(j => j.Id == i.StationTrainScheduleModel.IdStation).Name} меньше чем на предыдущую станцию!");
                    ok = false;
                }
            });
            return ok;
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
                    if(!ValidateDateBeforeAddTrain())
                        return;
                    Train TrainForAdd = currentTrainModel.GetTrain();
                    TrainForAdd.IdUserCreator = currentUser.Id;
                    db.Train.Create(TrainForAdd);
                    vans.ToList().ForEach(van => TrainForAdd.Van.Add(van.GetVan()));
                    modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                    {
                        TrainForAdd.StationTrainSchedule.Add(modelForEditingSchedule.StationTrainScheduleModel.GetStationTrainSchedule());
                    });
                    dateTimesForDeparture.ToList().ForEach(dateTimeModel =>
                    {
                        Track track = new DAL.Entities.Track();
                        TrainForAdd.Track.Add(track);
                        TimeSpan dif = dateTimeModel.DepartureTime - modelForEditingScheduleCollection[0].DepartureTime;
                        modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                        {
                            track.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime + dif, DepartureTime = modelForEditingSchedule.DepartureTime + dif });
                        });

                    }
                    );

                    db.Save();
                    TrainSaved?.Invoke();
                }
            });
            		

        }
        bool changingTime = true;
        void ChangeTimeInStartTime(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (changingTime && propertyChangedEventArgs.PropertyName == "DepartureTime")
            {
                changingTime = false;
                dateTimeModelForFirstStationTrainSchedule.DepartureTime = modelForEditingScheduleCollection[0].DepartureTime;
                changingTime = true;
            }
        }
        void ChangeTimeInStationTrainSchedule(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if(changingTime && propertyChangedEventArgs.PropertyName == "DepartureTime")
            {
                changingTime = false;
                TimeSpan dif = modelForEditingScheduleCollection[0].DepartureTime - dateTimeModelForFirstStationTrainSchedule.DepartureTime;
                modelForEditingScheduleCollection.ToList().ForEach(i =>
                {
                    i.DepartureTime -= dif;
                    i.ArrivalTime -= dif;
                });
                changingTime = true;
            }
        }
        private TimesForStationModel dateTimeModelForFirstStationTrainSchedule;
        void SetDateTimeForFirstStationTrainSchedule()
        {
            modelForEditingScheduleCollection[0].PropertyChanged += ChangeTimeInStartTime;
            dateTimeModelForFirstStationTrainSchedule.DepartureTime = modelForEditingScheduleCollection[0].DepartureTime;
        }
        void UnSetDateTimeForFirstStationTrainSchedule()
        {
            dateTimeModelForFirstStationTrainSchedule.PropertyChanged -= ChangeTimeInStationTrainSchedule;
            dateTimesForDeparture.Remove(dateTimeModelForFirstStationTrainSchedule);
        }
        public ICommand AddStationInSchedule
        {
            get => new RelayCommand(obj =>
            {
                modelForEditingScheduleCollection.Add(new ModelForEditingSchedule()
                {
                    PreviousModel = modelForEditingScheduleCollection.LastOrDefault() ?? new ModelForEditingSchedule() { DepartureTime = DateTime.Now },
                    StationTrainScheduleModel = new StationTrainScheduleModel() { NumberInTrip = modelForEditingScheduleCollection.Count + 1, IdStation = -1 }
                });
                modelForEditingScheduleCollection[modelForEditingScheduleCollection.Count - 1].ArrivalTime = modelForEditingScheduleCollection[modelForEditingScheduleCollection.Count - 1].DepartureTime =
                modelForEditingScheduleCollection[modelForEditingScheduleCollection.Count - 1].PreviousModel.DepartureTime;
                if (modelForEditingScheduleCollection.Count == 1)
                {
                    dateTimeModelForFirstStationTrainSchedule = new TimesForStationModel();
                    SetDateTimeForFirstStationTrainSchedule();
                    dateTimesForDeparture.Add(dateTimeModelForFirstStationTrainSchedule);
                    dateTimeModelForFirstStationTrainSchedule.PropertyChanged += ChangeTimeInStationTrainSchedule;
                }
            });
        }
        public ICommand RemoveStationFromSchedule
        {
            get => new RelayCommand(obj =>
            {
                if(obj is ModelForEditingSchedule modelForEditingSchedule)
                {
                    modelForEditingScheduleCollection.Remove(modelForEditingSchedule);
                    for (int i = modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1; i < modelForEditingScheduleCollection.Count; i++)
                        modelForEditingScheduleCollection[i].StationTrainScheduleModel.NumberInTrip--;
                    if (modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1 >= 0 && modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1 < modelForEditingScheduleCollection.Count)
                        modelForEditingScheduleCollection[modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip - 1].PreviousModel = modelForEditingSchedule.PreviousModel;
                    if(modelForEditingSchedule.StationTrainScheduleModel.NumberInTrip == 1)
                    {
                        modelForEditingSchedule.PropertyChanged -= ChangeTimeInStartTime;
                        if (modelForEditingScheduleCollection.Count > 0)
                            SetDateTimeForFirstStationTrainSchedule();
                        else
                            UnSetDateTimeForFirstStationTrainSchedule();
                    }
                }
            });
        }
    }
}
