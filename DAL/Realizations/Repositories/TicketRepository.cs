using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class TicketRepository : IRepositoryTicket
    {
        private readonly RailWayDB db;
        public TicketRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<Ticket> GetList()
        {
            return db.Ticket.ToList();
        }
        public Ticket GetItem(int id)
        {
            return db.Ticket.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Ticket item)
        {
            db.Ticket.Add(item);
        }
        public void Update(Ticket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
