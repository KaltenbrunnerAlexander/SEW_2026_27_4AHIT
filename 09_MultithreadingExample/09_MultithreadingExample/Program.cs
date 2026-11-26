using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace _09_MultithreadingExample
{
    class Counter
    {
        private int number = 0;

        public void Count()
        {
            for (int i = 0; i < 100000; i++)
            {
                number += 1;
            }
        }   
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Counter c1 = new Counter();
            Counter c2 = new Counter();

            Thread t1 = new Thread(c1.Count);
            Thread t2 = new Thread(c2.Count);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine($"Counter: {number}");

            Console.ReadKey();
        }
    }
}
