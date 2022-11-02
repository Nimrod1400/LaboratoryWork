using System;
using System.Collections.Generic;
using System.Data.Entity;
using Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer
{
    internal class StudentContext : DbContext
    {
        public StudentContext() : base("DBConnection") { }
        
        public DbSet<Student> Students { get; set; }
    }
}
