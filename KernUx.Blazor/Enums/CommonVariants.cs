namespace KernUx.Blazor.Enums;

public enum Size
{
    Small,
    Default,
    Large
}

public enum ExtendedSize
{
    XSmall,
    Small,
    Default,
    Large,
    XLarge
}

public enum AlertType
{
    Info,
    Success,
    Warning,
    Danger
}

public enum BadgeVariant
{
    Info,
    Success,
    Warning,
    Danger
}

public enum ButtonVariant
{
    Primary,
    Secondary,
    Tertiary
}

public enum TitleSize
{
    Small,
    Default,
    Large
}

public enum BodyModifier
{
    Default,
    Bold,
    Muted,
    Small,
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

