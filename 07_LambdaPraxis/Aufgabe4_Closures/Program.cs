// ============================================================================
//  AUFGABE 4: CLOSURES                                         (ca. 10 Minuten)
// ============================================================================
//  Ziel:  Verstehen, dass eine Lambda Variablen ihrer Umgebung "einfängt"
//         (Closure) - und dabei die VARIABLE selbst, nicht nur ihren Wert.
//
//  Beispiel:
//     int faktor = 2;
//     Func<int, int> mal = x => x * faktor;   // 'faktor' wird eingefangen
//     faktor = 10;
//     mal(5);                                  // ergibt 50, nicht 10!
// ============================================================================

using LambdaPraxis.Hilfe;
using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("4.1 Vermutung", Aufgabe4_1);
Aufgabe("4.2 Zähler", Aufgabe4_2);
Aufgabe("4.3 Lambda zurückgeben", Aufgabe4_3);
Aufgabe("4.4 Die Schleifen-Falle", Aufgabe4_4);
Zusammenfassung();

// ----------------------------------------------------------------------------
// 4.1  [VERWENDEN]  Vermutung                                       (ca. 2 min)
//      Lies den Code. Wie viele Schüler liefert FindAll beim ersten Aufruf
//      (ergebnis1) und wie viele beim zweiten (ergebnis2)?
//      Trage deine Vermutung ein, BEVOR du das Programm startest.
//      Erkläre danach in einem Kommentar: Warum ändert sich das Ergebnis,
//      obwohl 'gut' nicht neu zugewiesen wird?
// ----------------------------------------------------------------------------
static void Aufgabe4_1()
{
    var klasse = Daten.ErzeugeKlasse();

    int grenze = 2;
    Predicate<Schueler> gut = s => s.Note <= grenze;

    int ergebnis1 = klasse.FindAll(gut).Count;
    grenze = 4;
    int ergebnis2 = klasse.FindAll(gut).Count;

    int vermutung1 = 3;   // TODO: deine Vermutung für ergebnis1
    int vermutung2 = 6;   // TODO: deine Vermutung für ergebnis2
    // TODO: Begründung als Kommentar:

    Pruefe("Vermutung 1 stimmt", true, vermutung1 == ergebnis1);
    Pruefe("Vermutung 2 stimmt", true, vermutung2 == ergebnis2);
}

// ----------------------------------------------------------------------------
// 4.2  [SCHREIBEN]  Zähler                                          (ca. 2 min)
//      Rufe ForEach auf 'klasse' auf. Die Lambda soll die Variable 'zaehler'
//      (sie liegt AUSSERHALB der Lambda) um 1 erhöhen, wenn der Schüler
//      16 Jahre alt ist. Du brauchst einen Anweisungsblock { }.
// ----------------------------------------------------------------------------
static void Aufgabe4_2()
{
    var klasse = Daten.ErzeugeKlasse();
    int zaehler = 0;

    klasse.ForEach(s => { if (s.Alter == 16) zaehler++; });   // TODO: ForEach aufrufen)

    Pruefe("Anzahl 16-Jähriger", 3, zaehler);
}

// ----------------------------------------------------------------------------
// 4.3  [SCHREIBEN]  Lambda zurückgeben                              (ca. 3 min)
//      Ergänze die Methode ErzeugeAddierer unten: Sie soll eine Lambda
//      zurückgeben, die zu ihrem Parameter x den Wert n addiert.
//      Beachte: Die Methode ist längst beendet, wenn die Lambda aufgerufen wird.
//      Wo "lebt" n dann? (Antwort als Kommentar.)
// ----------------------------------------------------------------------------
static void Aufgabe4_3()
{
    Func<int, int> addiere5 = ErzeugeAddierer(5);
    Func<int, int> addiere10 = ErzeugeAddierer(10);

    Pruefe("addiere5(1)", 6, addiere5(1));
    Pruefe("addiere10(1)", 11, addiere10(1));
    Pruefe("addiere5(addiere10(0))", 15, addiere5(addiere10(0)));
}

static Func<int, int> ErzeugeAddierer(int n)
{
    throw new NotImplementedException();   // TODO: Lambda zurückgeben
}

// ----------------------------------------------------------------------------
// 4.4  [ANPASSEN]  Die Schleifen-Falle                              (ca. 3 min)
//      Die drei Lambdas sollen die Zahlen 0, 1 und 2 in 'ergebnisse' eintragen.
//      Starte das Programm: Es entstehen aber 3, 3, 3! Warum?
//      Tipp: Alle Lambdas fangen DIESELBE Variable i ein. Nach der Schleife ist i = 3.
//      Ändere den Code so, dass jede Lambda ihren eigenen Wert behält.
// ----------------------------------------------------------------------------
static void Aufgabe4_4()
{
    var ergebnisse = new List<int>();
    var aktionen = new List<Action>();

    for (int i = 0; i < 3; i++)
    {
        aktionen.Add(() => ergebnisse.Add(i));   // TODO: Fehler beheben
    }

    aktionen.ForEach(a => a());

    PruefeFolge("Ergebnisse", new[] { 0, 1, 2 }, ergebnisse);
}
