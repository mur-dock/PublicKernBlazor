# Changelog

Alle bemerkenswerten Änderungen an diesem Projekt werden in dieser Datei dokumentiert.

Das Format basiert auf [Keep a Changelog](https://keepachangelog.com/de/1.0.0/),
und dieses Projekt hält sich an [Semantic Versioning](https://semver.org/lang/de/).

## [Unreleased]

## [0.2.0] – 2026-03-11

### Behoben

#### Komponenten-Library (`PublicKernBlazor.Components`)

- **Layout/Grid**: `KernRow` und `KernCol` filtern beim `@attributes`-Splat den Schlüssel `class` heraus,
  damit generierte KERN-Grid-Klassen nicht überschrieben werden.
- **AdditionalAttributes-Merge**: CSS-Klassen aus `AdditionalAttributes` werden weiterhin über
  `AddClassFromAttributes(...)` sauber zusammengeführt.
- **Grid-Styles**: Spaltenregeln um `max-width` ergänzt, damit Spaltenbreiten responsiv stabil bleiben.

#### Demo & Tests

- **Demo-Layout**: Theme-Toggle im Header in einen Wrapper verschoben, damit Grid-Spalten links/rechts
  korrekt nebeneinander bleiben.
- **Unit-Tests**: Neue bUnit-Tests für `KernRow`/`KernCol` sichern ab, dass `class` aus
  `AdditionalAttributes` gemerged wird und andere Attribute (`data-*`, `aria-*`) am Host-Element erhalten bleiben.

## [0.1.0] – 2026-03-09

### Hinzugefügt

#### Komponenten-Library (`PublicKernBlazor.Components`)

- **Layout**: `KernContainer`, `KernRow`, `KernCol`, `KernThemeProvider`
- **Typografie**: `KernHeading`, `KernTitle`, `KernBody`, `KernLabel`, `KernSubline`, `KernPreline`
- **Navigation**: `KernKopfzeile`, `KernLink`, `KernList`
- **Shared**: `KernIcon`, `KernDivider`, `KernStyles`
- **Formular-Elemente**: `KernButton`, `KernInputText`, `KernInputEmail`, `KernInputPassword`,
  `KernInputTel`, `KernInputUrl`, `KernInputNumber`, `KernInputDate`, `KernInputFile`,
  `KernTextarea`, `KernSelect`, `KernCheckbox`, `KernCheckboxList`, `KernRadioGroup`, `KernFieldset`
- **Feedback**: `KernAlert`, `KernBadge`, `KernLoader`, `KernProgress`
- **Content**: `KernAccordion`, `KernAccordionGroup`, `KernCard`, `KernCardMedia`,
  `KernCardContainer`, `KernDialog`, `KernTable`, `KernDescriptionList`, `KernDescriptionItem`,
  `KernSummary`, `KernSummaryGroup`, `KernTaskList`, `KernTaskListItem`, `KernTaskListGroup`

#### Library-Erweiterungen (über KERN-UX-Standard hinaus)

- **`KernInputCurrency`** – Währungseingabe optimiert für DACH (de-DE/de-CH) mit `decimal?`-Binding;
  SCSS-Erweiterung in `Styles/extensions/` (update-sicher gegenüber KERN-UX-Core)

#### Services & Infrastruktur

- **`ThemeService`** – Light-/Dark-Theme-Verwaltung, Cookie-Persistenz (`kern-theme`),
  Anti-FOUC-Script
- **`IdGeneratorService`** – kollisionsfreie DOM-IDs pro Komponenten-Instanz für
  ARIA-Referenzattribute (`aria-describedby`, `aria-labelledby`)
- **`AddKernUx()`** – DI-Erweiterungsmethode registriert alle Services in einem Aufruf
- **Typsichere Enums** für alle Komponenten-Parameter (`ButtonVariant`, `AlertType`,
  `KernIconGlyph`, `HeadingLevel`, `Size`, u. v. m.)
- **KERN-UX-CSS** als statische Assets unter `_content/PublicKernBlazor.Components/css/`
- **`kern-dialog.js`** als statisches Asset unter `_content/PublicKernBlazor.Components/js/`
- **WCAG 2.1 AA** – ARIA-Attribute, semantisches HTML, Fokus-Management, Kontrast

#### Demo-App & Tests

- **`PublicKernBlazor.Demo`** – interaktive Showcase-App mit Beispielen für alle Komponenten,
  Antragsstrecke und Theme-Toggle
- **`PublicKernBlazor.Components.Tests`** – bUnit-Testprojekt mit Rendering-, Interaktions- und
  Accessibility-Audit-Tests für alle Komponenten
- **`PublicKernBlazor.Demo.SmokeTests`** – Playwright/NUnit-Smoke-Tests für die Demo-App

[Unreleased]: https://github.com/OWNER/PublicKernBlazor.Components/compare/v0.2.0...HEAD
[0.2.0]: https://github.com/OWNER/PublicKernBlazor.Components/compare/v0.1.0...v0.2.0
[0.1.0]: https://github.com/OWNER/PublicKernBlazor.Components/releases/tag/v0.1.0

