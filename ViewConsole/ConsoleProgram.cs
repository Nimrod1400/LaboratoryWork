using BusinessLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewConsole
{
    internal class ConsoleProgram
    {
        static void Main(string[] args)
        {
            BL logic = new BL();
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать? \n1 - добавить студента" +
                "\n2 - удалить студента \n3 - показать список студентов \n4 - выход из программы");

                int answer = -1;
                try
                {
                    answer = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Недопустимое действие.");
                    continue;
                }

                if (!(new int[] { 1, 2, 3, 4 }).Contains(answer))
                {
                    Console.WriteLine("Недопустимое действие.");
                    continue;
                }
                if (answer == 4)
                {
                    break;
                }
                if (answer == 1)
                {
                    Console.WriteLine("Введите имя, специальность и группу студента (через точку с запятой): ");
                    string[] studentInfo = Console.ReadLine().Split(';');

                    string name = studentInfo[0].Trim();
                    if (name.Length > 15)
                    {
                        Console.WriteLine("Имя студента не должно быть длиннее 15 символов.");
                        continue;
                    }

                    string spec = studentInfo[1].Trim();
                    if (spec.Length > 35)
                    {
                        Console.WriteLine("Специальность студента не должна быть длиннее 35 символов.");
                        continue;
                    }
                    string group = studentInfo[2].Trim();
                    logic.AddStudent(name, spec, group);
                }
                if (answer == 2)
                {
                    Console.WriteLine("Введите номер студента в списке: ");
                    int num = Int32.Parse(Console.ReadLine());
                    if (num > logic.CountStudents() || num <= 0)
                    {
                        Console.WriteLine("В списке нет студента с таким номером.");
                    }
                    logic.DeleteStudent(num - 1);
                }
                if (answer == 3)
                {
                    Console.WriteLine();
                    ViewStudents(logic);                    
                }
                Console.WriteLine();
            }
        }

        private static void ViewStudents(BL logic)
        {
            
            for (int i = 0; i < logic.CountStudents(); i++)
            {
                var (name, spec, group) = logic.GetStudent(i);
                string studentInfo = name.PadRight(16) + spec.PadRight(36) + group;
                Console.WriteLine($"{i + 1}. {studentInfo}");
            }
        }

        private static int Validation(int answer)
        {

        }
    }
}
