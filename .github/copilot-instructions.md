# Copilot Instructions – KernUxExample

## Projektübersicht

Dieses Projekt ist eine Blazor-Webanwendung, die das **KERN Design System** (KERN-UX) implementiert. KERN ist ein offener UX-Standard für die deutsche Verwaltung mit Fokus auf Barrierefreiheit und Nutzendenzentriertheit.

## Technologie-Stack

- **.NET 10** (aktuellstes Framework)
- **C#** als primäre Entwicklungssprache
- **Blazor** für die UI-Entwicklung (Server-Side Rendering und InteractiveServer)
- **SCSS/CSS** für Styling (kompiliert nach `wwwroot/css/`)
- **JavaScript nur wenn technisch unvermeidbar** (z.B. Anti-FOUC für Theme-Initialisierung)

## Styling-Richtlinien: KERN-UX

### Design System

Das Projekt verwendet das **KERN Design System** – siehe:
- `Styles/Kern-UX-Plain_README.md` für allgemeine Informationen
- `Styles/COMPONENTS.MD` für detaillierte Komponenten-Dokumentation
- `Styles/core/` für SCSS-Quelldateien
- `Styles/themes/kern/` für das Kern-Theme

### CSS-Klassen-Präfix

Alle KERN-Komponenten verwenden das Präfix `kern-`:
```html
<button class="kern-btn kern-btn--primary">Button</button>
<h1 class="kern-title">Titel</h1>
<p class="kern-body">Text</p>
```

### BEM-Methodologie

KERN nutzt die BEM-Syntax:
- **Block**: `.kern-btn`
- **Element**: `.kern-btn__icon`
- **Modifier**: `.kern-btn--primary`, `.kern-btn--secondary`

### Verfügbare Komponenten

- **Layout**: Grid-System (`.kern-container`, `.kern-row`, `.kern-col-*`)
- **Typografie**: Title, Heading, Body, Label, Preline, Subline
- **Formularelemente**: Button, Input, Checkbox, Radio, Select, Textarea
- **UI-Komponenten**: Accordion, Alert, Badge, Card, Dialog, Divider, Icon, Loader, Progress, Summary, Table, Task-List
- **Navigation**: Kopfzeile (Header)

### Theming

