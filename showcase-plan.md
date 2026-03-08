# Showcase-Plan – KernUx.Blazor.Demo

## 1. Ist-Analyse

### Demo-App (aktueller Zustand)

Die `KernUx.Blazor.Demo`-App ist eine **unveränderte Blazor-Projektvorlage**:

| Aspekt                     | Ist-Zustand                                                        | Problem                                                |
|----------------------------|--------------------------------------------------------------------|--------------------------------------------------------|
| **Styling**                | Bootstrap (`bootstrap.min.css`) + `app.css`                        | Widerspricht dem Anti-Pattern „kein Bootstrap mischen" |
| **Layout**                 | Standard-Sidebar-Layout mit Bootstrap-Klassen                      | Keine KERN-UX-Komponenten im Einsatz                   |
| **Pages**                  | Home (leer), Counter (Bootstrap-Button), Weather (Bootstrap-Table) | Zeigt keine Library-Funktionalität                     |
| **Navigation**             | `NavMenu.razor` mit Bootstrap-Klassen (`navbar`, `nav-link`)       | Kein `KernKopfzeile`, kein KERN-Styling                |
| **Theming**                | Nicht vorhanden                                                    | Kein `KernThemeProvider`, kein `ThemeService`          |
| **KernUx.Blazor-Referenz** | Fehlt komplett (kein `<ProjectReference>`)                         | Library wird gar nicht eingebunden                     |
| **`AddKernUx()`**          | Nicht aufgerufen                                                   | Services nicht registriert                             |
| **`KernStyles`**           | Nicht eingebunden                                                  | KERN-CSS fehlt                                         |

### Library (KernUx.Blazor)

Die Library ist **vollständig implementiert** mit 48+ Komponenten:

- **Layout**: `KernContainer`, `KernRow`, `KernCol`, `KernThemeProvider`
- **Typografie**: `KernHeading`, `KernTitle`, `KernBody`, `KernLabel`, `KernSubline`, `KernPreline`
- **Navigation**: `KernKopfzeile`, `KernLink`, `KernList`
- **Shared**: `KernIcon`, `KernDivider`, `KernStyles`
- **Forms**: `KernButton`, `KernInputText`, `KernInputEmail`, `KernInputPassword`, `KernInputTel`, `KernInputUrl`,
  `KernInputNumber`, `KernInputDate`, `KernInputFile`, `KernInputCurrency`, `KernTextarea`, `KernSelect`,
  `KernCheckbox`, `KernCheckboxList`, `KernRadioGroup`, `KernFieldset`
- **Feedback**: `KernAlert`, `KernBadge`, `KernLoader`, `KernProgress`
- **Content**: `KernAccordion`, `KernAccordionGroup`, `KernCard`, `KernCardMedia`, `KernCardContainer`, `KernDialog`,
  `KernTable`, `KernDescriptionList`, `KernDescriptionItem`, `KernSummary`, `KernSummaryGroup`, `KernTaskList`,
  `KernTaskListGroup`, `KernTaskListItem`
- **Services**: `ThemeService`, `IdGeneratorService`
- **Enums**: Vollständig typisiert (`ButtonVariant`, `AlertType`, `BadgeVariant`, `KernIconGlyph`, `HeadingLevel`,
  `Size`, etc.)

---

## 2. Ziele des Showcase

| Ziel                     | Beschreibung                                                                      |
|--------------------------|-----------------------------------------------------------------------------------|
| **Vollständigkeit**      | Jede Komponente der Library mindestens einmal in Aktion zeigen                    |
| **Praxisnähe**           | Reale Szenarien aus der Verwaltung demonstrieren (Formulare, Anträge, Dashboards) |
| **Selbst-Dokumentation** | Die Demo dient als interaktive API-Dokumentation – Parameter live variierbar      |
| **KERN-Konformität**     | 100 % KERN-UX, null Bootstrap – die Demo selbst ist ein Referenzbeispiel          |
| **Barrierefreiheit**     | Vorzeige-Implementierung: semantisches HTML, ARIA, Tastaturnavigation             |
| **Theming**              | Light/Dark-Wechsel über ThemeService live demonstrieren                           |

---

## 3. Informationsarchitektur

### Seitenstruktur

