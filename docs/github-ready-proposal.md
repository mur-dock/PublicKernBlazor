# GitHub-Ready: Vorschläge zur Repository-Umstrukturierung

**Erstellt:** 2026-03-09  
**Bezug:** PublicKernBlazor.Components – KERN-UX Design System als Blazor Component Library

---

## Inhaltsverzeichnis

1. [Ziel und Motivation](#1-ziel-und-motivation)
2. [Neue Verzeichnisstruktur](#2-neue-verzeichnisstruktur)
3. [Neue und umstrukturierte Dateien](#3-neue-und-umstrukturierte-dateien)
4. [Interne Arbeitsdateien bereinigen](#4-interne-arbeitsdateien-bereinigen)
5. [GitHub-spezifische Konfigurationen](#5-github-spezifische-konfigurationen)
6. [Migrations-Checkliste](#6-migrations-checkliste)

---

## 1. Ziel und Motivation

Das Repository soll für die **öffentliche Veröffentlichung auf GitHub** vorbereitet werden.
Ziele sind:

- **Einheitliche, branchenübliche Struktur** (`docs/`, `src/`, `tests/`)
- **Klare Außenwirkung** durch Standard-Dateien (LICENSE, CODE_OF_CONDUCT, CONTRIBUTING, CHANGELOG)
- **Gute Contributor-Erfahrung** durch Issue-/PR-Vorlagen und automatisierte Workflows
- **Keine internen Arbeitsdokumente** im Root (Planungsdateien → `docs/internal/`)
- **Sicherheit** durch SECURITY.md und Dependabot-Konfiguration

---

## 2. Neue Verzeichnisstruktur

### 2.1 Übersicht

```
PublicKernBlazor.Components/                          ← Repository-Root
│
├── .github/                            ← GitHub-Metadaten (bereits vorhanden, erweitern)
│   ├── ISSUE_TEMPLATE/
│   │   ├── bug_report.yml
│   │   └── feature_request.yml
│   ├── PULL_REQUEST_TEMPLATE.md
│   ├── workflows/
│   │   ├── ci.yml                      ← Build + Tests bei jedem Push/PR
│   │   ├── release.yml                 ← NuGet-Publish bei Git-Tag
│   │   └── dependabot-auto-merge.yml   ← Optional: Auto-Merge für Patch-Updates
│   ├── dependabot.yml
│   ├── copilot-instructions.md         ← bereits vorhanden
│   └── git-commit-instructions.md     ← bereits vorhanden
│
├── docs/                               ← NEU: gesamte Dokumentation
│   ├── internal/                       ← NEU: interne Planungsdokumente (nicht für Nutzer)
│   │   ├── plain-to-blazor.md          ← verschoben aus Root
│   │   ├── showcase-plan.md            ← verschoben aus Root
│   │   └── issues-smoketest.md         ← verschoben + umbenannt aus Issues.md
│   ├── contributing/
│   │   └── development-setup.md        ← NEU: ausführliche Entwickler-Dokumentation
│   └── adr/                            ← NEU: Architecture Decision Records (optional)
│       └── 001-keine-js-interop.md
│
├── src/                                ← NEU: alle Quellcode-Projekte
│   ├── PublicKernBlazor.Components/                  ← verschoben
│   └── PublicKernBlazor.Demo/            ← verschoben
│
├── tests/                              ← NEU: alle Test-Projekte
│   ├── PublicKernBlazor.Components.Tests/            ← verschoben
│   └── PublicKernBlazor.Demo.SmokeTests/ ← verschoben
│
├── scripts/                            ← NEU (optional): Shell-Skripte
│   ├── Fix-SassIfDeprecation.ps1       ← verschoben aus Root
│   └── Run-SmokeTests.ps1              ← verschoben aus Root
│
├── .gitignore                          ← bereits vorhanden
├── CHANGELOG.md                        ← NEU
├── CODE_OF_CONDUCT.md                  ← NEU
├── CONTRIBUTING.md                     ← NEU
├── LICENSE                             ← bereits referenziert, muss existieren
├── README.md                           ← bereits vorhanden, ggf. anpassen
├── SECURITY.md                         ← NEU
└── PublicKernBlazor.slnx                 ← bereits vorhanden, Pfade aktualisieren
```

### 2.2 Begründung der Struktur

| Entscheidung | Begründung |
|---|---|
| `src/` für Quellprojekte | GitHub-Konvention bei Libraries mit mehreren Projekten; trennt Library-Code klar von Test- und Konfigurationsdateien |
| `tests/` für alle Tests | Alle Testprojekte an einem Ort – üblich bei Open-Source-.NET-Projekten (vgl. `dotnet/runtime`, `dotnet/aspnetcore`) |
| `docs/` für Dokumentation | Macht GitHub's automatische Docs-Integration nutzbar; klar abgegrenzt von Code |
| `docs/internal/` für Planungsdokumente | Planungs- und Analyse-Dateien gehören nicht in den Root, sind aber weiterhin versionierbar und nachvollziehbar |
| `scripts/` für PowerShell-Skripte | Skripte aus Root-Verzeichnis heraus, da sie keine Library-Code-Dateien sind |

---

## 3. Neue und umstrukturierte Dateien

### 3.1 `LICENSE` (Root)

> **Status:** Muss erstellt werden – im `README.md` und `.csproj` bereits als MIT referenziert, die Datei selbst fehlt aber noch im Repository.

**Inhalt:** Standard-MIT-Lizenz

```text
MIT License

Copyright (c) 2024 KernUx Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

### 3.2 `CONTRIBUTING.md` (Root)

> **Status:** Neu erstellen

Abschnitte:

- **Verhaltenskodex** (Verweis auf `CODE_OF_CONDUCT.md`)
- **Wie melde ich einen Bug?** (Issue-Template-Link)
- **Wie schlage ich ein Feature vor?** (Issue-Template-Link)
- **Entwicklungsumgebung einrichten** (Voraussetzungen: .NET 10, Playwright für Smoke-Tests)
- **Branch-Strategie** (`main` = stabil, Feature-Branches, PR gegen `main`)
- **Commit-Konventionen** (Verweis auf `.github/git-commit-instructions.md` / Conventional Commits)
- **Pull-Request-Prozess** (PR-Template ausfüllen, Tests müssen grün sein, XML-Doku für öffentliche APIs)
- **KERN-UX-Komponenten hinzufügen** (Schritt-für-Schritt aus den Copilot-Instructions)
- **Coding-Standards** (Verweis auf `.github/copilot-instructions.md`)

---

### 3.3 `CODE_OF_CONDUCT.md` (Root)

> **Status:** Neu erstellen

Standard: **Contributor Covenant 2.1** (de).

Empfehlung: Die deutsche Verwaltungsbezogenheit des Projekts macht eine explizite, professionelle
Verhaltensregel wichtig. Contributor Covenant ist der de-facto-Standard auf GitHub.

---

### 3.4 `SECURITY.md` (Root)

> **Status:** Neu erstellen

Abschnitte:

- **Unterstützte Versionen** (Tabelle: welche Versionen erhalten Security-Updates)
- **Sicherheitslücke melden** (privates Reporting über GitHub Security Advisories – kein öffentliches Issue)
- **Reaktionszeit** (z.B. Erstreaktion innerhalb von 5 Werktagen)
- **Scope** (was ist im Scope: Library-Code, CSS; was ist out-of-scope: KERN-UX-Upstream)

---

### 3.5 `CHANGELOG.md` (Root)

> **Status:** Neu erstellen

Format: [Keep a Changelog](https://keepachangelog.com/de/1.0.0/) + [Semantic Versioning](https://semver.org/lang/de/).

```markdown
# Changelog

Alle bemerkenswerten Änderungen an diesem Projekt werden in dieser Datei dokumentiert.

Das Format basiert auf [Keep a Changelog](https://keepachangelog.com/de/1.0.0/),
und dieses Projekt hält sich an [Semantic Versioning](https://semver.org/lang/de/).

## [Unreleased]

## [0.1.0] – 2025-XX-XX

### Hinzugefügt
- 48 KERN-UX-Komponenten als typsichere Blazor-Komponenten
- WCAG 2.1 AA Barrierefreiheit (ARIA, semantisches HTML, Fokus-Management)
- Light- und Dark-Theme out-of-the-box
- `IdGeneratorService` für kollisionsfreie DOM-IDs
- `KernInputCurrency` als Library-Erweiterung (DACH-optimiert)
```

---

### 3.6 `.github/PULL_REQUEST_TEMPLATE.md`

> **Status:** Neu erstellen

```markdown
## Beschreibung

<!-- Was ändert dieser PR? Warum? -->

## Art der Änderung

- [ ] 🐛 Bugfix
- [ ] ✨ Neue Funktion
- [ ] 💥 Breaking Change
- [ ] 📝 Dokumentation
- [ ] ♻️ Refactoring
- [ ] 🔒 Sicherheit

## Checkliste

- [ ] Tests hinzugefügt oder aktualisiert
- [ ] XML-Dokumentation für neue öffentliche APIs vorhanden
- [ ] KERN-UX-Barrierefreiheits-Standards eingehalten (ARIA, semantisches HTML)
- [ ] `CHANGELOG.md` aktualisiert (bei Features/Bugfixes)
- [ ] Kein Bootstrap oder fremdes Design-System eingemischt

## Verwandte Issues

Closes #
```

---

### 3.7 `.github/ISSUE_TEMPLATE/bug_report.yml`

> **Status:** Neu erstellen (YAML-Format für strukturierte Issues)

Felder: Komponente, .NET-Version, Reproduktionsschritte, erwartetes/tatsächliches Verhalten,
Barrierefreiheitsbezug (ja/nein).

---

### 3.8 `.github/ISSUE_TEMPLATE/feature_request.yml`

> **Status:** Neu erstellen

Felder: KERN-UX-Komponente (Dropdown), Beschreibung, Begründung, WCAG-Relevanz.

---

### 3.9 `.github/workflows/ci.yml`

> **Status:** Neu erstellen

```yaml
name: CI

on:
  push:
    branches: [main]
  pull_request:
    branches: [main]

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.x'
      - name: Build
        run: dotnet build src/PublicKernBlazor.Components/PublicKernBlazor.Components.csproj --configuration Release
      - name: Unit-Tests
        run: dotnet test tests/PublicKernBlazor.Components.Tests/PublicKernBlazor.Components.Tests.csproj --configuration Release --logger "trx;LogFileName=test-results.trx"
      - name: Test-Ergebnisse hochladen
        uses: actions/upload-artifact@v4
        if: always()
        with:
          name: test-results
          path: "**/*.trx"
```

---

### 3.10 `.github/workflows/release.yml`

> **Status:** Neu erstellen

Trigger: `push` auf Tags `v*.*.*`.  
Schritte: Build → Test → `dotnet pack` → NuGet-Publish mit `NUGET_API_KEY`-Secret.

---

### 3.11 `.github/dependabot.yml`

> **Status:** Neu erstellen

```yaml
version: 2
updates:
  - package-ecosystem: "nuget"
    directory: "/src/PublicKernBlazor.Components"
    schedule:
      interval: "weekly"
    open-pull-requests-limit: 5

  - package-ecosystem: "github-actions"
    directory: "/"
    schedule:
      interval: "weekly"
```

---

### 3.12 `docs/contributing/development-setup.md`

> **Status:** Neu erstellen

Ausführliche Anleitung:
- Repository klonen
- .NET 10 SDK installieren
- SCSS-Kompilierung (SassCompiler läuft automatisch beim Build)
- Unit-Tests ausführen (`dotnet test`)
- Demo-App starten (`dotnet run --project src/PublicKernBlazor.Demo`)
- Smoke-Tests einrichten (Playwright-Installation)
- NuGet-Paket lokal bauen und testen

---

## 4. Interne Arbeitsdateien bereinigen

Die folgenden Dateien liegen aktuell im Repository-Root und sollten **verschoben** werden,
da sie keine Nutzer- oder Contributor-Relevanz haben:

| Aktuelle Datei | Ziel-Pfad | Begründung |
|---|---|---|
| `Issues.md` | `docs/internal/issues-smoketest.md` | Interne Testlauf-Analyse, kein öffentliches Dokument |
| `plain-to-blazor.md` | `docs/internal/plain-to-blazor.md` | Interner Implementierungsplan |
| `showcase-plan.md` | `docs/internal/showcase-plan.md` | Interner Demo-App-Plan |
| `Fix-SassIfDeprecation.ps1` | `scripts/Fix-SassIfDeprecation.ps1` | Hilfsskript gehört nicht in Root |
| `Run-SmokeTests.ps1` | `scripts/Run-SmokeTests.ps1` | Hilfsskript gehört nicht in Root |

> **Hinweis:** `KernUxExample.Blazor/` (das ältere Beispielprojekt) sollte evaluiert werden –
> entweder als `examples/KernUxExample.Blazor/` integrieren oder entfernen, falls es durch
> `PublicKernBlazor.Demo/` vollständig ersetzt wurde.

---

## 5. GitHub-spezifische Konfigurationen

### 5.1 Repository-Einstellungen (manuell auf GitHub)

| Einstellung | Empfehlung |
|---|---|
| **Topics** | `blazor`, `dotnet`, `kern-ux`, `design-system`, `accessibility`, `wcag`, `verwaltung` |
| **Description** | `KERN-UX Design System als Blazor Component Library – typsichere, barrierefreie Komponenten für die deutsche Verwaltung (WCAG 2.1 AA)` |
| **Website** | `https://www.kern-ux.de` |
| **Branch Protection** auf `main` | Require PR, require status checks (CI), no force push |
| **Discussions** | Aktivieren für Community-Austausch |
| **Security Advisories** | Aktivieren für private Vulnerability-Meldungen |
| **Releases** | Mit `CHANGELOG.md`-Inhalt pflegen |

### 5.2 README.md – Ergänzungen

Das bestehende `README.md` ist bereits sehr gut. Folgende Ergänzungen werden empfohlen:

```markdown
## Beitragen

Contributions sind willkommen! Bitte lies zuerst [CONTRIBUTING.md](CONTRIBUTING.md).

[![CI](https://github.com/OWNER/PublicKernBlazor.Components/actions/workflows/ci.yml/badge.svg)](...)
[![NuGet](https://img.shields.io/nuget/v/PublicKernBlazor.Components)](...)
[![Lizenz: MIT](https://img.shields.io/badge/Lizenz-MIT-yellow)](LICENSE)
```

Zusätzlich: Den Abschnitt **Projektstruktur** mit den neuen Pfaden (`src/`, `tests/`) aktualisieren.

### 5.3 `.gitignore` – Prüfen und ergänzen

Sicherstellen, dass folgende Einträge vorhanden sind:

```gitignore
# Build-Ausgaben
test-results.trx
build-output.txt

# Playwright-Artefakte
playwright-report/
test-results/

# NuGet-Pakete (lokal gebaut)
*.nupkg
```

---

## 6. Migrations-Checkliste

Die folgende Checkliste beschreibt die empfohlene Reihenfolge der Umsetzung:

### Phase 1 – Pflicht-Dateien (sofort)

- [x] `LICENSE` im Root erstellen (MIT)
- [x] `CODE_OF_CONDUCT.md` erstellen (Contributor Covenant 2.1)
- [x] `SECURITY.md` erstellen
- [x] `CONTRIBUTING.md` erstellen

### Phase 2 – Verzeichnisstruktur

- [x] Verzeichnisse `src/`, `tests/`, `docs/`, `docs/internal/`, `scripts/` anlegen
- [x] Projekte verschieben: `PublicKernBlazor.Components/` → `src/PublicKernBlazor.Components/`
- [x] Projekte verschieben: `PublicKernBlazor.Demo/` → `src/PublicKernBlazor.Demo/`
- [x] Projekte verschieben: `PublicKernBlazor.Components.Tests/` → `tests/PublicKernBlazor.Components.Tests/`
- [x] Projekte verschieben: `PublicKernBlazor.Demo.SmokeTests/` → `tests/PublicKernBlazor.Demo.SmokeTests/`
- [x] `PublicKernBlazor.slnx` – alle Projekt-Pfade aktualisieren
- [x] `.csproj`-Referenzen (z.B. `../README.md`) auf neue Pfade anpassen
- [x] Interne Planungsdateien nach `docs/internal/` verschieben
- [x] Skripte nach `scripts/` verschieben

### Phase 3 – GitHub-Workflows und Vorlagen

- [x] `.github/PULL_REQUEST_TEMPLATE.md` erstellen
- [x] `.github/ISSUE_TEMPLATE/bug_report.yml` erstellen
- [x] `.github/ISSUE_TEMPLATE/feature_request.yml` erstellen
- [x] `.github/workflows/ci.yml` erstellen und testen
- [x] `.github/workflows/release.yml` erstellen
- [x] `.github/dependabot.yml` erstellen
- [ ] `NUGET_API_KEY`-Secret in GitHub Repository Settings hinterlegen

### Phase 4 – Dokumentation vervollständigen

- [x] `CHANGELOG.md` mit Version 0.1.0 befüllen
- [x] `docs/contributing/development-setup.md` erstellen
- [x] `README.md` – Badges und Abschnitt „Beitragen" ergänzen
- [x] `README.md` – Projektstruktur-Abschnitt auf neue Pfade aktualisieren

### Phase 5 – Repository-Einstellungen (auf GitHub)

- [ ] Topics setzen
- [ ] Description und Website-URL eintragen
- [ ] Branch-Protection-Rules für `main` aktivieren
- [ ] GitHub Discussions aktivieren
- [ ] Security Advisories aktivieren

### Phase 6 – Evaluierung

- [ ] `KernUxExample.Blazor/` – Entscheiden: nach `examples/` verschieben oder entfernen
- [ ] ADR für zentrale Architekturentscheidungen anlegen (optional)
- [ ] GitHub Pages für Dokumentation einrichten (optional, z.B. mit DocFX)

---

*Dieses Dokument ist ein lebendiger Vorschlag – Anpassungen sind jederzeit möglich.*

