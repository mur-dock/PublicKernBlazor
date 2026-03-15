using Bunit;
using PublicKernBlazor.Components.Components.Layout;
using PublicKernBlazor.Components.Enums;

namespace PublicKernBlazor.Components.Tests.Components.Layout;

public sealed class LayoutComponentsTests
{
    [Fact(DisplayName = "Container rendert Fluid-Variante")]
    public void KernContainer_RendertFluidVariante()
    {
        // Given – bUnit rendert ohne Browser in ein virtuelles DOM.
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernContainer>(parameters => parameters
            .Add(p => p.Fluid, true)
            .AddChildContent("Inhalt"));

        // Then
        var container = cut.Find("div");
        Assert.Contains("kern-container-fluid", container.ClassList);
    }

    [Fact(DisplayName = "Row rendert Align- und Justify-Klassen")]
    public void KernRow_RendertAlignUndJustifyKlassen()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernRow>(parameters => parameters
            .Add(p => p.AlignItems, AlignItems.Center)
            .Add(p => p.JustifyContent, JustifyContent.Between)
            .AddChildContent("<div>Spalte</div>"));

        // Then
        var row = cut.Find("div");
        Assert.Contains("kern-row", row.ClassList);
        Assert.Contains("kern-align-items-center", row.ClassList);
        Assert.Contains("kern-justify-content-between", row.ClassList);
    }

    [Fact(DisplayName = "Col rendert Responsive-Span, Offset und AlignSelf")]
    public void KernCol_RendertResponsiveSpanOffsetUndAlignSelf()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernCol>(parameters => parameters
            .Add(p => p.SpanMd, 6)
            .Add(p => p.OffsetMd, 2)
            .Add(p => p.AlignSelf, AlignSelf.End)
            .AddChildContent("Spalte"));

        // Then
        var col = cut.Find("div");
        Assert.Contains("kern-col-md-6", col.ClassList);
        Assert.Contains("kern-col-md-offset-2", col.ClassList);
        Assert.Contains("kern-align-self-end", col.ClassList);
    }

    [Fact(DisplayName = "Row merged class aus AdditionalAttributes ohne Überschreiben")]
    public void KernRow_MergedKlasseOhneUeberschreiben()
    {
        // Given – bUnit rendert die Komponente in ein virtuelles DOM ohne echten Browser.
        using var context = new BunitContext();
        var additionalAttributes = new Dictionary<string, object>
        {
            ["class"] = "demo-row",
            ["data-testid"] = "main-row"
        };

        // When – AdditionalAttributes enthält sowohl class als auch ein data-* Attribut.
        var cut = context.Render<KernRow>(parameters => parameters
            .Add(p => p.JustifyContent, JustifyContent.Between)
            .Add(p => p.AdditionalAttributes, additionalAttributes)
            .AddChildContent("<div>Spalte</div>"));

        // Then – class wird gemerged (nicht überschrieben) und data-* bleibt am Host-Element erhalten.
        var row = cut.Find("div");
        Assert.Contains("kern-row", row.ClassList);
        Assert.Contains("kern-justify-content-between", row.ClassList);
        Assert.Contains("demo-row", row.ClassList);
        Assert.Equal("main-row", row.GetAttribute("data-testid"));
    }

    [Fact(DisplayName = "Col merged class aus AdditionalAttributes ohne Überschreiben")]
    public void KernCol_MergedKlasseOhneUeberschreiben()
    {
        // Given – Test-Context und zusätzliche Host-Attribute wie in Razor @attributes.
        using var context = new BunitContext();
        var additionalAttributes = new Dictionary<string, object>
        {
            ["class"] = "demo-header__actions",
            ["aria-label"] = "Aktionen"
        };

        // When – Span erzeugt kern-col-3, AdditionalAttributes liefert eine zusätzliche Klasse.
        var cut = context.Render<KernCol>(parameters => parameters
            .Add(p => p.Span, 3)
            .Add(p => p.AdditionalAttributes, additionalAttributes)
            .AddChildContent("Spalte"));

        // Then – beide Klassen müssen vorhanden sein; aria-* bleibt ebenfalls am Host-Element.
        var col = cut.Find("div");
        Assert.Contains("kern-col-3", col.ClassList);
        Assert.Contains("demo-header__actions", col.ClassList);
        Assert.Equal("Aktionen", col.GetAttribute("aria-label"));
    }
}
