using PublicKernBlazor.Demo.SmokeTests.Infrastructure;
using PublicKernBlazor.Demo.SmokeTests.PageObjects;

namespace PublicKernBlazor.Demo.SmokeTests.Smoke;

/// <summary>
/// Smoke-Tests für interaktive Komponenten:
/// Antragsstrecke (vollständiger Durchlauf), Dashboard-Interaktion, Dialog, Content-Seite.
/// </summary>
[TestFixture]
public sealed class InteractiveSmokeTests : BaseSmokeTest
{
    /// <summary>
    /// Die Antragsstrecke muss vollständig von Schritt 1 bis zur Bestätigungsseite
    /// durchlaufbar sein. Prüft das State-Management über alle 4 Schritte hinweg.
    /// </summary>
    [Test]
    public async Task Antrag_VollstaendigerDurchlauf_ZeigtErfolgsmeldung()
    {
        // Given – Antragsstrecke öffnen
        var antragPage = new AntragPage(Page);
        await antragPage.NavigiereAsync();
        await antragPage.KeinFehlerOverlayAsync();
        await Assertions.Expect(antragPage.Fortschritt).ToBeVisibleAsync();

        // When – Schritt 1: Persönliche Daten
        await antragPage.FuelleSchritt1Async();

        // When – Schritt 2: Adressdaten
        await antragPage.FuelleSchritt2Async();

        // When – Schritt 3: Zusammenfassung bestätigen
        await Assertions.Expect(antragPage.Zusammenfassung).ToBeVisibleAsync();
        await antragPage.BestaetigungsCheckbox.CheckAsync();
        await antragPage.KlickeAbsendenAsync();

        // Then – Bestätigungsseite mit Success-Alert
        await Assertions.Expect(antragPage.Erfolgsmeldung).ToBeVisibleAsync();
    }

    /// <summary>
    /// Ein nicht ausgefülltes Pflichtfeld muss eine Fehlermeldung auslösen,
    /// und der Wizard darf nicht zum nächsten Schritt wechseln.
    /// </summary>
    [Test]
    public async Task Antrag_PflichtfeldLeer_ZeigtValidierungsfehler()
    {
        // Given – Antragsstrecke, alle Felder leer lassen
        var antragPage = new AntragPage(Page);
        await antragPage.NavigiereAsync();

        // When – direkt auf Weiter klicken ohne Eingabe
        await antragPage.KlickeWeiterAsync();

        // Then – Fehlermeldung für Pflichtfeld sichtbar; Wizard bleibt auf Schritt 1
        var fehler = Page.Locator(".kern-error").First;
        await Assertions.Expect(fehler).ToBeVisibleAsync();
        await Assertions.Expect(antragPage.Fortschritt).ToBeVisibleAsync();
    }

    /// <summary>
    /// Der Dialog auf der Content-Seite muss sich öffnen und über den
    /// Abbrechen-Button wieder schließen lassen.
    /// </summary>
    [Test]
    public async Task ContentSeite_Dialog_OeffnetUndSchliessst()
    {
        // Given
        var contentSeite = new ContentSeite(Page);
        await contentSeite.NavigiereAsync();
        await contentSeite.KeinFehlerOverlayAsync();

        // When – Dialog öffnen
        await contentSeite.OeffneDialogAsync();

        // Then – Dialog ist offen
        await Assertions.Expect(contentSeite.Dialog).ToBeVisibleAsync();

        // When – Dialog schließen
        await contentSeite.SchliesseDialogAsync();

        // Then – Dialog ist geschlossen
        await Assertions.Expect(contentSeite.Dialog).ToBeHiddenAsync();
    }

    /// <summary>
    /// Der Simulieren-Button auf dem Dashboard muss den Fortschrittsbalken aktualisieren.
    /// </summary>
    [Test]
    public async Task Dashboard_SimulierenButton_AktualisiertFortschritt()
    {
        // Given
        var dashboard = new DashboardPage(Page);
        await dashboard.NavigiereAsync();
        await dashboard.KeinFehlerOverlayAsync();

        var fortschrittVorher = await dashboard.GetFortschrittsLabelAsync();

        // When
        await dashboard.SimuliereForstschrittAsync();

        // Then – Label hat sich geändert (Prozentwert höher)
        var fortschrittNachher = await dashboard.GetFortschrittsLabelAsync();
        Assert.That(fortschrittNachher, Is.Not.EqualTo(fortschrittVorher),
            "Fortschrittslabel hat sich nach Simulieren nicht geändert.");
    }

    /// <summary>
    /// Der Klick-Zähler auf der Button-Seite muss nach jedem Klick
    /// hochzählen und den aktualisierten Wert anzeigen.
    /// </summary>
    [Test]
    public async Task ButtonSeite_KlickZaehler_ErhoehtsichBeiKlick()
    {
        // Given
        var buttonSeite = new ButtonSeite(Page);
        await buttonSeite.NavigiereAsync();
        await buttonSeite.KeinFehlerOverlayAsync();

        // When – dreimal klicken
        await buttonSeite.ZaehlerButton.ClickAsync();
        await buttonSeite.ZaehlerButton.ClickAsync();
        await buttonSeite.ZaehlerButton.ClickAsync();

        // Then – Zähler zeigt 3
        var zaehlerText = Page.Locator("strong", new() { HasTextString = "3" });
        await Assertions.Expect(zaehlerText).ToBeVisibleAsync();
    }

    /// <summary>
    /// Die 404-Seite muss einen funktionierenden „Zur Startseite"-Button haben,
    /// der zurück auf <c>/</c> navigiert.
    /// </summary>
    [Test]
    public async Task NichtGefundenSeite_ZurStartseiteButton_NavigiertZurueck()
    {
        // Given
        var nichtGefunden = new NichtGefundenSeite(Page);
        await nichtGefunden.NavigiereAsync();
        await nichtGefunden.KeinFehlerOverlayAsync();

        // When
        await nichtGefunden.ZurStartseiteButton.ClickAsync();

        // Then – URL ist jetzt /
        await Assertions.Expect(Page).ToHaveURLAsync(TestConstants.BaseUrl + "/");
    }
}

