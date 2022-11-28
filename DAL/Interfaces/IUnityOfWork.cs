using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.Interfaces;

namespace DLL.Interfaces
{
    public interface IUnityOfWork
    {
        IRepositoryUser Users { get; }
        int Save();

    }
}
