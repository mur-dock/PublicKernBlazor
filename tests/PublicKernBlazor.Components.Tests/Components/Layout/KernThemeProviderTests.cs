using Bunit;
using PublicKernBlazor.Components.Components.Layout;
using PublicKernBlazor.Components.Enums;
using PublicKernBlazor.Components.Extensions;
using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Components.Layout;

public sealed class KernThemeProviderTests
{
    [Fact(DisplayName = "ThemeProvider setzt data-kern-theme korrekt auf dem Wrapper-Element")]
    public void KernThemeProvider_SetztDataAttributKorrekt()
    {
        // BunitContext ersetzt den Browser: Blazor-Komponenten werden in einem
        // virtuellen DOM gerendert – kein echter Webserver nötig.
        using var context = new BunitContext();

        // context.Services entspricht dem DI-Container der Blazor-App.
        // AddKernUx() registriert ThemeService und IdGeneratorService.
        context.Services.AddKernUx();

        // Render<T> rendert die Komponente in das virtuelle DOM.
        // AddChildContent(...) entspricht dem Inhalt zwischen den Tags: <KernThemeProvider>...</KernThemeProvider>
        var cut = context.Render<KernThemeProvider>(parameters => parameters
            .Add(p => p.Theme, KernTheme.Dark)
            .AddChildContent("<p>Inhalt</p>"));

        // cut.Find("div") sucht das erste div-Element – wie document.querySelector("div")
        var wrapper = cut.Find("div");

        // GetRequiredService<T> löst den registrierten Dienst aus dem DI-Container auf
        var themeService = context.Services.GetRequiredService<ThemeService>();

        // Then – HTML-Attribut, Service-Zustand und gerenderter Inhalt stimmen überein
        Assert.Equal("dark", wrapper.GetAttribute("data-kern-theme"));
        Assert.Equal(KernTheme.Dark, themeService.Current);
        Assert.Contains("Inhalt", cut.Markup);
    }

    [Fact(DisplayName = "ThemeProvider setzt Light-Theme als Standard")]
    public void KernThemeProvider_SetztLightAlsStandard()
    {
        // Given
        using var context = new BunitContext();
        context.Services.AddKernUx();

        // When – kein Theme-Parameter → Default ist Light
        var cut = context.Render<KernThemeProvider>(parameters => parameters
            .AddChildContent("<p>Inhalt</p>"));

        // Then
        Assert.Equal("light", cut.Find("div").GetAttribute("data-kern-theme"));
    }

    [Fact(DisplayName = "ThemeProvider aktualisiert data-kern-theme nach Toggle über ThemeService")]
    public void KernThemeProvider_ReagiertAufThemeWechsel()
    {
        // Given – Provider startet mit Light (Standard)
        using var context = new BunitContext();
        context.Services.AddKernUx();

        var cut = context.Render<KernThemeProvider>(parameters => parameters
            .AddChildContent("<p>Inhalt</p>"));

        Assert.Equal("light", cut.Find("div").GetAttribute("data-kern-theme"));

        // When – ThemeService.Toggle() wechselt intern auf Dark und feuert ThemeChanged.
        // KernThemeProvider abonniert ThemeChanged in OnInitialized und ruft
        // InvokeAsync(StateHasChanged) auf, was ein Re-Render auslöst.
        // Dank _lastThemeParameter-Tracking in OnParametersSet wird der Service-Zustand
        // bei diesem Re-Render nicht auf den (unveränderten) Parameter zurückgesetzt.
        var themeService = context.Services.GetRequiredService<ThemeService>();
        themeService.Toggle();

        // Then – data-kern-theme muss nun "dark" sein
        Assert.Equal("dark", cut.Find("div").GetAttribute("data-kern-theme"));
    }
}
