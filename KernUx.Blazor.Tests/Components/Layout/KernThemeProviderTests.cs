using Bunit;
using KernUx.Blazor.Components.Layout;
using KernUx.Blazor.Enums;
using KernUx.Blazor.Extensions;
using KernUx.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KernUx.Blazor.Tests.Components.Layout;

public sealed class KernThemeProviderTests
{
    [Fact]
    public void RendersCurrentThemeAsDataAttribute()
    {
        using var context = new BunitContext();
        context.Services.AddKernUx();

        var cut = context.Render<KernThemeProvider>(parameters => parameters
            .Add(parameter => parameter.Theme, KernTheme.Dark)
            .AddChildContent("<p>Inhalt</p>"));

        var wrapper = cut.Find("div");
        var themeService = context.Services.GetRequiredService<ThemeService>();

        Assert.Equal("dark", wrapper.GetAttribute("data-kern-theme"));
        Assert.Equal(KernTheme.Dark, themeService.Current);
        Assert.Contains("Inhalt", cut.Markup);
    }
}
