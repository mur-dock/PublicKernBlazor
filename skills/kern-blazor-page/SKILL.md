---
name: kern-blazor-page
description: >
  Implementiert Blazor-Pages und -Komponenten mit der PublicKernBlazor.Components-Bibliothek
  (KERN Design System). Verwende diesen Skill, wenn du eine neue Blazor-Page,
  eine neue Komponente oder ein Layout mit KERN-UX-Komponenten erstellen,
  erweitern oder überarbeiten sollst – inklusive Projekteinbindung,
  Namespaces, Enums, Formular-Pattern, Barrierefreiheit und Theming.
---

# Skill: Blazor-Pages mit PublicKernBlazor.Components implementieren

Dieser Skill beschreibt, wie Blazor-Pages und -Komponenten unter Verwendung
der `PublicKernBlazor.Components`-Bibliothek korrekt implementiert werden.

---

## 1 · Projekteinrichtung

### 1.1 NuGet-Referenz / Projektreferenz

**NuGet-Paket** (konsumierendes Projekt):

```xml
<PackageReference Include="PublicKernBlazor.Components" Version="..." />
```

**Projektreferenz** (innerhalb der Solution):

```xml
<ProjectReference Include="..\PublicKernBlazor.Components\PublicKernBlazor.Components.csproj" />
```

### 1.2 Services registrieren (`Program.cs`)

```csharp
using PublicKernBlazor.Components.Extensions;

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registriert ThemeService (Scoped) und IdGeneratorService (Scoped)
builder.Services.AddKernUx();
```

### 1.3 Stylesheets einbinden (`App.razor` / Root-HTML)

Füge `<KernStyles />` im `<head>` ein – die Komponente schreibt die CSS-Links
selbst via `<HeadContent>`:

```razor
<head>
    ...
    <KernStyles />
    <link rel="stylesheet" href="@Assets["MeinProjekt.styles.css"]" />
</head>
```

`KernStyles` akzeptiert optionale Parameter:

| Parameter           | Standard               | Beschreibung                                   |
|---------------------|------------------------|------------------------------------------------|
| `BasePath`          | `_content/PublicKernBlazor.Components` | Pfad zu den statischen Assets               |
| `ThemeName`         | `kern`                 | Name des aktiven Themes                        |
| `IncludeExtensions` | `true`                 | Extensions-Stylesheet (`css/extensions/`) einbinden |

### 1.4 Anti-FOUC-Script (Theme-Initialisierung, `App.razor`)

Das einzige erlaubte JavaScript-Snippet verhindert einen Theme-Flicker beim
ersten Laden und stellt nach Enhanced-Navigation das Theme wieder her:

```html
<script>
(() => {
    const readTheme = () => {
        const match = document.cookie.match(/(?:^|;\s*)kern-theme=(light|dark)/);
        return match ? match[1] : "light";
    };
    const applyTheme = (theme) => {
        const resolved = theme === "dark" ? "dark" : "light";
        document.documentElement.setAttribute("data-kern-theme", resolved);
        document.cookie = `kern-theme=${resolved}; path=/; max-age=31536000; samesite=lax`;
        return resolved;
    };
    applyTheme(readTheme());
    window.kernTheme = {
        read:   () => readTheme(),
        set:    (t) => applyTheme(t),
        toggle: () => {
            const cur = document.documentElement.getAttribute("data-kern-theme") || readTheme();
            return applyTheme(cur === "dark" ? "light" : "dark");
        }
    };
    document.addEventListener("blazor:after-enhanced-nav", () => applyTheme(readTheme()));
})();
</script>
```

---

## 2 · Namespaces & Imports

Füge in `Components/_Imports.razor` alle KERN-Namespaces ein:

