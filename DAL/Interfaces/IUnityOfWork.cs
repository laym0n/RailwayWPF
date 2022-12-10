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
        IRepositoryCellStructureVan CellStructureVan { get; }
        IRepositoryTrain Train { get; }
        IRepositorySeat Seat { get; }
        IRepositoryTypeOfVan TypeOfVan { get; }
        IRepositoryVan Van { get; }
        IRepositoryStation Station { get; }
        IRepositoryTimesForStation TimesForStation { get; }
        IRepositoryTrack Track { get; }
        IRepositoryStationTrainSchedule StationTrainSchedule { get; }
        int Save();

    }
}
