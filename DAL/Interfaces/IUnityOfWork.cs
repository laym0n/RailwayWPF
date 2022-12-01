using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;

namespace DLL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepositoryUser Users { get; }
        IRepositoryPassenger Passengers { get; }
        int Save();

    }
}