```razor
@using PublicKernBlazor.Components.Components
@using PublicKernBlazor.Components.Components.Content
@using PublicKernBlazor.Components.Components.Feedback
@using PublicKernBlazor.Components.Components.Forms
@using PublicKernBlazor.Components.Components.Layout
@using PublicKernBlazor.Components.Components.Navigation
@using PublicKernBlazor.Components.Components.Shared
@using PublicKernBlazor.Components.Components.Typography
@using PublicKernBlazor.Components.Enums
@using PublicKernBlazor.Components.Services
```

---

## 3 · Komponenten-Referenz

### 3.1 Layout

```razor
<!-- Feste Breite -->
<KernContainer>...</KernContainer>

<!-- Volle Breite -->
<KernContainer Fluid="true">...</KernContainer>

<!-- 12-Spalten-Grid -->
<KernRow JustifyContent="JustifyContent.Between" AlignItems="AlignItems.Center">
    <KernCol Span="12" SpanMd="6" SpanLg="4" Offset="0" AlignSelf="AlignSelf.Start">
        Inhalt
    </KernCol>
</KernRow>

<KernDivider />
```

**Enums Layout:**
- `JustifyContent`: `Start | Center | End | Around | Between | Evenly`
- `AlignItems`: `Start | Center | End`
- `AlignSelf`: `Start | Center | End`

> ⚠️ Kein BEM-Modifier für Container: `kern-container` und `kern-container-fluid`
> sind eigenständige Klassen – **nicht** `kern-container--fluid`.

### 3.2 Typografie

```razor
<KernHeading Level="HeadingLevel.Large" Element="h1">Seitenüberschrift</KernHeading>
<KernTitle Size="TitleSize.Default">Abschnittstitel</KernTitle>
<KernBody Muted="true">Einleitungstext</KernBody>
<KernBody Bold="true" Size="Size.Small">Hinweis klein fett</KernBody>
<KernLabel Optional="true">Feldbezeichnung</KernLabel>
<KernPreline>Vortext</KernPreline>
<KernSubline>Untertext</KernSubline>
```

**Enums Typografie:**
- `HeadingLevel`: `Display | XLarge | Large | Medium | Small`
- `TitleSize`: `Small | Default | Large`
- `Size`: `Small | Default | Large`
- `BodyModifier`: `Default | Bold | Muted | Small | Large`

### 3.3 Formular-Komponenten

```razor
<KernInputText    Label="Name"        Value="@_name"    ValueChanged="@(v => _name = v)"
                  Hint="Vollständigen Namen eingeben."  Error="@GetError()" />
<KernInputEmail   Label="E-Mail"      Value="@_email"   ValueChanged="@(v => _email = v)" />
<KernInputPassword Label="Passwort"   Value="@_pw"      ValueChanged="@(v => _pw = v)"    Optional="true" />
<KernInputTel     Label="Telefon"     Value="@_tel"     ValueChanged="@(v => _tel = v)" />
<KernInputUrl     Label="Webseite"    Value="@_url"     ValueChanged="@(v => _url = v)"   Optional="true" />
<KernInputNumber  Label="Menge"       Value="@_menge"   ValueChanged="@(v => _menge = v)" />
<KernInputCurrency Label="Betrag"     Value="@_betrag"  ValueChanged="@(v => _betrag = v)"
                   Hint="Deutsches Zahlenformat." />
<KernInputDate    Label="Datum"
                  Day="@_tag"   DayChanged="@(v => _tag = v)"
                  Month="@_monat" MonthChanged="@(v => _monat = v)"
                  Year="@_jahr" YearChanged="@(v => _jahr = v)"
                  Error="@GetError()" />
<KernInputFile    Label="Datei hochladen" Accept=".pdf,.png" OnFileSelected="HandleFile" />
<KernTextarea     Label="Bemerkung"   Value="@_notiz"   ValueChanged="@(v => _notiz = v)" Rows="5" />

<KernSelect Label="Auswahl" Value="@_auswahl" ValueChanged="@(v => _auswahl = v)" Options="Optionen" />
<!-- KernSelectOption-Record: new KernSelectOption("wert", "Anzeigetext") -->

<KernCheckbox Label="Ich akzeptiere die Bedingungen"
              Checked="@_akzeptiert" CheckedChanged="@(v => _akzeptiert = v)" />

<KernCheckboxList Legend="Schwerpunkte" Items="FokusOptionen"
                  SelectedValues="@_ausgewählt" SelectedValuesChanged="OnAuswahl" />
<!-- KernCheckboxItem-Record: new KernCheckboxItem("wert", "Anzeigetext") -->

<KernRadioGroup Legend="Kontaktweg" Items="KontaktOptionen"
                Value="@_kontakt" ValueChanged="@(v => _kontakt = v)" Horizontal="true" />
<!-- KernRadioItem-Record: new KernRadioItem("wert", "Anzeigetext") -->

<KernFieldset Legend="Einstellungen" Hint="Optionale Hinweistexte">
    <KernCheckbox ... />
</KernFieldset>
```

