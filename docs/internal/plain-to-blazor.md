# Plan: KERN-UX Komponenten → Blazor Component Library

## 1. Ziel

Erstellung einer **wiederverwendbaren Blazor Component Library** (`PublicKernBlazor.Components`), die alle KERN-UX-Komponenten als
typsichere, barrierefreie Blazor-Komponenten bereitstellt. Die Library soll:

- Per NuGet-Paket einbindbar sein
- Nur sinnvolle/notwendige Parameter exponieren
- KERN-UX-CSS als statische Assets mitliefern
- Barrierefreiheit (WCAG 2.1 AA) von Haus aus gewährleisten
- Theming (Light/Dark) out-of-the-box unterstützen

---

## 2. Architektur & Projektstruktur

### 2.1 Solution-Aufbau

```
KernUx/
├── PublicKernBlazor.Components/                    # Razor Class Library (die Library)
│   ├── PublicKernBlazor.Components.csproj
│   ├── _Imports.razor
│   ├── wwwroot/
│   │   └── css/                      # Kompilierte KERN-UX SCSS → CSS
│   │       ├── kern-core.css
│   │       └── kern-theme.css
│   ├── Components/
│   │   ├── Layout/                   # Grid, Container, Row, Col
│   │   ├── Typography/               # Heading, Title, Body, Label, etc.
│   │   ├── Navigation/               # Kopfzeile, Link
│   │   ├── Forms/                    # Input, Select, Checkbox, Radio, etc.
│   │   ├── Feedback/                 # Alert, Badge, Loader, Progress
│   │   ├── Content/                  # Accordion, Card, Dialog, Table, etc.
│   │   └── Shared/                   # Icon, Divider, Screenreader-Helfer
│   ├── Enums/                        # Enums für Varianten, Größen, etc.
│   ├── Services/                     # ThemeService, IdGenerator
│   └── Extensions/                   # ServiceCollection-Erweiterungen
├── PublicKernBlazor.Demo/              # Blazor-App als Demo/Playground
└── PublicKernBlazor.Components.Tests/             # Unit-/bUnit-Tests
```

### 2.2 Projekt-Typ

- **Razor Class Library** (`Sdk="Microsoft.NET.Sdk.Razor"`)
- Target: `net10.0` (abwärtskompatibel ab `net8.0` über Multi-Targeting möglich)
- Statische Assets (`wwwroot/`) werden automatisch als `_content/PublicKernBlazor.Components/…` bereitgestellt

### 2.3 CSS-Einbindung

Die kompilierten KERN-UX-CSS-Dateien werden als statische Assets mitgeliefert. Einbindung in der konsumierenden App:

```html
<link rel="stylesheet" href="_content/PublicKernBlazor.Components/css/kern-core.css" />
<link rel="stylesheet" href="_content/PublicKernBlazor.Components/css/kern-theme.css" />
```

Alternativ: Eine Blazor-Komponente `<KernStyles />`, die alle CSS-Referenzen rendert.

---

## 3. Entwurfsprinzipien

### 3.1 Parameter-Design

| Prinzip                     | Beschreibung                                                                                                             |
|-----------------------------|--------------------------------------------------------------------------------------------------------------------------|
| **Nur sinnvolle Parameter** | Nicht jedes CSS-Detail als Parameter – nur das, was Entwickler tatsächlich variieren (Variante, Größe, Inhalt, Zustände) |
| **Starke Typisierung**      | Enums statt Strings für Varianten (`ButtonVariant.Primary` statt `"primary"`)                                            |
| **Sinnvolle Defaults**      | Jeder Parameter hat einen sinnvollen Standardwert                                                                        |
| **ChildContent**            | Für flexible Inhalte `RenderFragment` nutzen                                                                             |
| **AdditionalAttributes**    | `[Parameter(CaptureUnmatchedValues = true)]` für HTML-Attribute wie `id`, `class`, `data-*`                              |
| **EventCallback**           | Für alle Interaktionen (`OnClick`, `OnChange`, `OnToggle`)                                                               |

