namespace LambdaPraxis.Hilfe;

/// <summary>Ein Schüler mit Name, Alter und Note (1,0 = sehr gut ... 5,0 = nicht genügend).</summary>
public record Schueler(string Name, int Alter, double Note);

/// <summary>Testdaten für die Aufgaben 2, 3 und 4.</summary>
public static class Daten
{
    /// <summary>Liefert bei jedem Aufruf eine frische Liste mit 8 Schülern.</summary>
    public static List<Schueler> ErzeugeKlasse() => new()
    {
        new("Anna", 16, 1.5),
        new("Ben", 17, 3.0),
        new("Clara", 16, 2.0),
        new("David", 18, 4.5),
        new("Eva", 17, 1.0),
        new("Felix", 16, 5.0),
        new("Greta", 17, 2.5),
        new("Hannes", 18, 3.5)
    };
}
