using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryUser
    {
        List<User> GetList();
        User GetItem(int id);
        User GetItem(string Login);
        void Create(User item);
        void Update(User item);
        void Delete(int id);

    }
}