### 3.2 Barrierefreiheit

- ARIA-Attribute werden automatisch gesetzt (z.B. `role="alert"` bei Alert)
- Icons sind standardmäßig `aria-hidden="true"`
- Fehlermeldungen werden per `aria-describedby` mit Inputs verknüpft (automatische ID-Generierung)
- Fokus-Styles werden niemals unterdrückt

### 3.3 Naming-Konventionen

| Element         | Konvention          | Beispiel                             |
|-----------------|---------------------|--------------------------------------|
| Komponentenname | `Kern` + PascalCase | `KernButton`, `KernAlert`            |
| Enum-Name       | PascalCase          | `ButtonVariant`, `AlertType`, `Size` |
| Parameter       | PascalCase          | `Variant`, `Size`, `Disabled`        |
| Events          | `On` + PascalCase   | `OnClick`, `OnToggle`                |

---

## 4. Übersetzungsmuster: KERN-UX HTML → Blazor Component

### 4.1 Allgemeines Muster

**KERN-UX (Plain HTML):**

```html
<button class="kern-btn kern-btn--primary" disabled>
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label">Bearbeiten</span>
</button>
```

**Blazor Component (Nutzung):**

```razor
<KernButton Variant="ButtonVariant.Primary" 
            Icon="KernIconGlyph.Edit" 
            IconPosition="IconPosition.Left"
            Disabled="true"
            OnClick="HandleEdit">
    Bearbeiten
</KernButton>
```

### 4.2 Schritt-für-Schritt: Übersetzungsprozess

Für jede KERN-UX-Komponente werden folgende Schritte durchgeführt:

#### Schritt 1 – Analyse der KERN-Dokumentation

- HTML-Struktur aus `COMPONENTS.MD` erfassen
- BEM-Klassen identifizieren (Block, Elemente, Modifier)
- Zustände erfassen (Hover, Focus, Disabled, Error, Open)
- Varianten erfassen (Modifier-Klassen)
- Barrierefreiheits-Anforderungen notieren

#### Schritt 2 – Parameter-Definition

- Jeden Modifier als typisierten Parameter abbilden
- Enums für endliche Wertemengen erstellen
- Entscheiden: `ChildContent` (flexibel) vs. String-Parameter (einfach)
- Pflichtparameter vs. optionale Parameter mit Defaults festlegen

#### Schritt 3 – Enum-Erstellung

```csharp
public enum ButtonVariant { Primary, Secondary, Tertiary }
public enum Size { Small, Default, Large }
public enum AlertType { Info, Success, Warning, Danger }
```

#### Schritt 4 – Blazor-Komponente implementieren

- `.razor`-Datei mit KERN-konformem HTML-Markup
- CSS-Klassen dynamisch aus Parametern berechnen
- ARIA-Attribute automatisch setzen
- EventCallbacks definieren

#### Schritt 5 – Barrierefreiheit prüfen

- Semantische HTML-Elemente verwenden
- ARIA-Rollen und -Attribute validieren
- Tastatur-Navigation sicherstellen
- Screenreader-Texte berücksichtigen

#### Schritt 6 – Testen

- bUnit-Tests für Rendering und Interaktion
- Accessibility-Audits (axe-core o.ä.)
- Visueller Abgleich mit KERN-Storybook

---

## 5. Beispielhafte Umsetzung: `KernButton`

### 5.1 Enums

```csharp
public enum ButtonVariant
{
    Primary,
    Secondary,
    Tertiary
}

public enum IconPosition
{
    Left,
    Right
}
```

### 5.2 Komponente (`KernButton.razor`)

