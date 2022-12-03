using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    internal class TimesForStationRepository : IRepositoryTimesForStation
    {
        private RailWayDB db;
        public TimesForStationRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<TimesForStation> GetList()
        {
            return db.TimesForStation.ToList();
        }
        public TimesForStation GetItem(int id)
        {
            return db.TimesForStation.FirstOrDefault(i => i.Id == id);
        }
        public void Create(TimesForStation item)
        {
            db.TimesForStation.Add(item);
        }
        public void Update(TimesForStation item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
