using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll();
        T GetById(string id);
        void Insert(T item);
        void Update(T item);
        void Delete(T item);
    }
}
