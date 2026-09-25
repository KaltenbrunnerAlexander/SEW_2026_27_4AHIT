using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Figurliste
{
    class Zylinder : Figur
    {
        public double Radius { get; }
        public double Hoehe { get; }

        public Zylinder(string beschreibung, double radius, double hoehe) : base(beschreibung)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius muss positiv sein.");
            if (hoehe <= 0)
                throw new ArgumentOutOfRangeException(nameof(hoehe), "Hoehe muss positiv sein.");

            Radius = radius;
            Hoehe = hoehe;
        }

        public override double BerechneOberflaeche()
        {
            return 2.0 * Math.PI * Radius * (Radius + Hoehe);
        }

        public override double BerechneVolumen()
        {
            return Math.PI * Math.Pow(Radius, 2) * Hoehe;
        }
    }
}

