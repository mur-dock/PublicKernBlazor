using Bunit;
using KernUx.Blazor.Components.Content;

namespace KernUx.Blazor.Tests.Components.Content;

public sealed class KernTableTests
{
    [Fact(DisplayName = "Table rendert Striped-Modifier und Caption")]
    public void KernTable_RendertStripedUndCaption()
    {
        // BunitContext: virtuelles DOM statt Browser.
        using var context = new BunitContext();

        // Render<T> erstellt das Component Under Test.
        var cut = context.Render<KernTable>(parameters => parameters
            .Add(p => p.Caption, "Ergebnisse")
            .Add(p => p.Striped, true)
            .AddChildContent("<tbody><tr><td>A</td></tr></tbody>"));

        // Then
        Assert.Contains("kern-table--striped", cut.Markup);
        Assert.Contains("kern-table-responsive", cut.Markup);
        Assert.Contains("Ergebnisse", cut.Markup);
    }

    [Fact(DisplayName = "Table rendert ohne Responsive-Wrapper")]
    public void KernTable_OhneResponsiveWrapper()
    {
        // Given
        using var context = new BunitContext();

        // When – AddChildContent löst die Ambiguität zwischen den Render-Überladungen.
        var cut = context.Render<KernTable>(parameters => parameters
            .Add(p => p.Responsive, false)
            .Add(p => p.Small, false)
            .AddChildContent("<tbody><tr><td>B</td></tr></tbody>"));

        // Then
        Assert.DoesNotContain("kern-table-responsive", cut.Markup);
        Assert.Contains("kern-table", cut.Markup);
    }
}