```
/                          → Startseite (Überblick + Schnellstart)
/components/typography     → Typografie-Showcase
/components/layout         → Grid & Layout
/components/buttons        → Button-Varianten
/components/forms          → Formular-Elemente
/components/feedback       → Alert, Badge, Loader, Progress
/components/content        → Accordion, Card, Dialog, Table, Summary, TaskList
/components/navigation     → Kopfzeile, Link, List
/components/icons          → Icon-Galerie (alle Glyphen)
/examples/antrag           → Praxisbeispiel: Mehrstufiger Antrag
/examples/dashboard        → Praxisbeispiel: Status-Dashboard
```

### Navigation

Die Demo bekommt eine **eigene KERN-basierte Navigation** (kein Bootstrap-Sidebar):

- `KernKopfzeile` als App-Header
- Horizontale oder vertikale Navigation mit `KernLink`-Komponenten
- Theme-Toggle in der Kopfzeile
- Breadcrumb oder aktive Markierung der aktuellen Seite

---

## 4. Änderungen im Detail

### 4.1 `KernUx.Blazor.Demo.csproj` – Projektdatei

**Erforderliche Änderungen:**

```xml
<!-- NEU: Projektreferenz auf die Library -->
<ItemGroup>
  <ProjectReference Include="..\KernUx.Blazor\KernUx.Blazor.csproj" />
</ItemGroup>
```

Bootstrap-Referenz wird nicht im `.csproj` entfernt, sondern in den Razor-/CSS-Dateien (siehe unten).

---

### 4.2 `Program.cs` – Service-Registrierung

**Erforderliche Änderung:**

```csharp
using KernUx.Blazor.Extensions;

// Nach AddRazorComponents():
builder.Services.AddKernUx();
```

---

### 4.3 `Components/App.razor` – Root-Layout

**Zu entfernen:**

- Bootstrap-CSS-Referenz (`lib/bootstrap/dist/css/bootstrap.min.css`)
- `app.css` (wird durch KERN-Styling ersetzt oder geleert)

**Hinzuzufügen:**

- `<KernStyles />` (KERN-CSS)
- Anti-FOUC-Script für Theme-Initialisierung (cookie-basiert)
- `kern-dialog.js`-Referenz

**Ergebnis:**

```razor
<!DOCTYPE html>
<html lang="de">
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <base href="/" />
    <ResourcePreloader />
    <KernStyles />
    <link rel="stylesheet" href="@Assets["KernUx.Blazor.Demo.styles.css"]" />
    <ImportMap />
    <link rel="icon" type="image/png" href="favicon.png" />
    <HeadOutlet />
</head>
<body>
    <script>/* Anti-FOUC: Theme aus Cookie laden */</script>
    <Routes />
    <script src="_content/KernUx.Blazor/js/kern-dialog.js"></script>
    <script src="@Assets["_framework/blazor.web.js"]"></script>
</body>
</html>
```

---

### 4.4 `Components/_Imports.razor` – Namespace-Importe

**Hinzuzufügen:**

```razor
@using KernUx.Blazor.Components
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

### 4.5 `Components/Layout/MainLayout.razor` – Neues KERN-Layout

**Komplett ersetzen.** Das Bootstrap-Sidebar-Layout wird durch ein KERN-basiertes Layout ersetzt:

```razor
@inherits LayoutComponentBase
@inject ThemeService ThemeService

<KernThemeProvider Theme="KernTheme.Light">
    <KernKopfzeile />
    
    <header class="demo-header">
        <KernContainer>
            <KernRow JustifyContent="JustifyContent.Between" AlignItems="AlignItems.Center">
                <KernCol>
                    <KernTitle Size="TitleSize.Large">KernUx.Blazor – Komponentenbibliothek</KernTitle>
                </KernCol>
                <KernCol>
                    <KernButton Variant="ButtonVariant.Tertiary"
                                Icon="KernIconGlyph.BrightnessMedium"
                                IconOnly="true"
                                Title="Theme wechseln"
                                OnClick="ThemeService.Toggle">
                        Theme wechseln
                    </KernButton>
                </KernCol>
            </KernRow>
        </KernContainer>
    </header>
    
    <KernContainer>
        <KernRow>
            <KernCol Span="3">
                <DemoNavMenu />
            </KernCol>
            <KernCol Span="9">
                <main>
                    @Body
                </main>
            </KernCol>
        </KernRow>
    </KernContainer>
