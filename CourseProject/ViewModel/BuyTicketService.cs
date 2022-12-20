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
        public event Action TicketPurchased;
        public event Action<List<WayModelForChooseTicket>> UserChooseWay;
        public BuyTicketService(IUnitOfWork db, IChooseTicketService ChooseTicketService)
        {
            this.db = db;
            this.ChooseTicketService = ChooseTicketService;
        }
        public IChooseTicketService ChooseTicketService { get; }
        public IFillPassengerForTicketService FillPassengerForTicketService { get; }
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
                object[] objects = (obj as object[]);
                CellStrucureVanModel cellStructureForChoose = objects[0] as CellStrucureVanModel;
                if(cellStructureForChoose.typeOccupied == TypeOccupied.ReserveForBuy || cellStructureForChoose.typeOccupied == TypeOccupied.Free)
                {
                    cellStructureForChoose.typeOccupied = cellStructureForChoose.typeOccupied == TypeOccupied.ReserveForBuy ? TypeOccupied.Free : TypeOccupied.ReserveForBuy;
                    WayModelForChooseTicket wayModel = WayModels.FirstOrDefault(i => i.StructureVanModels == (objects[0] as ObservableCollection<List<CellStrucureVanModel>>));
                    if(cellStructureForChoose.typeOccupied == TypeOccupied.ReserveForBuy)
                    {
                        wayModel.Tickets.Add(new DAL.Ticket()
                        {
                            Cost = (cellStructureForChoose.CostPerStation ?? 0) * wayModel.Way.EndStationTrainScheduleModel.NumberInTrip - wayModel.Way.StartStationTrainScheduleModel.NumberInTrip,
                            SeatId = cellStructureForChoose.NumberOfSeatInVan ?? 0,
                            IdTimesForStationSource = wayModel.Way.StartTimesForStationModel.Id,
                            IdTimesForStationDestiny = wayModel.Way.EndTimesForStationModel.Id
                        });
                    }
                    else
                    {
                        //wayModel.Tickets.Remove();
                    }
                }
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
