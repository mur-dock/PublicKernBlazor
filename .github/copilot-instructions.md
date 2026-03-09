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

## Weitere Ressourcen

- KERN-UX Website: https://www.kern-ux.de
- Komponenten-Dokumentation: https://www.kern-ux.de/komponenten
- GitLab Repository: https://gitlab.opencode.de/kern-ux/kern-ux-plain

