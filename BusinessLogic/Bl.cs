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
        IRepository<Student> repository = new EntityFrameworkRepository<Student>();
        private List<Student> students { get; set; } = new List<Student>();

        public void AddStudent(string name, string spec, string group)
        {
            students.Add(new Student { Name = name, Group = group, Speciality = spec });
        }

        public void DeleteStudent(int num)
        {
            students.RemoveAt(num);
        }

        public (string, string, string) GetStudent(int num)
        {
            return (students[num].Name, students[num].Speciality, students[num].Group);
        }

        public int CountStudents()
        {
            return students.Count;
        }

        public string[] GetSpecialities()
        {
            List<string> result = new List<string>();

            foreach (Student student in students)
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

                foreach (Student student in students)
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
    }
}
