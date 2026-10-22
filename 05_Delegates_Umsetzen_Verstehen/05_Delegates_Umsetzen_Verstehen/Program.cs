using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Delegates_Umsetzen_Verstehen
{
    public class Student
    {
        public string Name { get; set; }
        public string Klasse { get; set; } 
        public int Note { get; set; } 

        public Student(string name, string klasse, int note)
        {
            Name = name;
            Klasse = klasse;
            Note = note;
        }

        
    }
    public delegate void StudentAction(Student s);
    public delegate bool StudentFilter(Student s);
    internal class Program
    {
        /*
        static void PrintAll(List<Student> students)
        {
            foreach (var student in students)
            {
                Console.WriteLine($"Name: {student.Name}, Klasse: {student.Klasse}, Note: {student.Note}");
            }
        }
        */

        static void PrintShort(Student s)
        {
            Console.WriteLine(s.Name);
        }

        static void PrintLong(Student s)
        {
            Console.WriteLine($"Name: {s.Name}, Klasse: {s.Klasse}, Note: {s.Note}");
        }

        static void ForEachStudent(List<Student> list, StudentAction action)
        {
            foreach (Student s in list)
            {
                action(s); // hier wird die übergebene Methode aufgerufen
            }
        }

        static void PrintGrade(Student s)
        {
            string bewertung;

            switch (s.Note)
            {
                case 1:
                    bewertung = "Sehr gut";
                    break;
                case 2:
                    bewertung = "Gut";
                    break;
                case 3:
                    bewertung = "Befriedigend";
                    break;
                case 4:
                    bewertung = "Genügend";
                    break;
                case 5:
                    bewertung = "Nicht genügend";
                    break;
                default:
                    bewertung = "Ungültige Note";
                    break;
            }

            Console.WriteLine($"{s.Name}: {bewertung}");
        }

        static List<Student> Filter(List<Student> list, StudentFilter filter)
        {
            List<Student> result = new List<Student>();

            foreach (Student s in list)
            {
                if (filter(s))          
                {
                    result.Add(s);
                }
            }

            return result;
        }

        static bool IsPositive(Student s)
        {
            return s.Note >= 1 && s.Note <= 4;
        }

        static bool IsNegative(Student s)
        {
            return s.Note == 5;
        }

        static bool IsFourAHIT(Student s)
        {
            return s.Klasse == "4AHIT";
        }

        static bool IsVeryGood(Student s)
        {
            return s.Note == 1 || s.Note == 2;
        }

        static void Main(string[] args)
        {
            List<Student> students = new List<Student>
            {
                new Student("Max", "4AHIT", 1),
                new Student("Anna", "4AHIT", 2),
                new Student("Peter", "4BHIT", 3),
                new Student("Lisa", "4AHIT", 5),
                new Student("Tom", "4CHIT", 4),
                new Student("Eva", "4AHIT", 1),
                new Student("John", "4BHIT", 2),
                new Student("Sarah", "4AHIT", 3)
            };

            //PrintAll(students);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n---Teilaufgabe 1---");
            Console.ResetColor();

            StudentAction action = PrintShort;
            action(students[0]);   

            action = PrintLong;
            action(students[0]);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n---Teilaufgabe 2---");
            Console.ResetColor();

            Console.WriteLine("\n--- Kurz ---");
            ForEachStudent(students, PrintShort);

            Console.WriteLine("\n--- Lang ---");
            ForEachStudent(students, PrintLong);

            Console.WriteLine("\n--- Note als Text ---");
            ForEachStudent(students, PrintGrade);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n---Teilaufgabe 3---");
            Console.ResetColor();

            Console.WriteLine("\n--- Positive Schüler ---");
            ForEachStudent(Filter(students, IsPositive), PrintLong);

            Console.WriteLine("\n--- Negative Schüler ---");
            ForEachStudent(Filter(students, IsNegative), PrintLong);

            Console.WriteLine("\n--- Schüler der 4AHIT ---");
            ForEachStudent(Filter(students, IsFourAHIT), PrintLong);

            Console.WriteLine("\n--- Sehr gute Schüler ---");
            ForEachStudent(Filter(students, IsVeryGood), PrintShort);

            Console.ReadKey();
        }
    }
}