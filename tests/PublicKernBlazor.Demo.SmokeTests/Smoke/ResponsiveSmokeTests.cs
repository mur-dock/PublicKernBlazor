using PublicKernBlazor.Demo.SmokeTests.Infrastructure;
using PublicKernBlazor.Demo.SmokeTests.PageObjects;

namespace PublicKernBlazor.Demo.SmokeTests.Smoke;

/// <summary>
/// Smoke-Tests für Responsiveness: die drei Standard-Viewports Desktop, Tablet und Mobil.
/// Prüft, ob das KERN-Layout bei verschiedenen Bildschirmbreiten korrekt gerendert wird.
/// </summary>
[TestFixture]
public sealed class ResponsiveSmokeTests : BaseSmokeTest
{
    /// <summary>
    /// Definiert die zu testenden Viewports als Named-Records für lesbare Testausgaben.
    /// </summary>
    private static IEnumerable<TestCaseData> Viewports()
    {
        yield return new TestCaseData(1280, 800,  "Desktop")
            .SetName("Desktop (1280×800)");
        yield return new TestCaseData(768,  1024, "Tablet")
            .SetName("Tablet (768×1024)");
        yield return new TestCaseData(390,  844,  "Mobil")
            .SetName("Mobil (390×844)");
    }

    /// <summary>
    /// Prüft für jeden Viewport, dass die Startseite ohne Fehler-Overlay lädt
    /// und die KERN-Kopfzeile sichtbar ist.
    /// Ein Screenshot wird immer gespeichert (auch bei Erfolg), um visuelle
    /// Regressionserkennung zu ermöglichen.
    /// </summary>
    [TestCaseSource(nameof(Viewports))]
    public async Task Startseite_RendertKorrektFuer(int breite, int hoehe, string label)
    {
        // Given – Viewport-Größe setzen
        await Page.SetViewportSizeAsync(breite, hoehe);

        var seite = new HomePage(Page);
        await seite.NavigiereAsync();

        // Then – kein Fehler, Layout korrekt
        await seite.KeinFehlerOverlayAsync();
        await seite.KopfzeileIstSichtbarAsync();
        await Assertions.Expect(seite.MainContent).ToBeVisibleAsync();

        // Screenshot für visuelle Überprüfung immer speichern
        var screenshotDir = Path.Combine(TestConstants.ArtifactDir, "responsive");
        Directory.CreateDirectory(screenshotDir);
        await Page.ScreenshotAsync(new()
        {
            Path     = Path.Combine(screenshotDir, $"startseite-{label}.png"),
            FullPage = true
        });
    }

    /// <summary>
    /// Prüft, dass die Antragsstrecke auf Mobil-Viewport ohne Overflow-Fehler
    /// und ohne horizontales Scrollen rendert.
    /// </summary>
    [Test]
    public async Task Antragsstrecke_RendertAufMobilOhneHorizontalenScrollbalken()
    {
        // Given – Mobil-Viewport
        await Page.SetViewportSizeAsync(390, 844);

        var antragPage = new AntragPage(Page);
        await antragPage.NavigiereAsync();
        await antragPage.KeinFehlerOverlayAsync();

        // Then – kein horizontaler Scrollbalken
        // scrollWidth > clientWidth bedeutet horizontaler Overflow
        var hatHorizontalenScroll = await Page.EvaluateAsync<bool>(
            "() => document.documentElement.scrollWidth > document.documentElement.clientWidth");

        Assert.That(hatHorizontalenScroll, Is.False,
            "Auf Mobil-Viewport hat die Antragsstrecke einen horizontalen Scrollbalken.");
    }
}

