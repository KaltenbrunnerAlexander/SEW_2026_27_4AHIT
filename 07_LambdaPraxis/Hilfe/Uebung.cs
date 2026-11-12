using System.Globalization;
using System.Text;

namespace LambdaPraxis.Hilfe;

/// <summary>
/// Hilfsklasse zum Prüfen der Lösungen. Hier musst du nichts ändern.
/// </summary>
public static class Uebung
{
    private static int _ok;
    private static int _fehler;
    private static int _offen;

    static Uebung()
    {
        Console.OutputEncoding = Encoding.UTF8;
    }

    /// <summary>Führt eine Aufgabe aus und fängt Ausnahmen ab.</summary>
    public static void Aufgabe(string titel, Action code)
    {
        Ausgabe(ConsoleColor.Cyan, $"\n--- Aufgabe {titel} ---");
        try
        {
            code();
        }
        catch (NotImplementedException)
        {
            _offen++;
            Ausgabe(ConsoleColor.Yellow, "  [OFFEN]   noch nicht bearbeitet");
        }
        catch (NullReferenceException)
        {
            _offen++;
            Ausgabe(ConsoleColor.Yellow, "  [OFFEN]   NullReferenceException - ist eine Lambda noch null?");
        }
        catch (Exception ex)
        {
            _fehler++;
            Ausgabe(ConsoleColor.Red, $"  [FEHLER]  {ex.GetType().Name}: {ex.Message}");
        }
    }

    /// <summary>Vergleicht einen erwarteten mit dem erhaltenen Wert.</summary>
    public static void Pruefe<T>(string was, T erwartet, T erhalten)
    {
        if (EqualityComparer<T>.Default.Equals(erwartet, erhalten))
        {
            _ok++;
            Ausgabe(ConsoleColor.Green, $"  [OK]      {was}");
        }
        else
        {
            _fehler++;
            Ausgabe(ConsoleColor.Red, $"  [FEHLER]  {was}: erwartet {Text(erwartet)}, erhalten {Text(erhalten)}");
        }
    }

    /// <summary>Vergleicht zwei Folgen Element für Element (inklusive Reihenfolge).</summary>
    public static void PruefeFolge<T>(string was, IEnumerable<T> erwartet, IEnumerable<T> erhalten)
    {
        var e = erwartet.ToList();
        var h = erhalten.ToList();
        if (e.SequenceEqual(h))
        {
            _ok++;
            Ausgabe(ConsoleColor.Green, $"  [OK]      {was}");
        }
        else
        {
            _fehler++;
            Ausgabe(ConsoleColor.Red,
                $"  [FEHLER]  {was}: erwartet [{string.Join(", ", e.Select(Text))}], erhalten [{string.Join(", ", h.Select(Text))}]");
        }
    }

    /// <summary>Gibt am Ende des Programms eine Übersicht aus.</summary>
    public static void Zusammenfassung()
    {
        Ausgabe(ConsoleColor.Cyan, "\n==================================================");
        Ausgabe(ConsoleColor.White, $"  Ergebnis:  {_ok} OK   {_fehler} Fehler   {_offen} offen");
        Ausgabe(ConsoleColor.Cyan, "==================================================");
        if (!Console.IsInputRedirected)
        {
            Console.WriteLine("Beliebige Taste zum Beenden ...");
            Console.ReadKey();
        }
    }

    private static string Text<T>(T wert) =>
        wert is null ? "null" : Convert.ToString(wert, CultureInfo.InvariantCulture) ?? "null";

    private static void Ausgabe(ConsoleColor farbe, string text)
    {
        Console.ForegroundColor = farbe;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