```razor
@namespace PublicKernBlazor.Components.Components

<button type="@ButtonType"
        class="@CssClass"
        disabled="@Disabled"
        title="@Title"
        @attributes="AdditionalAttributes"
        @onclick="HandleClick">
    @if (Icon is not null && IconPosition == IconPosition.Left)
    {
        <KernIcon Glyph="Icon.Value" AriaHidden="true" />
    }
    @if (ChildContent is not null)
    {
        @if (IconOnly)
        {
            <span class="kern-label kern-sr-only">@ChildContent</span>
        }
        else
        {
            <span class="kern-label">@ChildContent</span>
        }
    }
    @if (Icon is not null && IconPosition == IconPosition.Right)
    {
        <KernIcon Glyph="Icon.Value" AriaHidden="true" />
    }
</button>

@code {
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public ButtonVariant Variant { get; set; } = ButtonVariant.Primary;
    [Parameter] public KernIconGlyph? Icon { get; set; }
    [Parameter] public IconPosition IconPosition { get; set; } = IconPosition.Left;
    [Parameter] public bool IconOnly { get; set; }
    [Parameter] public bool Block { get; set; }
    [Parameter] public bool Disabled { get; set; }
    [Parameter] public string ButtonType { get; set; } = "button";
    [Parameter] public string? Title { get; set; }
    [Parameter] public EventCallback OnClick { get; set; }
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string CssClass => new CssBuilder("kern-btn")
        .AddClass($"kern-btn--{Variant.ToString().ToLowerInvariant()}")
        .AddClass("kern-btn--block", Block)
        .Build();

    private async Task HandleClick() => await OnClick.InvokeAsync();
}
```

### 5.3 Hilfswerkzeug: CssBuilder

Ein interner `CssBuilder` (oder Nutzung eines vorhandenen NuGet wie z.B. `BlazorComponentUtilities`) zur sauberen
Klassen-Komposition:

```csharp
internal class CssBuilder
{
    private readonly List<string> _classes = new();

    public CssBuilder(string baseClass) => _classes.Add(baseClass);

    public CssBuilder AddClass(string cssClass, bool when = true)
    {
        if (when) _classes.Add(cssClass);
        return this;
    }

    public string Build() => string.Join(" ", _classes);
}
```

---

## 6. Gemeinsame Enums & Services

### 6.1 Zentrale Enums

```csharp
// Größen (wiederverwendbar über Komponenten hinweg)
public enum Size { Small, Default, Large }
public enum ExtendedSize { XSmall, Small, Default, Large, XLarge }

// Semantische Varianten
public enum AlertType { Info, Success, Warning, Danger }
public enum BadgeVariant { Info, Success, Warning, Danger }
public enum ButtonVariant { Primary, Secondary, Tertiary }

// Typografie
public enum HeadingLevel { Display, XLarge, Large, Medium, Small }
public enum TitleSize { Small, Default, Large }
public enum BodyModifier { Default, Bold, Muted, Small, Large }

// Icon-Glyphen
public enum KernIconGlyph
{
    Home, ArrowUp, ArrowDown, ArrowForward, ArrowBack,
    Info, Success, Warning, Danger, CalendarToday,
    OpenInNew, Download, Logout, Checklist, Mail,
    Edit, SignLanguage, EasyLanguage, Autorenew,
    Add, Close, Delete, Search, QuestionMark,
    MoreVert, ContentCopy, Visibility, VisibilityOff,
    Check, DriveFolderUpload, ChevronLeft, ChevronRight,
    KeyboardDoubleArrowLeft, KeyboardDoubleArrowRight,
    BrightnessMedium, LightMode, DarkMode, Help
}

// Grid
public enum JustifyContent { Start, Center, End, Around, Between, Evenly }
public enum AlignItems { Start, Center, End }

// Theming
public enum KernTheme { Light, Dark }
```

### 6.2 Services

| Service              | Zweck                                                         |
|----------------------|---------------------------------------------------------------|
| `ThemeService`       | Theme-Verwaltung (Light/Dark), Event-basiert                  |
| `IdGeneratorService` | Generiert eindeutige IDs für `aria-describedby`-Verknüpfungen |

