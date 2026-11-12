// ============================================================================
//  AUFGABE 7 (Teil 1): DIE KLASSE Lager                         (ca. 12 Minuten)
// ============================================================================
//  Ergänze die vier Methoden dieser Klasse mit Lambdas, FindAll, Sum, GroupBy
//  usw. Die Lambdas, mit denen die Methoden aufgerufen werden, stehen in
//  Program.cs. Dort findest du auch die Prüfungen und die Aufgabe 7.5.
// ============================================================================

public class Lager
{
    private readonly List<Produkt> _produkte = new()
    {
        new("Bleistift", "Schreibwaren", 0.80m, 120),
        new("Kugelschreiber", "Schreibwaren", 1.50m, 80),
        new("Notizbuch", "Papier", 4.90m, 30),
        new("Collegeblock", "Papier", 3.20m, 4),
        new("Taschenrechner", "Technik", 24.90m, 12),
        new("USB-Stick", "Technik", 8.50m, 3),
        new("Lineal", "Schreibwaren", 1.20m, 45),
        new("Ordner", "Papier", 2.80m, 2)
    };

    // ------------------------------------------------------------------------
    // 7.1  [SCHREIBEN]  Suche                                       (ca. 3 min)
    //      Liefere alle Produkte, für die 'filter' true ergibt (Reihenfolge
    //      wie im Lager).
    // ------------------------------------------------------------------------
    public List<Produkt> Suche(Predicate<Produkt> filter)
    {
        throw new NotImplementedException();   // TODO
    }

    // ------------------------------------------------------------------------
    // 7.2  [SCHREIBEN]  Gesamtwert                                  (ca. 3 min)
    //      Gesamtwert des Lagers = Summe von Preis * Bestand aller Produkte.
    //      Tipp: Sum(p => ...)
    // ------------------------------------------------------------------------
    public decimal Gesamtwert()
    {
        throw new NotImplementedException();   // TODO
    }

    // ------------------------------------------------------------------------
    // 7.3  [SCHREIBEN]  Wert je Kategorie                           (ca. 3 min)
    //      Liefere für jede Kategorie den Gesamtwert (Preis * Bestand) der
    //      Produkte dieser Kategorie. Tipp: GroupBy und ToDictionary.
    // ------------------------------------------------------------------------
    public Dictionary<string, decimal> WertProKategorie()
    {
        throw new NotImplementedException();   // TODO
    }

    // ------------------------------------------------------------------------
    // 7.4  [SCHREIBEN]  Bericht                                     (ca. 3 min)
    //      Liefere für ALLE Produkte, alphabetisch nach Name sortiert, einen
    //      Text. Wie der Text aussieht, bestimmt die Lambda 'format'.
    // ------------------------------------------------------------------------
    public List<string> Bericht(Func<Produkt, string> format)
    {
        throw new NotImplementedException();   // TODO
    }
}
