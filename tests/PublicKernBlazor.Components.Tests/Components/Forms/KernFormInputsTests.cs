using Bunit;
using PublicKernBlazor.Components.Components.Forms;
using PublicKernBlazor.Components.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Components.Forms;

public sealed class KernFormInputsTests
{
    [Fact(DisplayName = "Text-Input rendert Wrapper, Label und Eingabe mit KERN-Klassen")]
    public void KernInputText_RendertWrapperUndInput()
    {
        // BunitContext ersetzt den Browser: Komponenten werden im virtuellen DOM gerendert.
        using var context = new BunitContext();

        // context.Services.AddScoped(...) befüllt den DI-Container des Tests.
        context.Services.AddScoped<IdGeneratorService>();

        // Render<T> rendert die Komponente und liefert das Component Under Test (cut).
        var cut = context.Render<KernInputText>(parameters => parameters
            .Add(p => p.Label, "Name")
            .Add(p => p.Value, "Max"));

        // cut.Find(...) sucht Elemente wie document.querySelector im Browser.
        var wrapper = cut.Find("div.kern-form-input");
        var input = cut.Find("input.kern-form-input__input");

        Assert.NotNull(wrapper);
        Assert.Equal("Max", input.GetAttribute("value"));
    }

    [Fact(DisplayName = "Date-Input rendert Tag, Monat und Jahr Felder")]
    public void KernInputDate_RendertDreiTeilfelder()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernInputDate>(parameters => parameters
            .Add(p => p.Label, "Geburtsdatum")
            .Add(p => p.Day, "01")
            .Add(p => p.Month, "12")
            .Add(p => p.Year, "2000"));

        // Then
        var inputs = cut.FindAll("input.kern-form-input__input");
        Assert.Equal(3, inputs.Count);
    }

    [Fact(DisplayName = "Textarea rendert Fehlerklassen bei Error-Text")]
    public void KernTextarea_RendertFehlerBeiErrorText()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernTextarea>(parameters => parameters
            .Add(p => p.Label, "Beschreibung")
            .Add(p => p.Error, "Pflichtfeld"));

        // Then
        Assert.Contains("kern-form-input--error", cut.Markup);
        Assert.Contains("kern-form-input__input--error", cut.Markup);
    }

    [Fact(DisplayName = "File-Input löst OnFileSelected beim Change-Event aus")]
    public void KernInputFile_LoestOnFileSelectedAus()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();
        var called = false;

        // EventCallback in Blazor transportiert typsichere UI-Ereignisse.
        var cut = context.Render<KernInputFile>(parameters => parameters
            .Add(p => p.Label, "Upload")
            .Add(p => p.OnFileSelected, _ => called = true));

        // When – InvokeAsync triggert den EventCallback ohne Browser-Dateiobjekt.
        cut.Instance.OnFileSelected.InvokeAsync(default(InputFileChangeEventArgs));

        // Then
        Assert.True(called);
    }
}