### 6.3 DI-Registrierung

```csharp
public static class KernUxServiceExtensions
{
    public static IServiceCollection AddKernUx(this IServiceCollection services)
    {
        services.AddScoped<ThemeService>();
        services.AddScoped<IdGeneratorService>();
        return services;
    }
}
```

---

## 7. Vollständige Liste aller KERN-UX-Komponenten

Nachfolgend die vollständige Liste aller KERN-UX-Komponenten, gruppiert nach Kategorie, mit dem jeweiligen geplanten
Blazor-Komponentennamen und den wichtigsten Parametern.

### 7.1 Layout

| # | KERN-UX Komponente   | Blazor-Name     | Wichtigste Parameter                                                             |
|---|----------------------|-----------------|----------------------------------------------------------------------------------|
| 1 | **Grid – Container** | `KernContainer` | `Fluid`                                                                          |
| 2 | **Grid – Row**       | `KernRow`       | `AlignItems`, `JustifyContent`, `ChildContent`                                   |
| 3 | **Grid – Column**    | `KernCol`       | `Span`, `SpanSm`, `SpanMd`, `SpanLg`, `SpanXl`, `SpanXxl`, `Offset`, `AlignSelf` |

### 7.2 Typografie

| #  | KERN-UX Komponente | Blazor-Name   | Wichtigste Parameter                                           |
|----|--------------------|---------------|----------------------------------------------------------------|
| 4  | **Heading**        | `KernHeading` | `Level` (Display/XLarge/Large/Medium/Small), `Element` (h1–h6) |
| 5  | **Title**          | `KernTitle`   | `Size` (Small/Default/Large)                                   |
| 6  | **Body**           | `KernBody`    | `Size`, `Bold`, `Muted`                                        |
| 7  | **Label**          | `KernLabel`   | `Size`, `For`, `Optional`, `OptionalText`                      |
| 8  | **Subline**        | `KernSubline` | `Size`                                                         |
| 9  | **Preline**        | `KernPreline` | `Size`                                                         |
| 10 | **Link**           | `KernLink`    | `Href`, `Stretched`, `Target`                                  |
| 11 | **Lists**          | `KernList`    | `Variant` (Bullet/Number), `Size`                              |

### 7.3 Icons & Dekoration

| #  | KERN-UX Komponente | Blazor-Name   | Wichtigste Parameter                       |
|----|--------------------|---------------|--------------------------------------------|
| 12 | **Icon**           | `KernIcon`    | `Glyph`, `Size`, `AriaHidden`, `AriaLabel` |
| 13 | **Divider**        | `KernDivider` | `AriaHidden`                               |

### 7.4 Buttons

| #  | KERN-UX Komponente | Blazor-Name  | Wichtigste Parameter                                                                                       |
|----|--------------------|--------------|------------------------------------------------------------------------------------------------------------|
| 14 | **Button**         | `KernButton` | `Variant` (Primary/Secondary/Tertiary), `Block`, `Icon`, `IconPosition`, `IconOnly`, `Disabled`, `OnClick` |

### 7.5 Formular-Elemente

