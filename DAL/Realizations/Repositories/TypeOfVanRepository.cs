using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Realizations.Repositories
{
    public class TypeOfVanRepository : IRepositoryTypeOfVan
    {
        private readonly RailWayDB db;
        public TypeOfVanRepository(RailWayDB db)
        {
            this.db = db;
        }
        public List<TypeOfVan> GetList()
        {
            return db.TypeOfVan.ToList();
        }
        public TypeOfVan GetItem(int id)
        {
            return db.TypeOfVan.Where(i => i.Id == id).FirstOrDefault();
        }
        public void Create(TypeOfVan item)
        {
            db.TypeOfVan.Add(item);
        }
        public void Update(TypeOfVan item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
