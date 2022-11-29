using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepositoryPassenger
    {
        List<Passenger> GetList(int UserId);
        Passenger GetItem(int id);
        void Create(Passenger item);
        void Update(Passenger item);
        void Delete(int id);
    }
}
