using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class ChooseTicketService : IChooseTicketService
    {
        public event Action<List<Ticket>> ProcessComplete;
        public static event Action<List<WayModelForChooseTicket>> ShowNewWay;
        Dictionary<WayModelForChooseTicket, HashSet<CellStrucureVanModel>> ChoosenTickets = new Dictionary<WayModelForChooseTicket, HashSet<CellStrucureVanModel>>();
        public void SetConcreteWayFromStationToStation(List<WayModelForChooseTicket> way)
        {
            ChoosenTickets.Clear();
            way.ForEach(i => ChoosenTickets.Add(i, new HashSet<CellStrucureVanModel>()));
            ShowNewWay?.Invoke(way);
        }
        public ICommand СompleteProcess
        {
            get => new RelayCommand((obj) =>
            {
                List<Ticket> Tickets = new List<Ticket>();
                ChoosenTickets.ToList().ForEach(i => i.Value.ToList().ForEach(cell => Tickets.Add(new Ticket()
                {
                    SeatId = (int)cell.SeatId,
                    Cost = (i.Key.Way.EndStationTrainScheduleModel.NumberInTrip - i.Key.Way.StartStationTrainScheduleModel.NumberInTrip) * (int)cell.CostPerStation,
                    IdTimesForStationDestiny = i.Key.Way.EndTimesForStationModel.Id,
                    IdTimesForStationSource = i.Key.Way.StartTimesForStationModel.Id

                })));
                ProcessComplete?.Invoke(Tickets);
            }, (obj) =>
            {
                int count = ChoosenTickets.First().Value.Count;
                if (count == 0)
                    return false;
                return ChoosenTickets.All(i => i.Value.Count == count);
            });
        }
        public ICommand DoProcess
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is object[] objects && objects[0] is CellStrucureVanModel cell && objects[1] is WayModelForChooseTicket wayModel)
                    ReserveOrFreeCell(cell, wayModel);
            });
        }
        void ReserveOrFreeCell(CellStrucureVanModel cell, WayModelForChooseTicket wayModel)
        {
            if (cell.typeOccupied == TypeOccupied.ReserveForBuy || cell.typeOccupied == TypeOccupied.Free)
            {
                cell.typeOccupied = cell.typeOccupied == TypeOccupied.ReserveForBuy ? TypeOccupied.Free : TypeOccupied.ReserveForBuy;
                if (cell.typeOccupied == TypeOccupied.ReserveForBuy)
                    ChoosenTickets[wayModel].Add(cell);
                else
                    ChoosenTickets[wayModel].Remove(cell);
            }
        }
    }
}
