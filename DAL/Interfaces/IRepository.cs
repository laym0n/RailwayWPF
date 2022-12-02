using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Interfaces
{
    public interface IRepository <T> where T : class
    {
        List<T> GetList();
        void Create(T item);
        void Update(T item);

    }
}
