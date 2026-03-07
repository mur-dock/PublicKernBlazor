using Bunit;
using KernUx.Blazor.Components.Shared;
using Microsoft.AspNetCore.Components.Web;

namespace KernUx.Blazor.Tests.Components.Shared;

public sealed class KernStylesTests
{
    [Fact]
    public void InjectsThemeStylesheetIntoHead()
    {
        using var context = new BunitContext();
        context.JSInterop.Setup<string>("Blazor._internal.PageTitle.getAndRemoveExistingTitle").SetResult(string.Empty);

        var headOutlet = context.Render<HeadOutlet>();

        context.Render<KernStyles>(parameters => parameters
            .Add(parameter => parameter.BasePath, "_content/KernUx.Blazor")
            .Add(parameter => parameter.ThemeName, "kern"));

        headOutlet.MarkupMatches("<link rel=\"stylesheet\" href=\"_content/KernUx.Blazor/css/themes/kern/index.css\">");
    }
}
