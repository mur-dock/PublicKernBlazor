using Bunit;
using KernUx.Blazor.Components.Content;
using Microsoft.JSInterop;

namespace KernUx.Blazor.Tests.Components.Content;

public sealed class KernAccordionTests
{
    // bUnit-Hilfsmethode: registriert einen JSInterop-Stub und gibt den Context zurück.
    // KernAccordion ruft kernAccordion.open/close in OnAfterRenderAsync auf – diese
    // Aufrufe müssen im Test gestubbt werden, damit kein echter Browser nötig ist.
    private static BunitContext CreateContext()
    {
        var ctx = new BunitContext();
        // JSInterop im Strict-Modus: nur explizit registrierte Aufrufe sind erlaubt.
        // Alle anderen lösen eine Exception aus (Sicherheitsnetz gegen unerwartete JS-Aufrufe).
        ctx.JSInterop.Mode = JSRuntimeMode.Strict;
        // kernAccordion.open und kernAccordion.close geben nichts zurück (void).
        // SetVoidResult() stellt sicher, dass der Task sofort abgeschlossen wird.
        ctx.JSInterop.SetupVoid("kernAccordion.open",  _ => true).SetVoidResult();
        ctx.JSInterop.SetupVoid("kernAccordion.close", _ => true).SetVoidResult();
        return ctx;
    }

    [Fact(DisplayName = "Accordion rendert geöffneten Zustand")]
    public void KernAccordion_RendertOpenAttribut()
    {
        // Given
        using var context = CreateContext();

        // When
        var cut = context.Render<KernAccordion>(parameters => parameters
            .Add(p => p.Title, "Abschnitt")
            .Add(p => p.Open, true)
            .AddChildContent("Inhalt"));

        // Then – details-Element trägt die KERN-Klasse
        var details = cut.Find("details");
        Assert.Contains("kern-accordion", details.ClassList);
        // open wird via JS gesetzt; im bUnit-DOM bleibt das Attribut zunächst absent.
        // Wir prüfen stattdessen, dass kernAccordion.open aufgerufen wurde.
        context.JSInterop.VerifyInvoke("kernAccordion.open");
    }

    [Fact(DisplayName = "Accordion löst OnToggle beim Klick auf Summary aus")]
    public void KernAccordion_LoestOnToggleAus()
    {
        // Given
        using var context = CreateContext();
        bool? state = null;

        // EventCallback repräsentiert Blazors typsicheren Event-Mechanismus.
        var cut = context.Render<KernAccordion>(parameters => parameters
            .Add(p => p.Title, "Abschnitt")
            .Add(p => p.OnToggle, (bool value) => { state = value; }));

        // When – Klick auf <summary> löst HandleClick aus (onclick:preventDefault).
        // WaitForState wartet, bis der async Click-Handler vollständig ausgeführt wurde.
        cut.Find("summary").Click();
        cut.WaitForState(() => state.HasValue);

        // Then – OnToggle wurde mit true aufgerufen (war vorher closed → jetzt open)
        Assert.True(state);
    }

    [Fact(DisplayName = "AccordionGroup rendert Wrapper-Klasse")]
    public void KernAccordionGroup_RendertWrapperKlasse()
    {
        // Given – KernAccordionGroup braucht kein JSInterop.
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernAccordionGroup>(parameters => parameters
            .AddChildContent("<div>Eintrag</div>"));

        // Then – cut.Find() wirft eine Exception wenn kein Element gefunden wird,
        // daher ist keine zusätzliche Assert.NotNull-Prüfung notwendig.
        cut.Find("div.kern-accordion-group");
    }

    [Fact(DisplayName = "Accordion behält lokalen Toggle-Zustand bei unverändertem Open-Parameter")]
    public void KernAccordion_LokalerToggleWirdBeiGleichemOpenParameterNichtZurueckgesetzt()
    {
        // Given – Open=true setzt den initialen Zustand auf geöffnet.
        using var context = CreateContext();

        // Render<T> rendert die Komponente in ein virtuelles DOM.
        var cut = context.Render<KernAccordion>(parameters => parameters
            .Add(p => p.Title, "Abschnitt")
            .Add(p => p.Open, true)
            .AddChildContent("Inhalt"));

        // When – Klick auf Summary schließt das Accordion lokal.
        cut.Find("summary").Click();

        // Der Parent rendert mit demselben Open-Parameter erneut (kein Parameterwechsel).
        // Render() stößt einen erneuten Render-Zyklus der bestehenden Instanz an.
        cut.Render();

        // Then – kernAccordion.close muss aufgerufen worden sein (Accordion bleibt zu).
        context.JSInterop.VerifyInvoke("kernAccordion.close");
        // kernAccordion.open darf nach dem Klick NICHT erneut aufgerufen worden sein.
        // (Es wurde einmalig beim firstRender aufgerufen.)
        Assert.Single(context.JSInterop.Invocations["kernAccordion.open"]);
    }
}
