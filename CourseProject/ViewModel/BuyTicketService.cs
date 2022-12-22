using CourseProject.Model;
using CourseProject.Model.Enumerations;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DAL.Entities;
using DLL.Interfaces;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace CourseProject.ViewModel
{
    public class BuyTicketService : IBuyTicket
    {
        IUnitOfWork db;
        private UserModel user;
        public void SetUser(UserModel user) => this.user = user;
        public event Action TicketsPurchased;
        public event Action CancelProcess;
        IChooseTicketService chooseTicketService;
        public IChooseTicketService ChooseTicketService { get => chooseTicketService; }
        public BuyTicketService(IUnitOfWork db)
        {
            this.db = db;
        }
        List<TicketViewModel> tickets = new List<TicketViewModel>();
        public List<TicketViewModel> Tickets { get => tickets; }
        public void SetChooseTicketService()
        {
            FabricChooseTicket fabric = new FabricChooseTicket(user, db);
            chooseTicketService = fabric.GetChooseTicketService();
            ChooseTicketService.ProcessComplete += AddTicketsToUser;
        }
        void AddTicketsToUser(List<Ticket> tickets)
        {
            foreach (Ticket ticket in tickets)
            {
                ticket.UserId = user.Id;
                db.Ticket.Create(ticket);
            }
            db.Save();
            this.tickets = tickets.Select(i =>
            {
                Track CurrentTrack = (from timesForStation in db.TimesForStation.GetList()
                                      where timesForStation.Id == i.IdTimesForStationSource
                                      select timesForStation.Track).First();
                TimesForStation startTime = db.TimesForStation.GetItem(i.IdTimesForStationSource);
                TimesForStation endTime = db.TimesForStation.GetItem(i.IdTimesForStationDestiny);
                StationTrainSchedule StartScheduleModel = (
                from stationTrainSchedule in db.StationTrainSchedule.GetList()
                where stationTrainSchedule.NumberInTrip ==
                (from timesForStation in db.TimesForStation.GetList()
                 where timesForStation.TrackId == startTime.TrackId
                 && startTime.ArrivalTime > timesForStation.DepartureTime
                 select timesForStation).Count() + 1
                select stationTrainSchedule).First();
                StationTrainSchedule EndScheduleModel = (
                from stationTrainSchedule in db.StationTrainSchedule.GetList()
                where stationTrainSchedule.NumberInTrip ==
                (from timesForStation in db.TimesForStation.GetList()
                 where timesForStation.TrackId == endTime.TrackId
                 && endTime.ArrivalTime > timesForStation.DepartureTime
                 select timesForStation).Count() + 1
                select stationTrainSchedule).First();
                Station startStation = db.Station.GetItem(StartScheduleModel.IdStation);
                Station endStation = db.Station.GetItem(EndScheduleModel.IdStation);
                return new TicketViewModel()
                {
                    Way = new ConcreteWayTrainModel()
                    {
                        EndStationModel = new StationModel(endStation),
                        StartStationModel = new StationModel(startStation),
                        StartStationTrainScheduleModel = new StationTrainScheduleModel(StartScheduleModel),
                        EndStationTrainScheduleModel=new StationTrainScheduleModel(EndScheduleModel),
                        StartTimesForStationModel = new TimesForStationModel(startTime),
                        EndTimesForStationModel = new TimesForStationModel(endTime)
                    },
                    Passenger = new PassengerViewModel(db.Passengers.GetItem(i.Passenger.Id), true)
                };
            }
            ).ToList();
            TicketsPurchased?.Invoke();
        }
        public ICommand CancelCurrentProcess
        {
            get => new RelayCommand((obj) =>
            {
                CancelProcess?.Invoke();
                chooseTicketService.CancelCurrentProcessTicket();
            });
        }
        public ICommand StartTicketProcessing
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is ConcreteWayFromStationToStation way)
                {
                    SetChooseTicketService();
                    List<WayModelForChooseTicket> ways = way.ConcreteWayTrainModels.Select(i => new WayModelForChooseTicket() { Way = i }).ToList();
                    ChooseTicketService.SetConcreteWayFromStationToStation(ways);
                }
            }, (obj) => user != null);
        }
    }
}
