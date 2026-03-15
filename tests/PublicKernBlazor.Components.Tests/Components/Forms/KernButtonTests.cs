using Bunit;
using PublicKernBlazor.Components.Components.Forms;
using PublicKernBlazor.Components.Enums;

namespace PublicKernBlazor.Components.Tests.Components.Forms;

public sealed class KernButtonTests
{
    [Fact(DisplayName = "Button rendert Variante, Block-Modifier und Icon")]
    public void KernButton_RendertKlassenUndIcon()
    {
        // BunitContext ersetzt den Browser: die Komponente wird in einem virtuellen DOM getestet.
        using var context = new BunitContext();

        // Render<T> rendert die Komponente und liefert das Component Under Test (cut).
        var cut = context.Render<KernButton>(parameters => parameters
            .Add(p => p.Variant, ButtonVariant.Secondary)
            .Add(p => p.Block, true)
            .Add(p => p.Icon, KernIconGlyph.Edit)
            .AddChildContent("Bearbeiten"));

        // cut.Find(...) sucht im Markup wie ein CSS-Selektor (querySelector).
        var button = cut.Find("button");

        Assert.Contains("kern-btn", button.ClassList);
        Assert.Contains("kern-btn--secondary", button.ClassList);
        Assert.Contains("kern-btn--block", button.ClassList);
        Assert.Contains("kern-icon--edit", cut.Markup);
    }

    [Fact(DisplayName = "Aktivierter Button löst OnClick aus")]
    public void KernButton_AktiviertLoestOnClickAus()
    {
        // Given
        using var context = new BunitContext();
        var clicked = false;

        // When – EventCallback bildet Blazors Event-Mechanismus ab.
        var cut = context.Render<KernButton>(parameters => parameters
            .Add(p => p.OnClick, _ => clicked = true)
            .AddChildContent("Speichern"));

        cut.Find("button").Click();

        // Then
        Assert.True(clicked);
    }

    [Fact(DisplayName = "Deaktivierter Button trägt das disabled-Attribut")]
    public void KernButton_DisabledTraegtAttribut()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernButton>(parameters => parameters
            .Add(p => p.Disabled, true)
            .AddChildContent("Gesperrt"));

        // Then – das HTML-disabled-Attribut verhindert Browser-Interaktion und Assistive-Technology-Aktivierung.
        var button = cut.Find("button");
        Assert.NotNull(button.GetAttribute("disabled"));
    }

    [Fact(DisplayName = "Icon-Only-Button rendert Label als kern-sr-only")]
    public void KernButton_IconOnly_RendertScreenreaderLabel()
    {
        // Given – ein reiner Icon-Button muss für Screenreader trotzdem einen Text liefern.
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernButton>(parameters => parameters
            .Add(p => p.IconOnly, true)
            .Add(p => p.Icon, KernIconGlyph.Close)
            .AddChildContent("Schließen"));

        // Then – kern-sr-only macht den Text sichtbar nur für Screenreader (WCAG 1.1.1).
        Assert.Contains("kern-sr-only", cut.Markup);
        Assert.Contains("Schließen", cut.Markup);
    }

    [Fact(DisplayName = "Button rendert type-Attribut korrekt")]
    public void KernButton_RendertTypeAttribut()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernButton>(parameters => parameters
            .Add(p => p.Type, "submit")
            .AddChildContent("Absenden"));

        // Then – type="submit" ist semantisch wichtig für Formulare.
        Assert.Equal("submit", cut.Find("button").GetAttribute("type"));
    }
}

