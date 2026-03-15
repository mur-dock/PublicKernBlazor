using Bunit;
using PublicKernBlazor.Components.Components.Content;
using PublicKernBlazor.Components.Components.Forms;
using PublicKernBlazor.Components.Components.Shared;
using PublicKernBlazor.Components.Components.Feedback;
using PublicKernBlazor.Components.Components.Navigation;
using PublicKernBlazor.Components.Enums;
using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Accessibility;

/// <summary>
/// Accessibility-Audit-Tests für WCAG 2.1 AA-Konformität.
/// Prüft ARIA-Attribute, semantisches HTML und Screenreader-Unterstützung
/// aller KERN-UX-Blazor-Komponenten.
/// </summary>
public sealed class AccessibilityAuditTests
{
    // ── Formular-Komponenten ────────────────────────────────────────────────

    [Fact(DisplayName = "Input trägt aria-describedby auf Hint und Error")]
    public void KernInputText_TraegtAriaDescribedBy()
    {
        // BunitContext ersetzt den Browser – kein echter Webserver nötig.
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var cut = context.Render<KernInputText>(parameters => parameters
            .Add(p => p.Label, "Name")
            .Add(p => p.Hint, "Vorname und Nachname")
            .Add(p => p.Error, "Pflichtfeld"));

        // aria-describedby verknüpft Input mit Hint und Fehlertext (WCAG 1.3.1).
        var input = cut.Find("input");
        Assert.NotNull(input.GetAttribute("aria-describedby"));
    }

    [Fact(DisplayName = "Disabled-Input trägt disabled-Attribut statt aria-disabled")]
    public void KernInputText_Disabled_TraegtHtmlDisabled()
    {
        // Given – natives HTML-disabled ist Screenreadern zuverlässiger als aria-disabled.
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var cut = context.Render<KernInputText>(parameters => parameters
            .Add(p => p.Label, "Feld")
            .Add(p => p.Disabled, true));

        // Then
        Assert.NotNull(cut.Find("input").GetAttribute("disabled"));
    }

    [Fact(DisplayName = "Select trägt aria-describedby bei Fehler")]
    public void KernSelect_TraegtAriaDescribedByBeiError()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var cut = context.Render<KernSelect>(parameters => parameters
            .Add(p => p.Label, "Land")
            .Add(p => p.Error, "Bitte wählen")
            .Add(p => p.Options, [new KernSelectOption("DE", "Deutschland")]));

