using Lab1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class, IDomainObject
    {

        StudentContext context = new StudentContext();

        public void Add(T obj)
        {
            context.Set<T>().Add(obj);
            Save();
        }

        public void Delete(int id)
        {
            T item = context.Set<T>().Find(id);

            if (item != null)
                context.Set<T>().Remove(item);
            Save();
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T FindById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
