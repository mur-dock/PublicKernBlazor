using Bunit;
using PublicKernBlazor.Components.Components.Navigation;
using PublicKernBlazor.Components.Enums;

namespace PublicKernBlazor.Components.Tests.Components.Navigation;

public sealed class NavigationComponentsTests
{
    [Fact(DisplayName = "Link rendert Stretched-Modifier und Rel für neues Fenster")]
    public void KernLink_RendertStretchedUndRelBeiBlankTarget()
    {
        // Given – BunitContext rendert Blazor in ein virtuelles DOM.
        using var context = new BunitContext();

        // When – Render<T> erstellt das Component Under Test (cut).
        var cut = context.Render<KernLink>(parameters => parameters
            .Add(p => p.Href, "/details")
            .Add(p => p.Target, "_blank")
            .Add(p => p.Stretched, true)
            .AddChildContent("Details"));

        // Then – cut.Find(...) sucht wie querySelector und prüft das gerenderte <a>.
        var link = cut.Find("a");
        Assert.Contains("kern-link", link.ClassList);
        Assert.Contains("kern-link--stretched", link.ClassList);
        Assert.Equal("noreferrer noopener", link.GetAttribute("rel"));
    }

    [Fact(DisplayName = "Liste rendert als geordnete Liste mit Number-Variante")]
    public void KernList_RendertNummerierteListe()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernList>(parameters => parameters
            .Add(p => p.Variant, KernListVariant.Number)
            .Add(p => p.Size, Size.Small)
            .AddChildContent("<li>Eins</li><li>Zwei</li>"));

        // Then – MarkupMatches vergleicht semantisch und ignoriert irrelevante Whitespace-Unterschiede.
        cut.MarkupMatches("<ol class=\"kern-list kern-list--number kern-list--small\"><li>Eins</li><li>Zwei</li></ol>");
    }
}

