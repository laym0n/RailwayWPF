using CourseProject.Model;
using CourseProject.Model.ModelsForEditingInfo;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class FillPassengersForTicketService : IDecoratorChooseTicketService
    {
        IChooseTicketService chooseTicketService;
        IUnitOfWork db;
        UserModel currentUser;
        bool active = false;
        List<PassengerViewModel> passengers = new List<PassengerViewModel>();
        public event Action<List<Ticket>> ProcessComplete;
        public static event Action UserStartFillPassenger;
        public FillPassengersForTicketService(IUnitOfWork db, UserModel userModel, IChooseTicketService chooseTicketService)
        {
            this.db = db;
            this.currentUser = userModel;
            this.chooseTicketService = chooseTicketService;
            chooseTicketService.ProcessComplete += GetTicketsForFilling;
        }
        List<List<Ticket>> tickets = new List<List<Ticket>>();
        public void GetTicketsForFilling(List<Ticket> tickets)
        {
            (from ticket in tickets group ticket by ticket.IdTimesForStationSource).ToList().ForEach(i => this.tickets.Add(i.ToList()));
            UserStartFillPassenger?.Invoke();
            int count = this.tickets[0].Count;
            for(int i = 0;i < count;i++)
                passengers.Add(new PassengerViewModel(currentUser.Id, false));
            active = true;
            SetInfoForPage();
        }
        public void SetInfoForPage()
        {
            passengers.ForEach(i => InfoForPassToFillPassengersPage.PassengersForTickets.Add(i));
            InfoForPassToFillPassengersPage.PassengersInProfile.Clear();
            db.Passengers.GetList().Where(i => i.UserId == currentUser.Id).ToList().ForEach(i => InfoForPassToFillPassengersPage.PassengersInProfile.Add(new PassengerViewModel(i, true)));
            db.Station.GetList().ForEach(i => InfoForPassToFillPassengersPage.Stations.Add(new StationModel(i)));
        }
        public void SetConcreteWayFromStationToStation(List<WayModelForChooseTicket> way) => chooseTicketService.SetConcreteWayFromStationToStation(way);
        public void CancelCurrentProcessTicket()
        {
            if (active)
                ClearDate();
            else
                chooseTicketService.CancelCurrentProcessTicket();
        }
        void ClearDate()
        {
            tickets.Clear();
            InfoForPassToFillPassengersPage.PassengersInProfile.Clear();
            InfoForPassToFillPassengersPage.Stations.Clear();
            InfoForPassToFillPassengersPage.PassengersForTickets.Clear();
            passengers.Clear();
        }
        public ICommand DoProcess
        {
            get => !active ? chooseTicketService.DoProcess : null ;
        }
        public ICommand СompleteProcess
        {
            get => !active ? chooseTicketService.СompleteProcess : new RelayCommand((obj) =>
            {
                int index = 0;
                int count = tickets[0].Count;
                List<Ticket> UdatedTickets = new List<Ticket>();
                List<Passenger> PassengersForAddToTicket = new List<Passenger>(passengers.Count);
                for(int i = 0;i < passengers.Count;i++)
                    PassengersForAddToTicket.Add(passengers[i].LoadedInDB ? db.Passengers.GetItem(passengers[i].Id) : passengers[i].GetPassanger());
                tickets.ForEach(i =>
                {
                    i.ForEach(ticket =>
                    {
                        Passenger passengerForTicket = PassengersForAddToTicket[index];
                        if (!passengers[index].LoadedInDB)
                            passengerForTicket.UserId = null;
                        ticket.Passenger = passengerForTicket;
                        index = (index + 1) % count;
                        UdatedTickets.Add(ticket);
                    });
                });
                ProcessComplete(UdatedTickets);
            }, (obj =>
            {
                return true;
            }));
        }
    }
}
