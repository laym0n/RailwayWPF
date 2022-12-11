using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryStationTrainSchedule : IRepository<StationTrainSchedule>
    {
        StationTrainSchedule GetItem(int id);
        void Delete(int id);
    }
}
