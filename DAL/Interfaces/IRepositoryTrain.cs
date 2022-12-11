using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryTrain : IRepository<Train>
    {
        Train GetItem(int id);
        void Delete(int id);
    }
}
