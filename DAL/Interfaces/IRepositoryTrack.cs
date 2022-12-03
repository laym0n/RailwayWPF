using DAL.Entities;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryTrack : IRepository<Track>
    {
        Track GetItem(int id);
        void Delete(int id);
    }
}
