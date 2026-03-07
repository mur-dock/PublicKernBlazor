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

