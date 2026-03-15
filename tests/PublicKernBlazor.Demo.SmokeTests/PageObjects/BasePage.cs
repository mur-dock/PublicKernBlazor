using PublicKernBlazor.Demo.SmokeTests.Infrastructure;

namespace PublicKernBlazor.Demo.SmokeTests.PageObjects;

/// <summary>
/// Basisklasse für alle Page Objects.
/// <para>
/// Kapselt die gemeinsame Navigation und Assertions, die für jede Seite gelten:
/// Fehler-Overlay prüfen, Seitentitel lesen, KERN-Layout verifizieren.
/// </para>
/// <para>
/// Abgeleitete Klassen erhalten über den Konstruktor die <c>route</c> und optional
/// den erwarteten Seitentitel. Alle seitenspezifischen Locatoren und Aktionen
/// werden dort definiert – die Testklassen selbst enthalten keine CSS-Selektoren.
/// </para>
/// </summary>
public abstract class BasePage
{
    /// <summary>Die Playwright-Seite, auf der Aktionen ausgeführt werden.</summary>
    protected readonly IPage Page;

    /// <summary>Relative Route, z.B. <c>/components/buttons</c>.</summary>
    protected readonly string Route;

    /// <summary>
    /// Locator für das Blazor-Fehler-Overlay (<c>#blazor-error-ui</c>).
    /// Darf nach dem Laden niemals sichtbar sein.
    /// </summary>
    public ILocator FehlerOverlay => Page.Locator("#blazor-error-ui");

    /// <summary>Locator für die KERN-Kopfzeile – prüft, ob das Layout gerendert wurde.</summary>
    public ILocator Kopfzeile => Page.Locator(".kern-kopfzeile").First;

    /// <summary>Locator für den Haupt-Inhaltsbereich.</summary>
    public ILocator MainContent => Page.Locator("main");

    protected BasePage(IPage page, string route)
    {
        Page  = page;
        Route = route;
    }

    /// <summary>
    /// Navigiert zur Route dieser Seite.
    /// Wartet auf <c>NetworkIdle</c>, damit Blazor-Komponenten vollständig hydriert sind.
    /// </summary>
    public async Task NavigiereAsync()
    {
        await Page.GotoAsync(TestConstants.BaseUrl + Route,
            new() { WaitUntil = WaitUntilState.NetworkIdle });
        Page.SetDefaultTimeout(TestConstants.DefaultTimeoutMs);
    }

    /// <summary>Gibt den aktuellen Seitentitel des Browsers zurück.</summary>
    public Task<string> GetTitelAsync() => Page.TitleAsync();

    /// <summary>
    /// Prüft, dass das Fehler-Overlay unsichtbar ist.
    /// Wird in jedem Test als erste Assertion aufgerufen.
    /// </summary>
    public Task KeinFehlerOverlayAsync() =>
        Assertions.Expect(FehlerOverlay).ToBeHiddenAsync();

    /// <summary>Prüft, dass die KERN-Kopfzeile sichtbar ist.</summary>
    public Task KopfzeileIstSichtbarAsync() =>
        Assertions.Expect(Kopfzeile).ToBeVisibleAsync();
}
