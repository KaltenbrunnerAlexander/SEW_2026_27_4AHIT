using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace _03_Figurliste_VerketteteListe
{
    interface IFigur
    {
        string Beschreibung { get; }
        double BerechneOberflaeche();
        double BerechneVolumen();
    }

    abstract class Figur : IFigur
    {
        public Figur(string beschreibung)
        {
            Beschreibung = beschreibung;
        }

        public string Beschreibung { get; }

        public abstract double BerechneOberflaeche();
        public abstract double BerechneVolumen();
    }

    class Kugel : Figur
    {
        public double Radius { get; }

        public Kugel(string beschreibung, double radius) : base(beschreibung)
        {
            Radius = radius;
        }

        public override double BerechneOberflaeche()
        {
            return 4.0 * Math.PI * Math.Pow(Radius, 2);
        }

        public override double BerechneVolumen()
        {
            return (4.0 * Math.PI * Math.Pow(Radius, 3)) / 3.0;
        }
    }

    class Wuerfel : Figur
    {
        public double Seitenlaenge { get; }

        public Wuerfel(string beschreibung, double a) : base(beschreibung)
        {
            Seitenlaenge = a;
        }

        public override double BerechneOberflaeche()
        {
            return 6.0 * Math.Pow(Seitenlaenge, 2);
        }

        public override double BerechneVolumen()
        {
            return Math.Pow(Seitenlaenge, 3);
        }
    }

    class VerlinkteFigurListe : IEnumerable<IFigur>
    {
        private class Node
        {
            public Figur Data;
            public Node Next;

            public Node(Figur data)
            {
                Data = data;
            }
        }

        private Node head;

        public int Count { get; private set; }

        private Node GetNode(int index)
        {
            Node current = head;
            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            return current;
        }

        private Figur GetAt(int index)
        {
            if (index < 0 || index >= Count)
            {
                return null;
            }
            return GetNode(index).Data;
        }

        public void Add(Figur fig)
        {
            Node newNode = new Node(fig);

            if (head == null)
            {
                head = newNode;
            }
            else
            {
                Node lastNode = GetNode(Count - 1);
                lastNode.Next = newNode;
            }

            Count++;
        }

        public void AusgabeAllerFiguren()
        {
            foreach (IFigur figur in this)
            {
                Console.WriteLine($"{figur.Beschreibung} | Oberflaeche: {figur.BerechneOberflaeche():F2} | Volumen: {figur.BerechneVolumen():F2}");
            }
        }

        public void Push(Figur fig)
        {
            Node newNode = new Node(fig);
            newNode.Next = head;
            head = newNode;
            Count++;
        }

        public Figur Pop()
        {
            if (head == null)
            {
                return null;
            }

            Node oldHead = head;
            head = head.Next;
            oldHead.Next = null;
            Count--;

            return oldHead.Data;
        }

        public void InsertAt(int index, Figur fig)
        {
            if (index <= 0)
            {
                Push(fig);
                return;
            }

            if (index >= Count)
            {
                Add(fig);
                return;
            }

            Node previousNode = GetNode(index - 1);
            Node newNode = new Node(fig);
            newNode.Next = previousNode.Next;
            previousNode.Next = newNode;
            Count++;
        }

        public void Remove(int index)
        {
            if (index < 0 || index >= Count)
            {
                return;
            }

            if (index == 0)
            {
                Pop();
                return;
            }

            Node previousNode = GetNode(index - 1);
            Node nodeToRemove = previousNode.Next;
            previousNode.Next = nodeToRemove.Next;
            nodeToRemove.Next = null;
            Count--;
        }

        public void Umdrehen()
        {
            Node previousNode = null;
            Node currentNode = head;

            while (currentNode != null)
            {
                Node nextNode = currentNode.Next;
                currentNode.Next = previousNode;
                previousNode = currentNode;
                currentNode = nextNode;
            }

            head = previousNode;
        }

        public void VertauscheBenachbarte()
        {
            Node previousPair = null;
            Node currentNode = head;

            while (currentNode != null && currentNode.Next != null)
            {
                Node nextNode = currentNode.Next;

                currentNode.Next = nextNode.Next;
                nextNode.Next = currentNode;

                if (previousPair == null)
                {
                    head = nextNode;
                }
                else
                {
                    previousPair.Next = nextNode;
                }

                previousPair = currentNode;
                currentNode = currentNode.Next;
            }
        }

        public void Anhaengen(VerlinkteFigurListe other)
        {
            if (other == null || other.head == null)
            {
                return;
            }

            if (head == null)
            {
                head = other.head;
            }
            else
            {
                Node lastNode = GetNode(Count - 1);
                lastNode.Next = other.head;
            }

            Count += other.Count;

            other.head = null;
            other.Count = 0;
        }

        public IEnumerator<IFigur> GetEnumerator()
        {
            Node currentNode = head;
            while (currentNode != null)
            {
                yield return currentNode.Data;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            VerlinkteFigurListe liste = new VerlinkteFigurListe();

            liste.Add(new Kugel("K1", 10));
            liste.Add(new Wuerfel("W1", 3));
            liste.Add(new Kugel("K2", 8));
            liste.Add(new Wuerfel("W2", 4));
            liste.Add(new Kugel("K3", 5));
            Console.WriteLine("Nach 5x Add:");
            liste.AusgabeAllerFiguren();

            Console.ForegroundColor = ConsoleColor.Green;
            liste.Push(new Wuerfel("W0-Push", 2));
            Console.WriteLine("\nNach Push(W0-Push):");
            liste.AusgabeAllerFiguren();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Figur gepoppt = liste.Pop();
            Console.WriteLine($"\nNach Pop() -> entfernt: {gepoppt?.Beschreibung}");
            liste.AusgabeAllerFiguren();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Blue;
            liste.InsertAt(2, new Kugel("K-Insert", 6));
            Console.WriteLine("\nNach InsertAt(2, K-Insert):");
            liste.AusgabeAllerFiguren();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Yellow;
            liste.Remove(0);
            Console.WriteLine("\nNach Remove(0):");
            liste.AusgabeAllerFiguren();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Cyan;
            liste.VertauscheBenachbarte();
            Console.WriteLine("\nNach VertauscheBenachbarte():");
            liste.AusgabeAllerFiguren();
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            liste.Umdrehen();
            Console.WriteLine("\nNach Umdrehen():");
            liste.AusgabeAllerFiguren();
            Console.ResetColor();

            VerlinkteFigurListe zweiteListe = new VerlinkteFigurListe();
            zweiteListe.Add(new Kugel("Z1", 3));
            zweiteListe.Add(new Wuerfel("Z2", 2));

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            liste.Anhaengen(zweiteListe);
            Console.WriteLine("\nNach Anhaengen(zweiteListe):");
            liste.AusgabeAllerFiguren();
            Console.WriteLine($"zweiteListe.Count = {zweiteListe.Count} (muss 0 sein)");
            Console.ResetColor();

            Console.ReadKey();
        }
    }
}
