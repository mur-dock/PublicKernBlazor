using Bunit;
using PublicKernBlazor.Components.Components.Feedback;
using PublicKernBlazor.Components.Enums;
using PublicKernBlazor.Components.Services;
using Microsoft.Extensions.DependencyInjection;

namespace PublicKernBlazor.Components.Tests.Components.Feedback;

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

    [Fact(DisplayName = "Alert setzt role='alert' für Live-Region-Barrierefreiheit")]
    public void KernAlert_Setzt_Role_Alert()
    {
        // Given – role="alert" ist eine ARIA-Live-Region: Screenreader lesen den Inhalt
        // automatisch vor, wenn er ins DOM eingefügt wird (WCAG 4.1.3).
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernAlert>(parameters => parameters
            .Add(p => p.Type, AlertType.Danger)
            .Add(p => p.Title, "Fehler"));

        // Then – das äußere div muss role="alert" tragen.
        var alert = cut.Find("div.kern-alert");
        Assert.Equal("alert", alert.GetAttribute("role"));
    }

    [Fact(DisplayName = "Loader rendert ScreenreaderText als kern-sr-only")]
    public void KernLoader_RendertScreenreaderText()
    {
        // Given – der Ladeindikator muss für Screenreader einen lesbaren Text liefern.
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernLoader>(parameters => parameters
            .Add(p => p.Visible, true)
            .Add(p => p.ScreenReaderText, "Wird geladen..."));

        // Then – kern-sr-only macht Text visuell unsichtbar, aber für AT (Assistive Technology) lesbar.
        Assert.Contains("kern-sr-only", cut.Markup);
        Assert.Contains("Wird geladen...", cut.Markup);
    }
}

