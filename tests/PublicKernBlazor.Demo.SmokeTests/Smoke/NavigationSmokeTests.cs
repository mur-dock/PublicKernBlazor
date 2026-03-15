using PublicKernBlazor.Demo.SmokeTests.Infrastructure;
using PublicKernBlazor.Demo.SmokeTests.PageObjects;

namespace PublicKernBlazor.Demo.SmokeTests.Smoke;

/// <summary>
/// Smoke-Tests für die Navigation: jede Route muss ladbar sein,
/// den richtigen Seitentitel haben und kein Fehler-Overlay zeigen.
/// </summary>
[TestFixture]
public sealed class NavigationSmokeTests : BaseSmokeTest
{
    /// <summary>
    /// Parametrisierter Test: prüft alle definierten Routen in einem Durchlauf.
    /// Die Testdaten kommen aus <see cref="TestConstants.AllRoutes"/>, damit
    /// neue Seiten nur dort eingetragen werden müssen.
    /// </summary>
    [TestCaseSource(typeof(TestConstants), nameof(TestConstants.AllRoutes))]
    public async Task Seite_LaeadtMitKorrektemTitelOhneFehler((string Route, string ExpectedTitle) testCase)
    {
        // Given – Startseite über BasePage-Navigation
        var seite = new BasePage_Navigation(Page, testCase.Route);

        // When
        await seite.NavigiereAsync();

        // Then – kein Fehler-Overlay, korrekter Titel, KERN-Layout sichtbar
        await seite.KeinFehlerOverlayAsync();
        Assert.That(await seite.GetTitelAsync(), Is.EqualTo(testCase.ExpectedTitle),
            $"Falscher PageTitle für Route '{testCase.Route}'");
        await seite.KopfzeileIstSichtbarAsync();
    }

    /// <summary>
    /// Die Sidebar-Navigation muss auf jeder Showcase-Seite vollständig
    /// mit allen Links gerendert sein.
    /// </summary>
    [Test]
    public async Task Navigation_EnthaeltAlleShowcaseLinks()
    {
        // Given
        var startseite = new HomePage(Page);
        await startseite.NavigiereAsync();

        // When – alle erwarteten nav-Links sammeln
        var navLinks = Page.Locator(".demo-nav a");

        // Then – mindestens 9 Showcase-Links + 2 Praxisbeispiele
        var anzahl = await navLinks.CountAsync();
        Assert.That(anzahl, Is.GreaterThanOrEqualTo(11),
            "Sidebar-Navigation enthält weniger Links als erwartet.");
    }

    // Lokaler Adapter: erlaubt BasePage mit beliebiger Route ohne eigene Klasse
    private sealed class BasePage_Navigation(IPage page, string route)
        : BasePage(page, route);
}

