using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class TrainRepository : IRepositoryTrain
    {
        private readonly RailWayDB db;
        public TrainRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<Train> GetList()
        {
            return db.Train.ToList();
        }
        public Train GetItem(int id)
        {
            return db.Train.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Train item)
        {
            db.Train.Add(item);
        }
        public void Update(Train item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