</KernThemeProvider>
```

---

### 4.6 `Components/Layout/NavMenu.razor` – Neue KERN-Navigation

**Komplett ersetzen.** Bootstrap-Navigation wird durch KERN-Links ersetzt:

```razor
<nav aria-label="Komponentennavigation">
    <KernTitle Size="TitleSize.Small">Komponenten</KernTitle>
    <KernList>
        <li><KernLink Href="/components/typography">Typografie</KernLink></li>
        <li><KernLink Href="/components/layout">Layout & Grid</KernLink></li>
        <li><KernLink Href="/components/buttons">Buttons</KernLink></li>
        <li><KernLink Href="/components/forms">Formularelemente</KernLink></li>
        <li><KernLink Href="/components/feedback">Feedback</KernLink></li>
        <li><KernLink Href="/components/content">Content</KernLink></li>
        <li><KernLink Href="/components/navigation">Navigation</KernLink></li>
        <li><KernLink Href="/components/icons">Icons</KernLink></li>
    </KernList>
    <KernDivider />
    <KernTitle Size="TitleSize.Small">Praxisbeispiele</KernTitle>
    <KernList>
        <li><KernLink Href="/examples/antrag">Antragsstrecke</KernLink></li>
        <li><KernLink Href="/examples/dashboard">Dashboard</KernLink></li>
    </KernList>
