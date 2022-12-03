using DAL.Entities;
using DAL.Interfaces;
using DAL.Realizations.Repositories;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations
{
    public class DBReposSQLServer :IUnitOfWork
    {
        private readonly RailWayDB db;
        private UserRepository users;
        private PassengerRepository passengers;
        private CellStructureVanRepository cellStructureVan;
        private VanRepository van;
        private TypeOfVanRepository typeOfvan;
        private SeatRepository seat;
        private TrainRepository train;
        private StationRepository station;
        private TimesForStationRepository timesForStation;
        private TrackRepository track;
        public DBReposSQLServer(RailWayDB db)
        {
            this.db = db;
        }

        public IRepositoryUser Users 
        {
            get => users ?? (users = new UserRepository(db));
        }
        public IRepositoryPassenger Passengers
        {
            get => passengers ?? (passengers = new PassengerRepository(db));
        }
        public IRepositoryCellStructureVan CellStructureVan
        {
            get => cellStructureVan ?? (cellStructureVan = new CellStructureVanRepository(db));
        }
        public IRepositoryVan Van
        {
            get => van ?? (van = new VanRepository(db));
        }
        public IRepositorySeat Seat
        {
            get => seat ?? (seat = new SeatRepository(db));
        }
        public IRepositoryTrain Train
        {
            get => train ?? (train = new TrainRepository(db));
        }
        public IRepositoryTypeOfVan TypeOfVan
        {
            get => typeOfvan ?? (typeOfvan = new TypeOfVanRepository(db));
        }
        public IRepositoryStation Station
        {
            get => station ?? (station = new StationRepository(db));
        }
        public IRepositoryTimesForStation TimesForStation
        {
            get => timesForStation ?? (timesForStation = new TimesForStationRepository(db));
        }
        public IRepositoryTrack Track
        {
            get => track ?? (track = new TrackRepository(db));
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
