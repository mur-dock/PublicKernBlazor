using Bunit;
using KernUx.Blazor.Components.Shared;
using KernUx.Blazor.Enums;

namespace KernUx.Blazor.Tests.Components.Shared;

public sealed class KernIconTests
{
    [Fact]
    public void RendersExpectedKernIconClasses()
    {
        using var context = new BunitContext();

        var cut = context.Render<KernIcon>(parameters => parameters
            .Add(parameter => parameter.Glyph, KernIconGlyph.Info)
            .Add(parameter => parameter.Size, IconSize.Large));

        var icon = cut.Find("span");

        Assert.Contains("kern-icon", icon.ClassList);
        Assert.Contains("kern-icon--info", icon.ClassList);
        Assert.Contains("kern-icon--large", icon.ClassList);
        Assert.Equal("true", icon.GetAttribute("aria-hidden"));
    }

    [Fact]
    public void RendersAccessibleImageAttributesWhenNotHidden()
    {
        using var context = new BunitContext();

        var cut = context.Render<KernIcon>(parameters => parameters
            .Add(parameter => parameter.Glyph, KernIconGlyph.Help)
            .Add(parameter => parameter.AriaHidden, false)
            .Add(parameter => parameter.AriaLabel, "Hilfe"));

        var icon = cut.Find("span");

        Assert.Equal("false", icon.GetAttribute("aria-hidden"));
        Assert.Equal("img", icon.GetAttribute("role"));
        Assert.Equal("Hilfe", icon.GetAttribute("aria-label"));
    }
}
