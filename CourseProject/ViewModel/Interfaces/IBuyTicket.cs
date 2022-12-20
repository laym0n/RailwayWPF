using CourseProject.Model;
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
        event Action TicketPurchased;
        event Action<List<WayModelForChooseTicket>> UserChooseWay;
        IChooseTicketService ChooseTicketService { get; }
        IFillPassengerForTicketService FillPassengerForTicketService { get; }
        ObservableCollection<WayModelForChooseTicket> SeatsForBuy { get; }
        void GetWayForBuyticket(List<WayModelForChooseTicket> way);
        ICommand BuyTicket { get; }
        ICommand StartTicketProcessing { get; }
    }
}
