namespace PublicKernBlazor.Demo.SmokeTests.PageObjects;

/// <summary>
/// Page Object für die Content-Showcase-Seite (<c>/components/content</c>).
/// Enthält Dialog-spezifische Selektoren und Aktionen.
/// </summary>
public sealed class ContentSeite(IPage page) : BasePage(page, "/components/content")
{
    /// <summary>Der „Dialog öffnen"-Button.</summary>
    public ILocator DialogOeffnenButton => Page.Locator("button:has-text('Dialog öffnen')");

    /// <summary>Das geöffnete Dialog-Element.</summary>
    public ILocator Dialog => Page.Locator("dialog[open]");

    /// <summary>Der Schließen-Button innerhalb des Dialogs.</summary>
    public ILocator DialogSchliessenButton =>
        Page.Locator("dialog button:has-text('Abbrechen')");

    /// <summary>Öffnet den Dialog und wartet, bis er sichtbar ist.</summary>
    public async Task OeffneDialogAsync()
    {
        await DialogOeffnenButton.ClickAsync();
        await Assertions.Expect(Dialog).ToBeVisibleAsync();
    }

    /// <summary>Schließt den Dialog über den Abbrechen-Button.</summary>
    public async Task SchliesseDialogAsync()
    {
        await DialogSchliessenButton.ClickAsync();
        await Assertions.Expect(Dialog).ToBeHiddenAsync();
    }
}

