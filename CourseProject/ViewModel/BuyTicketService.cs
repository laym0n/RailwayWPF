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
        ObservableCollection<ObservableCollection<SeatViewModel>> seatsForBuy = new ObservableCollection<ObservableCollection<SeatViewModel>>();
        public ObservableCollection<ObservableCollection<SeatViewModel>> SeatsForBuy
        {
            get { return seatsForBuy; }
        }
        public void GetWayForBuyticket(ConcreteWayFromStationToStation way) { 

        }
        public ICommand BuyTicket
        {
            get => new RelayCommand((obj) =>
            {

            });
        }
    }
}
