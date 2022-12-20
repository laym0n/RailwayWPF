using CourseProject.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IFillPassengerForTicketService
    {
        event Action<List<Ticket>> PassengersFilled;
        void GetTicketsForFilling(List<Ticket> tickets);
        List<PassengerViewModel> passengers { get; }
        List<PassengerViewModel> loadedPassengers { get; }
        ICommand SavePassengers { get; }
    }
}
