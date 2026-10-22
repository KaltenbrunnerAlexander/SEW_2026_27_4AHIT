using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsBsp
{
    public class Node<X>
    {
        public Node<X> Next { get; set; }
        public X Data { get; set; }

        public Node(X data)
        {
            Data = data;
        }
    }

    public class GenericList<T>
    {
        Node<T> first;
        public int Count { get; set; }

        public void Ausgabe()
        {
            Node<T> current = first;
            for(int i = 0; i < Count; i++)
            {
                Console.WriteLine(current.Data);
                current = current.Next;
            }
        }

        public void Push(T data)
        {
            if(first == null)
            {
                first = new Node<T>(data);
            }
            else
            {
                Node<T> newNode = new Node<T>(data);
                newNode.Next = first;
                first = newNode;
                Count++;
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            GenericList<string> list = new GenericList<string>();
            list.Push("Hallo Welt!");
            list.Push("SEW");
            
            list.Ausgabe();
            Console.ReadKey();

        }
    }
}
