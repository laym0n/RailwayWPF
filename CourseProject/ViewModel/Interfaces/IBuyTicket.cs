using CourseProject.Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel.Interfaces
{
    public interface IBuyTicket
    {
        void SetUser(UserModel user);
        event Action TicketsPurchased;
        List<Ticket> Tickets { get; }
        IChooseTicketService ChooseTicketService { get; }
        ICommand StartTicketProcessing { get; }
    }
}
