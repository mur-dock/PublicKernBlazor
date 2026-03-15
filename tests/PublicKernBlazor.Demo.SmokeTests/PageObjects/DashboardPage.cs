namespace PublicKernBlazor.Demo.SmokeTests.PageObjects;

/// <summary>
/// Page Object für das Sachbearbeiter-Dashboard (<c>/examples/dashboard</c>).
/// </summary>
public sealed class DashboardPage(IPage page) : BasePage(page, "/examples/dashboard")
{
    // ── Locatoren ─────────────────────────────────────────────────────────────

    /// <summary>Alle drei Statistik-Karten im oberen Bereich.</summary>
    public ILocator StatistikKarten => Page.Locator(".kern-card");

    /// <summary>Die Fortschrittsanzeige für die Quartals-Zielerreichung.</summary>
    public ILocator Fortschritt => Page.Locator(".kern-progress");

    /// <summary>Alle Task-List-Items in der Aufgabenliste.</summary>
    public ILocator AufgabenItems => Page.Locator(".kern-task-list__item");

    /// <summary>Die Aktivitäten-Tabelle.</summary>
    public ILocator AktivitaetenTabelle => Page.Locator(".kern-table");

    /// <summary>Alle Accordion-Items in der FAQ-Sektion.</summary>
    public ILocator FaqAccordions => Page.Locator(".kern-accordion");

    /// <summary>Der „Simulieren"-Button, der den Fortschritt erhöht.</summary>
    public ILocator SimulierenButton => Page.Locator("button:has-text('Simulieren')");

    // ── Aktionen ──────────────────────────────────────────────────────────────

    /// <summary>Klickt den Simulieren-Button und wartet auf die DOM-Aktualisierung.</summary>
    public async Task SimuliereForstschrittAsync()
    {
        await SimulierenButton.ClickAsync();
        // Kurz warten, damit Blazor den State aktualisiert hat
        await Page.WaitForTimeoutAsync(300);
    }

    /// <summary>Gibt den Text des Fortschrittslabels zurück.</summary>
    public Task<string> GetFortschrittsLabelAsync() =>
        Fortschritt.Locator(".kern-label").InnerTextAsync();
}
