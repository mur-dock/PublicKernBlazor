using Bunit;
using KernUx.Blazor.Components.Shared;
using KernUx.Blazor.Enums;

namespace KernUx.Blazor.Tests.Components.Shared;

public sealed class KernIconTests
{
    [Fact(DisplayName = "Icon rendert korrekte KERN-CSS-Klassen")]
    public void KernIcon_RendertKorrekteCssKlassen()
    {
        // BunitContext ersetzt den Browser: Blazor-Komponenten können so ohne
        // echten Webserver gerendert und getestet werden.
        using var context = new BunitContext();

        // Render<T> rendert die Komponente in ein virtuelles DOM.
        // Der Rückgabewert ist das "Component Under Test" (cut).
        var cut = context.Render<KernIcon>(parameters => parameters
            .Add(p => p.Glyph, KernIconGlyph.Info)  // wie <KernIcon Glyph="Info" ... /> in Razor
            .Add(p => p.Size, IconSize.Large));

        // cut.Find(...) sucht im gerenderten HTML – funktioniert wie document.querySelector()
        var icon = cut.Find("span");

        // Then – alle drei KERN-BEM-Klassen müssen vorhanden sein
        Assert.Contains("kern-icon", icon.ClassList);
        Assert.Contains("kern-icon--info", icon.ClassList);
        Assert.Contains("kern-icon--large", icon.ClassList);
        // Dekorative Icons erhalten aria-hidden="true" für Screenreader
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact(DisplayName = "Semantisches Icon erhält ARIA-Attribute für Barrierefreiheit")]
    public void KernIcon_SemantischesIconErhältAriaAttribute()
    {
        // Given
        using var context = new BunitContext();

        // When – AriaHidden=false signalisiert: dieses Icon trägt inhaltliche Bedeutung
        var cut = context.Render<KernIcon>(parameters => parameters
            .Add(p => p.Glyph, KernIconGlyph.Help)
            .Add(p => p.AriaHidden, false)
            .Add(p => p.AriaLabel, "Hilfe"));

        var icon = cut.Find("span");

        // Then – Screenreader erhält role="img" und ein sprechendes Label
        Assert.Equal("false", icon.GetAttribute("aria-hidden"));
        Assert.Equal("img", icon.GetAttribute("role"));
        Assert.Equal("Hilfe", icon.GetAttribute("aria-label"));
    }
}
