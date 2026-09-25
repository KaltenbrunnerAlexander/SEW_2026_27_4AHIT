using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Figurliste
{
    class FigurListe : IEnumerable<IFigur>
    {
        private readonly List<IFigur> _figuren = new List<IFigur>();

        public int Count => _figuren.Count;

        public void Add(IFigur figur)
        {
            if (figur == null)
                throw new ArgumentNullException(nameof(figur));

            _figuren.Add(figur);
        }

        public bool Remove(IFigur figur)
        {
            return _figuren.Remove(figur);
        }

        public void RemoveAt(int index)
        {
            _figuren.RemoveAt(index);
        }

        public void AusgabeAllerFiguren()
        {
            foreach (IFigur figur in _figuren)
            {
                Console.WriteLine(figur);
            }
        }

        public double GesamtVolumen()
        {
            double summe = 0;
            foreach (IFigur figur in _figuren)
            {
                summe += figur.BerechneVolumen();
            }
            return summe;
        }

        public List<IFigur> FiltereNachVolumen(double minVolumen)
        {
            List<IFigur> ergebnis = new List<IFigur>();
            foreach (IFigur figur in _figuren)
            {
                if (figur.BerechneVolumen() > minVolumen)
                {
                    ergebnis.Add(figur);
                }
            }
            return ergebnis;
        }

        public List<IFigur> SortiertNachVolumen()
        {
            List<IFigur> kopie = new List<IFigur>(_figuren);

            kopie.Sort(delegate (IFigur a, IFigur b)
            {
                Figur figurA = (Figur)a;
                Figur figurB = (Figur)b;
                return figurA.CompareTo(figurB);
            });

            return kopie;
        }

        public IEnumerator<IFigur> GetEnumerator()
        {
            return _figuren.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
