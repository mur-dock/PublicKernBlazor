using Bunit;
using PublicKernBlazor.Components.Components.Content;
using PublicKernBlazor.Components.Enums;

namespace PublicKernBlazor.Components.Tests.Components.Content;

public sealed class KernDescriptionListTests
{
    [Fact(DisplayName = "DescriptionList rendert Column-Layout-Modifier")]
    public void KernDescriptionList_RendertColumnModifier()
    {
        // BunitContext: Blazor-Komponenten werden ohne Browser im virtuellen DOM gerendert.
        using var context = new BunitContext();

        // Render<T> erstellt das Component Under Test.
        var cut = context.Render<KernDescriptionList>(parameters => parameters
            .Add(p => p.Layout, DescriptionListLayout.Column)
            .AddChildContent("<div>Eintrag</div>"));

        // cut.Find(...) sucht Elemente wie ein CSS-Selektor.
        var dl = cut.Find("dl");
        Assert.Contains("kern-description-list", dl.ClassList);
        Assert.Contains("kern-description-list--col", dl.ClassList);
    }

    [Fact(DisplayName = "DescriptionItem rendert Key und Textwert")]
    public void KernDescriptionItem_RendertKeyUndValue()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernDescriptionItem>(parameters => parameters
            .Add(p => p.Key, "Name")
            .Add(p => p.Value, "Max Mustermann"));

        // Then
        Assert.Contains("kern-description-list-item__key", cut.Markup);
        Assert.Contains("Name", cut.Markup);
        Assert.Contains("Max Mustermann", cut.Markup);
    }

    [Fact(DisplayName = "DescriptionItem bevorzugt ChildContent gegenüber Value")]
    public void KernDescriptionItem_BevorzugtChildContent()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernDescriptionItem>(parameters => parameters
            .Add(p => p.Key, "Info")
            .Add(p => p.Value, "Wird ignoriert")
            .AddChildContent("<strong>Wichtig</strong>"));

        // Then
        Assert.Contains("<strong>Wichtig</strong>", cut.Markup);
        Assert.DoesNotContain("Wird ignoriert", cut.Markup);
    }
}

