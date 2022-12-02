using DAL.Interfaces;
using DLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    internal class PassengerRepository : IRepositoryPassenger
    {
        private RailWayDB db;
        public PassengerRepository(RailWayDB db)
        {
            this.db = db;
        }

        public List<Passenger> GetList()
        {
            return db.Passenger.ToList();
        }
        public Passenger GetItem(int id)
        {
            return db.Passenger.FirstOrDefault(i => i.Id == id);
        }
        public void Create(Passenger item)
        {
            db.Passenger.Add(item);
        }
        public void Update(Passenger item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Passenger passenger = GetItem(id);
            if (passenger != null)
                db.Passenger.Remove(passenger);
        }
    }
}
