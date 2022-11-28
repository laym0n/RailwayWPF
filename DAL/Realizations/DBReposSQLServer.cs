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
    public class DBReposSQLServer :IUnityOfWork
    {
        private readonly RailWayDB db;
        private UserRepository users;
        public DBReposSQLServer(RailWayDB db)
        {
            this.db = db;
        }

        public IRepositoryUser Users 
        {
            get => users ?? (users = new UserRepository(db));
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