</nav>
```

---

### 4.7 `Components/Layout/NavMenu.razor.css` – Entfernen

Die Bootstrap-spezifische CSS-Datei wird gelöscht oder geleert, da ausschließlich KERN-Klassen genutzt werden.

---

### 4.8 `Components/Layout/MainLayout.razor.css` – Entfernen/Ersetzen

Die Bootstrap-spezifische CSS-Datei wird gelöscht oder durch minimales KERN-kompatibles CSS ersetzt.

---

### 4.9 `wwwroot/app.css` – Leeren oder Entfernen

Die Bootstrap-abhängige `app.css` wird geleert. Falls Demo-spezifische Styles nötig sind, werden diese mit `kern-`
-kompatiblen Regeln geschrieben.

---

### 4.10 `wwwroot/lib/` – Bootstrap entfernen

Das gesamte `lib/bootstrap/`-Verzeichnis wird gelöscht.

---

## 5. Neue Dateien: Showcase-Pages

### 5.1 Startseite – `Components/Pages/Home.razor`

**Inhalt:** Willkommensseite mit Projektbeschreibung und Schnellstart.

**Demonstrierte Komponenten:**

- `KernHeading` (Display + verschiedene Level)
- `KernBody` (Default, Bold, Muted)
- `KernAlert` (Info: „Dies ist die Showcase-App")
- `KernButton` (Link zu Schnellstart-Doku)
- `KernCard` (3 Feature-Cards: Barrierefreiheit, Typsicherheit, Theming)
- `KernDivider`

**Sektionen:**

1. Hero-Bereich mit `KernHeading` Level=Display
2. Feature-Cards (3er-Grid) mit `KernCard`
3. Schnellstart-Code-Snippet (als Text, kein Prism/Highlight)
4. Link zu KERN-UX-Website

---

### 5.2 Typografie – `Components/Pages/TypographyShowcase.razor`

**Route:** `/components/typography`

**Demonstrierte Komponenten:**

- `KernHeading` – alle 5 Levels (Display, XLarge, Large, Medium, Small) + semantisches Element (h1–h6)
- `KernTitle` – Small, Default, Large
- `KernBody` – Default, Bold, Muted, Small, Large
- `KernLabel` – mit/ohne Optional-Marker
- `KernPreline` – alle Größen
- `KernSubline` – alle Größen

**Aufbau:** Jede Komponente in einer eigenen Sektion mit:

- Komponentenname als `KernTitle`
- Alle Varianten nebeneinander/untereinander
- Kurze Parameterbeschreibung als `KernBody`

---

### 5.3 Layout & Grid – `Components/Pages/LayoutShowcase.razor`

**Route:** `/components/layout`

**Demonstrierte Komponenten:**

- `KernContainer` (Default vs. Fluid)
- `KernRow` (JustifyContent-Varianten, AlignItems-Varianten)
- `KernCol` (Span 1–12, responsive Breakpoints: SpanSm, SpanMd, SpanLg, SpanXl, SpanXxl)
- `KernCol` mit Offset
- `KernDivider`

**Aufbau:**

1. Basis-Grid (12 Spalten visualisiert mit farbigen Boxen)
2. Responsive Grid (verschiedene Breakpoints)
3. Alignment-Beispiele (justify-content, align-items)
4. Verschachtelung (Row in Col)

---

### 5.4 Buttons – `Components/Pages/ButtonShowcase.razor`

**Route:** `/components/buttons`

**Demonstrierte Komponenten:**

- `KernButton` – Primary, Secondary, Tertiary
- `KernButton` – mit Icon (Left, Right)
- `KernButton` – IconOnly
- `KernButton` – Block
- `KernButton` – Disabled
- `KernButton` – als Submit-Button (`Type="submit"`)

**Aufbau:**

1. Varianten-Reihe (Primary, Secondary, Tertiary nebeneinander)
2. Icon-Varianten
3. Zustände (Disabled, Block)
4. Interaktiver Zähler (demonstriert `OnClick`-Binding)

---

### 5.5 Formularelemente – `Components/Pages/FormsShowcase.razor`

**Route:** `/components/forms`

**Demonstrierte Komponenten:**

- `KernInputText` – Standard, mit Hint, mit Error, Optional, Disabled, ReadOnly
- `KernInputEmail`, `KernInputPassword`, `KernInputTel`, `KernInputUrl`
- `KernInputNumber`, `KernInputCurrency`
- `KernInputDate` (Tag/Monat/Jahr)
- `KernInputFile`
- `KernTextarea`
- `KernSelect` (mit KernSelectOption-Liste)
- `KernCheckbox` (einzeln)
- `KernCheckboxList` (Gruppe)
- `KernRadioGroup`
- `KernFieldset` (horizontal + vertikal)
- `KernFormField` (als generisches Wrapper-Beispiel)

**Aufbau:**

1. Übersicht aller Input-Typen (je ein Beispiel)
2. Fehlerzustände (Error-Meldungen sichtbar)
3. Two-Way-Binding-Demo (Wert-Anzeige neben Input)
4. Vollständiges Formular-Beispiel (z.B. „Kontaktformular")

---

### 5.6 Feedback – `Components/Pages/FeedbackShowcase.razor`

**Route:** `/components/feedback`

**Demonstrierte Komponenten:**

- `KernAlert` – Info, Success, Warning, Danger + mit/ohne Titel + mit/ohne Body
- `KernBadge` – Info, Success, Warning, Danger + mit Icon
- `KernLoader` – sichtbar/unsichtbar (Toggle-Demo)
- `KernProgress` – statisch + animiert (Timer-basiert), Label oben/unten

**Aufbau:**

1. Alert-Galerie (alle 4 Typen)
2. Badge-Reihe (alle 4 Varianten, mit/ohne Icon)
3. Loader-Toggle (Button zeigt/versteckt Loader)
4. Progress-Animation (Slider oder Auto-Increment)

---

### 5.7 Content – `Components/Pages/ContentShowcase.razor`

**Route:** `/components/content`

**Demonstrierte Komponenten:**

- `KernAccordion` + `KernAccordionGroup`
- `KernCard` + `KernCardMedia` + `KernCardContainer` (Small, Default, Large, Hug, Active)
- `KernDialog` (mit Open/Close-Interaktion)
- `KernTable` (Standard, Small, Striped, Responsive, mit Caption)
- `KernDescriptionList` + `KernDescriptionItem` (Row-Layout, Column-Layout)
- `KernSummary` + `KernSummaryGroup` (mit Edit-Link)
- `KernTaskList` + `KernTaskListGroup` + `KernTaskListItem` (mit Badge-Status)

**Aufbau:**

1. Accordion-Beispiel (FAQ-Stil, einzeln + Gruppe)
2. Card-Grid (3–4 Cards in verschiedenen Größen, eine mit `Active`)
3. Dialog-Demo (Button öffnet Dialog, Schließen-Interaktion)
4. Tabelle mit Beispieldaten (Wetterdaten o.ä.)
5. Description List (2 Layouts)
6. Summary-Gruppe (mehrstufiger Antrag)
7. Task List (Aufgaben-Übersicht mit Status-Badges)

---

### 5.8 Navigation – `Components/Pages/NavigationShowcase.razor`

**Route:** `/components/navigation`

**Demonstrierte Komponenten:**

- `KernKopfzeile` (Default + Fluid + Custom-Label)
- `KernLink` (Standard, Stretched, mit Target)
- `KernList` (Default, Bullet, Number)

---

### 5.9 Icon-Galerie – `Components/Pages/IconShowcase.razor`

**Route:** `/components/icons`

**Demonstrierte Komponenten:**

- `KernIcon` – alle Glyphen aus `KernIconGlyph`-Enum
- Größenvarianten (Small, Default, Large, XLarge)

**Aufbau:**

1. Grid mit allen Icons (automatisch generiert aus Enum-Werten per `Enum.GetValues<KernIconGlyph>()`)
2. Jedes Icon mit Name darunter
3. Größen-Vergleich (ein Icon in allen 4 Größen)

---

### 5.10 Praxisbeispiel: Antragsstrecke – `Components/Pages/AntragExample.razor`

**Route:** `/examples/antrag`

**Szenario:** Mehrstufiger Online-Antrag (z.B. „Bürgergeld beantragen") – zeigt, wie die Komponenten in einem
realistischen Verwaltungs-Workflow zusammenspielen.

**Ablauf (3–4 Schritte):**

1. **Schritt 1 – Persönliche Daten**
    - `KernFieldset` mit `KernInputText` (Vorname, Nachname)
    - `KernInputEmail`, `KernInputTel`
    - `KernInputDate` (Geburtsdatum)
    - `KernRadioGroup` (Anrede)
    - Validierungs-Fehler mit `Error`-Parameter
    - `KernButton` (Primary: „Weiter", Secondary: „Abbrechen")

2. **Schritt 2 – Adressdaten**
    - `KernInputText` (Straße, Hausnummer, PLZ, Ort)
    - `KernSelect` (Bundesland)
    - `KernCheckbox` („Ich habe eine abweichende Postadresse")
    - Conditional: Zweites Adress-Fieldset

3. **Schritt 3 – Zusammenfassung**
    - `KernSummaryGroup` mit je einem `KernSummary` pro Schritt
    - `KernDescriptionList` in jedem Summary
    - Edit-Links zurück zu den Schritten
    - `KernCheckbox` („Ich bestätige die Richtigkeit der Angaben")
    - `KernButton` (Primary: „Antrag absenden")

4. **Bestätigung**
    - `KernAlert` (Success: „Ihr Antrag wurde eingereicht")
    - `KernTaskList` mit Status-Badges (nächste Schritte)

**Demonstrierte Patterns:**

- Two-Way-Binding über mehrere Schritte
- Formular-Validierung
- State-Management mit `@code`-Block
- Bedingte Darstellung
- Summary/Review-Pattern

---

### 5.11 Praxisbeispiel: Dashboard – `Components/Pages/DashboardExample.razor`

**Route:** `/examples/dashboard`

**Szenario:** Sachbearbeiter-Dashboard mit Aufgabenübersicht – zeigt Datenvisualisierung und Status-Tracking.

**Sektionen:**

1. **Status-Karten** (3er-Grid mit `KernCard`)
    - „12 Offene Anträge" (`KernBadge` Warning)
    - „5 In Bearbeitung" (`KernBadge` Info)
    - „87 Abgeschlossen" (`KernBadge` Success)

2. **Aufgabenliste** (`KernTaskList`)
    - Gruppiert nach Priorität (`KernTaskListGroup`)
    - Items mit `KernBadge`-Status (Offen, In Bearbeitung, Erledigt)

3. **Letzte Aktivitäten** (`KernTable`)
    - Responsive Tabelle mit Datum, Antragsteller, Aktion, Status
    - Striped + Small

4. **Fortschritt** (`KernProgress`)
    - Quartals-Zielerreichung

5. **Accordion: FAQ / Hilfe** (`KernAccordionGroup`)
    - 3 häufige Fragen als Accordion-Items

---

## 6. Zusätzliche Demo-Hilfskomponenten

### 6.1 `Components/Layout/DemoNavMenu.razor` (NEU)

Eigenständige KERN-basierte Seitennavigation (ersetzt die Bootstrap-`NavMenu`).

### 6.2 `Components/Shared/ComponentSection.razor` (NEU)

Wiederverwendbare Wrapper-Komponente für Showcase-Sektionen:

```razor
@* Einheitliche Darstellung: Titel + Beschreibung + Komponenten-Demo *@
<section>
    <KernTitle>@Title</KernTitle>
    @if (Description is not null)
    {
        <KernBody Modifier="BodyModifier.Muted">@Description</KernBody>
    }
    <KernDivider />
    @ChildContent
