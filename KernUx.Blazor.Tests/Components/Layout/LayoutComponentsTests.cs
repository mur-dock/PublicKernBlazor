using Bunit;
using KernUx.Blazor.Components.Layout;
using KernUx.Blazor.Enums;

namespace KernUx.Blazor.Tests.Components.Layout;

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
}

