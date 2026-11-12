// Diese Datei ist vollständig. Hier musst du nichts ändern.

/// <summary>Meldet jede Temperaturänderung über das Ereignis TemperaturGeaendert.</summary>
public class Thermometer
{
    public event Action<double>? TemperaturGeaendert;

    public void Setze(double temperatur)
    {
        TemperaturGeaendert?.Invoke(temperatur);
    }
}

/// <summary>Löst bei jedem Aufruf von Klick() das Ereignis Geklickt aus.</summary>
public class Knopf
{
    public event EventHandler? Geklickt;

    public void Klick()
    {
        Geklickt?.Invoke(this, EventArgs.Empty);
    }
}
