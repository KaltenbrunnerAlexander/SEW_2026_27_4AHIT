// Snippet 2 – Supermarktkassen
// Beobachtung: Die Umsätze der einzelnen Kassen stimmen immer,
// der Tagesumsatz ist aber fast immer zu klein.

using System.Threading;

var kassen = new[] { new Kassa("Kassa 1"), new Kassa("Kassa 2"), new Kassa("Kassa 3") };
var threads = new List<Thread>();

foreach (var k in kassen)
{
    var t = new Thread(() =>
    {
        for (int i = 0; i < 100_000; i++) k.Buche(1);
    });
    threads.Add(t);
    t.Start();
}
foreach (var t in threads) t.Join();

foreach (var k in kassen) Console.WriteLine($"{k.Name}: {k.Umsatz} €");
Console.WriteLine($"Tagesumsatz: {Kassa.Tagesumsatz} €");   // erwartet: 300000

class Kassa
{
    private static object _sperre = new();
    private static int tagesumsatz = 0;
    private int umsatz = 0;

    public string Name { get; }
    public Kassa(string name) => Name = name;

    public int Umsatz => umsatz;
    public static int Tagesumsatz => tagesumsatz;

    public void Buche(int betrag)
    {
        umsatz += betrag;
        lock (_sperre)
        {
            tagesumsatz += betrag;
        }
    }
}
