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

namespace CourseProject.ViewModel.StrategyForProfile
{
    public class SimpleTicketControlStrategy : ITicketContolStrategyInProfile
    {
        UserModel user;
        IUnitOfWork db;
        public SimpleTicketControlStrategy(IUnitOfWork db)
        {
            this.db = db;
        }

        public void SetUser(UserModel user)
        {
            this.user = user;
        }
        public List<TicketModelForProfile> LoadTickets()
        {
            return (from tickets in db.Ticket.GetList()
             where tickets.UserId == user.Id
             select tickets).ToList().Select(i => LoadTicket(i)).ToList();
        }
        private TicketModelForProfile LoadTicket(Ticket ticket)
        {
            PassengerViewModel passengerForTicket = new PassengerViewModel(db.Passengers.GetItem(ticket.PassengerId), true);
            TimesForStation startTimesForStation = db.TimesForStation.GetItem(ticket.IdTimesForStationSource);
            TimesForStation endTimesForStation = db.TimesForStation.GetItem(ticket.IdTimesForStationDestiny);
            StationTrainSchedule startStationTrainSchedule = db.StationTrainSchedule.GetList().Where(i => i.NumberInTrip ==
            db.TimesForStation.GetList().Where(j => j.TrackId == startTimesForStation.TrackId && j.DepartureTime < startTimesForStation.ArrivalTime).Count() + 1).First();
            StationTrainSchedule endStationTrainSchedule = db.StationTrainSchedule.GetList().Where(i => i.NumberInTrip ==
            db.TimesForStation.GetList().Where(j => j.TrackId == endTimesForStation.TrackId && j.DepartureTime < endTimesForStation.ArrivalTime).Count() + 1).First();
            Station startStation = db.Station.GetList().Where(i => i.Id == startStationTrainSchedule.IdStation).First();
            Station endStation = db.Station.GetList().Where(i => i.Id == endStationTrainSchedule.IdStation).First();
            TicketViewModel ticketViewModel = new TicketViewModel()
            {
                Passenger = passengerForTicket,
                Way = new ConcreteWayTrainModel()
                {
                    EndStationModel = new StationModel(endStation),
                    StartStationModel = new StationModel(startStation),
                    EndStationTrainScheduleModel = new StationTrainScheduleModel(endStationTrainSchedule),
                    StartStationTrainScheduleModel = new StationTrainScheduleModel(startStationTrainSchedule),
                    StartTimesForStationModel = new TimesForStationModel(startTimesForStation),
                    EndTimesForStationModel = new TimesForStationModel(endTimesForStation)
                }
            };
            return new TicketModelForProfile(ticketViewModel, ticket);
        }
        public void RemoveTicket(ObservableCollection<TicketModelForProfile> collection, TicketModelForProfile ticket)
        {
            if (collection.FirstOrDefault(i => i == ticket) == null)
                return;
            collection.Remove(ticket);
            Ticket ticketForRemoveFromProfile = db.Ticket.GetItem(ticket.Ticket.Id);
            ticketForRemoveFromProfile.UserId = null;
            db.Ticket.Update(ticketForRemoveFromProfile);
            db.Save();
        }
    }
}
