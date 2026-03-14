# KernUx.Blazor

**KERN-UX Design System** als Blazor Component Library –
typsichere, barrierefreie Komponenten für die deutsche Verwaltung (WCAG 2.1 AA).

[![CI](https://github.com/mur-dock/KernUx.Blazor/actions/workflows/ci.yml/badge.svg)](https://github.com/OWNER/KernUx.Blazor/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/v/KernUx.Blazor)](https://www.nuget.org/packages/KernUx.Blazor)
[![KERN-UX](https://img.shields.io/badge/KERN--UX-Design%20System-green)](https://www.kern-ux.de)
[![Lizenz: EUPL-1.2](https://img.shields.io/badge/Lizenz-EUPL_1.2-yellow)](LICENSE)

---

## Inhalt

- [Was ist KernUx.Blazor?](#was-ist-kernuxblazor)
- [Voraussetzungen](#voraussetzungen)
- [Installation](#installation)
- [Schnellstart](#schnellstart)
- [CSS-Assets einbinden](#css-assets-einbinden)
- [Theming (Light / Dark)](#theming-light--dark)
- [Komponenten-Übersicht](#komponenten-übersicht)
- [Barrierefreiheit](#barrierefreiheit)
- [Erweiterungen gegenüber KERN-UX](#erweiterungen-gegenüber-kern-ux)
- [Projektstruktur](#projektstruktur)
- [Entwicklung & Tests](#entwicklung--tests)
- [Beitragen](#beitragen)
- [Changelog](#changelog)
- [Lizenz](#lizenz)

---

## Was ist KernUx.Blazor?

`KernUx.Blazor` übersetzt das offizielle [KERN-UX Design System](https://www.kern-ux.de) in
eine **Razor Class Library**. Entwicklerinnen und Entwickler können alle KERN-Komponenten
direkt als Blazor-Komponenten verwenden – ohne KERN-CSS-Klassen manuell kennen zu müssen.

**Kern-Vorteile:**

- ✅ Alle 48 KERN-UX-Komponenten als typsichere Blazor-Komponenten
- ✅ KERN-CSS als statische Assets (`_content/KernUx.Blazor/css/`) inklusive
- ✅ WCAG 2.1 AA – ARIA-Attribute, semantisches HTML, Fokus-Management
- ✅ Light- und Dark-Theme out-of-the-box
- ✅ Kein JavaScript außer dem technisch unvermeidbaren Minimum
- ✅ Vollständige XML-Dokumentation für IntelliSense

---

## Voraussetzungen

| Anforderung                    | Version |
|--------------------------------|---------|
| .NET                           | 10.0    |
| Blazor Server oder WebAssembly | 10.0    |

---

## Installation

```bash
dotnet add package KernUx.Blazor
```

### DI-Registrierung (`Program.cs`)

```csharp
using KernUx.Blazor.Extensions;

builder.Services.AddKernUx();
```

`AddKernUx()` registriert:

- `ThemeService` (Scoped) – Theme-Verwaltung (Light/Dark)
- `IdGeneratorService` (Scoped) – eindeutige IDs für `aria-describedby`

### Imports (`_Imports.razor`)

```razor
@using KernUx.Blazor.Components.Content
@using KernUx.Blazor.Components.Feedback
@using KernUx.Blazor.Components.Forms
@using KernUx.Blazor.Components.Layout
@using KernUx.Blazor.Components.Navigation
@using KernUx.Blazor.Components.Shared
@using KernUx.Blazor.Components.Typography
@using KernUx.Blazor.Enums
@using KernUx.Blazor.Services
```

---

## CSS-Assets einbinden

### Option A – `KernStyles`-Komponente (empfohlen)

```razor
@* App.razor oder MainLayout.razor *@
<KernStyles />
```

Die Komponente rendert automatisch alle notwendigen `<link>`-Elemente.

### Option B – manuell in `App.razor`

```html

<link rel="stylesheet" href="_content/KernUx.Blazor/css/themes/kern/index.css"/>
```

### JS-Asset für Dialog

```html

<script src="_content/KernUx.Blazor/js/kern-dialog.js"></script>
```

---

## Theming (Light / Dark)

### Root-Layout mit `KernThemeProvider`

```razor
<KernThemeProvider Theme="KernTheme.Light">
    <Router AppAssembly="typeof(App).Assembly">
        <Found Context="routeData">
            <RouteView RouteData="routeData" />
        </Found>
    </Router>
</KernThemeProvider>
```

### Theme programmatisch wechseln

```razor
@inject ThemeService ThemeService

<KernButton OnClick="ThemeService.Toggle">Theme wechseln</KernButton>
```

Das Theme wird über das Attribut `data-kern-theme="light|dark"` am Wrapper-Element gesteuert
und im Cookie `kern-theme` persistiert.

---

## Komponenten-Übersicht

### Layout

| Komponente          | Beschreibung                                 | Wichtigste Parameter                 |
|---------------------|----------------------------------------------|--------------------------------------|
| `KernContainer`     | Inhalts-Container mit optionalem Fluid-Modus | `Fluid`                              |
| `KernRow`           | Grid-Zeile                                   | `AlignItems`, `JustifyContent`       |
| `KernCol`           | Grid-Spalte                                  | `Span`, `SpanSm`–`SpanXxl`, `Offset` |
| `KernThemeProvider` | Theme-Root-Wrapper                           | `Theme`                              |

### Typografie

| Komponente    | Beschreibung                | Wichtigste Parameter    |
|---------------|-----------------------------|-------------------------|
| `KernHeading` | Überschrift (Display–Small) | `Level`, `Element`      |
| `KernTitle`   | Seiten-/Abschnittstitel     | `Size`                  |
| `KernBody`    | Fließtext                   | `Bold`, `Muted`, `Size` |
| `KernLabel`   | Formular-Label              | `For`, `Optional`       |
| `KernSubline` | Unterzeile                  | `Size`                  |
| `KernPreline` | Vortext                     | `Size`                  |

### Shared

| Komponente    | Beschreibung               | Wichtigste Parameter                       |
|---------------|----------------------------|--------------------------------------------|
| `KernIcon`    | Symbolschrift-Icon         | `Glyph`, `Size`, `AriaHidden`, `AriaLabel` |
| `KernDivider` | Horizontale Trennlinie     | `AriaHidden`                               |
| `KernStyles`  | CSS-Link-Einbindung        | –                                          |
| `KernLink`    | Anker-Link                 | `Href`, `Stretched`, `Target`              |
| `KernList`    | Listen (Bullet/Nummeriert) | `Variant`, `Size`                          |

### Navigation

| Komponente      | Beschreibung             | Wichtigste Parameter |
|-----------------|--------------------------|----------------------|
| `KernKopfzeile` | Bundesbehörden-Kopfzeile | `Label`, `Fluid`     |

### Formular-Elemente

| Komponente          | Beschreibung          | Wichtigste Parameter                               |
|---------------------|-----------------------|----------------------------------------------------|
| `KernButton`        | Aktions-Schaltfläche  | `Variant`, `Icon`, `IconOnly`, `Block`, `Disabled` |
| `KernInputText`     | Text-Eingabe          | `Label`, `Value`, `Hint`, `Error`, `Optional`      |
| `KernInputEmail`    | E-Mail-Eingabe        | `Label`, `Value`, `Autocomplete`                   |
| `KernInputPassword` | Passwort-Eingabe      | `Label`, `Value`                                   |
| `KernInputTel`      | Telefon-Eingabe       | `Label`, `Value`, `Autocomplete`                   |
| `KernInputUrl`      | URL-Eingabe           | `Label`, `Value`                                   |
| `KernInputNumber`   | Numerische Eingabe    | `Label`, `Value`                                   |
| `KernInputDate`     | Datum (TT/MM/JJJJ)    | `Label`, `Day`, `Month`, `Year`                    |
| `KernInputFile`     | Datei-Upload          | `Label`, `Accept`, `OnFileSelected`                |
| `KernInputCurrency` | Währungsbetrag (DACH) | `Label`, `Value`, `CurrencySymbol`, `CultureName`  |
| `KernTextarea`      | Mehrzeiliger Text     | `Label`, `Value`, `Rows`                           |
| `KernSelect`        | Auswahlmenü           | `Label`, `Value`, `Options`                        |
| `KernCheckbox`      | Einzelne Checkbox     | `Label`, `Checked`                                 |
| `KernCheckboxList`  | Checkbox-Gruppe       | `Legend`, `Items`, `SelectedValues`                |
| `KernRadioGroup`    | Radio-Button-Gruppe   | `Legend`, `Items`, `Value`                         |
| `KernFieldset`      | Formular-Gruppe       | `Legend`, `Hint`, `Error`, `Horizontal`            |

### Feedback & Status

| Komponente     | Beschreibung           | Wichtigste Parameter          |
|----------------|------------------------|-------------------------------|
| `KernAlert`    | Hinweis-/Fehlermeldung | `Type`, `Title`               |
| `KernBadge`    | Status-Kennzeichnung   | `Variant`                     |
| `KernLoader`   | Ladeindikator          | `Visible`, `ScreenReaderText` |
| `KernProgress` | Fortschrittsanzeige    | `Value`, `Max`, `Label`       |

### Content & Darstellung

| Komponente            | Beschreibung               | Wichtigste Parameter                        |
|-----------------------|----------------------------|---------------------------------------------|
| `KernAccordion`       | Auf-/Zuklappbarer Bereich  | `Title`, `Open`, `OnToggle`                 |
| `KernAccordionGroup`  | Accordion-Container        | –                                           |
| `KernCard`            | Inhalts-Karte              | `Size`, `Active`                            |
| `KernCardMedia`       | Karten-Bild                | –                                           |
| `KernCardContainer`   | Karten-Inhalt-Wrapper      | –                                           |
| `KernDialog`          | Modaler Dialog             | `Title`, `Open`, `OnClose`                  |
| `KernTable`           | Datentabelle               | `Caption`, `Small`, `Striped`, `Responsive` |
| `KernDescriptionList` | Beschreibungsliste         | `Column`, `Items`                           |
| `KernSummary`         | Zusammenfassungs-Block     | `Title`, `EditHref`, `Number`               |
| `KernSummaryGroup`    | Summary-Gruppen-Container  | –                                           |
| `KernTaskList`        | Aufgaben-/Schritt-Liste    | –                                           |
| `KernTaskListItem`    | Einzelner Aufgaben-Schritt | `Title`, `Number`, `Href`, `StatusContent`  |
| `KernTaskListGroup`   | Aufgaben-Gruppen-Container | `Title`                                     |

---

## Barrierefreiheit

`KernUx.Blazor` implementiert **WCAG 2.1 AA**:

| Anforderung         | Umsetzung                                                                        |
|---------------------|----------------------------------------------------------------------------------|
| Semantisches HTML   | `<button>`, `<fieldset>`, `<legend>`, `<dialog>`, `<caption>`                    |
| ARIA-Attribute      | `aria-describedby`, `aria-expanded`, `aria-hidden`, `aria-label`, `role="alert"` |
| Eindeutige IDs      | `IdGeneratorService` generiert kollisionsfreie IDs pro Instanz                   |
| Fokus-Management    | KERN-Fokus-Styles werden nie unterdrückt                                         |
| Tastatur-Navigation | Alle interaktiven Elemente sind per Tastatur bedienbar                           |
| Kontrast            | KERN-Theme erfüllt WCAG 2.1 AA Kontrastanforderungen                             |

---

## Erweiterungen gegenüber KERN-UX

Folgende Komponenten sind **Blazor-Library-Erweiterungen** und nicht Teil des offiziellen KERN-UX-Standards:

| Komponente          | Beschreibung                                                             |
|---------------------|--------------------------------------------------------------------------|
| `KernInputCurrency` | Währungseingabe optimiert für DACH (de-DE/de-CH), mit `decimal?`-Binding |

Die zugehörigen SCSS-Erweiterungen liegen in `Styles/extensions/` und werden bei KERN-UX-Updates
**nicht** überschrieben. Siehe [`Styles/extensions/README.md`](src/KernUx.Blazor/Styles/extensions/README.md).

---

## Projektstruktur

```
KernUx.Blazor/                              ← Repository-Root
├── src/
│   ├── KernUx.Blazor/                      ← Razor Class Library (NuGet-Paket)
│   │   ├── Components/
│   │   │   ├── Content/                    # Accordion, Card, Dialog, Table, Summary, TaskList
│   │   │   ├── Feedback/                   # Alert, Badge, Loader, Progress
│   │   │   ├── Forms/                      # Button, alle Input-Typen, Select, Checkbox, Radio, Fieldset
│   │   │   ├── Layout/                     # Container, Row, Col, ThemeProvider
│   │   │   ├── Navigation/                 # Kopfzeile, Link, List
│   │   │   ├── Shared/                     # Icon, Divider, KernStyles
│   │   │   └── Typography/                 # Heading, Title, Body, Label, Subline, Preline
│   │   ├── Enums/                          # Typsichere Enums für alle Parameter
│   │   ├── Extensions/                     # AddKernUx() DI-Erweiterung
│   │   ├── Services/                       # ThemeService, IdGeneratorService
│   │   ├── Styles/
│   │   │   ├── core/                       # KERN-UX SCSS (nicht bearbeiten – wird bei Updates überschrieben)
│   │   │   ├── extensions/                 # Projekt-spezifische SCSS-Ergänzungen (update-sicher)
│   │   │   └── themes/kern/                # KERN-Theme Entry Point
│   │   ├── Utilities/                      # CssBuilder, EnumCssExtensions
│   │   └── wwwroot/
│   │       ├── css/                        # Kompilierte CSS-Dateien
│   │       └── js/                         # kern-dialog.js
│   └── KernUx.Blazor.Demo/                 ← Interaktive Showcase-App
├── tests/
│   ├── KernUx.Blazor.Tests/                ← bUnit-Unit-Tests (Rendering, Interaktion, Accessibility)
│   └── KernUx.Blazor.Demo.SmokeTests/      ← Playwright-Smoke-Tests
├── docs/
│   ├── contributing/                       # Entwickler-Dokumentation
│   └── internal/                           # Interne Planungsdokumente
├── scripts/                                # PowerShell-Hilfsskripte
└── .github/
    ├── workflows/                          # CI- und Release-Workflows
    ├── ISSUE_TEMPLATE/                     # Strukturierte Issue-Formulare
    └── PULL_REQUEST_TEMPLATE.md
```

---

## Entwicklung & Tests

### Tests ausführen

```bash
dotnet test tests/KernUx.Blazor.Tests/KernUx.Blazor.Tests.csproj
```

Das Testprojekt enthält:

- **Rendering-Tests** – alle Komponenten werden auf korrekte HTML-Ausgabe geprüft
- **Interaktions-Tests** – EventCallbacks, State-Änderungen
- **Accessibility-Audit-Tests** – ARIA-Attribute, semantisches HTML

### NuGet-Paket erstellen

```bash
dotnet pack src/KernUx.Blazor/KernUx.Blazor.csproj --configuration Release
```

### SCSS-Kompilierung

SCSS wird beim Build automatisch via `AspNetCore.SassCompiler` nach `wwwroot/css/` kompiliert.

### Eigene SCSS-Erweiterungen

Neue Stile gehören nach `Styles/extensions/components/` und müssen in
`Styles/extensions/index.scss` per `@use` registriert werden.
Niemals `Styles/core/` direkt bearbeiten.

Ausführliche Anleitung: [`docs/contributing/development-setup.md`](docs/contributing/development-setup.md)

---

## Beitragen

Contributions sind willkommen! Bitte lies zuerst [CONTRIBUTING.md](CONTRIBUTING.md).

- 🐛 **Bug melden**: [Bug-Report-Vorlage](.github/ISSUE_TEMPLATE/bug_report.yml)
- ✨ **Feature vorschlagen**: [Feature-Request-Vorlage](.github/ISSUE_TEMPLATE/feature_request.yml)
- 🛡️ **Sicherheitslücke melden**: Bitte vertraulich über [SECURITY.md](SECURITY.md)

---

## Changelog

Alle Änderungen sind in [CHANGELOG.md](CHANGELOG.md) dokumentiert.

---

## Lizenz

MIT – siehe [LICENSE](LICENSE).

---

*KERN-UX ist ein offener UX-Standard für die deutsche Verwaltung.*
*Mehr Infos: [kern-ux.de](https://www.kern-ux.de) | [GitLab](https://gitlab.opencode.de/kern-ux/kern-ux-plain)*

