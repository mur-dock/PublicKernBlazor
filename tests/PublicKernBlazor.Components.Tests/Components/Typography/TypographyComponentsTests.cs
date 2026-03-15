using Bunit;
using PublicKernBlazor.Components.Components.Typography;
using PublicKernBlazor.Components.Enums;

namespace PublicKernBlazor.Components.Tests.Components.Typography;

public sealed class TypographyComponentsTests
{
    [Fact(DisplayName = "Heading rendert Level-Klasse und semantisches Element")]
    public void KernHeading_RendertLevelKlasseUndElement()
    {
        // Given – BunitContext ersetzt den Browser, dadurch kann Razor ohne Webserver getestet werden.
        using var context = new BunitContext();

        // When – Render<T> rendert die Komponente in ein virtuelles DOM.
        var cut = context.Render<KernHeading>(parameters => parameters
            .Add(p => p.Level, HeadingLevel.XLarge)
            .Add(p => p.Element, "h3")
            .AddChildContent("Titel"));

        // Then – cut.Find(...) funktioniert wie document.querySelector im Browser.
        var heading = cut.Find("h3");
        Assert.Contains("kern-heading-x-large", heading.ClassList);
        Assert.Equal("Titel", heading.TextContent);
    }

    [Fact(DisplayName = "Title rendert Größen-Modifier korrekt")]
    public void KernTitle_RendertGroessenModifier()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernTitle>(parameters => parameters
            .Add(p => p.Size, TitleSize.Small)
            .AddChildContent("Abschnitt"));

        // Then
        var title = cut.Find("p");
        Assert.Contains("kern-title", title.ClassList);
        Assert.Contains("kern-title--small", title.ClassList);
    }

    [Fact(DisplayName = "Body rendert kombinierte Modifier für Größe, Fett und Muted")]
    public void KernBody_RendertKombinierteModifier()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernBody>(parameters => parameters
            .Add(p => p.Size, Size.Large)
            .Add(p => p.Bold, true)
            .Add(p => p.Muted, true)
            .AddChildContent("Text"));

        // Then
        var body = cut.Find("p");
        Assert.Contains("kern-body", body.ClassList);
        Assert.Contains("kern-body--large", body.ClassList);
        Assert.Contains("kern-body--bold", body.ClassList);
        Assert.Contains("kern-body--muted", body.ClassList);
    }

    [Fact(DisplayName = "Label rendert als label-Element mit Optional-Hinweis")]
    public void KernLabel_RendertAlsLabelMitOptionalHinweis()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernLabel>(parameters => parameters
            .Add(p => p.For, "name")
            .Add(p => p.Optional, true)
            .Add(p => p.OptionalText, "- Optional")
            .AddChildContent("Name"));

        // Then
        var label = cut.Find("label");
        Assert.Equal("name", label.GetAttribute("for"));
        Assert.Contains("kern-label", label.ClassList);
        Assert.Contains("- Optional", label.TextContent);
    }

    [Fact(DisplayName = "Subline und Preline rendern die erwarteten Klassen")]
    public void KernSublineUndKernPreline_RendernErwarteteKlassen()
    {
        // Given
        using var context = new BunitContext();

        // When
        var subline = context.Render<KernSubline>(parameters => parameters
            .Add(p => p.Size, Size.Small)
            .AddChildContent("Unterzeile"));
        var preline = context.Render<KernPreline>(parameters => parameters
            .Add(p => p.Size, Size.Large)
            .AddChildContent("Vorzeile"));

        // Then
        Assert.Contains("kern-subline", subline.Find("p").ClassList);
        Assert.Contains("kern-subline--small", subline.Find("p").ClassList);
        Assert.Contains("kern-preline", preline.Find("p").ClassList);
        Assert.Contains("kern-preline--large", preline.Find("p").ClassList);
    }
}

