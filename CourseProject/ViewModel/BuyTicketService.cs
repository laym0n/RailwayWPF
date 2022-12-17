using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class BuyTicketService : IBuyTicket
    {
        IUnitOfWork db;
        public BuyTicketService(IUnitOfWork db)
        {
            this.db = db;
        }
        ObservableCollection<WayModelForBuyTicket> seatsForBuy = new ObservableCollection<WayModelForBuyTicket>();
        public ObservableCollection<WayModelForBuyTicket> SeatsForBuy
        {
            get { return seatsForBuy; }
        }
        public void GetWayForBuyticket(List<WayModelForBuyTicket> way) {
            way.ForEach(i => seatsForBuy.Add(i));
        }
        public ICommand BuyTicket
        {
            get => new RelayCommand((obj) =>
            {

            });
        }
    }
}
