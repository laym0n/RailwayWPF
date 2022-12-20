using CourseProject.Model;
using CourseProject.Model.Enumerations;
using CourseProject.ViewModel.Interfaces;
using DAL;
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
        public event Action TicketPurchased;
        public event Action<List<WayModelForChooseTicket>> UserChooseWay;
        public BuyTicketService(IUnitOfWork db, TypeChooseTicket typeChooseTicket, IFillPassengerForTicketService fillPassenger)
        {
            this.db = db;
            SetChooseTicketService(typeChooseTicket);
            this.FillPassengerForTicketService = fillPassenger;
            this.FillPassengerForTicketService.PassengersFilled +=;
        }
        public void SetChooseTicketService(TypeChooseTicket type)
        {
            this.ChooseTicketService = FabricChooseTicket.GetChooseTicketService(type);
            ChooseTicketService.UserChooseTicket += SetPassengersForTickets;
        }
        void SetPassengersForTickets(List<Ticket> tickets)
        {
            FillPassengerForTicketService.GetTicketsForFilling(tickets);
        }
        public IChooseTicketService ChooseTicketService { get; set; }
        public IFillPassengerForTicketService FillPassengerForTicketService { get; set; }
        ObservableCollection<WayModelForChooseTicket> WayModels = new ObservableCollection<WayModelForChooseTicket>();
        public ObservableCollection<WayModelForChooseTicket> SeatsForBuy
        {
            get { return WayModels; }
        }
        public void GetWayForBuyticket(List<WayModelForChooseTicket> way) {
            way.ForEach(i => WayModels.Add(i));
        }
        public ICommand BuyTicket
        {
            get => new RelayCommand((obj) =>
            {
                
            });
        }
        public ICommand StartTicketProcessing
        {
            get => new RelayCommand((obj) =>
            {
                if(obj is ConcreteWayFromStationToStation way)
                {
                    List<WayModelForChooseTicket> ways = way.ConcreteWayTrainModels.Select(i => new WayModelForChooseTicket() { Way = i }).ToList();
                    ways.ForEach(i => WayModels.Add(i));
                    UserChooseWay?.Invoke(ways);
                    ChooseTicketService.SetConcreteWayFromStationToStation(ways);
                }
            });
        }
    }
}
