// ============================================================================
//  AUFGABE 3: LINQ MIT LAMBDAS                                 (ca. 20 Minuten)
// ============================================================================
//  Ziel:  Daten mit LINQ filtern, umformen, sortieren, gruppieren und auswerten.
//
//  Wichtige Methoden (jeweils mit einer Lambda als Parameter):
//     Where(s => bedingung)          nur passende Elemente
//     Select(s => wert)              jedes Element umformen
//     OrderBy / ThenBy(s => schlüssel), OrderByDescending
//     Take(n), First(), ToList()     ohne Lambda, aber oft in der Kette
//     Any / All(s => bedingung)      mindestens ein / alle Elemente passen
//     Average / Sum / Count / Max(s => ...)
//     GroupBy(s => schlüssel)        Gruppen; g.Key ist der Schlüssel
//     ToDictionary(g => key, g => wert)
//
//  LINQ verändert die Liste NICHT, sondern liefert ein neues Ergebnis.
//  Die Testdaten liefert Daten.ErzeugeKlasse().
// ============================================================================

using LambdaPraxis.Hilfe;
using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("3.1 Where und Select", Aufgabe3_1);
Aufgabe("3.2 OrderBy und Take", Aufgabe3_2);
Aufgabe("3.3 Durchschnitt anpassen", Aufgabe3_3);
Aufgabe("3.4 Any und All", Aufgabe3_4);
Aufgabe("3.5 Texte formatieren", Aufgabe3_5);
Aufgabe("3.6 GroupBy", Aufgabe3_6);
Aufgabe("3.7 Kette", Aufgabe3_7);
Zusammenfassung();

// ----------------------------------------------------------------------------
// 3.1  [SCHREIBEN]  Where und Select                                (ca. 2 min)
//      Erzeuge eine Liste mit den NAMEN aller 16-Jährigen.
// ----------------------------------------------------------------------------
static void Aufgabe3_1()
{
    var klasse = Daten.ErzeugeKlasse();

    List<string> namen16 = klasse.Where(s => s.Alter == 16).Select(s => s.Name).ToList();   // TODO: Where, Select, ToList

    PruefeFolge("Namen der 16-Jährigen", new[] { "Anna", "Clara", "Felix" }, namen16);
}

// ----------------------------------------------------------------------------
// 3.2  [SCHREIBEN]  OrderBy und Take                                (ca. 2 min)
//      Erzeuge eine Liste mit den Namen der DREI BESTEN Schüler
//      (kleinste Noten), beste zuerst. Tipp: OrderBy, Take(3), Select, ToList.
// ----------------------------------------------------------------------------
static void Aufgabe3_2()
{
    var klasse = Daten.ErzeugeKlasse();

    List<string> besteDrei = klasse.OrderBy(s => s.Note).Take(3).Select(s => s.Name).ToList();   // TODO

    PruefeFolge("Die drei besten", new[] { "Eva", "Anna", "Clara" }, besteDrei);
}

// ----------------------------------------------------------------------------
// 3.3  [ANPASSEN]  Durchschnitt anpassen                            (ca. 3 min)
//      Die Abfrage berechnet bisher die Durchschnittsnote der GANZEN Klasse.
//      Ändere sie so, dass nur die 17-Jährigen berücksichtigt werden.
//      Tipp: Füge VOR Average ein Where ein.
// ----------------------------------------------------------------------------
static void Aufgabe3_3()
{
    var klasse = Daten.ErzeugeKlasse();

    double schnitt = klasse.Where(s => s.Alter == 17).Average(s => s.Note);   // TODO: anpassen

    Pruefe("Schnitt der 17-Jährigen (auf 2 Stellen gerundet)", 2.17, Math.Round(schnitt, 2));
}

