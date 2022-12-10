using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class StationTrainScheduleRepository : IRepositoryStationTrainSchedule
    {
        private RailWayDB db;
        public StationTrainScheduleRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<StationTrainSchedule> GetList()
        {
            return db.StationTrainSchedule.ToList();
        }
        public StationTrainSchedule GetItem(int id)
        {
            return db.StationTrainSchedule.FirstOrDefault(i => i.Id == id);
        }
        public void Create(StationTrainSchedule item)
        {
            db.StationTrainSchedule.Add(item);
        }
        public void Update(StationTrainSchedule item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
