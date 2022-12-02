using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryCellStructureVan : IRepository<CellStructureVan>
    {
        CellStructureVan GetItem(int NumberOfRow, int NumberOfCell, int TypeOfVanId);
    }
}
