using Bunit;
using KernUx.Blazor.Components.Shared;

namespace KernUx.Blazor.Tests.Components.Shared;

public sealed class KernDividerTests
{
    [Fact(DisplayName = "Divider ist standardmäßig dekorativ und für Screenreader ausgeblendet")]
    public void KernDivider_IstStandardmaessigDekorativ()
    {
        // Given – BunitContext stellt die Testumgebung für Razor-Komponenten bereit.
        using var context = new BunitContext();

        // When – die Komponente wird in das virtuelle DOM gerendert.
        var cut = context.Render<KernDivider>();

        // Then – cut.Find(...) sucht das <hr>-Element im gerenderten Markup.
        var divider = cut.Find("hr");
        Assert.Contains("kern-divider", divider.ClassList);
        Assert.Equal("true", divider.GetAttribute("aria-hidden"));
    }
}

