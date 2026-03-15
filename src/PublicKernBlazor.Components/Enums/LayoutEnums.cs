namespace PublicKernBlazor.Components.Enums;

/// <summary>Horizontale Ausrichtung von Flex-Kindelementen (<c>justify-content</c>).</summary>
public enum JustifyContent
{
    /// <summary>Am Anfang ausrichten.</summary>
    Start,
    /// <summary>Zentriert ausrichten.</summary>
    Center,
    /// <summary>Am Ende ausrichten.</summary>
    End,
    /// <summary>Gleichmäßig verteilen mit Außenabstand.</summary>
    Around,
    /// <summary>Gleichmäßig verteilen ohne Außenabstand.</summary>
    Between,
    /// <summary>Vollständig gleichmäßig verteilen.</summary>
    Evenly
}

/// <summary>Vertikale Ausrichtung von Flex-Kindelementen (<c>align-items</c>).</summary>
public enum AlignItems
{
    /// <summary>Am Anfang ausrichten.</summary>
    Start,
    /// <summary>Zentriert ausrichten.</summary>
    Center,
    /// <summary>Am Ende ausrichten.</summary>
    End
}

/// <summary>Selbst-Ausrichtung eines einzelnen Flex-Kindelements (<c>align-self</c>).</summary>
public enum AlignSelf
{
    /// <summary>Am Anfang ausrichten.</summary>
    Start,
    /// <summary>Zentriert ausrichten.</summary>
    Center,
    /// <summary>Am Ende ausrichten.</summary>
    End
}
