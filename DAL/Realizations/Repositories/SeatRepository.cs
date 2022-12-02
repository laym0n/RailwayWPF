using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class SeatRepository : IRepositorySeat
    {
        private readonly RailWayDB db;
        public SeatRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<Seat> GetList()
        {
            return db.Seat.ToList();
        }
        public Seat GetItem(int id)
        {
            return db.Seat.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Seat item)
        {
            db.Seat.Add(item);
        }
        public void Update(Seat item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