| #  | KERN-UX Komponente | Blazor-Name         | Wichtigste Parameter                                                                          |
|----|--------------------|---------------------|-----------------------------------------------------------------------------------------------|
| 15 | **InputText**      | `KernInputText`     | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `ReadOnly`, `ValueChanged`         |
| 16 | **InputEmail**     | `KernInputEmail`    | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `Autocomplete`, `ValueChanged`     |
| 17 | **InputPassword**  | `KernInputPassword` | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `ValueChanged`                     |
| 18 | **InputTel**       | `KernInputTel`      | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `Autocomplete`, `ValueChanged`     |
| 19 | **InputUrl**       | `KernInputUrl`      | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `ValueChanged`                     |
| 20 | **InputNumber**    | `KernInputNumber`   | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `ValueChanged`                     |
| 21 | **InputDate**      | `KernInputDate`     | `Label`, `Hint`, `Error`, `Day`, `Month`, `Year`, `DayChanged`, `MonthChanged`, `YearChanged` |
| 22 | **InputFile**      | `KernInputFile`     | `Label`, `Hint`, `Error`, `Accept`, `OnFileSelected`                                          |
| 23 | **Textarea**       | `KernTextarea`      | `Label`, `Value`, `Hint`, `Error`, `Optional`, `Disabled`, `Rows`, `ValueChanged`             |
| 24 | **Select**         | `KernSelect`        | `Label`, `Value`, `Hint`, `Error`, `Options`, `Disabled`, `ValueChanged`                      |
| 25 | **Checkbox**       | `KernCheckbox`      | `Label`, `Checked`, `Error`, `Disabled`, `CheckedChanged`                                     |
| 26 | **Checkbox-List**  | `KernCheckboxList`  | `Legend`, `Items`, `Hint`, `Error`, `SelectedValues`, `SelectedValuesChanged`                 |
| 27 | **Radios**         | `KernRadioGroup`    | `Legend`, `Items`, `Hint`, `Error`, `Value`, `ValueChanged`                                   |
| 28 | **Fieldset**       | `KernFieldset`      | `Legend`, `Hint`, `Error`, `Horizontal`, `ChildContent`                                       |

### 7.6 Feedback & Status

| #  | KERN-UX Komponente | Blazor-Name    | Wichtigste Parameter                                          |
|----|--------------------|----------------|---------------------------------------------------------------|
| 29 | **Alert**          | `KernAlert`    | `Type` (Info/Success/Warning/Danger), `Title`, `ChildContent` |
| 30 | **Badge**          | `KernBadge`    | `Variant` (Info/Success/Warning/Danger), `Icon`, `Text`       |
| 31 | **Loader**         | `KernLoader`   | `Visible`, `ScreenReaderText`                                 |
| 32 | **Progress**       | `KernProgress` | `Value`, `Max`, `Label`, `LabelPosition` (Above/Below)        |

### 7.7 Content & Darstellung

| #  | KERN-UX Komponente   | Blazor-Name           | Wichtigste Parameter                                        |
|----|----------------------|-----------------------|-------------------------------------------------------------|
| 33 | **Accordion**        | `KernAccordion`       | `Title`, `Open`, `ChildContent`, `OnToggle`                 |
| 34 | **Accordion Group**  | `KernAccordionGroup`  | `ChildContent`                                              |
| 35 | **Card**             | `KernCard`            | `Size`, `Hug`, `Active`, `ChildContent`                     |
| 36 | **Card Media**       | `KernCardMedia`       | `ChildContent` (z.B. `<img>`)                               |
| 37 | **Card Header**      | `KernCardHeader`      | `Preline`, `Title`, `Subline`                               |
| 38 | **Card Body**        | `KernCardBody`        | `ChildContent`                                              |
| 39 | **Card Footer**      | `KernCardFooter`      | `ChildContent`                                              |
| 40 | **Dialog**           | `KernDialog`          | `Title`, `Open`, `ChildContent`, `FooterContent`, `OnClose` |
| 41 | **Table**            | `KernTable`           | `Caption`, `Small`, `Striped`, `Responsive`, `ChildContent` |
| 42 | **Description List** | `KernDescriptionList` | `Column`, `Items` oder `ChildContent`                       |
| 43 | **Summary**          | `KernSummary`         | `Number`, `Title`, `EditHref`, `ChildContent`               |
| 44 | **Summary Group**    | `KernSummaryGroup`    | `ChildContent`                                              |
| 45 | **Task List**        | `KernTaskList`        | `ChildContent`                                              |
| 46 | **Task List Item**   | `KernTaskListItem`    | `Number`, `Title`, `Href`, `StatusBadge`                    |
| 47 | **Task List Group**  | `KernTaskListGroup`   | `Title`, `ChildContent`                                     |

### 7.8 Navigation

