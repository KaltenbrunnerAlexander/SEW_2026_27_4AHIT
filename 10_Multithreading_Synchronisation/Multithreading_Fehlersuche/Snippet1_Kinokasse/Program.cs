// Snippet 1 – Kinokasse
// Beobachtung: Bei manchen Läufen werden mehr Karten verkauft, als es Plätze gibt.

using System.Threading;

var kasse = new Kinokasse();
var threads = new Thread[3];
for (int i = 0; i < threads.Length; i++)
{
    int nr = i + 1;
    threads[i] = new Thread(() => Kunde(kasse, nr));
    threads[i].Start();
}
foreach (var t in threads) t.Join();
Console.WriteLine($"Verkauft: {kasse.Verkauft}, frei: {kasse.FreiePlaetze}");

static void Kunde(Kinokasse kasse, int nr)
{
    for (int i = 0; i < 5; i++)
    {
        lock(kasse._sperre)  // Sperre muss hier sein, sonst kann es passieren, dass zwei Kunden gleichzeitig prüfen, ob noch Plätze frei sind
        {
            if (kasse.FreiePlaetze >= 2)
            {
                Thread.Sleep(1);                 // Kunde überlegt kurz
                kasse.Verkaufe(2);
                Console.WriteLine($"Kunde {nr}: 2 Karten gekauft");
            }
        }
        
    }
}

class Kinokasse
{
    public readonly object _sperre = new();
    private int freiePlaetze = 10;
    private int verkauft = 0;

    public int FreiePlaetze { get { lock (_sperre) return freiePlaetze; } }
    public int Verkauft     { get { lock (_sperre) return verkauft; } }

    public void Verkaufe(int anzahl)
    {
        lock (_sperre)
        {
            freiePlaetze -= anzahl;
            verkauft += anzahl;
        }
    }
}
