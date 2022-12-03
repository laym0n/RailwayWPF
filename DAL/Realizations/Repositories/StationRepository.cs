using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class StationRepository : IRepositoryStation
    {
        private RailWayDB db;
        public StationRepository(RailWayDB db)
        {
            this.db = db;
        }

        public List<Station> GetList()
        {
            return db.Station.ToList();
        }
        public Station GetItem(int id)
        {
            return db.Station.FirstOrDefault(i => i.Id == id);
        }
        public void Create(Station item)
        {
            db.Station.Add(item);
        }
        public void Update(Station item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
