// ============================================================================
//  AUFGABE 7: ABSCHLUSSAUFGABE LAGERVERWALTUNG                 (ca. 15 Minuten)
// ============================================================================
//  Ziel:  Alles zusammen anwenden. Die Aufgaben 7.1 bis 7.4 löst du in der Datei
//         Lager.cs, die Prüfungen stehen hier. Aufgabe 7.5 löst du in dieser Datei.
//
//  Ein Produkt hat Name, Kategorie, Preis (decimal) und Bestand (Stück).
// ============================================================================

using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("7.1 Suche (Lager.cs)", Aufgabe7_1);
Aufgabe("7.2 Gesamtwert (Lager.cs)", Aufgabe7_2);
Aufgabe("7.3 Wert je Kategorie (Lager.cs)", Aufgabe7_3);
Aufgabe("7.4 Bericht (Lager.cs)", Aufgabe7_4);
Aufgabe("7.5 Nachbestellliste", Aufgabe7_5);
Zusammenfassung();

static void Aufgabe7_1()
{
    var lager = new Lager();

    var guenstige = lager.Suche(p => p.Preis < 2m);

    PruefeFolge("Produkte unter 2 Euro",
        new[] { "Bleistift", "Kugelschreiber", "Lineal" },
        guenstige.Select(p => p.Name));
}

static void Aufgabe7_2()
{
    var lager = new Lager();

    Pruefe("Gesamtwert", 759.70m, lager.Gesamtwert());
}

static void Aufgabe7_3()
{
    var lager = new Lager();

    var werte = lager.WertProKategorie();

    Pruefe("Anzahl der Kategorien", 3, werte.Count);
    Pruefe("Schreibwaren", 270.00m, werte.GetValueOrDefault("Schreibwaren"));
    Pruefe("Papier", 165.40m, werte.GetValueOrDefault("Papier"));
    Pruefe("Technik", 324.30m, werte.GetValueOrDefault("Technik"));
}

static void Aufgabe7_4()
{
    var lager = new Lager();

    var zeilen = lager.Bericht(p => $"{p.Name} ({p.Kategorie})");

    Pruefe("Anzahl der Zeilen", 8, zeilen.Count);
    Pruefe("Erste Zeile", "Bleistift (Schreibwaren)", zeilen.FirstOrDefault());
    Pruefe("Letzte Zeile", "USB-Stick (Technik)", zeilen.LastOrDefault());
}

// ----------------------------------------------------------------------------
// 7.5  [SCHREIBEN]  Nachbestellliste                                (ca. 3 min)
//      Erstelle die Nachbestellliste:
//        a) 'nachbestellen': alle Produkte mit Preis unter 10 Euro UND einem
//           Bestand unter 5 Stück (verwende lager.Suche mit einer Lambda).
//        b) 'zeilen': aus 'nachbestellen' Texte im Format "Name: Bestand",
//           z. B. "Ordner: 2", alphabetisch nach Name sortiert
//           (OrderBy und Select).
//      Diese Aufgabe benötigt deine Lösung von 7.1.
// ----------------------------------------------------------------------------
static void Aufgabe7_5()
{
    var lager = new Lager();

    List<Produkt> nachbestellen = lager.Suche(p => false);   // TODO a): Lambda anpassen
    List<string> zeilen = new();                             // TODO b)

    PruefeFolge("Nachbestellliste",
        new[] { "Collegeblock: 4", "Ordner: 2", "USB-Stick: 3" },
        zeilen);
}
