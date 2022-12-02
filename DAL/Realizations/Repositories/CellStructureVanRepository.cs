using DAL.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class CellStructureVanRepository : IRepositoryCellStructureVan
    {
        private RailWayDB db;
        public CellStructureVanRepository(RailWayDB db)
        {
            this.db = db;
        }

        public List<CellStructureVan> GetList()
        {
            return db.CellStructureVan.ToList();
        }
        public CellStructureVan GetItem(int NumberOfRow, int NumberOfCell, int TypeOfVanId)
        {
            return db.CellStructureVan.FirstOrDefault(i => i.NumberOfRow == NumberOfRow && i.NumberOfCell == NumberOfCell && i.TypeOfVanId == TypeOfVanId);
        }
        public void Create(CellStructureVan item)
        {
            db.CellStructureVan.Add(item);
        }
        public void Update(CellStructureVan item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
