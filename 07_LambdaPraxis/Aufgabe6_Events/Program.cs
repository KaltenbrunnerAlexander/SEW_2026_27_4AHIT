// ============================================================================
//  AUFGABE 6: LAMBDAS UND EREIGNISSE (EVENTS)                  (ca. 13 Minuten)
// ============================================================================
//  Ziel:  Ereignisse mit Lambdas abonnieren (+=) und wieder abmelden (-=).
//
//  Die Klassen Thermometer und Knopf stehen in Klassen.cs (nichts ändern).
//
//     thermometer.TemperaturGeaendert += t => { ... };      // abonnieren
//     knopf.Geklickt += (sender, e) => { ... };              // abonnieren
//
//  Der Handler wird bei jeder Auslösung des Ereignisses ausgeführt.
// ============================================================================

using static LambdaPraxis.Hilfe.Uebung;

Aufgabe("6.1 Messwerte sammeln", Aufgabe6_1);
Aufgabe("6.2 Alarm", Aufgabe6_2);
Aufgabe("6.3 Klicks protokollieren", Aufgabe6_3);
Aufgabe("6.4 Abmelden", Aufgabe6_4);
Zusammenfassung();

// ----------------------------------------------------------------------------
// 6.1  [VERWENDEN]  Messwerte sammeln                               (ca. 3 min)
//      Abonniere TemperaturGeaendert mit einer Lambda (+=), die jeden neuen
//      Wert zur Liste 'messwerte' hinzufügt.
// ----------------------------------------------------------------------------
static void Aufgabe6_1()
{
    var thermometer = new Thermometer();
    var messwerte = new List<double>();

    // TODO: thermometer.TemperaturGeaendert += ...

    thermometer.Setze(20.0);
    thermometer.Setze(22.5);

    PruefeFolge("Messwerte", new[] { 20.0, 22.5 }, messwerte);
}

// ----------------------------------------------------------------------------
// 6.2  [SCHREIBEN]  Alarm                                           (ca. 3 min)
//      Abonniere das Ereignis mit einer Lambda, die die Variable 'alarm' auf
//      true setzt, sobald die Temperatur ÜBER 30 liegt.
//      Der Alarm bleibt aktiv, auch wenn die Temperatur wieder sinkt!
// ----------------------------------------------------------------------------
static void Aufgabe6_2()
{
    var thermometer = new Thermometer();
    bool alarm = false;

    // TODO: thermometer.TemperaturGeaendert += ... (Anweisungsblock mit if)

    thermometer.Setze(25);
    Pruefe("Kein Alarm bei 25", false, alarm);

    thermometer.Setze(35);
    Pruefe("Alarm bei 35", true, alarm);

    thermometer.Setze(20);
    Pruefe("Alarm bleibt aktiv bei 20", true, alarm);
}

// ----------------------------------------------------------------------------
// 6.3  [ANPASSEN]  Klicks protokollieren                            (ca. 3 min)
//      Der Handler erhöht bisher nur den 'zaehler'.
//      Ändere ihn (Ausdruck-Lambda -> Anweisungsblock) so, dass er zusätzlich
//      für jeden Klick einen Eintrag "Klick 1", "Klick 2", ... in 'log' schreibt.
// ----------------------------------------------------------------------------
static void Aufgabe6_3()
{
    var knopf = new Knopf();
    int zaehler = 0;
    var log = new List<string>();

    knopf.Geklickt += (sender, e) => zaehler++;   // TODO: anpassen

    knopf.Klick();
    knopf.Klick();
    knopf.Klick();

    Pruefe("Zähler", 3, zaehler);
    PruefeFolge("Protokoll", new[] { "Klick 1", "Klick 2", "Klick 3" }, log);
}

// ----------------------------------------------------------------------------
// 6.4  [SCHREIBEN]  Abmelden                                        (ca. 4 min)
//      Eine Lambda, die du direkt hinschreibst, kannst du nicht mit -= wieder
//      abmelden, denn du hast keine Referenz auf sie. Speichere sie deshalb
//      zuerst in einer Variablen 'handler':
//         1) handler: Lambda, die den Wert zu 'werte' hinzufügt
//         2) handler mit += abonnieren, dann Setze(10)
//         3) handler mit -= abmelden, dann Setze(20)
//      Nur der erste Wert (10) darf in 'werte' stehen.
// ----------------------------------------------------------------------------
static void Aufgabe6_4()
{
    var thermometer = new Thermometer();
    var werte = new List<double>();

    Action<double> handler = null!;   // TODO 1: Lambda zuweisen
    // TODO 2: handler abonnieren
    thermometer.Setze(10);
    // TODO 3: handler abmelden
    thermometer.Setze(20);

    PruefeFolge("Nur der erste Wert", new[] { 10.0 }, werte);
}
