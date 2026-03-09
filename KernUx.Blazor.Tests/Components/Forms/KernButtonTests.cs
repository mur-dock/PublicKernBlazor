using Bunit;
using KernUx.Blazor.Components.Forms;
using KernUx.Blazor.Enums;

namespace KernUx.Blazor.Tests.Components.Forms;

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
}

