using CourseProject.Model;
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
    public class FillPassengersForTicketService : IFillPassengerForTicketService
    {
        IUnitOfWork db;
        UserModel currentUser;
        List<PassengerViewModel> Passengers = new List<PassengerViewModel>();
        public FillPassengersForTicketService(IUnitOfWork db, UserModel userModel)
        {
            this.db = db;
            this.currentUser = userModel;
        }

        public event Action<List<Ticket>> PassengersFilled;
        List<List<Ticket>> tickets;
        public void GetTicketsForFilling(List<Ticket> tickets)
        {
            (from ticket in tickets group ticket by ticket.IdTimesForStationSource).ToList().ForEach(i => this.tickets.Add(i.ToList()));
            int count = this.tickets.Count();
            while (count-- != 0)
                Passengers.Add(new PassengerViewModel(currentUser.Id, false));
        }
        public List<PassengerViewModel> passengers { get => Passengers; }
        public List<PassengerViewModel> loadedPassengers
        {
            get
            {
                return db.Passengers.GetList().Where(i => i.UserId == currentUser.Id).Select(i => new PassengerViewModel(i, true)).ToList();
            }
        }
        public ICommand SavePassengers 
        {
            get => new RelayCommand((obj) =>
            {

            }, (obj =>
            {
                return true;
            }));
        }
    }
}
