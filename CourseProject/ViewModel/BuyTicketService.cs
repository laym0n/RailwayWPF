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
        ObservableCollection<WayModelForBuyTicket> WayModels = new ObservableCollection<WayModelForBuyTicket>();
        public ObservableCollection<WayModelForBuyTicket> SeatsForBuy
        {
            get { return WayModels; }
        }
        public void GetWayForBuyticket(List<WayModelForBuyTicket> way) {
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
                    WayModelForBuyTicket wayModel = WayModels.FirstOrDefault(i => i.StrucureVanModels == (objects[0] as ObservableCollection<List<CellStrucureVanModel>>));
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
                        wayModel.Tickets.Remove();
                    }
                }
            });
        }
        public ICommand GoToPurchase
        {
            get => new RelayCommand((obj) =>
            {

            }, (obj) =>
            {

            });
        }
    }
}
