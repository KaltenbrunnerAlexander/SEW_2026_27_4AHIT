using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Figurliste
{
    abstract class Figur : IFigur, IComparable<Figur>
    {
        public string Beschreibung { get; }

        protected Figur(string beschreibung)
        {
            if (string.IsNullOrWhiteSpace(beschreibung))
                throw new ArgumentException("Beschreibung darf nicht leer sein.", nameof(beschreibung));

            Beschreibung = beschreibung;
        }

        public abstract double BerechneOberflaeche();
        public abstract double BerechneVolumen();

        public int CompareTo(Figur other)
        {
            if (other == null) return 1;
            return BerechneVolumen().CompareTo(other.BerechneVolumen());
        }

        public override string ToString()
        {
            return $"{Beschreibung} | Oberflaeche: {BerechneOberflaeche():F2} | Volumen: {BerechneVolumen():F2}";
        }
    }
}
