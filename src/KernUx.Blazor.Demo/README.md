# KernUx.Blazor.Demo

Interaktive Showcase-App für die **KernUx.Blazor**-Komponentenbibliothek.  
Zeigt alle KERN-UX-Komponenten in praxisnahen Szenarien – direkt ausführbar ohne weitere Konfiguration.

---

## Schnellstart

### Voraussetzungen

| Werkzeug                                          | Mindestversion |
|---------------------------------------------------|----------------|
| [.NET SDK](https://dotnet.microsoft.com/download) | **10.0**       |
| Beliebiger Browser                                | –              |

> Eine IDE (Visual Studio, Rider) ist optional – die App lässt sich direkt über die Kommandozeile starten.

### App starten

```powershell
# Im Verzeichnis KernUx.Blazor.Demo
dotnet run
```

Danach ist die App erreichbar unter:

| Protokoll | URL                    |
|-----------|------------------------|
| HTTP      | http://localhost:5076  |
| HTTPS     | https://localhost:7023 |

Der Browser öffnet sich automatisch. Beim ersten Start mit HTTPS muss das Dev-Zertifikat einmalig vertraut werden:

```powershell
dotnet dev-certs https --trust
```

---

## Projektstruktur

```
KernUx.Blazor.Demo/
├── Components/
│   ├── App.razor                   # Root-Dokument (Anti-FOUC, KERN-Styles)
│   ├── Routes.razor                # Blazor-Router
│   ├── _Imports.razor              # Globale Namespaces
│   ├── Layout/
│   │   ├── MainLayout.razor        # Haupt-Layout: KernKopfzeile, Navigation, Theme-Toggle
│   │   ├── DemoNavMenu.razor       # KERN-basierte Sidebar-Navigation
│   │   └── ReconnectModal.razor    # Blazor-Reconnect-Dialog
│   ├── Pages/
│   │   ├── Home.razor              # Startseite mit Komponentenübersicht
│   │   ├── TypographyShowcase.razor
│   │   ├── LayoutShowcase.razor
│   │   ├── ButtonShowcase.razor
│   │   ├── IconShowcase.razor
│   │   ├── FeedbackShowcase.razor
│   │   ├── FormsShowcase.razor
│   │   ├── ContentShowcase.razor
│   │   ├── NavigationShowcase.razor
│   │   ├── AntragExample.razor     # Praxisbeispiel: Mehrstufiger Antrag
│   │   └── DashboardExample.razor  # Praxisbeispiel: Sachbearbeiter-Dashboard
│   └── Shared/
│       ├── ComponentSection.razor  # Hilfskomponente: Showcase-Sektion
│       └── VariantRow.razor        # Hilfskomponente: Varianten-Reihe
├── Program.cs                      # Host-Konfiguration
└── wwwroot/
    └── app.css                     # Leer – alle Styles kommen aus KernUx.Blazor
```

---

## Seiten und Routen

### Komponenten-Showcase

| Route                    | Seite         | Demonstrierte Komponenten                                                         |
|--------------------------|---------------|-----------------------------------------------------------------------------------|
| `/`                      | Startseite    | Übersicht, Links zu allen Sektionen                                               |
| `/components/typography` | Typografie    | `KernHeading`, `KernTitle`, `KernBody`, `KernLabel`, `KernPreline`, `KernSubline` |
| `/components/layout`     | Layout & Grid | `KernContainer`, `KernRow`, `KernCol` (responsive, Offset, Alignment)             |
| `/components/buttons`    | Buttons       | `KernButton` (Varianten, Icons, Zustände, Click-Events)                           |
| `/components/icons`      | Icon-Galerie  | `KernIcon` (alle Glyphen, alle Größen)                                            |
| `/components/feedback`   | Feedback      | `KernAlert`, `KernBadge`, `KernLoader`, `KernProgress`                            |
| `/components/forms`      | Formulare     | Alle Input-Komponenten, Select, Checkbox, Radio, Fieldset                         |
| `/components/content`    | Content       | Accordion, Card, Dialog, Table, Summary, TaskList                                 |
| `/components/navigation` | Navigation    | `KernKopfzeile`, `KernLink`, `KernList`                                           |

### Praxisbeispiele

| Route                 | Szenario                         | Techniken                                             |
|-----------------------|----------------------------------|-------------------------------------------------------|
| `/examples/antrag`    | Mehrstufiger Antrag (4 Schritte) | State-Management, Validierung, Summary/Review-Pattern |
| `/examples/dashboard` | Sachbearbeiter-Dashboard         | Datenvisualisierung, TaskList, Tabellen, Progress     |

---

## Einbindung in eigene Projekte

Die Demo zeigt, wie `KernUx.Blazor` in einer Blazor-Anwendung verwendet wird.

### 1. Projektabhängigkeit hinzufügen

```xml
<!-- .csproj -->
<ItemGroup>
    <ProjectReference Include="..\KernUx.Blazor\KernUx.Blazor.csproj"/>
</ItemGroup>
```

### 2. Services registrieren

```csharp
// Program.cs
builder.Services.AddKernUx();
```

`AddKernUx()` registriert automatisch:

- `ThemeService` – Light/Dark-Theme-Verwaltung
- `IdGeneratorService` – eindeutige ARIA-konforme DOM-IDs

### 3. Styles einbinden

```razor
@* App.razor – im <head> *@
<KernStyles />
<link rel="stylesheet" href="@Assets[\"css/themes/bw.css\"]" />
```

`KernStyles` bindet das Standard-Theme der Library aus `_content/KernUx.Blazor/css/themes/kern/index.css` ein.
Das Showcase lädt zusätzlich `wwwroot/css/themes/bw.css` explizit als projektspezifisches Overlay für Baden-Württemberg.

### 4. Anti-FOUC (Theme-Initialisierung)

```html
<!-- App.razor – vor KernStyles, im <head> -->
<script>
    (() => {
        const match = document.cookie.match(/(?:^|;\s*)kern-theme=(light|dark)/);
        const theme = match
                ? match[1]
                : (matchMedia("(prefers-color-scheme: dark)").matches ? "dark" : "light");
        document.documentElement.setAttribute("data-kern-theme", theme);
    })();
</script>
```

Setzt das Theme-Attribut **vor** dem ersten Paint – verhindert Aufblitzen des falschen Themes.

### 5. Namespaces importieren

```razor
@* _Imports.razor *@
@using KernUx.Blazor.Components
@using KernUx.Blazor.Components.Content
@using KernUx.Blazor.Components.Feedback
@using KernUx.Blazor.Components.Forms
@using KernUx.Blazor.Components.Layout
@using KernUx.Blazor.Components.Navigation
@using KernUx.Blazor.Components.Typography
@using KernUx.Blazor.Enums
@using KernUx.Blazor.Services
```

### 6. Layout einrichten

```razor
@* MainLayout.razor *@
@inject ThemeService ThemeService

<KernThemeProvider Theme="ThemeService.Current">
    <KernKopfzeile />
    <main>
        @Body
    </main>
</KernThemeProvider>
```

---

## Theme-System

Die App unterstützt **Light** und **Dark Theme**.

| Mechanismus             | Beschreibung                         |
|-------------------------|--------------------------------------|
| `ThemeService.Toggle()` | Wechselt das aktive Theme            |
| `ThemeService.Current`  | Gibt das aktuelle `KernTheme` zurück |
| Cookie `kern-theme`     | Persistiert die Präferenz            |
| `prefers-color-scheme`  | Fallback bei erstem Besuch           |
| `data-kern-theme`       | HTML-Attribut am `<html>`-Element    |

Das Theme wird ausschließlich über den `ThemeService` gesteuert – kein direktes DOM-Scripting.

---

## Barrierefreiheit

Alle Komponenten folgen dem KERN-UX-Standard:

- **Semantisches HTML** – korrekte Elemente (`<button>`, `<nav>`, `<main>`, `<fieldset>`, …)
- **ARIA-Attribute** – `aria-describedby`, `aria-labelledby`, `aria-live`, `aria-hidden`
- **Eindeutige IDs** – jede Komponente generiert per `IdGeneratorService` eine eindeutige ID
- **Tastaturbedienbarkeit** – alle Interaktionen per Tastatur nutzbar
- **Fokus-Styles** – KERN-Fokusring ist niemals entfernt
- **WCAG 2.1 AA** – Kontrastverhältnisse im KERN-Theme erfüllt

---

## Weiterführende Links

| Ressource                | URL                                                  |
|--------------------------|------------------------------------------------------|
| KERN-UX Website          | https://www.kern-ux.de                               |
| KERN-UX Komponenten-Doku | https://www.kern-ux.de/komponenten                   |
| KERN-UX GitLab           | https://gitlab.opencode.de/kern-ux/kern-ux-plain     |
| Blazor-Dokumentation     | https://learn.microsoft.com/de-de/aspnet/core/blazor |