// ----------------------------------------------------------------------------
// 3.4  [SCHREIBEN]  Any und All                                     (ca. 3 min)
//      Ersetze die Startwerte durch LINQ-Abfragen mit Any bzw. All:
//        - alleMindestens16:   sind ALLE mindestens 16 Jahre alt?
//        - jemandMitFuenf:     hat IRGENDJEMAND genau die Note 5,0?
//        - alleBestanden:      haben ALLE bestanden (Note <= 4,0)?
// ----------------------------------------------------------------------------
static void Aufgabe3_4()
{
    var klasse = Daten.ErzeugeKlasse();

    bool alleMindestens16 = klasse.All(s => s.Alter > 15);   // TODO
    bool jemandMitFuenf = klasse.Any(s => s.Note == 5.0);     // TODO
    bool alleBestanden = klasse.All(s => s.Note <= 4.0);       // TODO

    Pruefe("Alle mindestens 16", true, alleMindestens16);
    Pruefe("Jemand mit Note 5,0", true, jemandMitFuenf);
    Pruefe("Alle bestanden", false, alleBestanden);
}

// ----------------------------------------------------------------------------
// 3.5  [SCHREIBEN]  Texte formatieren                               (ca. 3 min)
//      Erzeuge eine Liste von Texten im Format "Anna (16)" (Name und Alter in
//      Klammern, mit String-Interpolation $"..."). Berücksichtige nur Schüler,
//      die bestanden haben (Note <= 4,0), alphabetisch nach Name sortiert.
// ----------------------------------------------------------------------------
static void Aufgabe3_5()
{
    var klasse = Daten.ErzeugeKlasse();

    List<string> texte = klasse.Where(s => s.Note <= 4.0).OrderBy(s => s.Name).Select(s => $"{s.Name} ({s.Alter})").ToList();   // TODO: Where, OrderBy, Select, ToList

    PruefeFolge("Bestandene Schüler",
        new[] { "Anna (16)", "Ben (17)", "Clara (16)", "Eva (17)", "Greta (17)", "Hannes (18)" },
        texte);
}

// ----------------------------------------------------------------------------
// 3.6  [SCHREIBEN]  GroupBy                                         (ca. 4 min)
//      Berechne die Durchschnittsnote JE ALTER als Dictionary<int, double>
//      (Schlüssel = Alter, Wert = Durchschnittsnote, auf 2 Stellen gerundet).
//      Tipp:   klasse.GroupBy(s => ...)
//                    .ToDictionary(g => g.Key, g => Math.Round(g.Average(...), 2));
// ----------------------------------------------------------------------------
static void Aufgabe3_6()
{
    var klasse = Daten.ErzeugeKlasse();

    Dictionary<int, double> schnittProAlter = klasse.GroupBy(s => s.Alter)
        .ToDictionary(g => g.Key, g => Math.Round(g.Average(s => s.Note), 2));   // TODO

    Pruefe("Anzahl der Altersgruppen", 3, schnittProAlter.Count);
    Pruefe("Schnitt der 16-Jährigen", 2.83, schnittProAlter.GetValueOrDefault(16));
    Pruefe("Schnitt der 17-Jährigen", 2.17, schnittProAlter.GetValueOrDefault(17));
    Pruefe("Schnitt der 18-Jährigen", 4.0, schnittProAlter.GetValueOrDefault(18));
}

// ----------------------------------------------------------------------------
// 3.7  [SCHREIBEN]  Kette                                           (ca. 3 min)
//      Bestimme den NAMEN des Schülers mit der besten Note unter allen
//      18-Jährigen. Kombiniere Where, OrderBy und First (oder MinBy).
// ----------------------------------------------------------------------------
static void Aufgabe3_7()
{
    var klasse = Daten.ErzeugeKlasse();

    string? besterAchtzehner = klasse.Where(s => s.Alter == 18).OrderBy(s => s.Note).Select(s => s.Name).First();   // TODO

    Pruefe("Bester 18-Jähriger", "Hannes", besterAchtzehner);
}