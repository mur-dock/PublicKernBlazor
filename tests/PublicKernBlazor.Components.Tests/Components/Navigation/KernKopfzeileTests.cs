using Bunit;
using PublicKernBlazor.Components.Components.Navigation;

namespace PublicKernBlazor.Components.Tests.Components.Navigation;

public sealed class KernKopfzeileTests
{
    [Fact(DisplayName = "Kopfzeile rendert Standardlabel")]
    public void KernKopfzeile_RendertStandardlabel()
    {
        // BunitContext: virtuelles DOM statt Browser.
        using var context = new BunitContext();

        // Render<T> erstellt das Component Under Test.
        var cut = context.Render<KernKopfzeile>();

        // Then
        Assert.Contains("kern-kopfzeile", cut.Markup);
        Assert.Contains("Offizielle Website", cut.Markup);
        Assert.Contains("Bundesrepublik Deutschland", cut.Markup);
    }

    [Fact(DisplayName = "Kopfzeile rendert benutzerdefiniertes Label")]
    public void KernKopfzeile_RendertCustomLabel()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernKopfzeile>(parameters => parameters
            .Add(p => p.Label, "Landesportal NRW"));

        // Then
        Assert.Contains("Landesportal NRW", cut.Markup);
        Assert.DoesNotContain("Bundesrepublik", cut.Markup);
    }

    [Fact(DisplayName = "Kopfzeile rendert Fluid-Container")]
    public void KernKopfzeile_RendertFluidContainer()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernKopfzeile>(parameters => parameters
            .Add(p => p.Fluid, true));

        // Then – kern-container-fluid ist eine eigenständige CSS-Klasse im KERN-Grid,
        // kein BEM-Modifier (also nicht kern-container--fluid).
        Assert.Contains("kern-container-fluid", cut.Markup);
    }
}

