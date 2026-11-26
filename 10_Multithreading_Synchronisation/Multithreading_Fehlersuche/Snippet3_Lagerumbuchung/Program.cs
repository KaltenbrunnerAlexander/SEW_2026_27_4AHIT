// Snippet 3 – Lagerumbuchung
// Beobachtung: Manchmal gibt das Programm gar nichts aus und muss mit Strg+C
// beendet werden. Eine Exception tritt nicht auf.

using System.Threading;

var wien = new Lager("Wien", 1000);
var linz = new Lager("Linz", 1000);

var t1 = new Thread(() =>
{
    for (int i = 0; i < 10_000; i++) wien.UmbuchenNach(linz, 1);
});
var t2 = new Thread(() =>
{
    for (int i = 0; i < 10_000; i++) linz.UmbuchenNach(wien, 1);
});
t1.Start(); 
t2.Start();
t1.Join();  
t2.Join();

Console.WriteLine($"Wien: {wien.Bestand}, Linz: {linz.Bestand}");

class Lager
{
    private readonly object _sperre = new();
    private int bestand;

    public string Name { get; }
    public Lager(string name, int bestand)
    {
        Name = name;
        this.bestand = bestand;
    }

    public int Bestand { 
        get { lock (_sperre) return bestand; } 
        set { lock (_sperre) bestand = value; } 
    }

    public void UmbuchenNach(Lager ziel, int menge)
    {
        if (this == ziel || menge <= 0) return;

        var first = string.CompareOrdinal(Name, ziel.Name) <= 0 ? this : ziel;
        var second = first == this ? ziel : this;

        lock (first._sperre)
        {
            lock (second._sperre)
            {
                if (bestand < menge) return;
                bestand -= menge;
                ziel.bestand += menge;
            }
        }
    }
}
