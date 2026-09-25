using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Figurliste
{
    internal class FigurFactory
    {
        public static IFigur ErstelleKugel(string beschreibung, double radius)
        {
            return new Kugel(beschreibung, radius);
        }

        public static IFigur ErstelleWuerfel(string beschreibung, double seitenlaenge)
        {
            return new Wuerfel(beschreibung, seitenlaenge);
        }

        public static IFigur ErstelleZylinder(string beschreibung, double radius, double hoehe)
        {
            return new Zylinder(beschreibung, radius, hoehe);
        }
    }
}
