using System;
using System.Collections;
using System.Collections.Generic;

namespace _01_Figurliste
{
    interface IFigur
    {
        string Beschreibung { get; }
        double BerechneOberflaeche();
        double BerechneVolumen();
    }

    
    internal class Program
    {
        static void Main(string[] args)
        {
            FigurListe liste = new FigurListe();

            Kugel k1 = new Kugel("K1", 10);

            liste.Add(FigurFactory.ErstelleKugel("K2", 8));
            liste.Add(FigurFactory.ErstelleWuerfel("W1", 3));
            liste.Add(FigurFactory.ErstelleWuerfel("W2", 4));
            liste.Add(FigurFactory.ErstelleZylinder("Z1", 2, 5));
            liste.Add(k1);

            liste.RemoveAt(1);
            liste.Remove(k1);

            Console.WriteLine($"Anzahl der Elemente in der Liste: {liste.Count}");
            Console.WriteLine();

            liste.AusgabeAllerFiguren();
            Console.WriteLine();

            Console.WriteLine($"Gesamtvolumen aller Figuren: {liste.GesamtVolumen():F2}");
            Console.WriteLine();

            Console.WriteLine("Figuren mit Volumen > 50:");
            List<IFigur> grosseFiguren = liste.FiltereNachVolumen(50);
            foreach (IFigur figur in grosseFiguren)
            {
                Console.WriteLine(figur);
            }
            Console.WriteLine();

            Console.WriteLine("Figuren sortiert nach Volumen (aufsteigend):");
            List<IFigur> sortiert = liste.SortiertNachVolumen();
            foreach (IFigur figur in sortiert)
            {
                Console.WriteLine(figur);
            }

            Console.ReadKey();
        }
    }
}
