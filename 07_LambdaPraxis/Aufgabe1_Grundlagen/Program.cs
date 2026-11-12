// ============================================================================
//  AUFGABE 1: LAMBDA-GRUNDLAGEN                                (ca. 12 Minuten)
// ============================================================================
//  Ziel:  Lambda Expressions lesen, schreiben und anpassen.
//
//  So arbeitest du:
//   - Suche im Code nach "TODO". Jede Teilaufgabe steht in einer eigenen Methode.
//   - Starte das Projekt (F5). Das Programm prüft deine Lösungen selbst:
//         [OK]      richtig
//         [FEHLER]  falsch (erwartet und erhalten werden angezeigt)
//         [OFFEN]   noch nicht bearbeitet
//   - Ändere nur den Code bei TODO. Die Prüfzeilen darunter bleiben unverändert.
//
//  Aufgabentypen:
//     [SCHREIBEN]  eine neue Lambda selbst schreiben
//     [ANPASSEN]   eine vorhandene Lambda ändern
//     [VERWENDEN]  eine vorhandene Methode mit einer Lambda aufrufen
//
//  Syntax-Erinnerung:
//     x => x * 2                      ein Parameter, Ausdruck
//     (a, b) => a + b                 mehrere Parameter
//     () => Console.WriteLine("Hi")   kein Parameter
//     x => { ...; return y; }         Anweisungsblock mit return
// ============================================================================

using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("1.1 Quadrat", Aufgabe1_1);
Aufgabe("1.2 Das Größere von zwei Zahlen", Aufgabe1_2);
Aufgabe("1.3 Lange Wörter", Aufgabe1_3);
Aufgabe("1.4 Wiederholen", Aufgabe1_4);
Aufgabe("1.5 Positiv und ungerade", Aufgabe1_5);
Aufgabe("1.6 Sicher dividieren", Aufgabe1_6);
Zusammenfassung();

// ----------------------------------------------------------------------------
// 1.1  [SCHREIBEN]  Quadrat                                         (ca. 2 min)
//      Schreibe eine Lambda, die eine Zahl mit sich selbst multipliziert.
// ----------------------------------------------------------------------------
static void Aufgabe1_1()
{
    Func<int, int> quadrat = x => x*x;   // TODO: Lambda zuweisen

    Pruefe("quadrat(5)", 25, quadrat(5));
    Pruefe("quadrat(-3)", 9, quadrat(-3));
}

// ----------------------------------------------------------------------------
// 1.2  [SCHREIBEN]  Das Größere von zwei Zahlen                     (ca. 2 min)
//      Die Lambda hat zwei Parameter und liefert die größere der beiden Zahlen.
//      Tipp: Der Bedingungsoperator  bedingung ? wennWahr : wennFalsch  hilft.
// ----------------------------------------------------------------------------
static void Aufgabe1_2()
{
    Func<int, int, int> groesser = (x, y) => x > y ? x: y;   // TODO: Lambda zuweisen

    Pruefe("groesser(3, 7)", 7, groesser(3, 7));
    Pruefe("groesser(9, 2)", 9, groesser(9, 2));
    Pruefe("groesser(4, 4)", 4, groesser(4, 4));
}

// ----------------------------------------------------------------------------
// 1.3  [SCHREIBEN]  Lange Wörter                                    (ca. 2 min)
//      Schreibe ein Predicate<string>, das true liefert, wenn der Text
//      MEHR als 5 Zeichen hat.
// ----------------------------------------------------------------------------
static void Aufgabe1_3()
{
    Predicate<string> istLang = word => word.Length > 5;   // TODO: Lambda zuweisen

    Pruefe("\"Lambda\" (6 Zeichen)", true, istLang("Lambda"));
    Pruefe("\"C#\" (2 Zeichen)", false, istLang("C#"));
    Pruefe("\"Fünf!\" (5 Zeichen)", false, istLang("Fünf!"));
}

// ----------------------------------------------------------------------------
// 1.4  [SCHREIBEN]  Wiederholen                                     (ca. 2 min)
//      Schreibe eine Action<string, int>, die den Text n-mal in die Liste 'log'
//      einträgt. Für die Schleife brauchst du einen Anweisungsblock { }.
// ----------------------------------------------------------------------------
static void Aufgabe1_4()
{
    var log = new List<string>();

    Action<string, int> wiederhole = (text, n) => {
        for (int i = 0; i < n; i++) {
            log.Add(text);
        }
    };   // TODO: Lambda mit Schleife zuweisen

    wiederhole("Lambda", 3);
    Pruefe("Anzahl der Einträge", 3, log.Count);
    Pruefe("Alle Einträge lauten \"Lambda\"", true, log.Count > 0 && log.All(t => t == "Lambda"));
}

// ----------------------------------------------------------------------------
// 1.5  [ANPASSEN]  Positiv und ungerade                             (ca. 2 min)
//      Die Lambda liefert bisher true für GERADE Zahlen.
//      Ändere sie so, dass sie true liefert, wenn die Zahl POSITIV UND UNGERADE ist.
// ----------------------------------------------------------------------------
static void Aufgabe1_5()
{
    Func<int, bool> bedingung = x => x > 0 && x % 2 != 0;   // TODO: anpassen

    Pruefe("bedingung(7)", true, bedingung(7));
    Pruefe("bedingung(-7)", false, bedingung(-7));
    Pruefe("bedingung(8)", false, bedingung(8));
    Pruefe("bedingung(1)", true, bedingung(1));
}

// ----------------------------------------------------------------------------
// 1.6  [SCHREIBEN]  Sicher dividieren                               (ca. 2 min)
//      Schreibe eine Lambda mit Anweisungsblock: Ist der Divisor 0, liefert sie
//      null (statt eine Ausnahme auszulösen), sonst das Ergebnis a / b.
//      Der Rückgabetyp int? erlaubt null.
// ----------------------------------------------------------------------------
static void Aufgabe1_6()
{
    Func<int, int, int?> sicherDividieren = (a, b) => {
        if (b == 0) {
            return null;
        }
        return a / b;
    };   // TODO: Lambda mit Anweisungsblock zuweisen

    Pruefe<int?>("10 / 2", 5, sicherDividieren(10, 2));
    Pruefe<int?>("7 / 0", null, sicherDividieren(7, 0));
}
