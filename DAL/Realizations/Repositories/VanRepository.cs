using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class VanRepository : IRepositoryVan
    {
        private readonly RailWayDB db;
        public VanRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<Van> GetList()
        {
            return db.Van.ToList();
        }
        public Van GetItem(int id)
        {
            return db.Van.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(Van item)
        {
            db.Van.Add(item);
        }
        public void Update(Van item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
        public void Delete(int id)
        {
            Van van = GetItem(id);
            if (van != null)
                db.Van.Remove(van);
        }
    }
}
