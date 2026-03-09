using Bunit;
using KernUx.Blazor.Components.Content;
using KernUx.Blazor.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace KernUx.Blazor.Tests.Components.Content;

public sealed class KernSummaryTests
{
    [Fact(DisplayName = "Summary rendert Nummer, Titel und Edit-Link")]
    public void KernSummary_RendertNummerTitelUndEditLink()
    {
        // BunitContext: virtuelles DOM statt echtem Browser.
        using var context = new BunitContext();

        // KernSummary injiziert IdGeneratorService für eindeutige Title-IDs.
        context.Services.AddScoped<IdGeneratorService>();

        // Render<T> rendert die Komponente und liefert das Component Under Test (cut).
        var cut = context.Render<KernSummary>(parameters => parameters
            .Add(p => p.Number, "1")
            .Add(p => p.Title, "Kontaktdaten")
            .Add(p => p.EditHref, "/edit/1")
            .AddChildContent("<p>Details</p>"));

        // Then
        Assert.Contains("kern-number", cut.Markup);
        Assert.Contains("Kontaktdaten", cut.Markup);
        Assert.Contains("kern-summary__actions", cut.Markup);
        Assert.Contains("/edit/1", cut.Markup);
    }

    [Fact(DisplayName = "SummaryGroup rendert Wrapper-Klasse")]
    public void KernSummaryGroup_RendertWrapperKlasse()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernSummaryGroup>(parameters => parameters
            .AddChildContent("<div>Eintrag</div>"));

        // Then – cut.Find() wirft wenn kein Element vorhanden.
        cut.Find("div.kern-summary-group");
    }

    [Fact(DisplayName = "TaskList rendert Titel und Listenstruktur")]
    public void KernTaskList_RendertTitelUndListe()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernTaskList>(parameters => parameters
            .Add(p => p.Title, "Aufgaben")
            .AddChildContent("<li>Eintrag</li>"));

        // Then
        Assert.Contains("kern-task-list__header", cut.Markup);
        Assert.Contains("kern-task-list__list", cut.Markup);
        Assert.Contains("Aufgaben", cut.Markup);
    }

    [Fact(DisplayName = "TaskListItem rendert Nummer, Titel und Status")]
    public void KernTaskListItem_RendertAlleTeile()
    {
        // Given
        using var context = new BunitContext();

        // context.Services.AddScoped(...) befüllt den DI-Container für den Test.
        context.Services.AddScoped<IdGeneratorService>();

        // RenderFragment kann in .cs-Dateien nicht mit Razor-Syntax erstellt werden.
        // Stattdessen wird ein builder-Delegate verwendet.
        RenderFragment statusBadge = builder =>
        {
            builder.AddMarkupContent(0, "<span class=\"kern-badge\">Offen</span>");
        };

        // When
        var cut = context.Render<KernTaskListItem>(parameters => parameters
            .Add(p => p.Number, "3")
            .Add(p => p.Title, "Dokumente einreichen")
            .Add(p => p.Href, "/task/3")
            .Add(p => p.StatusContent, statusBadge));

        // Then
        Assert.Contains("kern-number", cut.Markup);
        Assert.Contains("kern-link--stretched", cut.Markup);
        Assert.Contains("kern-task-list__status", cut.Markup);
        Assert.Contains("Dokumente einreichen", cut.Markup);
    }

    [Fact(DisplayName = "TaskListGroup rendert Gruppentitel")]
    public void KernTaskListGroup_RendertGruppentitel()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernTaskListGroup>(parameters => parameters
            .Add(p => p.Title, "Phase 1")
            .AddChildContent("<div>Inhalt</div>"));

        // Then
        Assert.Contains("kern-task-list-group", cut.Markup);
        Assert.Contains("Phase 1", cut.Markup);
    }
}

