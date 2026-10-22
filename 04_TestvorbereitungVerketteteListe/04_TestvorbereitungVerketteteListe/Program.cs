using System;
using System.Collections;
using System.Collections.Generic;

namespace _04_TestvorbereitungVerketteteListe
{
    interface IFigur
    {
        string Beschreibung { get; }
        double BerechneVolumen();
    }

    abstract class Figur : IFigur
    {
        public Figur(string beschreibung)
        {
            Beschreibung = beschreibung;
        }

        public string Beschreibung { get; }

        public abstract double BerechneVolumen();
    }

    class Kugel : Figur
    {
        public double Radius { get; }

        public Kugel(string beschreibung, double radius) : base(beschreibung)
        {
            Radius = radius;
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
                Console.Write($"{figur.Beschreibung} ");
            }
            Console.WriteLine();
        }

        public int IndexOf(string beschreibung)
        {
            Node current = head;
            int index = 0;

            while (current != null)
            {
                if (current.Data != null && current.Data.Beschreibung == beschreibung)
                {
                    return index;
                }
                current = current.Next;
                index++;
            }

            return -1;
        }

        public int EntferneKleiner(double grenze)
        {
            int entfernteAnzahl = 0;

            while (head != null && head.Data.BerechneVolumen() < grenze)
            {
                head = head.Next;
                Count--;
                entfernteAnzahl++;
            }

            Node current = head;
            while (current != null && current.Next != null)
            {
                if (current.Next.Data.BerechneVolumen() < grenze)
                {
                    current.Next = current.Next.Next;
                    Count--;
                    entfernteAnzahl++;
                }
                else
                {
                    current = current.Next;
                }
            }

            return entfernteAnzahl;
        }

