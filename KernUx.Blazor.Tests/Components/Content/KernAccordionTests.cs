﻿using Bunit;
using KernUx.Blazor.Components.Content;

namespace KernUx.Blazor.Tests.Components.Content;

public sealed class KernAccordionTests
{
    [Fact(DisplayName = "Accordion rendert geöffneten Zustand")]
    public void KernAccordion_RendertOpenAttribut()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernAccordion>(parameters => parameters
            .Add(p => p.Title, "Abschnitt")
            .Add(p => p.Open, true)
            .AddChildContent("Inhalt"));

        // Then
        var details = cut.Find("details");
        Assert.Contains("kern-accordion", details.ClassList);
        Assert.NotNull(details.GetAttribute("open"));
    }

    [Fact(DisplayName = "Accordion löst OnToggle beim Toggle-Event aus")]
    public void KernAccordion_LoestOnToggleAus()
    {
        // Given
        using var context = new BunitContext();
        bool? state = null;

        // EventCallback repräsentiert Blazors typsicheren Event-Mechanismus.
        var cut = context.Render<KernAccordion>(parameters => parameters
            .Add(p => p.Title, "Abschnitt")
            .Add(p => p.OnToggle, value => state = value));

        // When – TriggerEvent simuliert ein DOM-Ereignis im virtuellen DOM.
        cut.Find("details").TriggerEvent("ontoggle", EventArgs.Empty);

        // Then
        Assert.True(state);
    }

    [Fact(DisplayName = "AccordionGroup rendert Wrapper-Klasse")]
    public void KernAccordionGroup_RendertWrapperKlasse()
    {
        // Given
        using var context = new BunitContext();

        // When
        var cut = context.Render<KernAccordionGroup>(parameters => parameters
            .AddChildContent("<div>Eintrag</div>"));

        // Then – cut.Find() wirft eine Exception wenn kein Element gefunden wird,
        // daher ist keine zusätzliche Assert.NotNull-Prüfung notwendig.
        cut.Find("div.kern-accordion-group");
    }
}

