using Bunit;
using KernUx.Blazor.Components.Layout;
using KernUx.Blazor.Enums;
using KernUx.Blazor.Extensions;
using KernUx.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KernUx.Blazor.Tests.Components.Layout;

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
}
