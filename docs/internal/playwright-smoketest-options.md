# Playwright Smoke-Tests – Optionen für PublicKernBlazor.Demo

Dieses Dokument beschreibt die Alternativen zur Automatisierung der Smoke-Tests
(Punkt 23.2 im `showcase-plan.md`) mit [Microsoft Playwright für .NET](https://playwright.dev/dotnet/).

---

## Gemeinsame Grundlage

Alle Optionen teilen dieselben NuGet-Pakete und denselben Browser-Installationsschritt:

```xml
<!-- PublicKernBlazor.Demo.SmokeTests.csproj -->
<PackageReference Include="Microsoft.Playwright" Version="*" />

<!-- je nach gewähltem Test-Framework -->
<PackageReference Include="Microsoft.Playwright.NUnit" Version="*" />
<!-- oder -->
<PackageReference Include="Microsoft.Playwright.MSTest" Version="*" />
<!-- oder nur das Basis-Paket für xUnit -->
```

Browser-Installation (einmalig, lokal und in CI):

```powershell
# Nach dotnet build
pwsh bin/Debug/net10.0/playwright.ps1 install chromium
```

---

## Option 1 – NUnit + Playwright (Microsoft-Standard)

**Test-Framework:** NUnit  
**Struktur:** Flach, eine Klasse je Testkategorie  
**Architektur:** `PageTest`-Basisklasse aus `Microsoft.Playwright.NUnit`

### Beschreibung

Das offizielle Integrationspaket `Microsoft.Playwright.NUnit` stellt die Basisklasse `PageTest`
bereit. Sie übernimmt den vollständigen Browser-Lifecycle (Starten, Kontext anlegen, Seite öffnen,
Aufräumen) automatisch. Jede Testklasse erbt von `PageTest` und erhält direkt Zugriff auf `Page`.

### Projektstruktur

```
PublicKernBlazor.Demo.SmokeTests/
├── PublicKernBlazor.Demo.SmokeTests.csproj
└── Smoke/
    ├── NavigationSmokeTests.cs
    ├── ThemeSmokeTests.cs
    ├── InteractiveSmokeTests.cs
    └── ResponsiveSmokeTests.cs
```

### Beispiel

```csharp
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class NavigationSmokeTests : PageTest   // PageTest aus Microsoft.Playwright.NUnit
{
    private const string BaseUrl = "https://localhost:7023";

    [TestCase("/")]
    [TestCase("/components/typography")]
    [TestCase("/components/buttons")]
    [TestCase("/components/forms")]
    [TestCase("/components/feedback")]
    [TestCase("/components/content")]
    [TestCase("/components/navigation")]
    [TestCase("/components/icons")]
    [TestCase("/examples/antrag")]
    [TestCase("/examples/dashboard")]
    [TestCase("/not-found")]
    public async Task Seite_LaeadtOhneFehler(string route)
    {
        await Page.GotoAsync(BaseUrl + route);

        // Blazor-Fehler-Overlay darf nicht sichtbar sein
        await Expect(Page.Locator("#blazor-error-ui")).ToBeHiddenAsync();
    }
}
```

### Bewertung

| Aspekt | Einschätzung |
|---|---|
| Einstiegshürde | 🟢 Niedrig – Basisklasse übernimmt Browser-Setup |
| Nähe zu Playwright | 🟢 Sehr hoch – `PageTest` ist die offizielle Integration |
| Parallelisierung | 🟢 Direkt über `[Parallelizable]` konfigurierbar |
| Offizielle Unterstützung durch Microsoft | 🟢 Ja |
| Konsistenz mit `PublicKernBlazor.Components.Tests` (xUnit) | 🟡 Anderes Framework, aber separates Projekt |
| Geeignet als Einstieg | ✅ **Empfohlen** |

---

## Option 2 – xUnit + Playwright (ohne offizielle Integration)

**Test-Framework:** xUnit  
**Struktur:** Flach, eine Klasse je Testkategorie  
**Architektur:** Manuelle Initialisierung über `IAsyncLifetime`

### Beschreibung

xUnit ist das Test-Framework des bestehenden Projekts (`PublicKernBlazor.Components.Tests`). Da es kein
offizielles `Microsoft.Playwright.XUnit`-Paket gibt, wird der Browser-Lifecycle über
`IAsyncLifetime` vollständig selbst verwaltet. Das GWT-Pattern aus den Coding-Guidelines lässt
sich direkt anwenden – allerdings auf Kosten von mehr Boilerplate je Testklasse und ohne die
Playwright-nahen Hilfsfunktionen (z.B. `Expect`, `ContextOptions`), die `PageTest` mitbringt.

### Projektstruktur

```
PublicKernBlazor.Demo.SmokeTests/
├── PublicKernBlazor.Demo.SmokeTests.csproj
└── Smoke/
    ├── NavigationSmokeTests.cs
    ├── ThemeSmokeTests.cs
    ├── InteractiveSmokeTests.cs
    └── ResponsiveSmokeTests.cs
```

### Beispiel

```csharp
public class ThemeSmokeTests : IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IBrowserContext _context = null!;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync();
        _context = await _browser.NewContextAsync();
    }

    [Fact(DisplayName = "Theme-Cookie wird nach Toggle auf Dark gesetzt")]
    public async Task ThemeToggle_SetztkernThemeCookieAufDark()
    {
        // Given – frische Seite ohne Cookie
        var page = await _context.NewPageAsync();
        await page.GotoAsync("https://localhost:7023");

        // When – Theme-Toggle-Button klicken
        await page.ClickAsync("[title='Theme wechseln']");

        // Then – Cookie kern-theme muss den Wert "dark" haben
        var cookies = await _context.CookiesAsync();
        Assert.Contains(cookies, c => c.Name == "kern-theme" && c.Value == "dark");
    }

    public async Task DisposeAsync()
    {
        await _context.DisposeAsync();
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}
```

### Bewertung

| Aspekt | Einschätzung |
|---|---|
| Einstiegshürde | 🟡 Mittel – kein Basisklassen-Scaffolding, mehr Boilerplate |
| Nähe zu Playwright | 🔴 Gering – kein offizielles `Playwright.XUnit`-Paket |
| Konsistenz mit Bestandsprojekt (`PublicKernBlazor.Components.Tests`) | 🟢 Ja – xUnit + GWT-Pattern direkt verwendbar |
| Parallelisierung | 🟡 Über `[Collection]`-Isolation steuerbar |
| Boilerplate für Setup/Teardown | 🔴 Höher als Option 1 |
| Geeignet als Einstieg | 🟡 Nur wenn Konsistenz zu xUnit explizit gewünscht |

---

## Option 3 – xUnit + Playwright + `WebApplicationFactory` (In-Process)

**Test-Framework:** xUnit  
**Struktur:** Fixture-basiert mit gemeinsamer App-Instanz  
**Architektur:** Demo-App wird im Testprozess selbst gehostet – kein externer `dotnet run` nötig

### Beschreibung

`Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory<TEntryPoint>` startet die Demo-App
direkt im Testprozess auf einem zufälligen freien Port. Playwright verbindet sich gegen diese
lokale URL. Dadurch entfällt die manuelle App-Vorbereitung vor dem Testlauf, und Port-Konflikte
in CI-Umgebungen werden vermieden.

> **Hinweis:** Blazor-`InteractiveServer`-Komponenten benötigen eine echte WebSocket-Verbindung
> zum Server. Die `WebApplicationFactory` unterstützt dies seit .NET 8 mit
> `UseStatefulReconnect()`. Vollständige Interaktivität muss separat verifiziert werden.

### Projektstruktur

```
PublicKernBlazor.Demo.SmokeTests/
├── PublicKernBlazor.Demo.SmokeTests.csproj
├── Infrastructure/
│   └── DemoAppFixture.cs          ← startet die App, stellt BaseUrl bereit
└── Smoke/
    ├── NavigationSmokeTests.cs
    └── InteractiveSmokeTests.cs
```

### Beispiel

```csharp
// Infrastructure/DemoAppFixture.cs
public class DemoAppFixture : IAsyncLifetime
{
    private WebApplication? _app;
    public string BaseUrl { get; private set; } = string.Empty;

    public async Task InitializeAsync()
    {
        // Minimaler Host analog zu Program.cs der Demo-App
        var builder = WebApplication.CreateBuilder();
        builder.Services.AddRazorComponents().AddInteractiveServerComponents();
        builder.Services.AddKernUx();
        builder.WebHost.UseUrls("http://127.0.0.1:0");  // zufälliger freier Port

        _app = builder.Build();
        _app.MapStaticAssets();
        _app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

        await _app.StartAsync();
        BaseUrl = _app.Urls.First();
    }

    public async Task DisposeAsync()
    {
        if (_app is not null)
            await _app.StopAsync();
    }
}

// Smoke/NavigationSmokeTests.cs
public class NavigationSmokeTests(DemoAppFixture fixture)
    : IClassFixture<DemoAppFixture>, IAsyncLifetime
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;

    public async Task InitializeAsync()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync();
    }

    [Fact(DisplayName = "Startseite lädt ohne Fehler-Overlay")]
    public async Task Startseite_LaeadtOhneFehler()
    {
        var page = await _browser.NewPageAsync();
        await page.GotoAsync(fixture.BaseUrl + "/");
        await Assertions.Expect(page.Locator("#blazor-error-ui")).ToBeHiddenAsync();
    }

    public async Task DisposeAsync()
    {
        await _browser.DisposeAsync();
        _playwright.Dispose();
    }
}
```

### Bewertung

| Aspekt | Einschätzung |
|---|---|
| Einstiegshürde | 🔴 Hoch – App-Hosting im Test erfordert Kenntnisse von ASP.NET Core Internals |
| CI-Tauglichkeit | 🟢 Kein externer Prozess, kein Port-Konflikt |
| Parallelisierung | 🟢 Eine Fixture-Instanz für alle Tests derselben Klasse |
| Unterstützung für InteractiveServer | 🟡 Erfordert zusätzliche Konfiguration |
| Geeignet als Einstieg | ❌ Nein – besser als Ausbaustufe von Option 2 |

---

## Option 4 – NUnit + Playwright + Page Object Model (POM)

**Test-Framework:** NUnit  
**Struktur:** Page-Object-Klassen je Demo-Seite, Tests nur mit Aktionsaufrufen  
**Architektur:** Strikte Trennung von Selektoren (Page Objects) und Testlogik (Test-Klassen)

### Beschreibung

Das Page Object Model kapselt alle Selektoren und Aktionen einer Seite in einer dedizierten
Klasse. Tests selbst enthalten keine CSS-Selektoren mehr – sie rufen nur noch sprechende
Methoden auf. Das erhöht die Wartbarkeit erheblich, sobald die Suite wächst.

### Projektstruktur

```
PublicKernBlazor.Demo.SmokeTests/
├── PublicKernBlazor.Demo.SmokeTests.csproj
├── Infrastructure/
│   └── PlaywrightFixture.cs       ← Browser-Lifecycle (SetUpFixture)
├── PageObjects/
│   ├── BasePage.cs                ← GotoAsync, PageTitle prüfen, Fehler-Overlay prüfen
│   ├── AntragPage.cs              ← Selektoren + Aktionen für /examples/antrag
│   ├── DashboardPage.cs           ← Selektoren für /examples/dashboard
│   └── ContentPage.cs             ← Dialog öffnen/schließen auf /components/content
└── Smoke/
    ├── NavigationSmokeTests.cs
    ├── ThemeSmokeTests.cs
    └── InteractiveSmokeTests.cs
```

### Beispiel

```csharp
// PageObjects/BasePage.cs
public abstract class BasePage(IPage page, string route)
{
    protected const string BaseUrl = "https://localhost:7023";

    public Task NavigiereAsync() => page.GotoAsync(BaseUrl + route);
    public Task<string?> GetPageTitleAsync() => page.TitleAsync();
    public ILocator FehlerOverlay => page.Locator("#blazor-error-ui");
}

// PageObjects/AntragPage.cs
public class AntragPage(IPage page) : BasePage(page, "/examples/antrag")
{
    public Task FuelleVornameAsync(string wert) =>
        page.FillAsync("input[placeholder*='Vorname']", wert);

    public Task FuelleNachnameAsync(string wert) =>
        page.FillAsync("input[placeholder*='Nachname']", wert);

    public Task KlickeWeiterAsync() =>
        page.ClickAsync("button:has-text('Weiter')");

    public ILocator Erfolgsmeldung =>
        page.Locator(".kern-alert--success");
}

// Smoke/InteractiveSmokeTests.cs
[TestFixture]
public class InteractiveSmokeTests : PageTest
{
    [Test(Description = "Antragsstrecke läuft vollständig durch")]
    public async Task Antrag_VollstaendigerDurchlauf()
    {
        // Given
        var antragPage = new AntragPage(Page);
        await antragPage.NavigiereAsync();
        await Expect(antragPage.FehlerOverlay).ToBeHiddenAsync();

        // When – Schritt 1 ausfüllen und weiter
        await antragPage.FuelleVornameAsync("Max");
        await antragPage.FuelleNachnameAsync("Mustermann");
        await antragPage.KlickeWeiterAsync();

        // (weitere Schritte) ...

        // Then – Erfolgsmeldung muss sichtbar sein
        await Expect(antragPage.Erfolgsmeldung).ToBeVisibleAsync();
    }
}
```

### Bewertung

| Aspekt | Einschätzung |
|---|---|
| Einstiegshürde | 🟡 Mittel – mehr initialer Aufwand |
| Wartbarkeit bei wachsender Suite | 🟢 Sehr hoch – Selektoren zentral verwaltet |
| Lesbarkeit der Tests | 🟢 Tests lesen sich wie Prosa |
| Sinnvoll ab | ca. 15–20 Tests |
| Geeignet als Einstieg | ❌ Nein – besser als Ausbaustufe von Option 1 |

---

## Option 5 – NUnit + Playwright + Trace/Screenshot bei Fehler (CI-ready)

**Test-Framework:** NUnit  
**Struktur:** Wie Option 1, ergänzt um `TearDown`-Hook und Trace-Recording  
**Architektur:** Gemeinsame Basisklasse `BaseSmokeTest`, die bei fehlgeschlagenen Tests
automatisch Screenshots und Playwright-Traces speichert

### Beschreibung

Ergänzt Option 1 um eine Basisklasse `BaseSmokeTest`, die im `TearDown` bei einem
fehlgeschlagenen Test automatisch:
- einen vollseitigen Screenshot erstellt,
- ein Playwright-Trace-Archiv (`.zip`) speichert,
- optional ein Video aufgezeichnet hat.

Diese Artefakte werden in CI-Systemen (GitHub Actions, GitLab CI) als Build-Artefakte
hochgeladen und ermöglichen Post-mortem-Debugging ohne erneuten Testlauf.

### Projektstruktur

```
PublicKernBlazor.Demo.SmokeTests/
├── PublicKernBlazor.Demo.SmokeTests.csproj
├── Infrastructure/
│   ├── PlaywrightSetup.cs         ← Browser-Installation via [OneTimeSetUp]
│   └── BaseSmokeTest.cs           ← Screenshot + Trace bei Fehler
└── Smoke/
    ├── NavigationSmokeTests.cs
    ├── ThemeSmokeTests.cs
    └── InteractiveSmokeTests.cs
```

### Beispiel

```csharp
// Infrastructure/PlaywrightSetup.cs
[SetUpFixture]
public class PlaywrightSetup
{
    [OneTimeSetUp]
    public static void GlobalSetUp() =>
        // Installiert Chromium automatisch beim ersten CI-Lauf
        Program.Main(["install", "chromium"]);
}

// Infrastructure/BaseSmokeTest.cs
public abstract class BaseSmokeTest : PageTest
{
    private const string ArtifactDir = "test-results";

    [TearDown]
    public async Task SpeichereArtefakteBeiFehlschlag()
    {
        var status = TestContext.CurrentContext.Result.Outcome.Status;
        if (status != NUnit.Framework.Interfaces.TestStatus.Failed)
            return;

        var testName = TestContext.CurrentContext.Test.Name
            .Replace(" ", "_")
            .Replace("/", "-");

        Directory.CreateDirectory($"{ArtifactDir}/screenshots");
        Directory.CreateDirectory($"{ArtifactDir}/traces");

        // Vollseitiger Screenshot
        await Page.ScreenshotAsync(new()
        {
            Path = $"{ArtifactDir}/screenshots/{testName}.png",
            FullPage = true
        });

        // Playwright-Trace (ZIP mit DOM-Snapshots, Netzwerk, Konsole)
        await Context.Tracing.StopAsync(new()
        {
            Path = $"{ArtifactDir}/traces/{testName}.zip"
        });
    }

    // Trace bereits beim Kontextstart aktivieren
    protected override BrowserNewContextOptions ContextOptions() => new()
    {
        RecordVideoDir = $"{ArtifactDir}/videos/",
        ViewportSize = new() { Width = 1280, Height = 800 }
    };

    public override async Task<IBrowserContext> NewContextAsync(
        BrowserNewContextOptions? options = null)
    {
        var context = await base.NewContextAsync(options ?? ContextOptions());
        await context.Tracing.StartAsync(new()
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });
        return context;
    }
}

// Smoke/NavigationSmokeTests.cs
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class NavigationSmokeTests : BaseSmokeTest
{
    private const string BaseUrl = "https://localhost:7023";

    [TestCase("/", "PublicKernBlazor.Components Showcase")]
    [TestCase("/components/typography", "Typografie - PublicKernBlazor.Components Showcase")]
    [TestCase("/examples/antrag", "Antragsstrecke - PublicKernBlazor.Components Showcase")]
    [TestCase("/not-found", "Seite nicht gefunden - PublicKernBlazor.Components Showcase")]
    public async Task Seite_HatKorrektenPageTitle(string route, string erwartetterTitel)
    {
        await Page.GotoAsync(BaseUrl + route);

        await Expect(Page).ToHaveTitleAsync(erwartetterTitel);
        await Expect(Page.Locator("#blazor-error-ui")).ToBeHiddenAsync();
    }
}
```

### CI-Konfiguration (GitHub Actions – Beispiel)

```yaml
# .github/workflows/smoke-tests.yml
- name: Smoke-Tests ausführen
  run: dotnet test PublicKernBlazor.Demo.SmokeTests --no-build

- name: Test-Artefakte hochladen
  if: failure()
  uses: actions/upload-artifact@v4
  with:
    name: playwright-artefakte
    path: |
      test-results/screenshots/
      test-results/traces/
      test-results/videos/
```

### Bewertung

| Aspekt | Einschätzung |
|---|---|
| Einstiegshürde | 🟡 Mittel – Basisklasse einmalig einrichten |
| CI-Tauglichkeit | 🟢 Sehr gut – Traces + Screenshots als Artefakte |
| Debuggbarkeit bei Fehlschlag | 🟢 Playwright Trace Viewer zeigt exakten Fehlerzeitpunkt |
| Laufzeit | 🟡 Video-Recording erhöht Testzeit leicht |
| Geeignet als Einstieg | ✅ Ja, wenn CI von Anfang an gewünscht |

---

## Entscheidungsmatrix

| Kriterium | Option 1 (NUnit) | Option 2 (xUnit) | Option 3 (In-Process) | Option 4 (POM) | Option 5 (CI-ready) |
|---|:---:|:---:|:---:|:---:|:---:|
| Einstiegshürde | 🟢 Niedrig | 🟡 Mittel | 🔴 Hoch | 🟡 Mittel | 🟡 Mittel |
| Nähe zu Playwright (offizielle Integration) | 🟢 | 🔴 | 🟢 | 🟢 | 🟢 |
| Konsistenz mit `PublicKernBlazor.Components.Tests` (xUnit) | 🟡 | 🟢 | 🟢 | 🟡 | 🟡 |
| CI-Tauglichkeit | 🟡 | 🟡 | 🟢 | 🟡 | 🟢 |
| Wartbarkeit bei Wachstum | 🟡 | 🟡 | 🟡 | 🟢 | 🟢 |
| Kein externer App-Prozess | ❌ | ❌ | ✅ | ❌ | ❌ |
| Automatische Artefakte bei Fehler | ❌ | ❌ | ❌ | ❌ | ✅ |

---

## Empfehlung für dieses Projekt

**Einstieg:** **Option 1** (NUnit + `PageTest`) – die offizielle Playwright-Integration bietet
den geringsten Aufwand und den vollen Funktionsumfang: automatischer Browser-Lifecycle,
`Expect`-Assertions, `ContextOptions`-Overrides, `[Parallelizable]`-Support. Konsistenz zu
`PublicKernBlazor.Components.Tests` (xUnit) ist kein zwingendes Kriterium, da die Smoke-Suite als eigenes,
separates Projekt geführt wird.

**Mittelfristig:** **Option 4** (POM) auf Basis von Option 1 – sobald mehr als ~15 Tests
vorhanden sind, lohnt sich die Investition in Page-Object-Klassen. Selektoren werden
zentralisiert, Tests bleiben prosaisch lesbar.

**Für CI:** **Option 5** als ergänzende Basisklasse für Option 1 – `BaseSmokeTest` erweitert
`PageTest` um Screenshot- und Trace-Recording. Einmalig einrichten, dann automatisch in allen
Testklassen verfügbar.

**Nicht empfohlen als Einstieg:** Option 2 (xUnit) wegen fehlendem offiziellen Paket und
höherem Boilerplate; Option 3 wegen komplexem In-Process-Hosting.

---

## PowerShell-Skript: Showcase starten und Tests ausführen

Das Skript `Run-SmokeTests.ps1` übernimmt den vollständigen Ablauf:

1. Testprojekt auf Existenz prüfen (frühzeitiger Abbruch mit Hinweis)
2. Solution bauen (Build-Log in `build-output.txt`)
3. Playwright-Browser installieren (falls noch nicht vorhanden)
4. Demo-App im Hintergrund starten und auf Bereitschaft warten (max. 30 s)
5. Smoke-Tests ausführen, Ausgabe live auf Konsole **und** in `smoke-test-results.txt`
6. Ergebnis-Zusammenfassung ausgeben
7. Demo-App stoppen und Exit-Code weiterleiten

Das vollständige Skript liegt im Solution-Root: **`Run-SmokeTests.ps1`**

```powershell
# Ausschnitt – vollständige Version: Run-SmokeTests.ps1 im Solution-Root

param(
    [ValidateSet("Debug", "Release")]
    [string] $Configuration = "Debug",
    [bool]   $Headless       = $true
)

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8

# Schritt 4 – App starten und auf Bereitschaft warten
$AppProcess = Start-Process "dotnet" `
    -ArgumentList "run --project `"$DemoProject`" --configuration $Configuration --no-build" `
    -PassThru -WindowStyle Hidden

# Schritt 5 – Tests ausführen, Ausgabe in Datei UND Konsole
$Env:PLAYWRIGHT_HEADLESS = if ($Headless) { "1" } else { "0" }
dotnet test "$TestProject" --no-build `
    --logger "console;verbosity=normal" `
    --results-directory "$ResultsDir" `
    2>&1 | Tee-Object -FilePath $ResultsFile -Encoding utf8

$TestExitCode = $LASTEXITCODE

# Schritt 7 – App stoppen (im finally-Block)
Stop-Process -Id $AppProcess.Id -Force

exit $TestExitCode
```

### Verwendung

```powershell
# Lokal mit sichtbarem Browser
.\Run-SmokeTests.ps1 -Headless $false

# CI / headless (Standard)
.\Run-SmokeTests.ps1

# Release-Build testen
.\Run-SmokeTests.ps1 -Configuration Release
```

### Exit-Codes

| Code | Bedeutung |
|---|---|
| `0` | Alle Tests bestanden |
| `1` | Mindestens ein Test fehlgeschlagen oder Build-Fehler |

### Speicherort

Das Skript gehört in das Stammverzeichnis der Solution neben `KernUxExample.slnx`:

```
KernUxExample/
├── KernUxExample.slnx
├── Run-SmokeTests.ps1          ← hier ablegen
├── PublicKernBlazor.Components/
├── PublicKernBlazor.Demo/
└── PublicKernBlazor.Demo.SmokeTests/   ← noch zu erstellen (Phase E oder CI-Setup)
```

