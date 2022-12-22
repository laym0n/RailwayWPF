﻿using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using Ninject.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    internal class ProfileService: IInfoProfile
    {
        private UserModel currentUser;
        private IUnitOfWork unitOfWork;
        IStrategyAddPassengerForProfile strategyAddPassengerForProfile;
        ITicketContolStrategyInProfile ticketContolStrategyInProfile;
        public void SetCurrentUser(UserModel user) 
        { 
            currentUser = user; 
            if(strategyAddPassengerForProfile != null)
                this.strategyAddPassengerForProfile.SetUser(currentUser);
            if (this.ticketContolStrategyInProfile != null)
                this.ticketContolStrategyInProfile.SetUser(currentUser);
        }
        public void SetStrategyAddPassenger(IStrategyAddPassengerForProfile strategyAddPassengerForProfile)
        {
            this.strategyAddPassengerForProfile = strategyAddPassengerForProfile;
            if(currentUser != null)
                this.strategyAddPassengerForProfile.SetUser(currentUser);
        }
        public void SetStrategyTikcetControl(ITicketContolStrategyInProfile ticketContolStrategy)
        {
            this.ticketContolStrategyInProfile = ticketContolStrategy;
            if (currentUser != null)
                this.ticketContolStrategyInProfile.SetUser(currentUser);
        }
        public void ClearDate()
        {
            tickets.Clear();
            trainInProfileModels.Clear();
            passengers.Clear();
        }
        public void LoadDataForProfile()
        {
            if (currentUser == null)
                return;
            ClearDate();
            List<PassengerViewModel> loadedPassengers = strategyAddPassengerForProfile.LoadPassengers();
            loadedPassengers.ForEach(i=> passengers.Add(i));
            List<TicketModelForProfile> loadedTickets = ticketContolStrategyInProfile.LoadTickets();
            loadedTickets.ForEach(i => tickets.Add(i));

            (
            from train in unitOfWork.Train.GetList()
            where train.IdUserCreator == currentUser.Id
            select new TrainInProfileModel
            {
                TrainModel = new TrainModel() { Id = train.Id, IdUserCreator = (int)train.IdUserCreator, LoadedInDB = true },
                Stations = (from stationtrainschedule in unitOfWork.StationTrainSchedule.GetList()
                            where train.Id == stationtrainschedule.IdTrain
                            join station in unitOfWork.Station.GetList()
                            on stationtrainschedule.IdStation equals station.Id
                            orderby stationtrainschedule.NumberInTrip
                            select station.Name).ToList()
            }).ToList().ForEach(i => trainInProfileModels.Add(i));
        }
        private ObservableCollection<PassengerViewModel> passengers = new ObservableCollection<PassengerViewModel>();
        private ObservableCollection<TicketModelForProfile> tickets = new ObservableCollection<TicketModelForProfile>();
        public ObservableCollection<TicketModelForProfile> Tickets { get => tickets; }
        private ObservableCollection<TrainInProfileModel> trainInProfileModels = new ObservableCollection<TrainInProfileModel>();
        public ObservableCollection<TrainInProfileModel> TrainInProfileModels 
        {
            get
            {
                return trainInProfileModels;
            }
        }
        public ObservableCollection<PassengerViewModel> PassengerViewModels
        {
            get
            {
                return passengers;
                //когда буду переходить на другую страницу не забыть удалить из PassengerViewModels не добавленные профии пассажиров
            }
        }
        public ProfileService(IUnitOfWork unityOfWork, IStrategyAddPassengerForProfile strategyAddPassengerForProfile, ITicketContolStrategyInProfile tickeStrategy)
        {
            this.unitOfWork = unityOfWork;
            SetStrategyAddPassenger(strategyAddPassengerForProfile);
            SetStrategyTikcetControl(tickeStrategy);
        }
        public ICommand EditTrain
        {
            get => new RelayCommand(obj =>
            {
                if (obj is TrainInProfileModel trainInProfileModelForRemove)
                {
                    passengers.Clear();
                    trainInProfileModels.Clear();
                    //EditExistTrain?.Invoke(trainInProfileModelForRemove.TrainModel);
                }
            }); 
        }
        public ICommand AddPassenger 
        {
            get => new RelayCommand((obj) =>
            {
                strategyAddPassengerForProfile.AddPassenger(passengers);
            });
        }
        public ICommand RemovePassenger 
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForRemove)
                {
                    strategyAddPassengerForProfile.Remove(passengers, PassangerForRemove);
                }
            });
        }
        public ICommand SavePassenger
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PassengerViewModel PassangerForSave)
                {
                    strategyAddPassengerForProfile.Save(PassangerForSave);
                }
            }, (obj) =>
            {
                if (obj is PassengerViewModel PassangerForSave)
                    return strategyAddPassengerForProfile.Validate(PassangerForSave);
                return false;
            });
        }
        public ICommand ChangePassword
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is PasswordChangeModel passwordChangeModel && currentUser.Password == passwordChangeModel.OldPassword)
                {
                    currentUser.Password = passwordChangeModel.NewPassword;
                    User user = unitOfWork.Users.GetItem(currentUser.Id);
                    unitOfWork.Users.Update(user);
                    unitOfWork.Save();
                    MessageBox.Show("Пароль успешно сменен!");

                }
                else
                    MessageBox.Show("Старый пароль неверный!");
            }, 
                (obj) => 
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public ICommand RemoveTrain
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is TrainInProfileModel trainInProfileModelForRemove)
                {
                    //RemoveExistTrain(trainInProfileModelForRemove.TrainModel);
                    trainInProfileModels.Remove(trainInProfileModelForRemove);

                }
            },
                (obj) =>
                (obj is PasswordChangeModel passwordChangeModel && passwordChangeModel.NewPassword != null && passwordChangeModel.OldPassword != null));
        }
        public ICommand RemoveTicket
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is TicketModelForProfile ticketModel)
                {
                    ticketContolStrategyInProfile.RemoveTicket(tickets, ticketModel);
                }
            });
        }
    }
}