| #  | KERN-UX Komponente | Blazor-Name     | Wichtigste Parameter |
|----|--------------------|-----------------|----------------------|
| 48 | **Kopfzeile**      | `KernKopfzeile` | `Label`, `Fluid`     |

### 7.9 Hilfsklassen (keine eigenständigen Komponenten, aber als Utility nutzbar)

| KERN-UX Klasse        | Beschreibung                                               |
|-----------------------|------------------------------------------------------------|
| `kern-sr-only`        | Screenreader-only Text                                     |
| `kern-sr-only-mobile` | Nur mobil versteckt, für Screenreader sichtbar             |
| `kern-error`          | Fehlermeldung (wird intern von Form-Komponenten gerendert) |
| `kern-hint`           | Hinweistext (wird intern von Form-Komponenten gerendert)   |
| `kern-number`         | Zählelement (wird intern von Summary/TaskList gerendert)   |

---

## 8. Formular-Integration mit Blazor EditForm

Die Formular-Komponenten sollen nahtlos mit Blazor `EditForm` und `EditContext` zusammenarbeiten:

```razor
<EditForm Model="model" OnValidSubmit="HandleSubmit">
    <DataAnnotationsValidator />
    
    <KernInputText Label="Vorname" 
                   @bind-Value="model.FirstName"
                   Error="@(context.GetValidationMessages(nameof(model.FirstName)).FirstOrDefault())" />
    
    <KernSelect Label="Land" 
                @bind-Value="model.Country"
                Options="countries" />
    
    <KernCheckbox Label="AGB akzeptieren" 
                  @bind-Checked="model.AcceptTerms"
                  Error="@(context.GetValidationMessages(nameof(model.AcceptTerms)).FirstOrDefault())" />
    
    <KernButton Variant="ButtonVariant.Primary" ButtonType="submit">
        Absenden
    </KernButton>
</EditForm>
```

### Two-Way Binding

Alle Formularfelder unterstützen `@bind-Value` / `@bind-Checked` durch das Pattern:

```csharp
[Parameter] public string? Value { get; set; }
[Parameter] public EventCallback<string?> ValueChanged { get; set; }
```

### Automatische Fehler-ID-Verknüpfung

Intern wird eine eindeutige ID generiert, um `aria-describedby` zwischen Input und Fehlermeldung zu verknüpfen:

```csharp
private string _inputId = $"kern-input-{Guid.NewGuid():N}";
private string _errorId = $"kern-error-{Guid.NewGuid():N}";
private string _hintId = $"kern-hint-{Guid.NewGuid():N}";
```

---

## 9. Theming

### 9.1 ThemeProvider-Komponente

Eine Root-Komponente, die das Theme-Attribut setzt:

```razor
<KernThemeProvider Theme="KernTheme.Light">
    @* Gesamte App *@
    <Router AppAssembly="...">...</Router>
</KernThemeProvider>
```

Intern setzt diese `data-kern-theme="light|dark"` und stellt den `ThemeService` bereit.

### 9.2 Theme-Toggle

```razor
<KernThemeToggle />
```

Fertige Komponente mit Button, Icon-Wechsel und Screenreader-Ansage.

---

## 10. Umsetzungs-Reihenfolge (Phasen)

### Phase 1 – Fundament (Woche 1–2)

1. Razor Class Library Projekt aufsetzen
2. CSS/SCSS-Assets integrieren
3. `CssBuilder` Utility implementieren
4. Zentrale Enums erstellen
5. `ThemeService` + `KernThemeProvider` migrieren
6. `KernIcon` (Basis für alle anderen Komponenten)
7. `KernStyles` (CSS-Einbindung)

### Phase 2 – Typografie & Layout (Woche 2–3)

8. `KernHeading`, `KernTitle`, `KernBody`, `KernLabel`, `KernSubline`, `KernPreline`
9. `KernLink`, `KernList`
10. `KernContainer`, `KernRow`, `KernCol`
11. `KernDivider`

