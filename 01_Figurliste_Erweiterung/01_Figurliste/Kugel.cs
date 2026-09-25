using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Figurliste
{
    class Kugel : Figur
    {
        public double Radius { get; }

        public Kugel(string beschreibung, double radius) : base(beschreibung)
        {
            if (radius <= 0)
                throw new ArgumentOutOfRangeException(nameof(radius), "Radius muss positiv sein.");

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
}
