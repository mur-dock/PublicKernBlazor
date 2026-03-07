using KernUx.Blazor.Enums;
using KernUx.Blazor.Services;

namespace KernUx.Blazor.Tests.Services;

public sealed class ThemeServiceTests
{
    [Fact]
    public void Toggle_SwitchesBetweenLightAndDark()
    {
        var service = new ThemeService();

        service.Toggle();

        Assert.Equal(KernTheme.Dark, service.Current);
        Assert.Equal("dark", service.AttributeValue);
    }

    [Fact]
    public void Initialize_UsesStoredThemeValue()
    {
        var service = new ThemeService();

        service.Initialize("dark");

        Assert.Equal(KernTheme.Dark, service.Current);
        Assert.Equal("dark", service.AttributeValue);
    }

    [Fact]
    public void Set_RaisesThemeChangedOnlyWhenValueActuallyChanges()
    {
        var service = new ThemeService();
        var raised = 0;

        service.ThemeChanged += _ => raised++;

        service.Set(KernTheme.Light);
        service.Set(KernTheme.Dark);
        service.Set(KernTheme.Dark);

        Assert.Equal(1, raised);
    }
}

