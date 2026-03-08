using Bunit;
using KernUx.Blazor.Components.Shared;
using Microsoft.AspNetCore.Components.Web;

namespace KernUx.Blazor.Tests.Components.Shared;

public sealed class KernStylesTests
{
    [Fact(DisplayName = "Theme- und Extensions-Stylesheet werden korrekt eingebunden")]
    public void KernStyles_BindetBeideStylesheetsEin()
    {
        // BunitContext stellt einen virtuellen Browser bereit.
        using var context = new BunitContext();

        // HeadOutlet ist Blazors interner Mechanismus, um <HeadContent>-Inhalte
        // in den <head> zu projizieren – ähnlich wie ein Portal in React.
        // Beim ersten Rendern ruft es intern eine JS-Funktion auf; diese muss
        // im Test als Stub konfiguriert werden, da kein Browser verfügbar ist.
        context.JSInterop
            .Setup<string>("Blazor._internal.PageTitle.getAndRemoveExistingTitle")
            .SetResult(string.Empty);

        // HeadOutlet zuerst rendern – er nimmt später die <link>-Tags entgegen
        var headOutlet = context.Render<HeadOutlet>();

        // When – KernStyles rendert standardmäßig zwei <link>-Tags via <HeadContent>:
        // 1. Theme-CSS (aus KERN-UX Core)
        // 2. Extensions-CSS (projektspezifisch, separat kompiliert)
        context.Render<KernStyles>(parameters => parameters
            .Add(p => p.BasePath, "_content/KernUx.Blazor")
            .Add(p => p.ThemeName, "kern"));

        // MarkupMatches führt einen semantischen HTML-Vergleich durch:
        // Leerzeichen und Attribut-Reihenfolge spielen keine Rolle.
        headOutlet.MarkupMatches(
            """
            <link rel="stylesheet" href="_content/KernUx.Blazor/css/themes/kern/index.css">
            <link rel="stylesheet" href="_content/KernUx.Blazor/css/extensions/index.css">
            """);
    }

    [Fact(DisplayName = "Extensions-Stylesheet wird bei IncludeExtensions=false ausgeblendet")]
    public void KernStyles_OhneExtensions_RendertNurThemeStylesheet()
    {
        // BunitContext stellt einen virtuellen Browser bereit.
        using var context = new BunitContext();

        // HeadOutlet-JS-Interop stubben (siehe erster Test).
        context.JSInterop
            .Setup<string>("Blazor._internal.PageTitle.getAndRemoveExistingTitle")
            .SetResult(string.Empty);

        var headOutlet = context.Render<HeadOutlet>();

        // When – IncludeExtensions=false deaktiviert den zweiten <link>-Tag.
        // Sinnvoll, wenn ein Consumer-Projekt keine eigenen Extensions hat.
        context.Render<KernStyles>(parameters => parameters
            .Add(p => p.BasePath, "_content/KernUx.Blazor")
            .Add(p => p.ThemeName, "kern")
            .Add(p => p.IncludeExtensions, false));

        // Then – nur das Theme-Stylesheet, kein Extensions-Link
        headOutlet.MarkupMatches(
            """<link rel="stylesheet" href="_content/KernUx.Blazor/css/themes/kern/index.css">""");
    }
}
