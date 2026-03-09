using Bunit;
using KernUx.Blazor.Components.Shared;
using Microsoft.AspNetCore.Components.Web;

namespace KernUx.Blazor.Tests.Components.Shared;

public sealed class KernStylesTests
{
    [Fact(DisplayName = "Theme-Stylesheet wird korrekt in den Head eingebunden")]
    public void KernStyles_BindetStylesheetInDenHeadEin()
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

        // HeadOutlet zuerst rendern – er nimmt später den <link>-Tag entgegen
        var headOutlet = context.Render<HeadOutlet>();

        // When – KernStyles rendert einen <link>-Tag via <HeadContent>,
        // der automatisch in den bereits gerenderten HeadOutlet projiziert wird
        context.Render<KernStyles>(parameters => parameters
            .Add(p => p.BasePath, "_content/KernUx.Blazor")
            .Add(p => p.ThemeName, "kern"));

        // MarkupMatches führt einen semantischen HTML-Vergleich durch:
        // Leerzeichen und Attribut-Reihenfolge spielen keine Rolle.
        headOutlet.MarkupMatches(
            "<link rel=\"stylesheet\" href=\"_content/KernUx.Blazor/css/themes/kern/index.css\">");
    }
}
