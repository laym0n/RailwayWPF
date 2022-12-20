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
using System.Windows.Navigation;

namespace CourseProject.ViewModel
{
    public class BuyTicketService : IBuyTicket
    {
        IUnitOfWork db;
        private UserModel user;
        public void SetUser(UserModel user) => this.user = user;
        public event Action<List<Ticket>> TicketsPurchased;
        IChooseTicketService chooseTicketService;
        public IChooseTicketService ChooseTicketService { get => chooseTicketService; }
        public BuyTicketService(IUnitOfWork db)
        {
            this.db = db;
        }
        public void SetChooseTicketService()
        {
            FabricChooseTicket fabric = new FabricChooseTicket(user, db);
            chooseTicketService = fabric.GetChooseTicketService();
            ChooseTicketService.ProcessComplete += AddTicketsToUser;
        }
        void AddTicketsToUser(List<Ticket> tickets)
        {
            
        }
        public ICommand StartTicketProcessing
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is ConcreteWayFromStationToStation way)
                {
                    SetChooseTicketService();
                    List<WayModelForChooseTicket> ways = way.ConcreteWayTrainModels.Select(i => new WayModelForChooseTicket() { Way = i }).ToList();
                    ChooseTicketService.SetConcreteWayFromStationToStation(ways);
                }
            }, (obj) => user != null);
        }
    }
}
