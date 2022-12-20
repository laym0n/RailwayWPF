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
        public event Action<List<Ticket>> UserChooseTicket;
        List<WayModelForChooseTicket> wayModels;
        Dictionary<WayModelForChooseTicket, HashSet<CellStrucureVanModel>> ChoosenTickets = new Dictionary<WayModelForChooseTicket, HashSet<CellStrucureVanModel>>();
        public void SetConcreteWayFromStationToStation(List<WayModelForChooseTicket> way)
        {
            wayModels = way;
            ChoosenTickets.Clear();
            way.ForEach(i => ChoosenTickets.Add(i, new HashSet<CellStrucureVanModel>()));
        }
        public ICommand СompleteChoose
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
                UserChooseTicket?.Invoke(Tickets);
            }, (obj) =>
            {
                int count = ChoosenTickets.First().Value.Count;
                return ChoosenTickets.All(i => i.Value.Count == count);
            });
        }
        public ICommand ChooseTicket
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
