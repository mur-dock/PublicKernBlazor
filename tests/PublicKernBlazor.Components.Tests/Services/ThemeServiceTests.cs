using PublicKernBlazor.Components.Enums;
using PublicKernBlazor.Components.Services;

namespace PublicKernBlazor.Components.Tests.Services;

public sealed class ThemeServiceTests
{
    [Fact(DisplayName = "Theme wechselt von Hell auf Dunkel")]
    public void Toggle_WechseltVonLightAufDark()
    {
        // Given – Ausgangszustand ist immer Light (Default-Konstruktor)
        var service = new ThemeService();

        // When – Toggle einmalig aufrufen
        service.Toggle();

        // Then – Theme muss Dark sein; AttributeValue liefert den HTML-String für data-kern-theme
        Assert.Equal(KernTheme.Dark, service.Current);
        Assert.Equal("dark", service.AttributeValue);
    }

    [Fact(DisplayName = "Gespeichertes Theme wird beim Start korrekt geladen")]
    public void Initialize_LädtGespeichertesTheme()
    {
        // Given – kein gespeichertes Theme vorhanden, Service beginnt im Light-Modus
        var service = new ThemeService();

        // When – Initialize simuliert das Lesen des Cookie-Werts beim App-Start
        service.Initialize("dark");

        // Then – Service zeigt das korrekte Theme an
        Assert.Equal(KernTheme.Dark, service.Current);
        Assert.Equal("dark", service.AttributeValue);
    }

    [Fact(DisplayName = "ThemeChanged-Event wird nur bei tatsächlichem Wechsel ausgelöst")]
    public void Set_LöstEventNurBeiWertänderungAus()
    {
        // Given – Event-Zähler für die Verifikation vorbereiten
        var service = new ThemeService();
        var raised = 0;

        // ThemeChanged ist ein C#-Event – += abonniert es, wie ein DOM-EventListener
        service.ThemeChanged += _ => raised++;

        // When – Light→Dark wechselt (1×), zweifaches Dark ändert nichts
        service.Set(KernTheme.Light);  // kein Wechsel (bereits Light)
        service.Set(KernTheme.Dark);   // Wechsel → Event +1
        service.Set(KernTheme.Dark);   // kein Wechsel → Event bleibt 1

        // Then – Event genau einmal ausgelöst
        Assert.Equal(1, raised);
    }
}