Das Projekt unterstützt **Light** und **Dark** Theme:
- Theme wird über das Attribut `data-kern-theme="light|dark"` gesteuert
- CSS-Variablen definiert in `Styles/core/tokens/_theme.scss`
- Theme-Verwaltung erfolgt über `ThemeService.cs` (C#-basiert)
- Cookies speichern die Präferenz (`kern-theme`)
- Anti-FOUC-Script in `App.razor` (einziges JavaScript, technisch notwendig)

### Spacing & Alignment

KERN verwendet Token-basierte Abstände:
```scss
--kern-metric-space-none: 0
--kern-metric-space-2x-small: 2px
--kern-metric-space-x-small: 4px
--kern-metric-space-small: 8px
--kern-metric-space-default: 16px
--kern-metric-space-large: 24px
--kern-metric-space-x-large: 32px
```

Alignment-Utilities:
- `.kern-align-items-{start|center|end}`
- `.kern-justify-content-{start|center|end|around|between|evenly}`
- `.kern-align-self-{start|center|end}`

## UI-Entwicklung mit Blazor

### Komponentenstruktur

- Blazor-Komponenten in `Components/`
- Layout-Komponenten in `Components/Layout/`
- Pages in `Components/Pages/`
- Services in `Services/`

### Komponenten-Implementierung

Bei der Implementierung von UI-Komponenten:

1. **KERN-Standards befolgen**: Verwende die HTML-Struktur und CSS-Klassen aus `COMPONENTS.MD`
2. **Blazor-Syntax nutzen**: `@code`, `@inject`, Parameter, EventCallback
3. **Typsicherheit**: Nutze C#-Typen und Enums (z.B. `KernTheme`)
4. **Keine JavaScript-Interop wenn vermeidbar**: Bevorzuge reine C#/Blazor-Lösungen

Beispiel einer KERN-konformen Blazor-Komponente:
```razor
<button type="button" 
        class="kern-btn kern-btn--primary" 
        @onclick="HandleClick">
    <span class="kern-icon kern-icon--check" aria-hidden="true"></span>
    <span class="kern-label">@ButtonText</span>
</button>

@code {
    [Parameter]
    public string ButtonText { get; set; } = "Button";
    
    [Parameter]
    public EventCallback OnClick { get; set; }
    
    private async Task HandleClick()
    {
        await OnClick.InvokeAsync();
    }
}
```

## Barrierefreiheit (Accessibility)

Barrierefreiheit ist ein **Kernprinzip** von KERN-UX:

### Pflicht-Anforderungen

1. **Semantisches HTML**: Verwende die richtigen HTML-Elemente (`<button>`, `<nav>`, `<main>`, etc.)
2. **ARIA-Attribute**:
    - Icons: `aria-hidden="true"` (wenn dekorativ) oder `aria-label` (wenn inhaltlich relevant)
    - Buttons: `aria-pressed` für Toggle-Buttons
    - Live-Regions: `aria-live="polite"` für dynamische Inhalte
3. **Fokus-Management**: Fokuszustände sind in KERN vorgesehen – niemals entfernen
4. **Tastaturbedienbarkeit**: Alle Interaktionen müssen über Tastatur möglich sein
5. **Alternativtexte**: Für alle informativen Bilder/Icons
6. **Kontrastverhältnis**: KERN-Theme erfüllt WCAG 2.1 AA-Standard

### Best Practices

```html
<!-- ✅ Gut: Semantisch und barrierefrei -->
<button type="button" 
        class="kern-btn kern-btn--primary"
        aria-pressed="false"
        title="Beschreibender Tooltip">
    <span class="kern-icon kern-icon--check" aria-hidden="true"></span>
    <span class="kern-label">Speichern</span>
</button>

<!-- ❌ Schlecht: Fehlendes Semantik und ARIA -->
<div onclick="save()" class="kern-btn">
    <i class="icon-check"></i>
    Speichern
</div>
```

## Code-Stil & Konventionen

### C#
- **Naming**: PascalCase für öffentliche Member, camelCase für private
- **Nullability**: Nutze nullable reference types (`string?`)
- **Async/Await**: Für alle asynchronen Operationen
- **Services**: Dependency Injection über `@inject`

### Razor
- **Components**: PascalCase-Dateinamen (z.B. `MainLayout.razor`)
- **Parameters**: Mit `[Parameter]`-Attribut kennzeichnen
- **Events**: `EventCallback<T>` für typsichere Events

### SCSS
- **Struktur**: Folge der vorhandenen KERN-Struktur in `Styles/core/`
- **Variablen**: Nutze KERN-Token (`var(--kern-color-*)`)
- **Keine eigenen Präfixe**: Halte dich an `kern-` Namensraum

### Umlaute und Sonderzeichen

**Grundregel**: Deutsche Umlaute (ä, ö, ü, ß) und Sonderzeichen immer korrekt verwenden – **nie** durch "ae", "oe", "ue", "ss" ersetzen.

#### Benutzersichtbare Texte

Alle Inhalte, die dem Nutzer präsentiert werden, **müssen** korrekte Umlaute verwenden:

```razor
✅ Gut: Korrekte Umlaute
<KernBody>Bitte prüfen Sie die fehlenden Pflichtfelder.</KernBody>
<KernButton>Zurück</KernButton>
<KernAlert Title="Änderungen gespeichert">Die Daten wurden erfolgreich übermittelt.</KernAlert>

❌ Schlecht: Ersatz durch "ae/oe/ue"
<KernBody>Bitte pruefen Sie die fehlenden Pflichtfelder.</KernBody>
<KernButton>Zurueck</KernButton>
<KernAlert Title="Aenderungen gespeichert">Die Daten wurden erfolgreich uebermittelt.</KernAlert>
```

**Betrifft:**
- Alle Texte in Razor-Markup (`<KernBody>`, `<KernAlert>`, `<KernButton>`, etc.)
- String-Literale in `@code`-Blöcken, die als UI-Text verwendet werden
- `PageTitle`, `Description`, Label-Texte
- Fehlermeldungen, Hinweistexte, Tooltips
- Dokumentationskommentare in XML-Doku (`<summary>`, `<param>`)

#### Technische Bezeichner (Code)

In Code-Bezeichnern sind Ersatz-Schreibweisen **erlaubt** (aber nicht zwingend):

```csharp
✅ Erlaubt: Ersatz in Bezeichnern
private string? _emailAdresse;
private bool _datenschutzAkzeptiert;
public string Rueckmeldung { get; set; }

✅ Auch erlaubt: Umlaute in Bezeichnern
private string? _emailAdresse;
private bool _datenschutzAkzeptiert;
public string Rückmeldung { get; set; }
```

**Betrifft:**
- Variablennamen, Methodennamen, Klassennamen
- Enum-Werte
- Property- und Parameter-Namen
- Private Felder

**Ausnahme**: Auch in Bezeichnern sollte man Umlaute verwenden, wenn sie die Lesbarkeit deutlich erhöhen und keine technischen Probleme verursachen.

## Unit-Tests

### Struktur: Given / When / Then

Jeder Test folgt dem **GWT-Muster** und wird durch Inline-Kommentare klar in drei Abschnitte unterteilt:

- **Given** – Testkontext aufbauen: Eingaben, Zustände, Mocks und Stubs vorbereiten
- **When** – die zu testende Aktion ausführen (genau eine pro Test)
- **Then** – das erwartete Ergebnis prüfen

```csharp
[Fact(DisplayName = "Theme wechselt von Hell auf Dunkel")]
public void Toggle_WechseltVonLightAufDark()
{
    // Given – Ausgangszustand ist immer Light (Default-Konstruktor)
    var service = new ThemeService();

    // When – Toggle einmalig aufrufen
    service.Toggle();

    // Then – Theme muss jetzt Dark sein
    Assert.Equal(KernTheme.Dark, service.Current);
}
```

Abweichungen (z. B. keine separaten Abschnitte bei trivialen Ein-Zeilen-Tests) sind erlaubt,
wenn die Lesbarkeit dadurch steigt. In solchen Fällen **muss** der Testname den Kontext erklären.

---

### Code-Kommentare für Framework-Einstieg

Tests im Projekt werden auch als **Lerndokument** für Framework-Neulinge verwendet.
Kommentiere deshalb jede technische Implementierung, die nicht offensichtlich ist:

| Situation | Pflicht-Kommentar |
|---|---|
| `BunitContext` anlegen | Kurze Erklärung, was bUnit ist und warum kein Browser nötig ist |
| `context.Services.Add*(...)` | Hinweis, dass dies den DI-Container für den Test befüllt |
| `context.JSInterop.Setup<T>(...)` | Erklärung, warum JS-Interop im Test gestubbt werden muss |
| `context.Render<TComponent>(...)` | Hinweis, dass die Komponente in einem virtuellen DOM gerendert wird |
| `cut.Find(...)` / `cut.FindAll(...)` | Erklärung: `cut` = *Component Under Test*, Selektor wie in CSS |
| `MarkupMatches(...)` | Hinweis auf semantischen HTML-Vergleich (ignoriert Leerzeichen/Reihenfolge) |
| `EventCallback` / `InvokeAsync` | Erklärung des Blazor-Event-Mechanismus |

Beispiel mit vollständiger Kommentierung:

```csharp
[Fact(DisplayName = "Icon rendert korrekte CSS-Klassen")]
public void KernIcon_RendertKorrekteCssKlassen()
{
    // BunitContext ersetzt den Browser: Blazor-Komponenten können so ohne
    // echten Webserver gerendert und geprüft werden.
    using var context = new BunitContext();

    // Render<T> rendert die Komponente in ein virtuelles DOM.
    // Der Rückgabewert ist das "Component Under Test" (cut).
    var cut = context.Render<KernIcon>(parameters => parameters
        .Add(p => p.Glyph, KernIconGlyph.Info)   // Parameter wie in Razor: <KernIcon Glyph="Info" />
        .Add(p => p.Size, IconSize.Large));

    // cut.Find(...) sucht im gerenderten HTML nach einem Element – wie document.querySelector().
    var icon = cut.Find("span");

    // Assert.Contains prüft, ob die CSS-Klasse im ClassList-Array vorhanden ist.
    Assert.Contains("kern-icon--info", icon.ClassList);
    Assert.Contains("kern-icon--large", icon.ClassList);
}
```

---

### DisplayNames – kurz, prägnant, deutsch

- Jeder Test erhält einen **`DisplayName`** als deutschen Freitext.
- Der Name beschreibt **was** getestet wird, **nicht wie**.
- Format: `[Fact(DisplayName = "...")]` / `[Theory(DisplayName = "...")]`

**Regeln:**

| ✅ Gut | ❌ Schlecht |
|---|---|
| `"Theme wechselt von Hell auf Dunkel"` | `"Toggle_SwitchesBetweenLightAndDark"` |
| `"Deaktivierter Button akzeptiert keinen Klick"` | `"Button_Disabled_NoClick_Test"` |
| `"Pflichtfeld zeigt Fehlermeldung bei leerem Wert"` | `"TestInputValidationEmpty"` |
| `"Icon rendert korrekte CSS-Klassen"` | `"RendersExpectedKernIconClasses"` |

- Der Methodenname bleibt englisch (C#-Konvention) und spiegelt DisplayName kompakt wider:
  `Toggle_WechseltVonLightAufDark`, `Button_DeaktiviertAkzeptiertKeinenKlick`
- Keine Abkürzungen im DisplayName – Lesbarkeit hat Vorrang.
- Bei parametrisierten Tests (`[Theory]`) beschreibt der DisplayName den **allgemeinen Fall**;
  die einzelnen `[InlineData]`-Werte dokumentieren sich durch sprechende Eingabewerte selbst.

---

## Typische Aufgaben

### Neue KERN-Komponente hinzufügen

1. Dokumentation in `COMPONENTS.MD` nachschlagen
2. HTML-Struktur und CSS-Klassen übernehmen
3. Als Blazor-Komponente implementieren (`.razor` Datei)
4. KERN-Standards für Barrierefreiheit befolgen
5. Im Layout/Page einbinden

### Theme-Unterstützung erweitern

- CSS-Variablen in `Styles/core/tokens/_theme.scss` anpassen
- Beide Theme-Mixins (`light-color`, `dark-color`) pflegen
- `ThemeService` verwenden, niemals direktes DOM-Manipulation

### Formular implementieren

- KERN-Formular-Komponenten verwenden (`.kern-input`, `.kern-fieldset`, etc.)
- Blazor EditForm mit DataAnnotations
- Fehlerzustände mit KERN-Modifiern (`.kern-input--error`)

## Wichtige Dateien

- `Components/App.razor`: Root-Layout mit Theme-Initialisierung
- `Components/Layout/MainLayout.razor`: Haupt-Layout mit Navigation und Theme-Toggle
- `Services/ThemeService.cs`: Theme-Verwaltung (C#)
- `Styles/core/index.scss`: KERN-Hauptdatei
- `Styles/themes/kern/index.scss`: Kern-Theme
- `COMPONENTS.MD`: Komplette Komponenten-Referenz

## Anti-Patterns vermeiden

❌ **Nicht tun:**
- Bootstrap oder andere Design-Systeme mischen
- Eigene CSS-Präfixe statt `kern-` verwenden
- JavaScript verwenden, wenn C#/Blazor-Lösung möglich ist
- Fokus-Styles entfernen
- ARIA-Attribute weglassen
- Inline-Styles statt KERN-Klassen

✅ **Stattdessen:**
- Ausschließlich KERN-UX Design System
- `kern-` Präfix konsequent nutzen
- C#/Blazor-native Lösungen bevorzugen (z.B. `ThemeService` statt JS)
- KERN-Fokus-Styles beibehalten
- ARIA-Attribute gemäß KERN-Dokumentation
- KERN-Utility-Klassen und Token verwenden

---

## XML-Dokumentation

### Pflicht für öffentliche APIs

Alle öffentlichen Member in der Library **müssen** XML-Dokumentation haben:

```csharp
/// <summary>Beschriftung des Formularfeldes, wird als <c>&lt;label&gt;</c> gerendert.</summary>
[Parameter]
public string Label { get; set; } = string.Empty;

/// <summary>Aktueller Wert des Eingabefeldes (Two-Way-Binding mit <see cref="ValueChanged"/>).</summary>
[Parameter]
public string? Value { get; set; }
```

### Dokumentations-Checkliste

| Element | Pflicht-Dokumentation |
|---|---|
| `[Parameter]`-Properties | Ja – was macht der Parameter, welche Werte sind gültig? |
| `EventCallback`-Properties | Ja – wann wird das Event ausgelöst? |
| `record`-Typen | Ja – wofür wird der Typ verwendet? |
| Private Felder/Methoden | Nur bei komplexer Logik (z.B. State-Synchronisation) |

---

## Performance-Patterns für Blazor-Komponenten

### IEnumerable-Parameter materialisieren

Wenn eine Komponente über einen `IEnumerable<T>`-Parameter iteriert, muss dieser in `OnParametersSet` 
einmalig materialisiert werden – **nicht** bei jedem Property-Zugriff:

```csharp
// ❌ Schlecht: Neuallokation bei jedem Zugriff
private List<MyItem> ResolvedItems => [.. Items];

// ✅ Gut: Einmalige Materialisierung pro Render-Zyklus
private List<MyItem> _materializedItems = [];
private IReadOnlyList<MyItem> ResolvedItems => _materializedItems;

protected override void OnParametersSet()
{
    _materializedItems = [.. Items];
}
```

**Begründung:** Bei großen Listen führt die wiederholte Neuallokation zu erheblichem GC-Druck 
und Performance-Einbußen. Das Pattern ist besonders wichtig in `@for`-Schleifen, die mehrfach 
auf die Liste zugreifen.

### Kommentar-Pflicht bei nicht-offensichtlichem Code

Wenn Performance-Optimierungen oder State-Synchronisations-Logik verwendet wird, 
muss ein erklärender Kommentar hinzugefügt werden:

```csharp
// _materializedItems wird einmalig pro Render-Zyklus in OnParametersSet befüllt.
// Das vermeidet die Neuallokation bei jedem Property-Zugriff auf ResolvedItems.
private List<MyItem> _materializedItems = [];
```

---

## Namespace-Konventionen

### Imports in Razor-Komponenten

- **Bevorzuge explizite `@using`-Direktiven** am Anfang der Datei statt vollqualifizierter Namespaces im Code
- **Vermeide redundante Qualifizierung** wenn der Namespace bereits importiert ist

```razor
@* ✅ Gut: Import am Anfang, kurze Verwendung im Code *@
@using System.Globalization

@code {
    await ValueChanged.InvokeAsync(Convert.ToString(args.Value, CultureInfo.InvariantCulture));
}

@* ❌ Schlecht: Vollqualifizierter Namespace im Code *@
@code {
    await ValueChanged.InvokeAsync(Convert.ToString(args.Value, System.Globalization.CultureInfo.InvariantCulture));
}
```

---

## CLI-Operationen & Ausgabeverwaltung

### Output-Handling bei Terminal-Kommandos

Bei der Ausführung von CLI-Befehlen (z.B. `dotnet build`, `dotnet test`) gilt folgende Best Practice:

**Immer den Output (stdout + stderr) in eine separate Datei schreiben und aus dieser lesen:**

```powershell
# ❌ Schlecht: Output direkt auf Konsole oder abgeschnitten
dotnet build "Project.csproj"

# ✅ Gut: Output in Datei umleiten und später lesen
dotnet build "Project.csproj" 2>&1 | Out-File -FilePath "build-output.txt" -Encoding utf8
Get-Content "build-output.txt"
```

**Begründung:** 
- Große Outputs können in der Terminal-Session abgeschnitten werden
- Systematische Erfassung ermöglicht sichere Fehlerdiagnose
- Dateibasierte Ausgabe ist zuverlässiger als Streaming

---

### PowerShell: UTF8-Encoding zwingend erforderlich

Bei allen PowerShell-Operationen **muss explizit UTF-8 als Encoding festgelegt werden** – sowohl beim Schreiben in Dateien als auch für die Konsole:

#### 1. Schreiben in Dateien

```powershell
# ❌ Schlecht: Standardencoding (Windows-1252 auf DE-Systemen)
Get-Content "file.txt" | Out-File "output.txt"

# ✅ Gut: Explizit UTF8
Get-Content "file.txt" | Out-File -FilePath "output.txt" -Encoding utf8
```

#### 2. Konsolen-Output

```powershell
# ❌ Schlecht: Standardencoding
Write-Output "Ü Ö Ä äöü"

# ✅ Gut: UTF8 für die PowerShell-Konsole selbst
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
Write-Output "Ü Ö Ä äöü"
```

#### 3. Komplettes Beispiel

```powershell
# Setze die Konsole auf UTF8 zu Beginn der Sitzung
[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Führe Befehl aus und schreibe Output in Datei
dotnet test "TestProject.csproj" 2>&1 | Out-File -FilePath "test-results.txt" -Encoding utf8

# Lese Datei zur Verarbeitung
$output = Get-Content "test-results.txt" -Encoding utf8
if ($output -match "bestanden") {
    Write-Output "Alle Tests bestanden"
}
```

**Warum UTF8 so wichtig ist:**
- Deutsch enthält Umlaute (Ä, Ö, Ü, ä, ö, ü, ß) und Sonderzeichen (–, —, « »)
- Standardencoding auf Windows ist oft Windows-1252/CP1252 (Westeuropäisch)
- Fehlendes UTF8-Encoding führt zu beschädigten Umlauten und unlesbarem Output
- **Besonders kritisch** bei internationalen Build-Logs, Fehlermeldungen und Testergebnissen

---

## KERN-Grid: Klassen vs. BEM-Modifier

Das KERN-Grid kennt **eigenständige Klassen** – kein BEM-Modifier-Doppelstrich:

| ✅ Korrekt | ❌ Falsch |
|---|---|
| `kern-container` | `kern-container--default` |
| `kern-container-fluid` | `kern-container--fluid` |

In Blazor-Komponenten daher **nie** `CssBuilder` mit einem Fluid-Modifier verwenden, sondern direkt zwischen zwei Klassen wählen:

```csharp
// ✅ Gut: zwei eigenständige Klassen
private string ContainerClass => Fluid ? "kern-container-fluid" : "kern-container";

// ❌ Schlecht: BEM-Modifier existiert im KERN-Grid nicht
private string ContainerClass => new CssBuilder("kern-container")
    .AddClass("kern-container--fluid", Fluid)
    .Build();
```

**Regel:** Vor dem Hinzufügen eines CSS-Modifiers immer in `Styles/core/layout/_grid.scss` und der jeweiligen Komponenten-SCSS prüfen, ob er als BEM-Modifier oder eigenständige Klasse existiert.

---

## DOM-IDs: Immer eindeutig pro Instanz

Statische Fallback-IDs (`"kern-summary-title"`, `"kern-input"`, etc.) führen bei mehrfach verwendeten Komponenten zu **doppelten DOM-IDs** – das bricht ARIA-Referenzen (`aria-labelledby`, `aria-describedby`) und verstößt gegen WCAG 1.3.1.

**Pflicht-Pattern** für jede Komponente, die eine ID nach außen exponiert oder intern auf IDs verweist:

```csharp
// ✅ Gut: eindeutige ID pro Instanz via IdGeneratorService
@inject IdGeneratorService IdGenerator

private string? _generatedId;

protected override void OnInitialized()
{
    _generatedId = IdGenerator.Create("kern-summary-title");
}

// Explizite Parameter-ID hat Vorrang; Fallback ist die generierte ID.
private string ResolvedId => string.IsNullOrWhiteSpace(Id) ? _generatedId! : Id;

// ❌ Schlecht: statischer Fallback-String → doppelte DOM-IDs bei mehreren Instanzen
private string ResolvedId => string.IsNullOrWhiteSpace(Id) ? "kern-summary-title" : Id;
```

---

## ARIA-Attribute: Nur setzen wenn Ziel-Element existiert

Ein `aria-describedby`, `aria-labelledby` oder `aria-controls`, das auf eine nicht existierende ID zeigt, ist ein **WCAG 1.3.1-Fehler** und verwirrt Screenreader.

```razor
@* ✅ Gut: aria-describedby nur wenn StatusContent gerendert wird *@
<a aria-describedby="@(StatusContent is null ? null : ResolvedStatusId)">@Title</a>

@* ❌ Schlecht: immer gesetzt, auch wenn das Ziel-Element fehlt *@
<a aria-describedby="@ResolvedStatusId">@Title</a>
```

**Regel:** ARIA-Referenz-Attribute (`aria-describedby`, `aria-labelledby`, `aria-controls`) immer konditionieren: Nur setzen, wenn das referenzierte Element tatsächlich im DOM gerendert wird.

---

## AdditionalAttributes immer ans Ziel-HTML-Element weitergeben

`CaptureUnmatchedValues = true` sammelt alle unbekannten Attribute – diese müssen auch **physisch ans Ziel-Element** weitergegeben werden. `AddClassFromAttributes` im `CssBuilder` fusioniert nur den `class`-Key; alle anderen Attribute (`aria-*`, `data-*`, `role`, etc.) gehen ohne `@attributes` verloren.

```razor
@* ✅ Gut: AdditionalAttributes direkt am Ziel-Element *@
<table class="@CssClass" @attributes="AdditionalAttributes">

@* ❌ Schlecht: @attributes fehlt am Element – aria-*, data-* gehen verloren *@
<table class="@CssClass">
```

**Regel:** Jedes Element, das als primäres Host-Element einer Blazor-Komponente dient, muss `@attributes="AdditionalAttributes"` tragen – auch wenn `CssBuilder` bereits `AddClassFromAttributes` nutzt.

---

## JS-Interop: Stabile eigene Funktionen statt Prototype-Calls

Der direkte Aufruf von `HTMLElement.prototype.METHOD.call(element)` via `InvokeVoidAsync` ist fragil – er kann je nach Browser-Umgebung und Blazor-Runtime fehlschlagen.

```csharp
// ❌ Schlecht: fragiles Prototype-Muster
await JS.InvokeVoidAsync("HTMLDialogElement.prototype.showModal.call", _ref);

// ✅ Gut: stabile eigene JS-Funktion in wwwroot/js/
await JS.InvokeVoidAsync("kernDialog.showModal", _ref);
```

Dazugehörige JS-Datei (`wwwroot/js/kern-dialog.js`):
```js
window.kernDialog = {
    showModal: el => el?.showModal(),
    close:     el => el?.close()
};
```

**Regel:** Für jeden JS-Interop-Bedarf eine eigene benannte Funktion unter `window.kern*` anlegen. Die Datei gehört nach `wwwroot/js/` und muss im Host-Dokument eingebunden werden:
```html
<script src="_content/PublicKernBlazor.Components/js/kern-dialog.js"></script>
```

---

## XML-Dokumentation: Nur existierende Typen referenzieren

`<see cref="TypName"/>` in XML-Kommentaren erzeugt zur Laufzeit keine Fehler, führt aber zu kaputten Links in IntelliSense und generierter Doku, wenn der Typ nicht existiert.

```csharp
// ❌ Schlecht: KernCardHeader existiert nicht als eigener Typ
/// <summary>Inhalt – z.B. <c>KernCardHeader</c>.</summary>

// ✅ Gut: nur tatsächlich existierende Typen referenzieren
/// <summary>Inhalt – typischerweise <see cref="KernCardMedia"/> und <see cref="KernCardContainer"/>.</summary>
```

**Regel:** Bei jeder Sub-Komponenten-Referenz in XML-Doku prüfen, ob der referenzierte Typ als eigene `.razor`-Datei existiert. Andernfalls `<c>PseudoName</c>` statt `<see cref="..."/>` verwenden.

---

## Weitere Ressourcen

- KERN-UX Website: https://www.kern-ux.de
- Komponenten-Dokumentation: https://www.kern-ux.de/komponenten
- GitLab Repository: https://gitlab.opencode.de/kern-ux/kern-ux-plain

