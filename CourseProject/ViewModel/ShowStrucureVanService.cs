using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        public void SetStructureVanWithoutSeats(TypeOfVanModel van)
        {
            structureVanWithoutSeats.Clear();
            (from cell in db.CellStructureVan.GetList().Where(i => i.TypeOfVanId == van.Id)
                     from seat in db.Seat.GetList().Where(i => i.TypeOfVanId == van.Id && cell.NumberOfSeatInVan == i.NumberOfSeatInVan).DefaultIfEmpty()
                     group new CellStrucureVanModel(seat?.CostPerStation, cell.NumberOfSeatInVan) by cell.NumberOfRow into CellSeat
                     select CellSeat.ToList()).ToList().ForEach(i=> structureVanWithoutSeats.Add(i));
        }
        ObservableCollection<List<CellStrucureVanModel>> structureVanWithoutSeats = new ObservableCollection<List<CellStrucureVanModel>>();
        public ObservableCollection<List<CellStrucureVanModel>> StructureVanWithoutSeats
        {
            get
            {
                return structureVanWithoutSeats;
            }
        }
    }
}