**Gemeinsame Parameter aller Eingabe-Komponenten:**

| Parameter   | Typ      | Beschreibung                              |
|-------------|----------|-------------------------------------------|
| `Label`     | `string` | Pflichtfeld-Beschriftung                  |
| `Hint`      | `string?` | Optionaler Hinweistext unterhalb          |
| `Error`     | `string?` | Fehlermeldung (aktiviert Fehlerzustand)   |
| `Optional`  | `bool`   | Kennzeichnet optionale Felder             |
| `Disabled`  | `bool`   | Deaktiviert das Eingabefeld               |

### 3.4 Schaltflächen

```razor
<KernButton Variant="ButtonVariant.Primary"   OnClick="Speichern">Speichern</KernButton>
<KernButton Variant="ButtonVariant.Secondary" OnClick="Abbrechen">Abbrechen</KernButton>
<KernButton Variant="ButtonVariant.Tertiary"  Icon="KernIconGlyph.Settings" IconOnly="true"
            AriaLabel="Einstellungen" Title="Einstellungen öffnen" />
<KernButton Block="true" Disabled="true">Gesperrt</KernButton>
```

**`ButtonVariant`:** `Primary | Secondary | Tertiary`

`OnClick` erhält `MouseEventArgs` – Signatur: `Task HandleClick(MouseEventArgs _)`.

### 3.5 Feedback-Komponenten

```razor
<KernAlert Type="AlertType.Info"    Title="Information">Sachverhalt gespeichert.</KernAlert>
<KernAlert Type="AlertType.Success" Title="Erfolg">Antrag übermittelt.</KernAlert>
<KernAlert Type="AlertType.Warning" Title="Hinweis">Bitte Pflichtfelder prüfen.</KernAlert>
<KernAlert Type="AlertType.Danger"  Title="Fehler">Speichern fehlgeschlagen.</KernAlert>

<KernBadge Variant="BadgeVariant.Success" Icon="KernIconGlyph.Success">Abgeschlossen</KernBadge>

<KernLoader Visible="_ladeVorgang" ScreenReaderText="Daten werden geladen" />

<KernProgress Value="_fortschritt" Max="100" Label="Bearbeitungsstand" LabelBelow="true" />
```

**`AlertType`:** `Info | Success | Warning | Danger`  
**`BadgeVariant`:** `Info | Success | Warning | Danger`

### 3.6 Content-Komponenten

