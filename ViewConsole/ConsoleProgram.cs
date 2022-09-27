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
        private static BL logic = new BL();
        private const int AllowedNameLength = 15;
        private const int AllowedSpecialityLength = 35;
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Что вы хотите сделать? \n1 - добавить студента" +
                "\n2 - удалить студента \n3 - показать список студентов \n4 - выход из программы");

                int answer = -1;

                try
                {
                    answer = GetAction();
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nОтвет должен быть числом.\n");
                    continue;
                }
                catch (Exception)
                {
                    Console.WriteLine("\nНедопустимое действие.\n");
                    continue;
                }                
                
                if (answer == 1)
                {
                    try
                    {
                        AddStudent();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }

                if (answer == 2)
                {
                    try
                    {
                        DeleteStudent();
                        Console.WriteLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Ответ должен быть числом.");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }

                if (answer == 3)
                {
                    Console.WriteLine();
                    ViewStudents(logic);
                    Console.WriteLine();
                }

                if (answer == 4)
                {
                    break;
                }
            }
        }

        private static void ViewStudents(BL logic)
        {
            
            for (int i = 0; i < logic.CountStudents(); i++)
            {
                var (name, spec, group) = logic.GetStudent(i);

                string studentInfo = name.PadRight(AllowedNameLength + 1) + 
                    spec.PadRight(AllowedSpecialityLength + 1) + group;

                Console.WriteLine($"{i + 1}. {studentInfo}");
            }
        }

        private static int GetAction()
        {
            int answer = Int32.Parse(Console.ReadLine());
            
            if (!(new int[] { 1, 2, 3, 4 }).Contains(answer))
            {
                throw new Exception();
            }

            return answer;
        }

        private static void AddStudent()
        {
            Console.WriteLine("Введите имя, специальность и группу студента (через точку с запятой): ");
            string[] studentInfo = Console.ReadLine().Split(';');

            string name = studentInfo[0].Trim();
            if (name.Length > AllowedNameLength)
            {
                throw new Exception($"Имя студента не должно быть длиннее {AllowedNameLength} символов.");
            }

            string spec = studentInfo[1].Trim();
            if (spec.Length > AllowedSpecialityLength)
            {
                throw new Exception($"Специальность студента не должна быть длиннее {AllowedSpecialityLength} символов.");
            }

            string group = studentInfo[2].Trim();

            logic.AddStudent(name, spec, group);
        }

        private static void DeleteStudent()
        {
            Console.WriteLine("Введите номер студента в списке: ");
            int num = Int32.Parse(Console.ReadLine());
            if (num > logic.CountStudents() || num <= 0)
            {
                throw new Exception("В списке нет студента с таким номером.");
            }
            logic.DeleteStudent(num - 1);
        }
    }
}
