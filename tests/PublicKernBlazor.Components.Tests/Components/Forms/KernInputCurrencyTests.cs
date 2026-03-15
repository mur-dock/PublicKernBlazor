using Bunit;
using PublicKernBlazor.Components.Components.Forms;
using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Components.Forms;

public sealed class KernInputCurrencyTests
{
    [Fact(DisplayName = "Währungsfeld rendert Symbol, Input und KERN-Klassen")]
    public void KernInputCurrency_RendertSymbolUndInput()
    {
        // BunitContext ersetzt den Browser: Blazor-Komponenten werden im virtuellen DOM gerendert.
        using var context = new BunitContext();

        // context.Services.AddScoped befüllt den DI-Container mit dem IdGeneratorService,
        // der für eindeutige Input-IDs und aria-describedby benötigt wird.
        context.Services.AddScoped<IdGeneratorService>();

        // When – Render<T> erstellt das Component Under Test (cut).
        var cut = context.Render<KernInputCurrency>(parameters => parameters
            .Add(p => p.Label, "Betrag")
            .Add(p => p.Value, 1234.56m));

        // Then – cut.Find(...) sucht wie document.querySelector im Browser.
        cut.Find("div.kern-form-input__currency-wrapper");
        Assert.Contains("€", cut.Markup);
        Assert.Contains("kern-form-input__input", cut.Markup);
    }

    [Fact(DisplayName = "Währungsfeld zeigt formatierten Wert im deutschen Format")]
    public void KernInputCurrency_ZeigtDeutschesFormat()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernInputCurrency>(parameters => parameters
            .Add(p => p.Label, "Preis")
            .Add(p => p.Value, 1234.56m)
            .Add(p => p.CultureName, "de-DE"));

        // Then – 1234,56 ist die de-DE-Formatierung (Komma als Dezimaltrenner)
        var input = cut.Find("input");
        Assert.Equal("1.234,56", input.GetAttribute("value"));
    }

    [Fact(DisplayName = "Währungsfeld akzeptiert benutzerdefiniertes Währungssymbol")]
    public void KernInputCurrency_AkzeptiertBenutzerdefiniertesSymbol()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernInputCurrency>(parameters => parameters
            .Add(p => p.Label, "Betrag CHF")
            .Add(p => p.CurrencySymbol, "CHF"));

        // Then
        Assert.Contains("CHF", cut.Markup);
    }

    [Fact(DisplayName = "Währungsfeld zeigt Fehlerklasse bei gesetztem Error")]
    public void KernInputCurrency_ZeigtFehlerklasse()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernInputCurrency>(parameters => parameters
            .Add(p => p.Label, "Betrag")
            .Add(p => p.Error, "Ungültiger Betrag"));

        // Then
        Assert.Contains("kern-form-input--error", cut.Markup);
        Assert.Contains("kern-form-input__input--error", cut.Markup);
    }

    [Fact(DisplayName = "Währungsfeld löst ValueChanged mit geparster Dezimalzahl aus")]
    public void KernInputCurrency_LoestValueChangedAus()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        decimal? received = null;

        var cut = context.Render<KernInputCurrency>(parameters => parameters
            .Add(p => p.Label, "Betrag")
            .Add(p => p.ValueChanged, v => received = v));

        // When – Eingabe simulieren; "1234,56" ist gültig im de-DE-Format.
        cut.Find("input").Input("1234,56");

        // Then – ValueChanged wurde mit dem geparsten Dezimalwert aufgerufen.
        Assert.Equal(1234.56m, received);
    }

    [Fact(DisplayName = "Währungsfeld liefert null bei leerer Eingabe")]
    public void KernInputCurrency_LiefertNullBeiLeererEingabe()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        decimal? received = 99m; // Startwert zum Nachweis der Änderung

        var cut = context.Render<KernInputCurrency>(parameters => parameters
            .Add(p => p.Label, "Betrag")
            .Add(p => p.Value, 99m)
            .Add(p => p.ValueChanged, v => received = v));

        // When
        cut.Find("input").Input(string.Empty);

        // Then
        Assert.Null(received);
    }
}



