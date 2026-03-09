using Bunit;
using KernUx.Blazor.Components.Feedback;
using KernUx.Blazor.Enums;
using KernUx.Blazor.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KernUx.Blazor.Tests.Components.Feedback;

public sealed class FeedbackComponentsTests
{
    [Fact(DisplayName = "Alert rendert Typ-Klasse und Standard-Icon")]
    public void KernAlert_RendertTypUndIcon()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernAlert>(parameters => parameters
            .Add(p => p.Type, AlertType.Warning)
            .Add(p => p.Title, "Warnung")
            .AddChildContent("Achtung"));

        // Then
        var alert = cut.Find("div.kern-alert");
        Assert.Contains("kern-alert--warning", alert.ClassList);
        Assert.Contains("kern-icon--warning", cut.Markup);
    }

    [Fact(DisplayName = "Badge rendert Variante und Label")]
    public void KernBadge_RendertVarianteUndLabel()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernBadge>(parameters => parameters
            .Add(p => p.Variant, BadgeVariant.Success)
            .AddChildContent("Erledigt"));

        // Then
        var badge = cut.Find("span.kern-badge");
        Assert.Contains("kern-badge--success", badge.ClassList);
        Assert.Contains("Erledigt", cut.Markup);
    }

    [Fact(DisplayName = "Loader rendert visible-Modifier bei sichtbarem Zustand")]
    public void KernLoader_RendertVisibleModifier()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernLoader>(parameters => parameters
            .Add(p => p.Visible, true));

        // Then
        var loader = cut.Find("div.kern-loader");
        Assert.Contains("kern-loader--visible", loader.ClassList);
    }

    [Fact(DisplayName = "Progress begrenzt Value auf Max")]
    public void KernProgress_BegrenztValueAufMax()
    {
        // BunitContext: virtuelles DOM statt Browser.
        using var context = new BunitContext();

        // context.Services.AddScoped(...) befüllt den DI-Container der Komponente.
        context.Services.AddScoped<IdGeneratorService>();

        // Render<T> erstellt das Component Under Test.
        var cut = context.Render<KernProgress>(parameters => parameters
            .Add(p => p.Value, 250)
            .Add(p => p.Max, 100)
            .Add(p => p.Label, "Fortschritt"));

        // cut.Find(...) liest das gerenderte <progress>-Element.
        var progress = cut.Find("progress");

        Assert.Equal("100", progress.GetAttribute("value"));
        Assert.Equal("100", progress.GetAttribute("max"));
    }
}

