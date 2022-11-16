using DataAccesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BusinessLogic
{
    public class BL
    {
        private IRepository<Student> Repository { get; set; }
        
        public BL(IRepository<Student> repository) 
        {
            Repository = repository;
        }

        public void AddStudent(string name, string spec, string group)
        {
            Repository.Add(new Student() { Name = name, Speciality = spec, Group = group });
        }

        public void DeleteStudent(int index)
        {
            Repository.Delete(TransformIndexIntoID(index));
        }

        public (string, string, string) GetStudent(int index)
        {
            Student student = Repository.FindById(TransformIndexIntoID(index));
            return (student.Name, student.Speciality, student.Group);
        }

        public int CountStudents()
        {
            return Repository.GetAll().Count();
        }

        public string[] GetSpecialities()
        {
            List<string> result = new List<string>();

            foreach (Student student in Repository.GetAll())
            {
                if (!result.Contains(student.Speciality))
                    result.Add(student.Speciality);
            }

            return result.ToArray();
        }

        public double[] DistributionByScpecialities()
        {
            double AmountInSpeciality(string spec)
            {
                double result = 0.0;
                foreach (Student student in Repository.GetAll())
                {
                    if (student.Speciality == spec)
                        result++;
                }
                return result;
            }

            List<double> amount = new List<double>();

            foreach (string spec in GetSpecialities())
                amount.Add(AmountInSpeciality(spec));

            return amount.ToArray();
        }

        private int TransformIndexIntoID(int index)
        {
            return Repository.GetAll().ElementAt(index).ID;
        }
    }
}
