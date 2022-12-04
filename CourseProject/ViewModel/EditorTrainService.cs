using CourseProject.Model;
using CourseProject.Model.ModelsForEditingViewStyle;
using CourseProject.View;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DAL.Entities;
using DLL.Interfaces;
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
        ObservableCollection<DateTimeModel> dateTimesForDeparture = null;
        public ObservableCollection<DateTimeModel> DateTimesForDeparture
        {
            get => dateTimesForDeparture ?? (dateTimesForDeparture = new ObservableCollection<DateTimeModel>());
        }
        private TrainModel currentTrainModel = null;
        private ButtonInfoTrainEditPage buttonInfo = new ButtonInfoTrainEditPage();
        public ButtonInfoTrainEditPage ButtonInfo { get; }
        public void EditTrain(TrainModel trainModel)
        {
            currentTrainModel = trainModel;
            InfoButtonsOnTrainEditPage.Instance.IsEnable = false;
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
                dateTimesForDeparture.Add(new DateTimeModel() { DateTime = DateTime.Now });
            });
        }
        public ICommand RemoveStartTripDateTime
        {
            get => new RelayCommand(obj =>
            {
                if (obj is DateTimeModel dateTime)
                {
                    if (dateTime != dateTimeModelForFirstStationTrainSchedule)
                        dateTimesForDeparture.Remove(dateTime);
                    else
                    {
                        DateTimeModel NewTime = dateTimesForDeparture.FirstOrDefault(i => i != dateTimeModelForFirstStationTrainSchedule);
                        if(NewTime != null)
                        {
                            dateTimeModelForFirstStationTrainSchedule.DateTime = NewTime.DateTime;
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
                    ValidateDateBeforeAddTrain();
                    //Train TrainForAdd = currentTrainModel.GetTrain();
                    //TrainForAdd.IdUserCreator = currentUser.Id;
                    //db.Train.Create(TrainForAdd);
                    //vans.ToList().ForEach(van => TrainForAdd.Van.Add(van.GetVan()));
                    //Track track = new DAL.Entities.Track();
                    //TrainForAdd.Track.Add(track);
                    //modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                    //{
                    //    TrainForAdd.StationTrainSchedule.Add(modelForEditingSchedule.StationTrainScheduleModel.GetStationTrainSchedule());
                    //    track.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime, DepartureTime = modelForEditingSchedule.DepartureTime });
                    //});
                    //dateTimesForDeparture.ToList().ForEach(dateTimeModel =>
                    //{
                    //    TrainForAdd.Track.Add(track = new Track());
                    //    TimeSpan dif = dateTimeModel.DateTime - modelForEditingScheduleCollection[0].DepartureTime;
                    //    modelForEditingScheduleCollection.ToList().ForEach(modelForEditingSchedule =>
                    //    {
                    //        track.TimesForStation.Add(new TimesForStation() { ArrivalTime = modelForEditingSchedule.ArrivalTime + dif, DepartureTime = modelForEditingSchedule.DepartureTime + dif });
                    //    });

                    //}
                    //);

                    //db.Save();
                }
            });
            		

        }
        bool changingTime = true;
        void ChangeTimeInStartTime(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (changingTime && propertyChangedEventArgs.PropertyName == "DepartureTime")
            {
                changingTime = false;
                dateTimeModelForFirstStationTrainSchedule.DateTime = modelForEditingScheduleCollection[0].DepartureTime;
                changingTime = true;
            }
        }
        void ChangeTimeInStationTrainSchedule(object e, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if(changingTime && propertyChangedEventArgs.PropertyName == "DateTime")
            {
                changingTime = false;
                TimeSpan dif = modelForEditingScheduleCollection[0].DepartureTime - dateTimeModelForFirstStationTrainSchedule.DateTime;
                modelForEditingScheduleCollection.ToList().ForEach(i =>
                {
                    i.DepartureTime -= dif;
                    i.ArrivalTime -= dif;
                });
                changingTime = true;
            }
        }
        private DateTimeModel dateTimeModelForFirstStationTrainSchedule;
        void SetDateTimeForFirstStationTrainSchedule()
        {
            modelForEditingScheduleCollection[0].PropertyChanged += ChangeTimeInStartTime;
            dateTimeModelForFirstStationTrainSchedule.DateTime = modelForEditingScheduleCollection[0].DepartureTime;
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
                    dateTimeModelForFirstStationTrainSchedule = new DateTimeModel();
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