```razor
<!-- Accordion -->
<KernAccordion Title="FAQ-Frage?" Open="true">Antworttext</KernAccordion>
<KernAccordionGroup>
    <KernAccordion Title="Frage 1">...</KernAccordion>
    <KernAccordion Title="Frage 2">...</KernAccordion>
</KernAccordionGroup>

<!-- Card -->
<KernCard Size="CardSize.Default" Active="false">
    <KernCardMedia><!-- Bildbereich --></KernCardMedia>
    <KernCardContainer Title="Titel" Preline="Vor-Text" Subline="Unterzeile"
                       FooterContent="@FusszeileFragment">
        <KernBody>Karteninhalt</KernBody>
    </KernCardContainer>
</KernCard>

<!-- Dialog -->
<KernDialog Title="Rückfrage" Open="_dialogOffen" OnClose="DialogSchließen">
    <ChildContent>
        <KernBody>Dialoginhalt</KernBody>
    </ChildContent>
    <FooterContent>
        <KernButton Variant="ButtonVariant.Secondary" OnClick="@(_ => DialogSchließen())">Abbrechen</KernButton>
        <KernButton OnClick="DialogBestätigen">Bestätigen</KernButton>
    </FooterContent>
</KernDialog>

<!-- Table -->
<KernTable Caption="Datentabelle" Small="true" Striped="true" Responsive="true">
    <thead><tr><th scope="col">Spalte</th></tr></thead>
    <tbody><tr><td>Wert</td></tr></tbody>
</KernTable>

<!-- Summary / Zusammenfassung -->
<KernSummaryGroup>
    <KernSummary Title="Abschnitt 1" Number="1" EditLabel="Bearbeiten" EditHref="/schritt-1">
        <KernDescriptionList Layout="DescriptionListLayout.Column">
            <KernDescriptionItem Key="Name" Value="@_name" />
        </KernDescriptionList>
    </KernSummary>
</KernSummaryGroup>

<!-- Task-Liste -->
<KernTaskListGroup Title="Aufgaben">
    <KernTaskList>
        <KernTaskListItem Number="1" Title="Antrag ausfüllen" Href="/schritt-1"
                          StatusContent="@StatusBadge" />
    </KernTaskList>
</KernTaskListGroup>
```

**`CardSize`:** `Small | Default | Large`  
**`DescriptionListLayout`:** `Row | Column`

### 3.7 Navigation

```razor
<KernKopfzeile />   <!-- Verwaltungs-Header (Logo, Titel) -->
<KernLink Href="/seite" Target="_blank">Linktext</KernLink>
<KernList Variant="KernListVariant.Bullet">
    <li>Eintrag 1</li>
    <li>Eintrag 2</li>
</KernList>
```

**`KernListVariant`:** `Default | Bullet | Number`

### 3.8 Icons

```razor
<KernIcon Glyph="KernIconGlyph.Info" aria-hidden="true" />
<KernIcon Glyph="KernIconGlyph.Success" Size="IconSize.Large" aria-label="Erfolgreich" />
```

Wichtige `KernIconGlyph`-Werte: `Info`, `Success`, `Warning`, `Danger`,
`Settings`, `BrightnessMedium`, `Autorenew`, `ArrowForward`, `ArrowBack`,
`Check`, `Close`, `Add`, `Delete`, `Edit`, `Search`, `Download`, `Upload`.

---

## 4 · Theming

Der `ThemeService` steuert Light/Dark-Theme **ohne JavaScript** (außer dem
Anti-FOUC-Script oben):

```razor
@inject ThemeService ThemeService

<div data-kern-theme="@ThemeService.AttributeValue">
    @Body
</div>

@code {
    private async Task ThemeWechseln(MouseEventArgs _)
    {
        await ThemeService.ToggleAsync(HttpContext);
    }
}
```

- `ThemeService.Current` → `KernTheme.Light` oder `KernTheme.Dark`
- `ThemeService.AttributeValue` → `"light"` oder `"dark"`
- Cookie-Name: `kern-theme`

---

## 5 · Typische Page-Struktur

