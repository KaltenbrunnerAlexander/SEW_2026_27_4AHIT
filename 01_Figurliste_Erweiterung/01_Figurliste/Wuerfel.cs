using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Figurliste
{
    class Wuerfel : Figur
    {
        public double Seitenlaenge { get; }

        public Wuerfel(string beschreibung, double a) : base(beschreibung)
        {
            if (a <= 0)
                throw new ArgumentOutOfRangeException(nameof(a), "Seitenlaenge muss positiv sein.");

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
}
