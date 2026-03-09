# Mitwirken an KernUx.Blazor

Vielen Dank für dein Interesse, zu **KernUx.Blazor** beizutragen!
Dieses Dokument erklärt, wie du Fehler melden, Features vorschlagen und Code beisteuern kannst.

---

## Verhaltenskodex

Dieses Projekt hält sich an den [Contributor Covenant 2.1](CODE_OF_CONDUCT.md).
Bitte lies ihn, bevor du dich beteiligst. Durch deine Mitwirkung stimmst du zu,
diesen Kodex einzuhalten.

---

## Wie melde ich einen Bug?

1. Prüfe zunächst, ob das Problem bereits als [Issue](../../issues) gemeldet wurde.
2. Falls nicht, erstelle ein neues Issue über die **Bug-Report-Vorlage**.
3. Fülle alle Felder aus – insbesondere:
   - Betroffene Komponente (z. B. `KernDialog`, `KernInputText`)
   - .NET-Version und Blazor-Renderingmodus
   - Reproduktionsschritte (minimales Beispiel)
   - Erwartetes vs. tatsächliches Verhalten
   - Barrierefreiheitsbezug (ja/nein)

> **Sicherheitslücken** bitte **nicht** als öffentliches Issue melden –
> siehe [SECURITY.md](SECURITY.md).

---

## Wie schlage ich ein Feature vor?

1. Prüfe, ob das Feature bereits diskutiert wird (Issues oder Discussions).
2. Erstelle ein neues Issue über die **Feature-Request-Vorlage**.
3. Beschreibe den Anwendungsfall und den Bezug zum
   [KERN-UX Design System](https://www.kern-ux.de/komponenten).

---

## Entwicklungsumgebung einrichten

### Voraussetzungen

| Werkzeug | Mindestversion | Zweck |
|---|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | **10.0** | Build, Tests, Demo-App |
| [Node.js](https://nodejs.org/) | 18 LTS | Playwright-Installation (Smoke-Tests) |
| Git | – | Versionskontrolle |

Eine IDE ist optional – [Rider](https://www.jetbrains.com/rider/) oder
[Visual Studio](https://visualstudio.microsoft.com/) werden empfohlen.

### Repository klonen und bauen

```bash
git clone https://github.com/OWNER/KernUx.Blazor.git
cd KernUx.Blazor
dotnet build
```

### Unit-Tests ausführen

```bash
dotnet test KernUx.Blazor.Tests/KernUx.Blazor.Tests.csproj
```

### Demo-App starten

```bash
dotnet run --project KernUx.Blazor.Demo/KernUx.Blazor.Demo.csproj
```

Die App ist erreichbar unter `https://localhost:7023`.

### Smoke-Tests (Playwright)

```powershell
# Playwright-Browser einmalig installieren
dotnet tool install --global Microsoft.Playwright.CLI
playwright install chromium

# Smoke-Tests ausführen (Demo-App muss laufen)
.\Run-SmokeTests.ps1
```

### SCSS-Kompilierung

SCSS wird beim Build automatisch über `AspNetCore.SassCompiler` nach `wwwroot/css/` kompiliert –
kein manueller Schritt nötig.

---

## Branch-Strategie

| Branch | Zweck |
|---|---|
| `main` | Stabiler Stand, entspricht dem letzten Release |
| `feature/<name>` | Neue Funktionen |
| `fix/<name>` | Fehlerbehebungen |
| `docs/<name>` | Ausschließlich Dokumentationsänderungen |

Pull Requests werden immer gegen `main` gestellt.

---

## Commit-Konventionen

Dieses Projekt verwendet **Conventional Commits**.
Alle Details sind in [`.github/git-commit-instructions.md`](.github/git-commit-instructions.md)
dokumentiert.

Kurzübersicht:

```
feat(accordion): add keyboard navigation support
fix(dialog): prevent focus trap leak on close
docs: update installation instructions
chore(deps): bump AspNetCore.SassCompiler to 1.98.0
```

---

## Pull-Request-Prozess

1. **Fork** erstellen und Feature-Branch von `main` abzweigen.
2. Änderungen implementieren – dabei die Coding-Standards einhalten (siehe unten).
3. **Tests hinzufügen oder aktualisieren** – alle bestehenden Tests müssen grün bleiben.
4. **`CHANGELOG.md`** unter `[Unreleased]` ergänzen (bei Features und Bugfixes).
5. Pull Request gegen `main` öffnen und die **PR-Vorlage** vollständig ausfüllen.
6. CI muss bestehen (Build + Unit-Tests).
7. Mindestens ein Review durch einen Maintainer ist erforderlich.

---

## Neue KERN-UX-Komponente hinzufügen

1. Komponentendokumentation in [`KernUx.Blazor/Styles/COMPONENTS.MD`](KernUx.Blazor/Styles/COMPONENTS.MD)
   nachschlagen – HTML-Struktur und CSS-Klassen übernehmen.
2. Neue `.razor`-Datei im passenden Unterordner von `KernUx.Blazor/Components/` anlegen
   (z. B. `Components/Forms/KernInputXyz.razor`).
3. Alle öffentlichen `[Parameter]`-Properties mit XML-Dokumentation versehen.
4. KERN-UX-Barrierefreiheitsstandards einhalten:
   - Semantisches HTML (`<button>`, `<fieldset>`, `<dialog>`, …)
   - ARIA-Attribute (`aria-describedby`, `aria-expanded`, `aria-hidden`, …)
   - `IdGeneratorService` für eindeutige DOM-IDs verwenden
5. Unit-Tests in `KernUx.Blazor.Tests/Components/` hinzufügen (bUnit, Given/When/Then).
6. Komponente in der Demo-App (`KernUx.Blazor.Demo/`) auf der passenden Showcase-Seite einbinden.

---

## Coding-Standards

Die vollständigen Coding-Richtlinien sind in
[`.github/copilot-instructions.md`](.github/copilot-instructions.md) dokumentiert.

Kurzfassung der wichtigsten Regeln:

- **Kein JavaScript**, außer wenn technisch unvermeidbar
- **Kein Bootstrap** oder anderes Design-System neben KERN-UX
- **BEM-Syntax** für CSS-Klassen (`kern-btn`, `kern-btn--primary`, `kern-btn__icon`)
- **ARIA-Attribute** niemals weglassen, Fokus-Styles niemals entfernen
- **XML-Dokumentation** für alle öffentlichen Parameter und EventCallbacks
- **Umlaute** in benutzersichtbaren Texten immer korrekt (ä, ö, ü, ß – nie ae/oe/ue/ss)
- **`AdditionalAttributes`** am Host-Element weitergeben (`@attributes="AdditionalAttributes"`)

---

## Lizenz

Durch das Einreichen eines Beitrags stimmst du zu, dass dein Beitrag unter der
[MIT-Lizenz](LICENSE) dieses Projekts veröffentlicht wird.

