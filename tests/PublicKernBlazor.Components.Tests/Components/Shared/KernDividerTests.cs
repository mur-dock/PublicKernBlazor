using Bunit;
using PublicKernBlazor.Components.Components.Shared;

namespace PublicKernBlazor.Components.Tests.Components.Shared;

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

    [Fact(DisplayName = "Divider mit AriaHidden=false trägt kein aria-hidden-Attribut")]
    public void KernDivider_OhneAriaHidden_TraegtKeinAttribut()
    {
        // Given – wenn der Trenner semantisch relevant ist (z.B. zwischen thematischen Abschnitten),
        // darf aria-hidden nicht gesetzt sein.
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernDivider>(parameters => parameters
            .Add(p => p.AriaHidden, false));

        // Then – aria-hidden fehlt oder ist explizit "false".
        var divider = cut.Find("hr");
        var ariaHidden = divider.GetAttribute("aria-hidden");
        Assert.True(ariaHidden is null or "false");
    }
}