```razor
@page "/meine-seite"

<PageTitle>Seitentitel – App-Name</PageTitle>

<KernContainer>
    <KernHeading Level="HeadingLevel.Large" Element="h1">Seitenüberschrift</KernHeading>
    <KernBody Muted="true">Kurze Beschreibung der Seite.</KernBody>

    @if (_ladeVorgang)
    {
        <KernLoader Visible="true" ScreenReaderText="Daten werden geladen" />
    }
    else
    {
        <KernRow>
            <KernCol Span="12" SpanMd="8">
                <!-- Hauptinhalt -->
            </KernCol>
            <KernCol Span="12" SpanMd="4">
                <!-- Sidebar / Aktionen -->
            </KernCol>
        </KernRow>
    }
</KernContainer>

@code {
    private bool _ladeVorgang = true;

    protected override async Task OnInitializedAsync()
    {
        // Daten laden
        await Task.Delay(0); // Platzhalter
        _ladeVorgang = false;
    }
}
```

---

## 6 · Mehrstufiges Formular (Antragsstrecke)

```razor
@page "/antrag"

<PageTitle>Antrag – App-Name</PageTitle>

<KernContainer>
    <KernHeading Level="HeadingLevel.Large" Element="h1">Antragsstrecke</KernHeading>

    @if (_schritt < _gesamtSchritte)
    {
        <KernProgress Value="_schritt" Max="_gesamtSchritte" Label="Fortschritt" />
    }

    @if (_schritt == 1)
    {
        <KernInputText Label="Vorname"
                       Value="@_vorname"
                       ValueChanged="@(v => _vorname = v)"
                       Error="@GetFehler(nameof(_vorname))" />

        <KernRow>
            <KernCol Span="12" SpanMd="6">
                <KernButton Variant="ButtonVariant.Secondary" OnClick="Abbrechen" Block="true">
                    Abbrechen
                </KernButton>
            </KernCol>
            <KernCol Span="12" SpanMd="6">
                <KernButton Variant="ButtonVariant.Primary" OnClick="NächsterSchritt" Block="true">
                    Weiter
                </KernButton>
            </KernCol>
        </KernRow>
    }
    else if (_schritt == 2)
    {
        <!-- weiterer Schritt -->
    }
    else if (_schritt == _gesamtSchritte)
    {
        <!-- Bestätigungsseite / Zusammenfassung -->
        <KernAlert Type="AlertType.Success" Title="Antrag eingereicht">
            Ihr Antrag wurde erfolgreich übermittelt.
        </KernAlert>
    }
</KernContainer>

@code {
    private int _schritt = 1;
    private const int _gesamtSchritte = 3;
    private string? _vorname;
    private Dictionary<string, string> _fehler = [];

    private string? GetFehler(string feldName)
        => _fehler.TryGetValue(feldName, out var msg) ? msg : null;

    private bool Validieren()
    {
        _fehler.Clear();
        if (string.IsNullOrWhiteSpace(_vorname))
            _fehler[nameof(_vorname)] = "Vorname ist ein Pflichtfeld.";
        return _fehler.Count == 0;
    }

    private Task NächsterSchritt(MouseEventArgs _)
    {
        if (Validieren()) _schritt++;
        return Task.CompletedTask;
    }

    private Task Abbrechen(MouseEventArgs _)
    {
        _schritt = 1;
        _fehler.Clear();
        return Task.CompletedTask;
    }
}
```

---

## 7 · Barrierefreiheit – Pflicht-Checkliste

| Anforderung | Umsetzung |
|---|---|
| Semantisches HTML | `<main>`, `<nav>`, `<header>`, `<button>` etc. verwenden |
| Dekorative Icons | `aria-hidden="true"` auf `<KernIcon>` |
| Informative Icons | `aria-label="Beschreibung"` auf `<KernIcon>` |
| Toggle-Buttons | `aria-pressed="true/false"` als Attribut übergeben |
| Dynamische Inhalte | `aria-live="polite"` auf Container |
| ARIA-Referenzen | `aria-describedby` / `aria-labelledby` nur setzen, wenn Ziel-Element im DOM vorhanden |
| Fokus-Styles | Niemals entfernen – KERN stellt sie bereit |
| Tastatur | Alle Aktionen per Tab/Enter/Space erreichbar |

