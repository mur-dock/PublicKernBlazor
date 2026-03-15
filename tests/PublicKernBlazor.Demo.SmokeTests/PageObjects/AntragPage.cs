namespace PublicKernBlazor.Demo.SmokeTests.PageObjects;

/// <summary>
/// Page Object für die Antragsstrecke (<c>/examples/antrag</c>).
/// <para>
/// Kapselt alle Selektoren und Aktionen für den mehrstufigen Antragsprozess.
/// Die Schritte (1–4) spiegeln den Ablauf in <c>AntragExample.razor</c> wider.
/// </para>
/// </summary>
public sealed class AntragPage(IPage page) : BasePage(page, "/examples/antrag")
{
    // ── Schritt 1 – Persönliche Daten ─────────────────────────────────────────

    /// <summary>Eingabefeld für den Vornamen.</summary>
    public ILocator VornameInput => Page.GetByLabel("Vorname", new() { Exact = true });

    /// <summary>Eingabefeld für den Nachnamen.</summary>
    public ILocator NachnameInput => Page.GetByLabel("Nachname", new() { Exact = true });

    /// <summary>Eingabefeld für die E-Mail-Adresse.</summary>
    public ILocator EmailInput => Page.GetByLabel("E-Mail-Adresse", new() { Exact = true });

    /// <summary>Fortschrittsanzeige (KernProgress).</summary>
    public ILocator Fortschritt => Page.Locator(".kern-progress");

    // ── Schritt 3 – Zusammenfassung ────────────────────────────────────────────

    /// <summary>Zusammenfassungsgruppe mit den eingegebenen Daten.</summary>
    public ILocator Zusammenfassung => Page.Locator(".kern-summary").First;

    /// <summary>Bestätigungs-Checkbox.</summary>
    public ILocator BestaetigungsCheckbox => Page.Locator("input[type='checkbox']");

    // ── Schritt 4 – Bestätigung ────────────────────────────────────────────────

    /// <summary>Success-Alert nach erfolgreichem Absenden.</summary>
    public ILocator Erfolgsmeldung => Page.Locator(".kern-alert--success");

    // ── Aktionen ──────────────────────────────────────────────────────────────

    /// <summary>Klickt den „Weiter"-Button.</summary>
    public Task KlickeWeiterAsync() =>
        Page.Locator("button:has-text('Weiter')").ClickAsync();

    /// <summary>Klickt den „Zurück"-Button.</summary>
    public Task KlickeZurueckAsync() =>
        Page.Locator("button:has-text('Zurück')").ClickAsync();

    /// <summary>Klickt „Antrag absenden" (nur im letzten Schritt aktiv).</summary>
    public Task KlickeAbsendenAsync() =>
        Page.Locator("button:has-text('Antrag absenden')").ClickAsync();

    /// <summary>Klickt „Neuen Antrag starten" nach der Bestätigung.</summary>
    public Task KlickeNeuenAntragAsync() =>
        Page.Locator("button:has-text('Neuen Antrag starten')").ClickAsync();

    /// <summary>
    /// Führt Schritt 1 vollständig mit gültigen Testdaten aus.
    /// Gibt das Page Object zurück für Method-Chaining.
    /// </summary>
    public async Task<AntragPage> FuelleSchritt1Async(
        string vorname  = "Max",
        string nachname = "Mustermann",
        string email    = "max@example.org")
    {
        await VornameInput.FillAsync(vorname);
        await NachnameInput.FillAsync(nachname);
        await EmailInput.FillAsync(email);

        // Geburtsdatum wird über aria-label je Feld adressiert (Tag/Monat/Jahr).
        await Page.GetByLabel("Tag", new() { Exact = true }).FillAsync("15");
        await Page.GetByLabel("Monat", new() { Exact = true }).FillAsync("06");
        await Page.GetByLabel("Jahr", new() { Exact = true }).FillAsync("1990");

        await KlickeWeiterAsync();
        return this;
    }

    /// <summary>
    /// Führt Schritt 2 (Adressdaten) vollständig mit gültigen Testdaten aus.
    /// </summary>
    public async Task<AntragPage> FuelleSchritt2Async(
        string strasse    = "Musterweg",
        string hausnummer = "12",
        string plz        = "10115",
        string ort        = "Berlin")
    {
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Straße", Exact = true }).First.FillAsync(strasse);
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Hausnummer", Exact = true }).First.FillAsync(hausnummer);
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "PLZ", Exact = true }).First.FillAsync(plz);
        await Page.GetByRole(AriaRole.Textbox, new() { Name = "Ort", Exact = true }).First.FillAsync(ort);
        await KlickeWeiterAsync();
        return this;
    }
}
