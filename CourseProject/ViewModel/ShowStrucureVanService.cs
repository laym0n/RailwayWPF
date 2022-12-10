using CourseProject.Model;
using CourseProject.ViewModel.Interfaces;
using DAL;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
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
        public List<List<CellStrucureVanModel>> StructureVan
        {
            get
            {
                var b = (from cell in db.CellStructureVan.GetList().Where(i => i.TypeOfVanId == 2)
                        from seat in db.Seat.GetList().Where(i => i.TypeOfVanId == 2 && cell.NumberOfSeatInVan == i.NumberOfSeatInVan).DefaultIfEmpty()
                        group new CellStrucureVanModel(seat?.CostPerStation, cell.NumberOfSeatInVan) by cell.NumberOfRow into CellSeat
                        select CellSeat.ToList()).ToList();
                return b;
            }
        }
    }
}
