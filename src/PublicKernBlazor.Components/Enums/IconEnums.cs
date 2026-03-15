namespace PublicKernBlazor.Components.Enums;

/// <summary>Größenvariante eines <c>KernIcon</c>.</summary>
public enum IconSize
{
    /// <summary>Kleines Icon (16 px).</summary>
    Small,
    /// <summary>Standard-Icon (24 px).</summary>
    Default,
    /// <summary>Großes Icon (32 px).</summary>
    Large,
    /// <summary>Extra-großes Icon (48 px).</summary>
    XLarge
}

/// <summary>
/// Alle verfügbaren Icon-Glyphen des KERN-UX Icon-Sets.
/// Jeder Wert entspricht einem CSS-Modifier der Form <c>kern-icon--{glyph}</c>.
/// </summary>
public enum KernIconGlyph
{
    /// <summary>Hinzufügen / Plus.</summary>
    Add,
    /// <summary>Pfeil nach unten.</summary>
    ArrowDown,
    /// <summary>Pfeil nach oben.</summary>
    ArrowUp,
    /// <summary>Pfeil vorwärts.</summary>
    ArrowForward,
    /// <summary>Pfeil zurück.</summary>
    ArrowBack,
    /// <summary>Aktualisieren / Drehen.</summary>
    Autorenew,
    /// <summary>Heutiges Datum / Kalender.</summary>
    CalendarToday,
    /// <summary>Häkchen / Bestätigen.</summary>
    Check,
    /// <summary>Checkliste.</summary>
    Checklist,
    /// <summary>Pfeil-Chevron nach links.</summary>
    ChevronLeft,
    /// <summary>Pfeil-Chevron nach rechts.</summary>
    ChevronRight,
    /// <summary>Schließen / X.</summary>
    Close,
    /// <summary>Inhalt kopieren.</summary>
    ContentCopy,
    /// <summary>Gefahr / Fehler-Symbol.</summary>
    Danger,
    /// <summary>Löschen / Mülleimer.</summary>
    Delete,
    /// <summary>Herunterladen.</summary>
    Download,
    /// <summary>Entwurf / Dokument.</summary>
    Draft,
    /// <summary>Ordner hochladen.</summary>
    DriveFolderUpload,
    /// <summary>Einfache Sprache.</summary>
    EasyLanguage,
    /// <summary>Bearbeiten / Stift.</summary>
    Edit,
    /// <summary>Startseite / Haus.</summary>
    Home,
    /// <summary>Hilfe / Fragezeichen-Kreis.</summary>
    Help,
    /// <summary>Information / Info-Kreis.</summary>
    Info,
    /// <summary>Doppel-Chevron ganz links springen.</summary>
    KeyboardDoubleArrowLeft,
    /// <summary>Doppel-Chevron ganz rechts springen.</summary>
    KeyboardDoubleArrowRight,
    /// <summary>Abmelden / Logout.</summary>
    Logout,
    /// <summary>E-Mail / Brief.</summary>
    Mail,
    /// <summary>Drei Punkte vertikal / Mehr-Optionen.</summary>
    MoreVert,
    /// <summary>In neuem Fenster öffnen.</summary>
    OpenInNew,
    /// <summary>Fragezeichen.</summary>
    QuestionMark,
    /// <summary>Suche / Lupe.</summary>
    Search,
    /// <summary>Gebärdensprache.</summary>
    SignLanguage,
    /// <summary>Erfolg / Haken-Kreis.</summary>
    Success,
    /// <summary>Sichtbarkeit einschalten / Auge.</summary>
    Visibility,
    /// <summary>Sichtbarkeit ausschalten / Auge durchgestrichen.</summary>
    VisibilityOff,
    /// <summary>Warnung / Ausrufezeichen-Dreieck.</summary>
    Warning,
    /// <summary>Helligkeit / Theme-Wechsel.</summary>
    BrightnessMedium,
    /// <summary>Helles Theme / Sonne.</summary>
    LightMode,
    /// <summary>Dunkles Theme / Mond.</summary>
    DarkMode
}
