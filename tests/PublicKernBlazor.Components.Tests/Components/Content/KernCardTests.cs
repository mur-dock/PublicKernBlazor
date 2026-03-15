using Bunit;
using PublicKernBlazor.Components.Components.Content;
using PublicKernBlazor.Components.Enums;
using Microsoft.AspNetCore.Components;

namespace PublicKernBlazor.Components.Tests.Components.Content;

public sealed class KernCardTests
{
    [Fact(DisplayName = "Card rendert Basis-Klasse und Size-Modifier")]
    public void KernCard_RendertBasisKlasseUndSizeModifier()
    {
        // BunitContext ersetzt den Browser: Blazor-Komponenten können im virtuellen DOM gerendert werden.
        using var context = new BunitContext();

        // Render<T> rendert die Komponente und liefert das Component Under Test (cut).
        var cut = context.Render<KernCard>(parameters => parameters
            .Add(p => p.Size, CardSize.Large)
            .Add(p => p.Active, true)
            .AddChildContent("<div>Inhalt</div>"));

        // cut.Find(...) sucht Elemente wie document.querySelector im Browser.
        var article = cut.Find("article");

        Assert.Contains("kern-card", article.ClassList);
        Assert.Contains("kern-card--large", article.ClassList);
        Assert.Contains("kern-card--active", article.ClassList);
    }

    [Fact(DisplayName = "Card rendert Hug-Modifier")]
    public void KernCard_RendertHugModifier()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernCard>(parameters => parameters
            .Add(p => p.Hug, true)
            .AddChildContent("<div>Inhalt</div>"));

        // Then
        var article = cut.Find("article");
        Assert.Contains("kern-card--hug", article.ClassList);
    }

    [Fact(DisplayName = "CardMedia rendert Media-Container")]
    public void KernCardMedia_RendertMediaContainer()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernCardMedia>(parameters => parameters
            .AddChildContent("<img src='test.png' alt='Test' />"));

        // Then – cut.Find() wirft wenn kein Element gefunden wird.
        cut.Find("div.kern-card__media");
        Assert.Contains("test.png", cut.Markup);
    }

    [Fact(DisplayName = "CardContainer rendert Header, Body und Footer")]
    public void KernCardContainer_RendertAlleAbschnitte()
    {
        // Given
        using var context = new BunitContext();

        // RenderFragment kann in .cs-Dateien nicht mit Razor-Syntax (@<text>) erstellt werden.
        // Stattdessen wird ein builder-Delegate verwendet.
        RenderFragment footerMarkup = builder =>
        {
            builder.AddMarkupContent(0, "<button>OK</button>");
        };

        // When
        var cut = context.Render<KernCardContainer>(parameters => parameters
            .Add(p => p.Preline, "Kategorie")
            .Add(p => p.Title, "Titel")
            .Add(p => p.Subline, "Untertitel")
            .AddChildContent("<p>Body-Text</p>")
            .Add(p => p.FooterContent, footerMarkup));

        // Then
        Assert.Contains("kern-card__header", cut.Markup);
        Assert.Contains("kern-preline", cut.Markup);
        Assert.Contains("kern-title", cut.Markup);
        Assert.Contains("kern-subline", cut.Markup);
        Assert.Contains("kern-card__body", cut.Markup);
        Assert.Contains("kern-card__footer", cut.Markup);
    }
}

