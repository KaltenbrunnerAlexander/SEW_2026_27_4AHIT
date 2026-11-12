// ============================================================================
//  AUFGABE 2: LAMBDAS UND List<T>                              (ca. 15 Minuten)
// ============================================================================
//  Ziel:  Methoden von List<T> mit Lambdas verwenden:
//         ForEach, FindAll, Find, Exists, Sort, RemoveAll.
//
//  Ablauf wie in Aufgabe 1: "TODO" suchen, Projekt starten, Ausgabe lesen.
//  Die Testdaten liefert Daten.ErzeugeKlasse() (8 Schüler mit Name, Alter, Note).
//  Note 1,0 ist die beste, 5,0 die schlechteste.
//
//  Welcher Parameter gehört zu welcher Methode?
//     ForEach            Action<Schueler>        tut etwas mit jedem Element
//     FindAll, Find,
//     Exists, RemoveAll  Predicate<Schueler>     Bedingung: true / false
//     Sort               Comparison<Schueler>    vergleicht zwei Elemente (a, b)
//                                                 < 0: a zuerst, 0: gleich, > 0: b zuerst
// ============================================================================

using LambdaPraxis.Hilfe;
using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("2.1 ForEach", Aufgabe2_1);
Aufgabe("2.2 FindAll", Aufgabe2_2);
Aufgabe("2.3 Find", Aufgabe2_3);
Aufgabe("2.4 Exists", Aufgabe2_4);
Aufgabe("2.5a Sort nach Note", Aufgabe2_5a);
Aufgabe("2.5b Sort nach Alter und Name", Aufgabe2_5b);
Aufgabe("2.6 RemoveAll", Aufgabe2_6);
Zusammenfassung();

// ----------------------------------------------------------------------------
// 2.1  [VERWENDEN]  ForEach                                         (ca. 2 min)
//      Rufe ForEach auf 'klasse' auf. Die Lambda soll den Namen jedes Schülers
//      in GROSSBUCHSTABEN (ToUpper()) zur Liste 'namen' hinzufügen.
// ----------------------------------------------------------------------------
static void Aufgabe2_1()
{
    var klasse = Daten.ErzeugeKlasse();
    var namen = new List<string>();

    // TODO: klasse.ForEach(...) aufrufen

    klasse.ForEach(klasse => namen.Add(klasse.Name.ToUpper()));

    Pruefe("Anzahl der Namen", 8, namen.Count);
    Pruefe("Erster Name", "ANNA", namen.FirstOrDefault());
}

// ----------------------------------------------------------------------------
// 2.2  [SCHREIBEN]  FindAll                                         (ca. 2 min)
//      Finde mit FindAll alle Schüler mit Note 2,0 oder besser (Note <= 2.0).
// ----------------------------------------------------------------------------
static void Aufgabe2_2()
{
    var klasse = Daten.ErzeugeKlasse();

    List<Schueler> gute = klasse.FindAll(s => s.Note <= 2.0);   // TODO: klasse.FindAll(...) zuweisen

    Pruefe("Anzahl guter Schüler", 3, gute.Count);
    Pruefe("Alle haben Note <= 2,0", true, gute.Count > 0 && gute.All(s => s.Note <= 2.0));
}

// ----------------------------------------------------------------------------
// 2.3  [ANPASSEN]  Find                                             (ca. 2 min)
//      Die Lambda sucht bisher den Schüler mit dem Namen "Ben".
//      Ändere sie so, dass der ERSTE Schüler gefunden wird, der 18 Jahre alt ist
//      UND eine Note schlechter als 4,0 hat (Note > 4.0).
// ----------------------------------------------------------------------------
static void Aufgabe2_3()
{
    var klasse = Daten.ErzeugeKlasse();

    Schueler? gefunden = klasse.Find(s => s.Alter == 18 && s.Note > 4.0);   // TODO: anpassen

    Pruefe("Gefundener Name", "David", gefunden?.Name);
}

// ----------------------------------------------------------------------------
// 2.4  [SCHREIBEN]  Exists                                          (ca. 2 min)
//      Ersetze die beiden Startwerte durch Aufrufe von klasse.Exists(...):
//        - gibtEinser:               gibt es einen Schüler mit Note 1,0?
//        - gibtSchlechterAlsFuenf:   gibt es einen Schüler mit Note > 5,0?
// ----------------------------------------------------------------------------
static void Aufgabe2_4()
{
    var klasse = Daten.ErzeugeKlasse();

    bool gibtEinser = klasse.Exists(s => s.Note == 1.0);                // TODO
    bool gibtSchlechterAlsFuenf = klasse.Exists(s => s.Note > 5.0);     // TODO

    Pruefe("Es gibt einen Einser", true, gibtEinser);
    Pruefe("Es gibt jemanden schlechter als 5,0", false, gibtSchlechterAlsFuenf);
}

// ----------------------------------------------------------------------------
// 2.5a  [ANPASSEN]  Sort nach Note                                  (ca. 1 min)
//       Die Lambda sortiert bisher alphabetisch nach Name.
//       Ändere sie so, dass nach Note AUFSTEIGEND sortiert wird (beste zuerst).
//       Tipp: double hat ebenfalls die Methode CompareTo.
// ----------------------------------------------------------------------------
static void Aufgabe2_5a()
{
    var klasse = Daten.ErzeugeKlasse();

    klasse.Sort((a, b) => a.Note.CompareTo(b.Note));   // TODO: anpassen

    Pruefe("Erster (beste Note)", "Eva", klasse[0].Name);
    Pruefe("Letzter (schlechteste Note)", "Felix", klasse[^1].Name);
}

// ----------------------------------------------------------------------------
// 2.5b  [ANPASSEN]  Sort nach Alter und Name                        (ca. 3 min)
//       Sortiere nach Alter ABSTEIGEND (Älteste zuerst). Bei gleichem Alter
//       soll alphabetisch nach Name sortiert werden.
//       Tipp: Du brauchst einen Anweisungsblock. Vergleiche zuerst das Alter
//       (b.Alter.CompareTo(a.Alter) sortiert absteigend). Ist das Ergebnis 0,
//       vergleiche den Namen.
// ----------------------------------------------------------------------------
static void Aufgabe2_5b()
{
    var klasse = Daten.ErzeugeKlasse();

    klasse.Sort((a, b) => {
        int compareAge = b.Alter.CompareTo(a.Alter);
        if (compareAge != 0) {
            return compareAge;
        }
        return a.Name.CompareTo(b.Name);
    });   // TODO: anpassen (Anweisungsblock)

    PruefeFolge("Reihenfolge",
        new[] { "David", "Hannes", "Ben", "Eva", "Greta", "Anna", "Clara", "Felix" },
        klasse.Select(s => s.Name));
}

// ----------------------------------------------------------------------------
// 2.6  [SCHREIBEN]  RemoveAll                                       (ca. 3 min)
//      Entferne mit RemoveAll alle Schüler, die nicht bestanden haben
//      (Note schlechter als 4,0). RemoveAll liefert die Anzahl der entfernten
//      Elemente zurück: Speichere sie in 'entfernt'.
// ----------------------------------------------------------------------------
static void Aufgabe2_6()
{
    var klasse = Daten.ErzeugeKlasse();

    int entfernt = klasse.RemoveAll(s => s.Note > 4.0);   // TODO: klasse.RemoveAll(...) zuweisen

    Pruefe("Anzahl entfernt", 2, entfernt);
    Pruefe("Anzahl verbleibend", 6, klasse.Count);
}
