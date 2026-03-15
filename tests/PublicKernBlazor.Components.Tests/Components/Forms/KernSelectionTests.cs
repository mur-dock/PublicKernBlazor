using Bunit;
using PublicKernBlazor.Components.Components.Forms;
using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Components.Forms;

public sealed class KernSelectionTests
{
    [Fact(DisplayName = "Select rendert Optionen und markiert den gewählten Eintrag")]
    public void KernSelect_RendertOptionenUndSelection()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var options = new[]
        {
            new KernSelectOption("de", "Deutschland"),
            new KernSelectOption("at", "Österreich")
        };

        // When
        var cut = context.Render<KernSelect>(parameters => parameters
            .Add(p => p.Label, "Land")
            .Add(p => p.Options, options)
            .Add(p => p.Value, "at"));

        // Then
        var selected = cut.Find("option[selected]");
        Assert.Equal("at", selected.GetAttribute("value"));
    }

    [Fact(DisplayName = "Checkbox rendert checked-Zustand")]
    public void KernCheckbox_RendertCheckedZustand()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        // When
        var cut = context.Render<KernCheckbox>(parameters => parameters
            .Add(p => p.Label, "AGB")
            .Add(p => p.Checked, true));

        // Then
        var input = cut.Find("input[type=checkbox]");
        Assert.Equal("", input.GetAttribute("checked"));
    }

    [Fact(DisplayName = "RadioGroup rendert alle Optionen")]
    public void KernRadioGroup_RendertAlleOptionen()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        var items = new[]
        {
            new KernRadioItem("m", "Herr"),
            new KernRadioItem("f", "Frau"),
            new KernRadioItem("d", "Divers")
        };

        // When
        var cut = context.Render<KernRadioGroup>(parameters => parameters
            .Add(p => p.Legend, "Ansprechpartner")
            .Add(p => p.Items, items)
            .Add(p => p.Value, "f"));

        // Then
        var radios = cut.FindAll("input[type=radio]");
        Assert.Equal(3, radios.Count);
    }

    [Fact(DisplayName = "CheckboxList gibt aktualisierte Auswahl per Event zurück")]
    public void KernCheckboxList_GibtAuswahlZurueck()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddScoped<IdGeneratorService>();

        IReadOnlyCollection<string>? selected = null;
        var items = new[]
        {
            new KernCheckboxItem("a", "A"),
            new KernCheckboxItem("b", "B")
        };

        // When
        var cut = context.Render<KernCheckboxList>(parameters => parameters
            .Add(p => p.Legend, "Auswahl")
            .Add(p => p.Items, items)
            .Add(p => p.SelectedValuesChanged, values => selected = values));

        cut.Find("input[type=checkbox]").Change(true);

        // Then
        Assert.NotNull(selected);
        Assert.Contains("a", selected!);
    }
}

