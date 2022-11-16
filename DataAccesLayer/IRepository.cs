using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1;
using Model;

namespace DataAccesLayer
{
    public interface IRepository<T> where T : class, IDomainObject
    {
        IEnumerable<T> GetAll();
        T FindById(int id);
        void Add(T obj);
        void Delete(int id);
        void Save();
    }
}
