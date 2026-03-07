using KernUx.Blazor.Utilities;

namespace KernUx.Blazor.Tests.Utilities;

public sealed class CssBuilderTests
{
    [Fact]
    public void Build_AddsClassesAndAvoidsDuplicates()
    {
        var result = new CssBuilder("kern-icon")
            .AddClass("kern-icon--info")
            .AddClass("kern-icon kern-icon--info kern-icon--large")
            .Build();

        Assert.Equal("kern-icon kern-icon--info kern-icon--large", result);
    }

    [Fact]
    public void Build_IncludesClassFromAdditionalAttributes()
    {
        var attributes = new Dictionary<string, object>
        {
            ["class"] = "custom-a custom-b"
        };

        var result = new CssBuilder("kern-btn")
            .AddClassFromAttributes(attributes)
            .Build();

        Assert.Equal("kern-btn custom-a custom-b", result);
    }
}