### Phase 3 – Interaktive Basis-Komponenten (Woche 3–4)

12. `KernButton`
13. `KernAlert`
14. `KernBadge`
15. `KernAccordion` + `KernAccordionGroup`
16. `KernLoader`, `KernProgress`

### Phase 4 – Formular-Komponenten (Woche 4–6)

17. Basis-Input-Pattern (gemeinsames Wrapper-Template)
18. `KernInputText`, `KernInputEmail`, `KernInputPassword`, `KernInputTel`, `KernInputUrl`
19. `KernInputNumber`, `KernInputDate`
20. `KernInputFile`
21. `KernTextarea`
22. `KernSelect`
23. `KernCheckbox`, `KernCheckboxList`
24. `KernRadioGroup`
25. `KernFieldset`

### Phase 5 – Komplexe Komponenten (Woche 6–8)

26. `KernCard` + Sub-Komponenten (Media, Header, Body, Footer)
27. `KernDialog`
28. `KernTable` (+ responsive Wrapper)
29. `KernDescriptionList`
30. `KernSummary` + `KernSummaryGroup`
31. `KernTaskList` + Sub-Komponenten
32. `KernKopfzeile`

### Phase 6 – Qualitätssicherung & Dokumentation (Woche 8–10)

33. bUnit-Tests für alle Komponenten
34. Accessibility-Audit
35. NuGet-Paketierung
36. README, API-Dokumentation
37. Demo-App als Showcase

---

## 11. Qualitätskriterien

| Kriterium                 | Beschreibung                                                                    |
|---------------------------|---------------------------------------------------------------------------------|
| **HTML-Konformität**      | Gerendertes HTML muss 1:1 der KERN-UX-Dokumentation entsprechen                 |
| **Barrierefreiheit**      | WCAG 2.1 AA – alle ARIA-Attribute, semantisches HTML, Fokus-Management          |
| **Typensicherheit**       | Enums statt Magic-Strings, nullable Reference Types                             |
| **Testabdeckung**         | Mindestens Rendering-Tests + Interaktions-Tests pro Komponente                  |
| **Keine JS-Abhängigkeit** | Kein JavaScript, es sei denn technisch unvermeidbar (z.B. Dialog `showModal()`) |
| **Performance**           | Minimale Render-Zyklen, effiziente CSS-Klassen-Berechnung                       |

---

## 12. NuGet-Paket

```xml
<PropertyGroup>
    <PackageId>PublicKernBlazor.Components</PackageId>
    <Description>KERN-UX Design System als Blazor Component Library</Description>
    <PackageTags>blazor;kern-ux;design-system;barrierefreiheit;accessibility</PackageTags>
    <PackageLicenseExpression>MIT</PackageLicenseExpression>
</PropertyGroup>
```

Einbindung durch Konsumenten:

```bash
dotnet add package PublicKernBlazor.Components
```

```csharp
// Program.cs
builder.Services.AddKernUx();
```

```razor
@* _Imports.razor *@
@using PublicKernBlazor.Components.Components
@using PublicKernBlazor.Components.Enums
```

---

## 13. Zusammenfassung

Dieser Plan beschreibt die systematische Übersetzung aller **48 KERN-UX-Komponenten** in typsichere, barrierefreie
Blazor-Komponenten. Der Kern des Ansatzes:

1. **Analyse** der KERN-HTML/CSS-Struktur aus `COMPONENTS.MD`
2. **Abstraktion** durch sinnvolle Parameter und Enums
3. **Implementierung** als Razor Class Library mit statischen CSS-Assets
4. **Qualitätssicherung** durch Tests und Accessibility-Audits
5. **Distribution** als NuGet-Paket

Die Library ermöglicht Entwicklern, KERN-UX-konforme Blazor-Anwendungen zu erstellen, ohne die KERN-CSS-Klassen direkt
kennen zu müssen – die Komponenten kapseln das vollständig.

