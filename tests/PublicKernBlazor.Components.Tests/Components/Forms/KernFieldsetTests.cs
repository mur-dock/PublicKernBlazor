using Bunit;
using PublicKernBlazor.Components.Components.Forms;
using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Components.Forms;

public sealed class KernFieldsetTests
{
    [Fact(DisplayName = "Fieldset rendert Fehlerzustand und Hint-Verknüpfung")]
    public void KernFieldset_RendertFehlerUndHint()
    {
        // Given
        using var context = new BunitContext();

        // Bunit DI-Container erhält den IdGenerator für automatische IDs.
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernFieldset>(parameters => parameters
            .Add(p => p.Legend, "Kontakt")
            .Add(p => p.Hint, "Bitte vollständig ausfüllen")
            .Add(p => p.Error, "Fehler vorhanden")
            .AddChildContent("<div>Inhalt</div>"));

        // Then
        Assert.Contains("kern-fieldset--error", cut.Markup);
        Assert.Contains("kern-hint", cut.Markup);
        Assert.Contains("kern-error", cut.Markup);
    }

    [Fact(DisplayName = "Fieldset rendert horizontal-Modifier am Body-Wrapper")]
    public void KernFieldset_RendertHorizontalModifier()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernFieldset>(parameters => parameters
            .Add(p => p.Legend, "Auswahl")
            .Add(p => p.Horizontal, true)
            .AddChildContent("<div>Option</div>"));

        // Then – kern-fieldset__body--horizontal liegt am inneren Body-Wrapper,
        // nicht am <fieldset>-Element selbst (so ist es in KernFieldset.razor implementiert).
        Assert.Contains("kern-fieldset__body--horizontal", cut.Markup);
    }

    [Fact(DisplayName = "Fieldset rendert Legende als semantisches legend-Element")]
    public void KernFieldset_RendertLegendeSemantisch()
    {
        // Given – <fieldset> + <legend> ist das semantisch korrekte HTML-Muster für
        // Gruppen von Formularfeldern (WCAG 1.3.1).
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernFieldset>(parameters => parameters
            .Add(p => p.Legend, "Persönliche Daten")
            .AddChildContent("<div>Feld</div>"));

        // Then – die Legende muss im <legend>-Element stehen.
        var legend = cut.Find("legend");
        Assert.Contains("Persönliche Daten", legend.TextContent);
    }
}

