using PublicKernBlazor.Demo.SmokeTests.Infrastructure;
using PublicKernBlazor.Demo.SmokeTests.PageObjects;

namespace PublicKernBlazor.Demo.SmokeTests.Smoke;

/// <summary>
/// Smoke-Tests für das Light/Dark-Theme:
/// Toggle-Interaktion, Cookie-Persistenz und Anti-FOUC-Attribut.
/// </summary>
[TestFixture]
public sealed class ThemeSmokeTests : BaseSmokeTest
{
    /// <summary>
    /// Nach dem ersten Klick auf den Theme-Toggle muss das Attribut
    /// <c>data-kern-theme</c> am <c>&lt;html&gt;</c>-Element auf "dark" wechseln.
    /// </summary>
    [Test]
    public async Task ThemeToggle_WechseltAttributAufDark()
    {
        // Given – Startseite laden, Ausgangszustand ist "light"
        var seite = new HomePage(Page);
        await seite.NavigiereAsync();

        var htmlElement = Page.Locator("html");
        await Assertions.Expect(htmlElement)
            .ToHaveAttributeAsync("data-kern-theme", "light");

        // When – Theme-Toggle klicken
        await Page.Locator("[title='Theme wechseln']").ClickAsync();

        // Then – Attribut muss auf "dark" wechseln
        await Assertions.Expect(htmlElement)
            .ToHaveAttributeAsync("data-kern-theme", "dark");
    }

    /// <summary>
    /// Nach dem Toggle muss ein Cookie <c>kern-theme=dark</c> gesetzt sein,
    /// damit das Theme über Seitenladevorgänge hinweg persistiert wird.
    /// </summary>
    [Test]
    public async Task ThemeToggle_SetztkernThemeCookie()
    {
        // Given
        var seite = new HomePage(Page);
        await seite.NavigiereAsync();

        // When
        await Page.Locator("[title='Theme wechseln']").ClickAsync();

        // Then – Cookie vorhanden
        var cookies = await Context.CookiesAsync();
        var themeCookie = cookies.FirstOrDefault(c => c.Name == "kern-theme");

        Assert.That(themeCookie, Is.Not.Null,  "Cookie 'kern-theme' fehlt.");
        Assert.That(themeCookie!.Value, Is.EqualTo("dark"), "Cookie-Wert ist nicht 'dark'.");
    }

    /// <summary>
    /// Ein erneuter Toggle-Klick muss das Theme zurück auf "light" setzen
    /// und den Cookie entsprechend aktualisieren.
    /// </summary>
    [Test]
    public async Task ThemeToggle_ZweimalKlick_WechseltZurueckAufLight()
    {
        // Given
        var seite = new HomePage(Page);
        await seite.NavigiereAsync();
        var toggleButton = Page.Locator("[title='Theme wechseln']");

        // When – zweimal klicken
        await toggleButton.ClickAsync();
        await toggleButton.ClickAsync();

        // Then – zurück auf light
        await Assertions.Expect(Page.Locator("html"))
            .ToHaveAttributeAsync("data-kern-theme", "light");

        var cookies = await Context.CookiesAsync();
        var themeCookie = cookies.FirstOrDefault(c => c.Name == "kern-theme");
        Assert.That(themeCookie?.Value, Is.EqualTo("light"));
    }

    /// <summary>
    /// Regressions-Test: Nach zwei Toggle-Klicks (Light→Dark→Light) und anschließender
    /// Navigation zu einer anderen Seite darf das Theme NICHT auf "dark" zurückfallen.
    /// Reproduziert den gemeldeten Bug: Toggle 2× schalten, dann Nav-Link klicken → Theme "dark".
    /// </summary>
    [Test]
    public async Task ThemeToggle_ZweimalKlickDannNavigation_BleibLight()
    {
        // Given – Startseite laden, Ausgangszustand ist "light"
        var startseite = new HomePage(Page);
        await startseite.NavigiereAsync();
        var toggleButton = Page.Locator("[title='Theme wechseln']");
        var htmlElement  = Page.Locator("html");

        // Ausgangszustand sicherstellen
        await Assertions.Expect(htmlElement).ToHaveAttributeAsync("data-kern-theme", "light");

        // When – Schritt 1: Toggle einmal → "dark"
        await toggleButton.ClickAsync();
        await Assertions.Expect(htmlElement).ToHaveAttributeAsync("data-kern-theme", "dark");

        // When – Schritt 2: Toggle erneut → zurück auf "light"
        await toggleButton.ClickAsync();
        await Assertions.Expect(htmlElement).ToHaveAttributeAsync("data-kern-theme", "light");

        // Sicherstellen, dass Cookie ebenfalls "light" zeigt
        var cookiesVorNav = await Context.CookiesAsync();
        var cookieVorNav  = cookiesVorNav.FirstOrDefault(c => c.Name == "kern-theme");
        Assert.That(cookieVorNav?.Value, Is.EqualTo("light"),
            "Cookie muss nach zweitem Toggle 'light' sein – noch vor der Navigation.");

        // When – Schritt 3: Navigationslink anklicken (Blazor Enhanced Navigation)
        // Dies ist die kritische Aktion: der Bug trat genau hier auf.
        await startseite.LinkButtons.First.ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        // Then – Theme muss WEITERHIN "light" sein, nicht "dark"
        await Assertions.Expect(htmlElement)
            .ToHaveAttributeAsync("data-kern-theme", "light");

        Assert.That(
            await htmlElement.GetAttributeAsync("data-kern-theme"), Is.EqualTo("light"),
            "Theme darf nach Navigation nicht auf 'dark' zurückfallen.");

        // Cookie muss ebenfalls "light" sein
        var cookiesNachNav = await Context.CookiesAsync();
        var cookieNachNav  = cookiesNachNav.FirstOrDefault(c => c.Name == "kern-theme");
        Assert.That(cookieNachNav?.Value, Is.EqualTo("light"),
            "Cookie darf nach Navigation nicht auf 'dark' zurückfallen.");
    }

    /// <summary>
    /// Das Anti-FOUC-Script muss <c>data-kern-theme</c> bereits vor dem
    /// vollständigen Blazor-Rendering setzen (Attribut ist direkt nach DOMContentLoaded da).
    /// </summary>
    [Test]
    public async Task AntiFouc_ThemeAttributIstVorBlazorHydrationGesetzt()
    {
        // Given – Seite laden und sofort nach DOMContentLoaded prüfen
        // (NetworkIdle wäre zu spät; wir wollen den Zustand VOR Blazor prüfen)
        await Page.GotoAsync(TestConstants.BaseUrl,
            new() { WaitUntil = WaitUntilState.DOMContentLoaded });

        // Then – Attribut muss bereits gesetzt sein, bevor Blazor fertig ist
        var attribut = await Page.Locator("html").GetAttributeAsync("data-kern-theme");
        Assert.That(attribut, Is.AnyOf("light", "dark"),
            "data-kern-theme muss direkt nach DOMContentLoaded gesetzt sein (Anti-FOUC).");
    }
}

