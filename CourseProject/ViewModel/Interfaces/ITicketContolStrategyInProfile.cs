using CourseProject.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProject.ViewModel.Interfaces
{
    public interface ITicketContolStrategyInProfile
    {
        void SetUser(UserModel user);
        List<TicketModelForProfile> LoadTickets();
        void RemoveTicket(ObservableCollection<TicketModelForProfile> collection, TicketModelForProfile ticket);
    }
}