        public void Rotieren(int k)
        {
            if (Count <= 1 || k <= 0) return;

            k = k % Count;
            if (k == 0) return;

            Node neueLetzte = GetNode(Count - k - 1);
            Node neueErste = neueLetzte.Next;
            Node alteLetzte = GetNode(Count - 1);

            alteLetzte.Next = head;
            head = neueErste;
            neueLetzte.Next = null;
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

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    class DoppeltVerketteteFigurListe : IEnumerable<IFigur>
    {
        private class Node
        {
            public Figur Data;
            public Node Next;
            public Node Previous;

            public Node(Figur data)
            {
                Data = data;
            }
        }

        private Node first;
        private Node last;

        public int Count { get; private set; }

        public void Add(Figur fig)
        {
            Node newNode = new Node(fig);

            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                last.Next = newNode;
                newNode.Previous = last;
                last = newNode;
            }

            Count++;
        }

        public void Push(Figur fig)
        {
            Node newNode = new Node(fig);

            if (first == null)
            {
                first = newNode;
                last = newNode;
            }
            else
            {
                newNode.Next = first;
                first.Previous = newNode;
                first = newNode;
            }

            Count++;
        }

        public void AusgabeVorwaerts()
        {
            Node current = first;
            while (current != null)
            {
                Console.Write($"{current.Data.Beschreibung} ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public void AusgabeRuckwaerts()
        {
            Node current = last;
            while (current != null)
            {
                Console.Write($"{current.Data.Beschreibung} ");
                current = current.Previous;
            }
            Console.WriteLine();
        }

        private Node FindNode(Figur fig)
        {
            if (fig == null) return null;
            Node current = first;
            while (current != null)
            {
                if (current.Data == fig) return current;
                current = current.Next;
            }
            return null;
        }

        public Figur PopLast()
        {
            if (last == null) return null;

            Figur data = last.Data;

            if (first == last)
            {
                first = null;
                last = null;
            }
            else
            {
                last = last.Previous;
                last.Next = null;
            }

            Count--;
            return data;
        }

        public void InsertSortiert(Figur fig)
        {
            if (fig == null) return;

            if (first == null || fig.BerechneVolumen() < first.Data.BerechneVolumen())
            {
                Push(fig);
                return;
            }

            if (fig.BerechneVolumen() >= last.Data.BerechneVolumen())
            {
                Add(fig);
                return;
            }

            Node current = first;
            while (current != null && current.Data.BerechneVolumen() <= fig.BerechneVolumen())
            {
                current = current.Next;
            }

            Node newNode = new Node(fig);
            Node prevNode = current.Previous;

            newNode.Next = current;
            newNode.Previous = prevNode;
            prevNode.Next = newNode;
            current.Previous = newNode;

            Count++;
        }

        public void Vertausche(Figur a, Figur b)
        {
            if (a == null || b == null || a == b) return;

            Node nodeA = FindNode(a);
            Node nodeB = FindNode(b);

            if (nodeA == null || nodeB == null) return;

            if (nodeA.Previous == nodeB)
            {
                Node temp = nodeA;
                nodeA = nodeB;
                nodeB = temp;
            }

            bool istBenachbart = (nodeA.Next == nodeB);

            Node aPrev = nodeA.Previous;
            Node aNext = nodeA.Next;
            Node bPrev = nodeB.Previous;
            Node bNext = nodeB.Next;

            if (istBenachbart)
            {
                nodeA.Next = bNext;
                nodeA.Previous = nodeB;

                nodeB.Next = nodeA;
                nodeB.Previous = aPrev;

                if (aPrev != null) aPrev.Next = nodeB;
                if (bNext != null) bNext.Previous = nodeA;
            }
            else
            {
                nodeA.Next = bNext;
                nodeA.Previous = bPrev;

                nodeB.Next = aNext;
                nodeB.Previous = aPrev;

                if (aPrev != null) aPrev.Next = nodeB;
                if (aNext != null) aNext.Previous = nodeB;

                if (bPrev != null) bPrev.Next = nodeA;
                if (bNext != null) bNext.Previous = nodeA;
            }

            if (first == nodeA) first = nodeB;
            else if (first == nodeB) first = nodeA;

            if (last == nodeA) last = nodeB;
            else if (last == nodeB) last = nodeA;
        }

        public IEnumerator<IFigur> GetEnumerator()
        {
            Node current = first;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("             BEISPIEL A - VerlinkteFigurListe     ");
            Console.WriteLine("==================================================");

            VerlinkteFigurListe listeA = new VerlinkteFigurListe();
            listeA.Add(new Kugel("K1", 10));
            listeA.Add(new Wuerfel("W1", 3));
            listeA.Add(new Kugel("K2", 8));
            listeA.Add(new Wuerfel("W2", 4));
            listeA.Add(new Kugel("K3", 5));

            Console.Write("Ausgangsliste:            ");
            listeA.AusgabeAllerFiguren();
            Console.WriteLine($"Count:                    {listeA.Count}");
            Console.WriteLine();

            Console.WriteLine($"IndexOf(\"W2\"):             {listeA.IndexOf("W2")}");
            Console.WriteLine($"IndexOf(\"X\"):              {listeA.IndexOf("X")}");
            Console.WriteLine();

            int entfernte = listeA.EntferneKleiner(100);
            Console.WriteLine($"EntferneKleiner(100):     {entfernte} Figuren entfernt");
            Console.Write("Liste nach Entfernen:     ");
            listeA.AusgabeAllerFiguren();
            Console.WriteLine($"Count:                    {listeA.Count}");
            Console.WriteLine();

            listeA.Rotieren(1);
            Console.Write("Nach Rotieren(1):         ");
            listeA.AusgabeAllerFiguren();

            listeA.Rotieren(7);
            Console.Write("Nach Rotieren(7):         ");
            listeA.AusgabeAllerFiguren();
            Console.WriteLine();

            listeA.Add(new Wuerfel("W5", 5));
            Console.Write("Nach Add(W5):             ");
            listeA.AusgabeAllerFiguren();
            Console.WriteLine($"Count:                    {listeA.Count}");
            Console.WriteLine();

            Console.WriteLine("==================================================");
            Console.WriteLine("      BEISPIEL B - DoppeltVerketteteFigurListe    ");
            Console.WriteLine("==================================================");

            DoppeltVerketteteFigurListe listeB = new DoppeltVerketteteFigurListe();
            Figur w0 = new Wuerfel("W0", 0.5);
            Figur k1 = new Kugel("K1", 1);
            Figur w3 = new Wuerfel("W3", 3);
            Figur w4 = new Wuerfel("W4", 4);

            listeB.Add(new Wuerfel("W1", 1));
            listeB.Add(w3);
            listeB.Add(new Kugel("K2", 2));
            listeB.Add(w4);

            Console.Write("Ausgangsliste (vorwaerts):  ");
            listeB.AusgabeVorwaerts();
            Console.Write("Ausgangsliste (rueckwaerts): ");
            listeB.AusgabeRuckwaerts();
            Console.WriteLine($"Count:                      {listeB.Count}");
            Console.WriteLine();

            listeB.InsertSortiert(k1);
            Console.Write("Nach InsertSortiert(K1):    ");
            listeB.AusgabeVorwaerts();

            listeB.InsertSortiert(new Wuerfel("W5", 5));
            Console.Write("Nach InsertSortiert(W5):    ");
            listeB.AusgabeVorwaerts();

            listeB.InsertSortiert(w0);
            Console.Write("Nach InsertSortiert(W0):    ");
            listeB.AusgabeVorwaerts();
            Console.WriteLine($"Count:                      {listeB.Count}");
            Console.WriteLine();

            Figur geloescht = listeB.PopLast();
            Console.WriteLine($"PopLast():                  Entfernt wurde {geloescht?.Beschreibung}");
            Console.Write("Liste nach PopLast():       ");
            listeB.AusgabeVorwaerts();
            Console.WriteLine($"Count:                      {listeB.Count}");
            Console.WriteLine();

            listeB.Vertausche(w0, w4);
            Console.Write("Nach Vertausche(W0, W4):    ");
            listeB.AusgabeVorwaerts();

            listeB.Vertausche(k1, w3);
            Console.Write("Nach Vertausche(K1, W3):    ");
            listeB.AusgabeVorwaerts();
            Console.WriteLine();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("                FINALE UEBERPRUEFUNG              ");
            Console.WriteLine("--------------------------------------------------");
            Console.Write("Vorwaerts:                  ");
            listeB.AusgabeVorwaerts();
            Console.Write("Rueckwaerts:                 ");
            listeB.AusgabeRuckwaerts();
            Console.WriteLine($"Count:                      {listeB.Count}");
            Console.WriteLine("==================================================");

            Console.ReadKey();
        }
    }
}