```razor
<!-- ✅ Toggle-Button korrekt -->
<KernButton Variant="ButtonVariant.Tertiary"
            Icon="KernIconGlyph.BrightnessMedium"
            IconOnly="true"
            AriaLabel="Theme wechseln"
            aria-pressed="@(ThemeService.Current == KernTheme.Dark ? "true" : "false")"
            OnClick="ThemeWechseln" />

<!-- ✅ Bedingte ARIA-Referenz -->
<p aria-describedby="@(_hinweisVorhanden ? _hinweisId : null)">Eingabefeld</p>
@if (_hinweisVorhanden)
{
    <span id="@_hinweisId">Hinweistext</span>
}
```

---

## 8 · Häufige Fehler

| ❌ Falsch | ✅ Richtig |
|---|---|
| `kern-container--fluid` (CSS-Klasse) | `kern-container-fluid` |
| `@onclick` auf `<div>` | `<KernButton>` mit `OnClick` |
| Inline-Styles | KERN-Token (`var(--kern-color-*)`) |
| Statische DOM-IDs als Fallback | `IdGeneratorService.Create("kern-xyz")` |
| Umlaute als „ae/oe/ue" in UI-Texten | Korrekte Umlaute: ä, ö, ü, ß |
| Bootstrap-Klassen mischen | Ausschließlich `kern-`-Klassen |
| JavaScript für Theme-Toggle | `ThemeService.ToggleAsync(...)` |

---

## 9 · Vollständiges Beispiel: Info-Page

```razor
@page "/info"
@inject ThemeService ThemeService

<PageTitle>Informationen – Demo</PageTitle>

<KernContainer>
    <KernRow>
        <KernCol Span="12">
            <KernHeading Level="HeadingLevel.Large" Element="h1">Informationen</KernHeading>
            <KernBody Muted="true">
                Übersicht der wichtigsten Informationen für Antragstellende.
            </KernBody>
        </KernCol>
    </KernRow>

    <KernDivider />

    <KernRow>
        <KernCol Span="12" SpanMd="4">
            <KernCard>
                <KernCardContainer Title="Bearbeitungszeit" Preline="Allgemein">
                    <KernBody>Anträge werden innerhalb von 4 Wochen bearbeitet.</KernBody>
                    <KernBadge Variant="BadgeVariant.Info" Icon="KernIconGlyph.Info">Aktuell</KernBadge>
                </KernCardContainer>
            </KernCard>
        </KernCol>
        <KernCol Span="12" SpanMd="4">
            <KernCard>
                <KernCardContainer Title="Benötigte Unterlagen" Preline="Checkliste">
                    <KernList Variant="KernListVariant.Bullet">
                        <li>Personalausweis</li>
                        <li>Meldebescheinigung</li>
                        <li>Einkommensnachweis</li>
                    </KernList>
                </KernCardContainer>
            </KernCard>
        </KernCol>
        <KernCol Span="12" SpanMd="4">
            <KernCard>
                <KernCardContainer Title="Kontakt" Preline="Rückfragen">
                    <KernBody>Montag–Freitag, 8–16 Uhr</KernBody>
                    <KernLink Href="mailto:info@behoerde.de">info@behoerde.de</KernLink>
                </KernCardContainer>
            </KernCard>
        </KernCol>
    </KernRow>

    <KernDivider />

    <KernAccordionGroup>
        <KernAccordion Title="Wie stelle ich einen Antrag?">
            Füllen Sie das Formular vollständig aus und laden Sie alle
            erforderlichen Unterlagen hoch.
        </KernAccordion>
        <KernAccordion Title="Wo finde ich den Bearbeitungsstand?">
            Nach der Einreichung erhalten Sie eine Bestätigungs-E-Mail mit
            einer Vorgangsnummer.
        </KernAccordion>
    </KernAccordionGroup>
</KernContainer>
```