</section>
```

**Parameter:** `Title`, `Description` (optional), `ChildContent`

### 6.3 `Components/Shared/VariantRow.razor` (NEU)

Komponente für nebeneinander dargestellte Varianten (z.B. 3 Button-Varianten in einer Row):

```razor
<KernRow AlignItems="AlignItems.Center" class="demo-variant-row">
    @ChildContent
</KernRow>
```

---

## 7. Datei-Übersicht: Zu erstellen / zu ändern

### Zu ändernde Dateien

| Datei                                    | Art der Änderung                                  |
|------------------------------------------|---------------------------------------------------|
| `KernUx.Blazor.Demo.csproj`              | `<ProjectReference>` auf Library hinzufügen       |
| `Program.cs`                             | `AddKernUx()` hinzufügen                          |
| `Components/App.razor`                   | Bootstrap → KERN-CSS, Anti-FOUC, `kern-dialog.js` |
| `Components/_Imports.razor`              | KernUx.Blazor-Namespaces hinzufügen               |
| `Components/Layout/MainLayout.razor`     | Komplett auf KERN-Layout umstellen                |
| `Components/Layout/NavMenu.razor`        | Entfernen (wird durch `DemoNavMenu` ersetzt)      |
| `Components/Layout/NavMenu.razor.css`    | Entfernen                                         |
| `Components/Layout/MainLayout.razor.css` | Entfernen oder leeren                             |
| `Components/Pages/Home.razor`            | Willkommensseite mit Feature-Cards                |
| `Components/Pages/Counter.razor`         | Entfernen (wird durch ButtonShowcase ersetzt)     |
| `Components/Pages/Weather.razor`         | Entfernen (wird durch ContentShowcase ersetzt)    |
| `Components/Pages/NotFound.razor`        | Auf KERN-Komponenten umstellen                    |
| `Components/Pages/Error.razor`           | Auf KERN-Komponenten umstellen                    |
| `wwwroot/app.css`                        | Leeren (nur Demo-spezifisches Minimal-CSS)        |
| `wwwroot/lib/`                           | Bootstrap-Verzeichnis entfernen                   |

### Neu zu erstellende Dateien

| Datei                                       | Beschreibung                                      |
|---------------------------------------------|---------------------------------------------------|
| `Components/Layout/DemoNavMenu.razor`       | KERN-basierte Seitennavigation                    |
| `Components/Shared/ComponentSection.razor`  | Wiederverwendbarer Showcase-Abschnitt             |
| `Components/Shared/VariantRow.razor`        | Varianten-Darstellung                             |
| `Components/Pages/TypographyShowcase.razor` | Typografie-Showcase                               |
| `Components/Pages/LayoutShowcase.razor`     | Grid-/Layout-Showcase                             |
| `Components/Pages/ButtonShowcase.razor`     | Button-Showcase                                   |
| `Components/Pages/FormsShowcase.razor`      | Formular-Showcase                                 |
| `Components/Pages/FeedbackShowcase.razor`   | Feedback-Showcase                                 |
| `Components/Pages/ContentShowcase.razor`    | Accordion, Card, Dialog, Table, Summary, TaskList | 
| `Components/Pages/NavigationShowcase.razor` | Navigation-Showcase                               |
| `Components/Pages/IconShowcase.razor`       | Icon-Galerie                                      |
| `Components/Pages/AntragExample.razor`      | Praxisbeispiel Antrag                             |
| `Components/Pages/DashboardExample.razor`   | Praxisbeispiel Dashboard                          |

---

## 8. Implementierungs-Reihenfolge

### Phase A – Grundgerüst (Pflicht zuerst)

1. **`KernUx.Blazor.Demo.csproj`**: ProjectReference hinzufügen
2. **`Program.cs`**: `AddKernUx()` aufrufen
3. **`_Imports.razor`**: Namespaces ergänzen
4. **`App.razor`**: Bootstrap entfernen, KERN einbinden
5. **`MainLayout.razor`**: KERN-Layout mit `KernThemeProvider`, `KernKopfzeile`, Grid
6. **`DemoNavMenu.razor`**: KERN-basierte Navigation erstellen
7. **Bootstrap entfernen**: `wwwroot/lib/`, `app.css` leeren, `NavMenu.razor.css`/`MainLayout.razor.css` entfernen
8. **`Home.razor`**: Willkommensseite

→ **Ergebnis:** Lauffähige KERN-App ohne Bootstrap

### Phase B – Komponenten-Showcase-Pages

9. **`ComponentSection.razor`** + **`VariantRow.razor`**: Hilfskomponenten
10. **`TypographyShowcase.razor`**: Typografie
11. **`LayoutShowcase.razor`**: Grid
12. **`ButtonShowcase.razor`**: Buttons
13. **`IconShowcase.razor`**: Icon-Galerie
14. **`FeedbackShowcase.razor`**: Alerts, Badges, Loader, Progress
15. **`FormsShowcase.razor`**: Alle Formularelemente
16. **`ContentShowcase.razor`**: Accordion, Card, Dialog, Table, Summary, TaskList
17. **`NavigationShowcase.razor`**: Kopfzeile, Link, List

→ **Ergebnis:** Vollständiger Komponenten-Katalog

### Phase C – Praxisbeispiele

18. **`AntragExample.razor`**: Mehrstufiger Antrag
19. **`DashboardExample.razor`**: Status-Dashboard

→ **Ergebnis:** Praxisnahe Demonstrations-Szenarien

### Phase D – Polish

20. **`NotFound.razor`** + **`Error.razor`**: Auf KERN umstellen
21. **Demo-spezifisches CSS** in `app.css` (z.B. Grid-Visualisierung mit Hintergrundfarben)
22. **PageTitle** auf jeder Seite setzen
23. **Smoke-Test**: Alle Seiten durchklicken, Theme-Toggle, Responsive-Test

→ **Ergebnis:** Abgerundete, produktionsnahe Showcase-App

### Phase E (optional) – Erweiterte Praxisbeispiele

24. **`FormWizardExample.razor`**: Formular-Wizard mit Schritt-Navigation, Fortschrittsanzeige und Zwischenspeicherung
25. **`DataTableFilterExample.razor`**: Datentabelle mit Filterung, Sortierung und leerem Zustand
26. **Navigation erweitern**: Links auf die optionalen Beispiele in `DemoNavMenu.razor`
27. **Dokumentation ergänzen**: README um die neuen Beispielseiten und Anwendungsfälle erweitern

→ **Ergebnis:** Vertiefte Praxisabdeckung für komplexe Interaktionsmuster

---

## 9. Interaktivitäts-Konzept

Da alle interaktiven Demos `@rendermode InteractiveServer` benötigen, wird dies auf den relevanten Pages gesetzt:

| Page       | RenderMode        | Begründung                          |
|------------|-------------------|-------------------------------------|
| Home       | Static SSR        | Nur Darstellung                     |
| Typography | Static SSR        | Nur Darstellung                     |
| Layout     | Static SSR        | Nur Darstellung                     |
| Icons      | Static SSR        | Nur Darstellung                     |
| Navigation | Static SSR        | Nur Darstellung                     |
| Buttons    | InteractiveServer | Click-Events, Zähler                |
| Forms      | InteractiveServer | Two-Way-Binding, Validierung        |
| Feedback   | InteractiveServer | Loader-Toggle, Progress-Animation   |
| Content    | InteractiveServer | Dialog-Open/Close, Accordion-Toggle |
| Antrag     | InteractiveServer | Multi-Step-Formular                 |
| Dashboard  | InteractiveServer | Live-Interaktion                    |

---

## 10. Qualitätskriterien für den Showcase

| Kriterium                 | Prüfung                                          |
|---------------------------|--------------------------------------------------|
| **Kein Bootstrap**        | Keine Bootstrap-Klasse im gesamten HTML-Output   |
| **100 % KERN-Klassen**    | Alle UI-Elemente nutzen `kern-`-Präfix           |
| **Barrierefreiheit**      | Alle Seiten mit Tastatur bedienbar, ARIA korrekt |
| **Theme-Wechsel**         | Light ↔ Dark funktioniert auf jeder Seite        |
| **Responsive**            | Layout funktioniert auf Desktop, Tablet, Mobil   |
| **Alle Komponenten**      | Jede Library-Komponente mindestens 1× sichtbar   |
| **Interaktion**           | Events (Click, Change, Toggle) live demonstriert |
| **Keine JS-Abhängigkeit** | Einziges JS: `kern-dialog.js` + Anti-FOUC        |
| **Kompiliert fehlerfrei** | `dotnet build` ohne Errors/Warnings              |
