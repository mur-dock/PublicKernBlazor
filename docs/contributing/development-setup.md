# Entwicklungsumgebung einrichten

Diese Anleitung beschreibt, wie du PublicKernBlazor.Components lokal zum Laufen bringst –
von der ersten Einrichtung bis zum ersten grünen Test.

---

## Voraussetzungen

| Werkzeug                                              | Mindestversion | Installationshinweis              |
|-------------------------------------------------------|----------------|-----------------------------------|
| [.NET SDK](https://dotnet.microsoft.com/download)     | **10.0**       | Pflicht                           |
| [Git](https://git-scm.com/)                           | –              | Pflicht                           |
| [Node.js](https://nodejs.org/)                        | **18 LTS**     | Nur für Smoke-Tests (Playwright)  |
| [PowerShell](https://learn.microsoft.com/powershell/) | 5.1 oder 7.x   | Für Hilfsskripte unter `scripts/` |

Eine IDE ist optional. Empfohlen werden:

- [JetBrains Rider](https://www.jetbrains.com/rider/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (17.12+)
- [VS Code](https://code.visualstudio.com/) mit der C#-Extension

---

## Repository klonen

```bash
git clone https://github.com/OWNER/PublicKernBlazor.Components.git
cd PublicKernBlazor.Components
```

---

## Solution bauen

```bash
dotnet build PublicKernBlazor.slnx
```

SCSS wird beim Build automatisch über `AspNetCore.SassCompiler` nach
`src/PublicKernBlazor.Components/wwwroot/css/` kompiliert – kein manueller Schritt nötig.

---

## Unit-Tests ausführen

```bash
dotnet test tests/PublicKernBlazor.Components.Tests/PublicKernBlazor.Components.Tests.csproj
```

Das Testprojekt verwendet [bUnit](https://bunit.dev/) und xUnit.
Alle Tests laufen ohne Browser oder laufenden Server.

### Test-Kategorien

| Kategorie        | Inhalt                                                          |
|------------------|-----------------------------------------------------------------|
| `Components/`    | Rendering-Tests und Interaktions-Tests für alle 48+ Komponenten |
| `Services/`      | Unit-Tests für `ThemeService` und `IdGeneratorService`          |
| `Utilities/`     | Tests für `CssBuilder` und `EnumCssExtensions`                  |
| `Accessibility/` | ARIA-Attribut- und Semantik-Audits (WCAG 2.1 AA)                |

---

## Demo-App starten

```bash
dotnet run --project src/PublicKernBlazor.Demo/PublicKernBlazor.Demo.csproj
```

Die App ist erreichbar unter:

| Protokoll | URL                      |
|-----------|--------------------------|
| HTTP      | `http://localhost:5076`  |
| HTTPS     | `https://localhost:7023` |

Beim ersten Start mit HTTPS muss das Entwicklerzertifikat einmalig vertraut werden:

```bash
dotnet dev-certs https --trust
```

---

## Smoke-Tests ausführen

Die Smoke-Tests verwenden [Playwright](https://playwright.dev/) und benötigen eine
**laufende Demo-App** (oder das Skript startet sie automatisch).

### Playwright-Browser einmalig installieren

```bash
dotnet build tests/PublicKernBlazor.Demo.SmokeTests/PublicKernBlazor.Demo.SmokeTests.csproj
pwsh tests/PublicKernBlazor.Demo.SmokeTests/bin/Debug/net10.0/playwright.ps1 install chromium
```

### Smoke-Tests über das Hilfsskript starten

Das Skript baut die Solution, startet die Demo-App, führt alle Tests aus und stoppt die App wieder:

```powershell
.\scripts\Run-SmokeTests.ps1
```

Optionale Parameter:

```powershell
# Browser sichtbar (für lokales Debugging)
.\scripts\Run-SmokeTests.ps1 -Headless $false

# Release-Konfiguration
.\scripts\Run-SmokeTests.ps1 -Configuration Release
```

---

## NuGet-Paket lokal bauen

```bash
dotnet pack src/PublicKernBlazor.Components/PublicKernBlazor.Components.csproj --configuration Release --output ./nupkg
```

Das fertige Paket liegt danach unter `nupkg/PublicKernBlazor.Components.0.1.0.nupkg`.

### Paket lokal testen

Um das Paket in einem anderen Projekt zu testen, ohne es auf nuget.org zu veröffentlichen:

```bash
# Lokale NuGet-Quelle registrieren (einmalig)
dotnet nuget add source ./nupkg --name KernUxLocal

# Im Zielprojekt installieren
dotnet add package PublicKernBlazor.Components --source KernUxLocal
```

---

## SCSS-Erweiterungen hinzufügen

Neue, projekt-eigene Stile gehören **nicht** in `src/PublicKernBlazor.Components/Styles/core/`
(dieser Ordner enthält den KERN-UX-Upstream und wird bei Updates überschrieben).

Stattdessen:

1. Neue `.scss`-Datei unter `src/PublicKernBlazor.Components/Styles/extensions/components/` anlegen
2. In `src/PublicKernBlazor.Components/Styles/extensions/index.scss` per `@use` einbinden
3. Ausschließlich KERN-Token (`var(--kern-color-*)`, `var(--kern-metric-space-*)`) verwenden

---

## Neue KERN-UX-Komponente hinzufügen

1. HTML-Struktur und CSS-Klassen in
   [`src/PublicKernBlazor.Components/Styles/COMPONENTS.MD`](../../src/PublicKernBlazor.Components/Styles/COMPONENTS.MD)
   nachschlagen
2. `.razor`-Datei im passenden Unterordner von `src/PublicKernBlazor.Components/Components/` anlegen
3. Alle `[Parameter]`-Properties mit XML-Dokumentation versehen
4. `IdGeneratorService` für DOM-IDs verwenden (niemals statische Fallback-IDs)
5. `@attributes="AdditionalAttributes"` am Host-Element weitergeben
6. Unit-Tests in `tests/PublicKernBlazor.Components.Tests/Components/` hinzufügen
7. Komponente in `src/PublicKernBlazor.Demo/` auf der passenden Showcase-Seite einbinden

Die vollständigen Coding-Richtlinien sind in
[`.github/copilot-instructions.md`](../../.github/copilot-instructions.md) dokumentiert.

