// ============================================================================
//  AUFGABE 5: EIGENE METHODEN MIT LAMBDA-PARAMETERN            (ca. 15 Minuten)
// ============================================================================
//  Ziel:  Methoden schreiben, die selbst eine Lambda als Parameter erwarten
//         oder eine Lambda zurückgeben - so funktionieren Where, Select & Co.
//
//  Die aufrufenden Lambdas stehen schon in den Testmethoden.
//  Du schreibst die Methoden, die sie ausführen, und in 5.3 / 5.5 selbst Lambdas.
//  Aufgabe 5.3 hängt von 5.1 und 5.2 ab (sonst bleibt sie [OFFEN]).
// ============================================================================

using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("5.1 Filtere", Aufgabe5_1);
Aufgabe("5.2 Transformiere", Aufgabe5_2);
Aufgabe("5.3 Methoden verwenden", Aufgabe5_3);
Aufgabe("5.4 Verkette", Aufgabe5_4);
Aufgabe("5.5 WiederholeMal verwenden", Aufgabe5_5);
Aufgabe("5.6 Zusatzaufgabe: generisches Filtere", Aufgabe5_6);
Zusammenfassung();

// ----------------------------------------------------------------------------
// 5.1  [SCHREIBEN]  Filtere                                         (ca. 4 min)
//      Implementiere die Methode Filtere unten: Sie liefert eine neue Liste mit
//      allen Zahlen, für die 'bedingung' true liefert (wie Where / FindAll).
//      Verwende hier bewusst eine Schleife und rufe 'bedingung' wie eine
//      Methode auf: bedingung(z).
// ----------------------------------------------------------------------------
static void Aufgabe5_1()
{
    var zahlen = Enumerable.Range(1, 10).ToList();

    PruefeFolge("gerade Zahlen", new[] { 2, 4, 6, 8, 10 }, Filtere(zahlen, z => z % 2 == 0));
    PruefeFolge("größer als 7", new[] { 8, 9, 10 }, Filtere(zahlen, z => z > 7));
}

static List<int> Filtere(List<int> zahlen, Predicate<int> bedingung)
{
    throw new NotImplementedException();   // TODO
}

// ----------------------------------------------------------------------------
// 5.2  [SCHREIBEN]  Transformiere                                   (ca. 3 min)
//      Implementiere Transformiere unten: Die Methode wendet 'regel' auf JEDE
//      Zahl an und liefert die Ergebnisse als neue Liste (wie Select).
// ----------------------------------------------------------------------------
static void Aufgabe5_2()
{
    var zahlen = new List<int> { 1, 2, 3 };

    PruefeFolge("Quadrate", new[] { 1, 4, 9 }, Transformiere(zahlen, z => z * z));
    PruefeFolge("plus 10", new[] { 11, 12, 13 }, Transformiere(zahlen, z => z + 10));
}

static List<int> Transformiere(List<int> zahlen, Func<int, int> regel)
{
    throw new NotImplementedException();   // TODO
}

// ----------------------------------------------------------------------------
// 5.3  [VERWENDEN]  Methoden verwenden                              (ca. 3 min)
//      Berechne die QUADRATE der UNGERADEN Zahlen von 1 bis 10 mit deinen
//      Methoden Filtere und Transformiere. Schreibe dazu die beiden Lambdas.
// ----------------------------------------------------------------------------
static void Aufgabe5_3()
{
    var zahlen = Enumerable.Range(1, 10).ToList();

    Predicate<int> ungerade = null!;   // TODO: Lambda zuweisen
    Func<int, int> quadrat = null!;    // TODO: Lambda zuweisen

    var ergebnis = Transformiere(Filtere(zahlen, ungerade), quadrat);

    PruefeFolge("Quadrate der ungeraden Zahlen", new[] { 1, 9, 25, 49, 81 }, ergebnis);
}

// ----------------------------------------------------------------------------
// 5.4  [SCHREIBEN]  Verkette                                        (ca. 3 min)
//      Verkette liefert eine NEUE Lambda zurück: Sie wendet zuerst 'erste' und
//      auf das Ergebnis dann 'zweite' an.  Verkette(f, g)(x)  entspricht  g(f(x)).
// ----------------------------------------------------------------------------
static void Aufgabe5_4()
{
    var plus1DannMal2 = Verkette(x => x + 1, x => x * 2);
    var mal2DannPlus1 = Verkette(x => x * 2, x => x + 1);

    Pruefe("(3 + 1) * 2", 8, plus1DannMal2(3));
    Pruefe("(3 * 2) + 1", 7, mal2DannPlus1(3));
}

static Func<int, int> Verkette(Func<int, int> erste, Func<int, int> zweite)
{
    throw new NotImplementedException();   // TODO
}

// ----------------------------------------------------------------------------
// 5.5  [VERWENDEN]  WiederholeMal verwenden                         (ca. 2 min)
//      Die Methode WiederholeMal ist schon fertig (siehe unten).
//      Rufe sie so auf, dass dreimal der Text "Hallo" zur Liste 'begruessungen'
//      hinzugefügt wird. Du brauchst eine Lambda ohne Parameter: () => ...
// ----------------------------------------------------------------------------
static void Aufgabe5_5()
{
    var begruessungen = new List<string>();

    // TODO: WiederholeMal(3, ...) aufrufen

    PruefeFolge("Begrüßungen", new[] { "Hallo", "Hallo", "Hallo" }, begruessungen);
}

static void WiederholeMal(int n, Action aktion)
{
    for (int i = 0; i < n; i++)
        aktion();
}

// ----------------------------------------------------------------------------
// 5.6  [SCHREIBEN]  Zusatzaufgabe (nicht in den 100 Minuten eingerechnet)
//      Schreibe FiltereGenerisch<T> unten: Wie Filtere, aber für Listen
//      BELIEBIGEN Typs T mit einem Predicate<T>.
// ----------------------------------------------------------------------------
static void Aufgabe5_6()
{
    var woerter = new List<string> { "Anna", "Ben", "Alex", "Clara" };

    PruefeFolge("Wörter mit A", new[] { "Anna", "Alex" }, FiltereGenerisch(woerter, w => w.StartsWith("A")));
    PruefeFolge("Kurze Wörter", new[] { "Ben" }, FiltereGenerisch(woerter, w => w.Length < 4));
}

static List<T> FiltereGenerisch<T>(List<T> liste, Predicate<T> bedingung)
{
    throw new NotImplementedException();   // TODO
}
