using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class EntityFrameworkRepository<T> : IRepository<T> where T : class
    {

        StudentContext context = new StudentContext();

        public void Add(T obj)
        {
            context.Set<T>().Add(obj);
        }

        public void Delete(int id)
        {
            T item = context.Set<T>().Find(id);

            if (item != null)
                context.Set<T>().Remove(item);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList<T>();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
