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
        ICommand BuyTicket { get; }
        ObservableCollection<WayModelForBuyTicket> SeatsForBuy { get; }
        void GetWayForBuyticket(List<WayModelForBuyTicket> way);
    }
}
