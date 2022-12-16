﻿using CourseProject.Model;
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
             group new CellStrucureVanModel(seat?.CostPerStation, cell.NumberOfSeatInVan, (seat == null ? TypeOccupied.NotSeat : TypeOccupied.Free)) by cell.NumberOfRow into CellSeat
             select CellSeat.ToList()).ToList().ForEach(i => Added.Add(i));
            loadedStructureVans.Add(IdTypeOfVan, Added);
            return Added;
        }
        public void SetStrucureWithSeats(ConcreteWayFromStationToStation way)
        {
            structureVanWithoutSeats.Clear();
            way.ConcreteWayTrainModels.ForEach(concreteWayTrain =>
            {
                VanModel VanForShow = new VanModel(
                (from van in db.Van.GetList()
                 where van.TrainId == concreteWayTrain.StartStationTrainScheduleModel.IdTrain && van.NumberInTrain == 1
                 select van).First());
                List<List<CellStrucureVanModel>> StructureVanWithoutSeats = GetStrucureVanModels(VanForShow.TypeOfVanId);
                //List<List<CellStrucureVanModel>>  AddedVan = (
                // from cellRow in StructureVanWithoutSeats
                // select (
                //    (
                //    from cell in cellRow
                //    join timesForStation in db.TimesForStation.GetList()
                //    on concreteWayTrain.EndTimesForStationModel.TrackId equals timesForStation.TrackId
                //    join ticket in db.Ticket.GetList()
                //    on timesForStation.Id equals ticket.IdTimesForStationSource
                //    from seat in db.Seat.GetList().Where(i => i.TypeOfVanId == VanForShow.TypeOfVanId && cell.NumberOfSeatInVan == i.NumberOfSeatInVan).DefaultIfEmpty()
                //    select new CellStrucureVanModel(cell.CostPerStation, cell.NumberOfSeatInVan, (cell.typeOccupied == TypeOccupied.NotSeat ? TypeOccupied.NotSeat :
                //    ticket == null ? TypeOccupied.Free : TypeOccupied.Occupied))
                //    ).ToList()
                // )).ToList();
                //structureVansWithSeats.Add(AddedVan);
                structureVansWithSeats.Add(StructureVanWithoutSeats);
            });
        }
        ObservableCollection<List<CellStrucureVanModel>> structureVanWithoutSeats = new ObservableCollection<List<CellStrucureVanModel>>();
        ObservableCollection<List<List<CellStrucureVanModel>>> structureVansWithSeats = new ObservableCollection<List<List<CellStrucureVanModel>>>();
        public ObservableCollection<List<List<CellStrucureVanModel>>> StructureVansWithSeats
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
