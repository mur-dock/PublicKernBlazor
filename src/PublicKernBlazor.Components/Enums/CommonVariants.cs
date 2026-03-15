namespace PublicKernBlazor.Components.Enums;

/// <summary>Dreistufige Größenvariante für KERN-UX-Komponenten.</summary>
public enum Size
{
    /// <summary>Kleine Darstellung.</summary>
    Small,
    /// <summary>Standardgröße.</summary>
    Default,
    /// <summary>Große Darstellung.</summary>
    Large
}

/// <summary>Fünfstufige Größenvariante für KERN-UX-Komponenten.</summary>
public enum ExtendedSize
{
    /// <summary>Extra-kleine Darstellung.</summary>
    XSmall,
    /// <summary>Kleine Darstellung.</summary>
    Small,
    /// <summary>Standardgröße.</summary>
    Default,
    /// <summary>Große Darstellung.</summary>
    Large,
    /// <summary>Extra-große Darstellung.</summary>
    XLarge
}

/// <summary>Semantischer Typ einer <c>KernAlert</c>-Komponente.</summary>
public enum AlertType
{
    /// <summary>Hinweis (neutral-informativ).</summary>
    Info,
    /// <summary>Erfolgsmeldung.</summary>
    Success,
    /// <summary>Warnmeldung.</summary>
    Warning,
    /// <summary>Fehlermeldung.</summary>
    Danger
}

/// <summary>Visuelle Variante einer <c>KernBadge</c>-Komponente.</summary>
public enum BadgeVariant
{
    /// <summary>Neutrale Info-Variante.</summary>
    Info,
    /// <summary>Erfolgs-Variante.</summary>
    Success,
    /// <summary>Warn-Variante.</summary>
    Warning,
    /// <summary>Fehler-Variante.</summary>
    Danger
}

/// <summary>Visuelle Variante einer <c>KernButton</c>-Komponente.</summary>
public enum ButtonVariant
{
    /// <summary>Primäre Schaltfläche (hervorgehoben).</summary>
    Primary,
    /// <summary>Sekundäre Schaltfläche.</summary>
    Secondary,
    /// <summary>Tertiäre Schaltfläche (dezent).</summary>
    Tertiary
}

/// <summary>Größenvariante eines <c>KernTitle</c>.</summary>
public enum TitleSize
{
    /// <summary>Kleiner Titel.</summary>
    Small,
    /// <summary>Standard-Titel.</summary>
    Default,
    /// <summary>Großer Titel.</summary>
    Large
}

/// <summary>Darstellungs-Modifier für <c>KernBody</c>-Text.</summary>
public enum BodyModifier
{
    /// <summary>Standarddarstellung.</summary>
    Default,
    /// <summary>Fettgedruckter Text.</summary>
    Bold,
    /// <summary>Gedimmter (muted) Text.</summary>
    Muted,
    /// <summary>Kleinerer Text.</summary>
    Small,
    /// <summary>Größerer Text.</summary>
    Large
}

/// <summary>Größenvariante einer <c>KernCard</c>.</summary>
public enum CardSize
{
    /// <summary>Standardgröße.</summary>
    Default,

    /// <summary>Kompakte Darstellung mit reduziertem Spacing.</summary>
    Small,

    /// <summary>Größere Darstellung mit mehr Whitespace.</summary>
    Large
}

/// <summary>Layout-Variante einer <c>KernDescriptionList</c>.</summary>
public enum DescriptionListLayout
{
    /// <summary>Key und Value nebeneinander (Standard).</summary>
    Row,

    /// <summary>Key und Value untereinander (<c>--col</c>).</summary>
    Column
}
