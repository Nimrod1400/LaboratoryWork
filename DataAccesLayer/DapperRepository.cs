using Dapper;
using Lab1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    public class DapperRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        static private string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbConnection;" +
            "Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;" +
            "ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        IDbConnection db = new SqlConnection(connectionString);

        public void Add(T obj)
        {
            var sqlQuery = "INSERT INTO Students (Name, [Group], Speciality) VALUES(@Name, @Group, @Speciality); " +
                "SELECT CAST(SCOPE_IDENTITY() as int)";
            int Id = db.Query<int>(sqlQuery, obj).FirstOrDefault();
            obj.ID = Id;
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Students WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }

        public IEnumerable<T> GetAll()
        {
            return db.Query<T>("SELECT * FROM Students").ToList();
        }

        public T FindById(int id)
        {
            return db.Query<T>("SELECT * FROM Students WHERE Id = @id", new { id })
                .FirstOrDefault();
        }

        public void Save() { }
    }
}
