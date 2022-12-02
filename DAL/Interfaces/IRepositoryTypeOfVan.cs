using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryTypeOfVan : IRepository<TypeOfVan>
    {
        TypeOfVan GetItem(int id);
    }
}