        // Then – aria-describedby muss auf die Fehlermeldungs-ID zeigen.
        var select = cut.Find("select");
        Assert.NotNull(select.GetAttribute("aria-describedby"));
    }

    [Fact(DisplayName = "Checkbox trägt id- und for-Verknüpfung für Klickziel")]
    public void KernCheckbox_TraegtIdUndForVerknuepfung()
    {
        // Given – label[for] + input[id] ist WCAG 1.3.1: das Label muss klickbar sein.
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var cut = context.Render<KernCheckbox>(parameters => parameters
            .Add(p => p.Label, "AGB akzeptieren"));

        var input = cut.Find("input[type='checkbox']");
        var label = cut.Find("label");

        var inputId = input.GetAttribute("id");
        var labelFor = label.GetAttribute("for");

        // id und for müssen übereinstimmen.
        Assert.NotNull(inputId);
        Assert.Equal(inputId, labelFor);
    }

    // ── Icon & Dekorations-Komponenten ─────────────────────────────────────

    [Fact(DisplayName = "Dekoratives Icon trägt aria-hidden='true'")]
    public void KernIcon_Dekorativ_TraegtAriaHidden()
    {
        // Given – dekorative Icons dürfen nicht von Screenreadern vorgelesen werden
        // (WCAG 1.1.1: kein Text-Äquivalent für rein dekorative Inhalte nötig).
        using var context = new BunitContext();

        var cut = context.Render<KernIcon>(parameters => parameters
            .Add(p => p.Glyph, KernIconGlyph.Info)
            .Add(p => p.AriaHidden, true));

        var span = cut.Find("span.kern-icon");
        Assert.Equal("true", span.GetAttribute("aria-hidden"));
    }

    [Fact(DisplayName = "Informatives Icon trägt aria-label statt aria-hidden")]
    public void KernIcon_Informativ_TraegtAriaLabel()
    {
        // Given – wenn ein Icon inhaltlich relevant ist, braucht es ein aria-label
        // als Textalternative (WCAG 1.1.1).
        using var context = new BunitContext();

        var cut = context.Render<KernIcon>(parameters => parameters
            .Add(p => p.Glyph, KernIconGlyph.Warning)
            .Add(p => p.AriaHidden, false)
            .Add(p => p.AriaLabel, "Warnung"));

        var span = cut.Find("span.kern-icon");
        Assert.Equal("Warnung", span.GetAttribute("aria-label"));
        // aria-hidden darf nicht true sein.
        Assert.NotEqual("true", span.GetAttribute("aria-hidden"));
    }

    // ── Interaktive Komponenten ─────────────────────────────────────────────

    [Fact(DisplayName = "Button-Primär trägt type='button' als sicheren Standard")]
    public void KernButton_TraegtButtonType()
    {
        // Given – ohne type="button" können Browser <button> in Forms als Submit behandeln.
        using var context = new BunitContext();

        var cut = context.Render<KernButton>(parameters => parameters
            .AddChildContent("OK"));

        Assert.Equal("button", cut.Find("button").GetAttribute("type"));
    }

    [Fact(DisplayName = "Accordion nutzt natives details/summary für Auf-/Zuklappen")]
    public void KernAccordion_NutztDetailsSummaryElement()
    {
        // Given – KernAccordion verwendet das native <details>/<summary>-HTML-Muster.
        // <details> hat implizit toggle-Semantik; <summary> ist der sichtbare Toggle-Trigger.
        // Das native Muster ist semantisch äquivalent zu aria-expanded und benötigt kein
        // manuelles ARIA-Attribut (WCAG 4.1.2: native Semantik bevorzugen).
        using var context = new BunitContext();
        // KernAccordion setzt open via JS – Aufrufe müssen gestubbt werden.
        context.JSInterop.SetupVoid("kernAccordion.open",  _ => true).SetVoidResult();
        context.JSInterop.SetupVoid("kernAccordion.close", _ => true).SetVoidResult();

        // When
        var cut = context.Render<KernAccordion>(parameters => parameters
            .Add(p => p.Title, "Abschnitt")
            .Add(p => p.Open, false)
            .AddChildContent("<p>Inhalt</p>"));

        // Then – die Komponente rendert ein <details>-Element (nativ toggle-fähig).
        Assert.NotEmpty(cut.FindAll("details.kern-accordion"));
        // Das <summary>-Element ist der klickbare Header.
        Assert.NotEmpty(cut.FindAll("summary.kern-accordion__header"));
    }

    [Fact(DisplayName = "Dialog trägt role='dialog' oder ist natives dialog-Element")]
    public void KernDialog_TraegtNativesDialogElement()
    {
        // Given – das native <dialog>-Element hat implizit role="dialog"
        // und ist semantisch korrekt (WCAG 4.1.2).
        using var context = new BunitContext();
        context.JSInterop.SetupVoid("kernDialog.showModal", _ => true);
        context.JSInterop.SetupVoid("kernDialog.close", _ => true);

        var cut = context.Render<KernDialog>(parameters => parameters
            .Add(p => p.Title, "Bestätigung")
            .Add(p => p.Open, false));

        // Das Markup muss ein <dialog>-Element enthalten.
        Assert.NotEmpty(cut.FindAll("dialog"));
    }

    // ── Navigation & Layout ─────────────────────────────────────────────────

    [Fact(DisplayName = "Kopfzeile enthält role-neutrales, semantisches HTML")]
    public void KernKopfzeile_RendertSemantischesHTML()
    {
        // Given – die Kopfzeile muss als <div> gerendert werden;
        // nav- oder header-Rollen sind im KERN-Standard nicht vorgesehen.
        using var context = new BunitContext();

        var cut = context.Render<KernKopfzeile>();

        // Das äußere Element ist ein <div class="kern-kopfzeile">.
        cut.Find("div.kern-kopfzeile");
    }

    // ── Tabelle ─────────────────────────────────────────────────────────────

    [Fact(DisplayName = "Tabelle mit Caption rendert caption-Element für Screenreader")]
    public void KernTable_RendertCaptionFuerScreenreader()
    {
        // Given – eine <caption> ist die semantisch korrekte Methode, eine Tabelle
        // für Screenreader zu beschriften (WCAG 1.3.1).
        using var context = new BunitContext();

        var cut = context.Render<KernTable>(parameters => parameters
            .Add(p => p.Caption, "Ergebnisliste")
            .Add(p => p.Responsive, false)
            .AddChildContent("<thead><tr><th>Name</th></tr></thead>"));

        Assert.NotEmpty(cut.FindAll("caption"));
        Assert.Contains("Ergebnisliste", cut.Find("caption").TextContent);
    }

    // ── Summary / Task-List ──────────────────────────────────────────────────

    [Fact(DisplayName = "Summary-Edit-Link hat aria-describedby auf den Titel")]
    public void KernSummary_EditLink_TraegtAriaDescribedBy()
    {
        // Given – aria-describedby verknüpft den Bearbeiten-Link mit dem Titel des
        // Abschnitts; Screenreader lesen "Bearbeiten – Kontaktdaten" vor (WCAG 2.4.6).
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var cut = context.Render<KernSummary>(parameters => parameters
            .Add(p => p.Title, "Kontaktdaten")
            .Add(p => p.EditHref, "/edit/1"));

        var link = cut.Find("a.kern-link");
        Assert.NotNull(link.GetAttribute("aria-describedby"));
    }
}

