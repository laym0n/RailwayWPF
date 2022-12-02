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
                //
                //).ToList();
                //var a = from CellSeat in b
                //        select List<List<CellStrucureVanModel>>(CellSeat.ToList().Select(i=> new CellStrucureVanModel(i, i.NumberOfSeatInVan, i.NumberOfRow, i.NumberOfCell)))
                //        select new List<List<CellStrucureVanModel>>();
                //        select new CellStrucureVanModel(CellSeat..CostPerStation, cell.NumberOfSeatInVan, cell.NumberOfRow, cell.NumberOfCell);
                //var a = db.CellStructureVan.GetList().Where(i => i.TypeOfVanId == 2)
                //    .Join(db.Seat.GetList().Where(i => i.TypeOfVanId == 2).DefaultIfEmpty(), i => i.NumberOfSeatInVan, i => i.NumberOfSeatInVan,
                //    (i, j) => new CellStrucureVanModel((int?)j.CostPerStation, (int?)i.NumberOfSeatInVan, i.NumberOfRow, i.NumberOfCell))
                //    .GroupBy(i => i.NumberOfRow).ToList();
                return b;
            }
        }
    }
}
