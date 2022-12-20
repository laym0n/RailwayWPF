using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents.Serialization;
using System.Windows.Input;

namespace CourseProject.ViewModel
{
    public class ShowStrucureVanService : IShowerStructureVan
    {
        private IUnitOfWork db;
        public ShowStrucureVanService(IUnitOfWork db)
        {
            this.db = db;
        }
        Dictionary<int, List<List<CellStrucureVanModel>>> loadedStructureVans = new Dictionary<int, List<List<CellStrucureVanModel>>>();
        public void SetStructureVanWithoutSeats(TypeOfVanModel van)
        {
            structureVanWithoutSeats.Clear();
            List<List<CellStrucureVanModel>> finded = GetStrucureVanModels(van.Id);
            finded.ForEach(i => structureVanWithoutSeats.Add(i));

        }
        List<List<CellStrucureVanModel>> GetStrucureVanModels(int IdTypeOfVan)
        {
            List<List<CellStrucureVanModel>> finded = loadedStructureVans.FirstOrDefault(i => i.Key == IdTypeOfVan).Value;
            if (finded == null)
                finded = LoadVan(IdTypeOfVan);
            return finded;
        }
        List<List<CellStrucureVanModel>> LoadVan(int IdTypeOfVan)
        {
            List<List<CellStrucureVanModel>> Added = new List<List<CellStrucureVanModel>>();
            (from cell in db.CellStructureVan.GetList().Where(i => i.TypeOfVanId == IdTypeOfVan)
             from seat in db.Seat.GetList().Where(i => i.TypeOfVanId == IdTypeOfVan && cell.NumberOfSeatInVan == i.NumberOfSeatInVan).DefaultIfEmpty()
             group new CellStrucureVanModel(seat?.CostPerStation, cell.NumberOfSeatInVan, (seat == null ? TypeOccupied.NotSeat : TypeOccupied.Free), seat?.Id) by cell.NumberOfRow into CellSeat
             select CellSeat.ToList()).ToList().ForEach(i => Added.Add(i));
            loadedStructureVans.Add(IdTypeOfVan, Added);
            return Added;
        }
        public void SetStrucureWithSeats(List<WayModelForChooseTicket> way)
        {
            structureVanWithoutSeats.Clear();
            structureVansWithSeats.Clear();
            way.ForEach(concreteWayTrain =>
            {
                ShowVanWithNumberInTrainMoreThanInParametr(concreteWayTrain, 0);
                structureVansWithSeats.Add(concreteWayTrain);
                //VanModel VanForShow = new VanModel(
                //(from van in db.Van.GetList()
                // where van.TrainId == concreteWayTrain.Way.StartStationTrainScheduleModel.IdTrain && van.NumberInTrain == 1
                // select van).First());
                //List<List<CellStrucureVanModel>> StructureVanWithoutSeats = GetStrucureVanModels(VanForShow.TypeOfVanId);
                //List<List<CellStrucureVanModel>> AddedVan = (
                // from cellRow in StructureVanWithoutSeats
                // select (
                //    (
                //    from cell in cellRow
                //    from ticket in (from ticket in db.Ticket.GetList().DefaultIfEmpty()
                //    join timesForStation in db.TimesForStation.GetList()
                //    on concreteWayTrain.Way.EndTimesForStationModel.TrackId equals timesForStation.TrackId
                //    where ticket?.IdTimesForStationSource == timesForStation.Id
                //    select ticket).DefaultIfEmpty()
                //    select new CellStrucureVanModel(cell.CostPerStation, cell.NumberOfSeatInVan, (cell.typeOccupied == TypeOccupied.NotSeat ? TypeOccupied.NotSeat :
                //    ticket == null ? TypeOccupied.Free : TypeOccupied.Occupied))
                //    ).ToList()
                // )).ToList();
                //structureVansWithSeats.Add(AddedVan);
            });
            
        }
        void ShowVanWithNumberInTrainMoreThanInParametr(WayModelForChooseTicket concreteWayTrain, int NumberInTrain)
        {
            concreteWayTrain.VanForShow = new VanModel(
                ((from van in db.Van.GetList()
                 where van.TrainId == concreteWayTrain.Way.StartStationTrainScheduleModel.IdTrain && van.NumberInTrain == NumberInTrain + 1
                 select van).DefaultIfEmpty()).First());
            List<List<CellStrucureVanModel>> StructureVanWithoutSeats = GetStrucureVanModels(concreteWayTrain.VanForShow.TypeOfVanId);
            List<List<CellStrucureVanModel>> AddedVan = (
             from cellRow in StructureVanWithoutSeats
             select (
                (
                from cell in cellRow
                from ticket in (from ticket in db.Ticket.GetList().DefaultIfEmpty()
                                join timesForStation in db.TimesForStation.GetList()
                                    on concreteWayTrain.Way.EndTimesForStationModel.TrackId equals timesForStation.TrackId
                                where ticket?.IdTimesForStationSource == timesForStation.Id && cell.SeatId == ticket.SeatId
                                select ticket).DefaultIfEmpty()
                select new CellStrucureVanModel(cell.CostPerStation, cell.NumberOfSeatInVan, (cell.typeOccupied == TypeOccupied.NotSeat ? TypeOccupied.NotSeat :
                ticket == null ? TypeOccupied.Free : TypeOccupied.Occupied), cell.SeatId)
                ).ToList()
             )).ToList();
            concreteWayTrain.StructureVanModels.Clear();
            AddedVan.ForEach(i => concreteWayTrain.StructureVanModels.Add(i));
        }
        public ICommand ShowNextVan
        {
            get => new RelayCommand((obj) =>
            {
                if(obj is WayModelForChooseTicket way)
                {
                    int MaxNumber = db.Van.GetList().Where(i => i.TrainId == way.VanForShow.TrainId).Max(i => i.NumberInTrain);
                    ShowVanWithNumberInTrainMoreThanInParametr(way,
                        (way.VanForShow.NumberInTrain < MaxNumber ? way.VanForShow.NumberInTrain : 0 ));
                }
            });
        }
        public ICommand ShowPreviousVan
        {
            get => new RelayCommand((obj) =>
            {
                if (obj is WayModelForChooseTicket way)
                {
                    ShowVanWithNumberInTrainMoreThanInParametr(way, 
                        (way.VanForShow.NumberInTrain - 2 >= 0 ? way.VanForShow.NumberInTrain - 2:
                        db.Van.GetList().Where(i=>i.TrainId == way.VanForShow.TrainId).Max(i=>i.NumberInTrain) - 1));
                }
            });
        }
        ObservableCollection<List<CellStrucureVanModel>> structureVanWithoutSeats = new ObservableCollection<List<CellStrucureVanModel>>();
        ObservableCollection<WayModelForChooseTicket> structureVansWithSeats = new ObservableCollection<WayModelForChooseTicket>();
        public ObservableCollection<WayModelForChooseTicket> StructureVansWithSeats
        {
            get => structureVansWithSeats;
        }
        public ObservableCollection<List<CellStrucureVanModel>> StructureVanWithoutSeats
        {
            get
            {
                return structureVanWithoutSeats;
            }
        }
    }
}
