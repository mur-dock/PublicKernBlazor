using PublicKernBlazor.Demo.SmokeTests.Infrastructure;

namespace PublicKernBlazor.Demo.SmokeTests.Infrastructure;

/// <summary>
/// Gemeinsame Basisklasse für alle Smoke-Tests.
/// <para>
/// Erbt von <see cref="PageTest"/> aus <c>Microsoft.Playwright.NUnit</c>. Diese Klasse
/// übernimmt automatisch den vollständigen Browser-Lifecycle: Playwright starten,
/// Browser-Kontext anlegen, <c>Page</c>-Objekt bereitstellen, nach dem Test aufräumen.
/// </para>
/// <para>
/// Eigene Erweiterungen hier:
/// <list type="bullet">
///   <item>Standard-Viewport und Timeout konfigurieren</item>
///   <item>Trace-Recording aktivieren</item>
///   <item>Bei Fehlschlag: Screenshot + Trace-Archiv speichern</item>
/// </list>
/// </para>
/// </summary>
[Parallelizable(ParallelScope.Self)]
public abstract class BaseSmokeTest : PageTest
{
    // ── Playwright-Konfiguration ──────────────────────────────────────────────

    /// <summary>
    /// Konfiguriert den Browser-Kontext mit Viewport und Video-Recording-Verzeichnis.
    /// <para>
    /// <c>PageTest</c> (Playwright 1.50) erlaubt als einzigen Override <c>ContextOptions()</c>
    /// und <c>ConnectOptionsAsync()</c>. Der Viewport wird daher hier gesetzt; er gilt dann
    /// für alle Pages, die innerhalb dieses Kontexts geöffnet werden.
    /// </para>
    /// </summary>
    public override BrowserNewContextOptions ContextOptions() => new()
    {
        ViewportSize   = new ViewportSize { Width = 1280, Height = 800 },
        // Videos werden immer aufgenommen; bei bestandenen Tests wird der Ordner geleert.
        RecordVideoDir = System.IO.Path.Combine(TestConstants.ArtifactDir, "videos")
    };

    // ── Setup / Teardown ──────────────────────────────────────────────────────

    /// <summary>Startet das Trace-Recording vor jedem einzelnen Test.</summary>
    [SetUp]
    public async Task StartTracing()
    {
        await Context.Tracing.StartAsync(new()
        {
            Title       = TestContext.CurrentContext.Test.FullName,
            Screenshots = true,   // DOM-Snapshot bei jeder Aktion
            Snapshots   = true,   // CSS + Ressourcen für Trace-Viewer
            Sources     = true    // Quelldatei-Links im Trace-Viewer
        });
    }

    /// <summary>
    /// Speichert bei Fehlschlag einen vollseitigen Screenshot und ein Trace-Archiv.
    /// Beides ist im Playwright Trace Viewer (<c>playwright show-trace</c>) öffenbar.
    /// </summary>
    [TearDown]
    public async Task SaveArtifactsOnFailure()
    {
        var outcome  = TestContext.CurrentContext.Result.Outcome.Status;
        var testName = SanitizeFileName(TestContext.CurrentContext.Test.FullName);

        var screenshotDir = System.IO.Path.Combine(TestConstants.ArtifactDir, "screenshots");
        var traceDir      = System.IO.Path.Combine(TestConstants.ArtifactDir, "traces");

        System.IO.Directory.CreateDirectory(screenshotDir);
        System.IO.Directory.CreateDirectory(traceDir);

        // Trace immer stoppen – bei Erfolg kein Speichern
        var tracePath = outcome == NUnit.Framework.Interfaces.TestStatus.Failed
            ? System.IO.Path.Combine(traceDir, $"{testName}.zip")
            : null;

        await Context.Tracing.StopAsync(new() { Path = tracePath });

        if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            await Page.ScreenshotAsync(new()
            {
                Path     = System.IO.Path.Combine(screenshotDir, $"{testName}.png"),
                FullPage = true
            });

            TestContext.Out.WriteLine($"Screenshot: {screenshotDir}/{testName}.png");
            TestContext.Out.WriteLine($"Trace:      {traceDir}/{testName}.zip");
            TestContext.Out.WriteLine("Trace öffnen: pwsh playwright.ps1 show-trace <pfad>");
        }
    }

    // ── Hilfsmethoden ─────────────────────────────────────────────────────────

    /// <summary>
    /// Navigiert zur angegebenen Route und setzt den Standard-Timeout.
    /// Gibt die fertig geladene Seite zurück, damit Assertion-Chaining möglich ist.
    /// </summary>
    protected async Task<IPage> GotoAsync(string route)
    {
        Page.SetDefaultTimeout(TestConstants.DefaultTimeoutMs);
        await Page.GotoAsync(TestConstants.BaseUrl + route,
            new() { WaitUntil = WaitUntilState.NetworkIdle });
        return Page;
    }

    /// <summary>Ersetzt Zeichen, die in Dateinamen unzulässig sind.</summary>
    private static string SanitizeFileName(string name) =>
        System.Text.RegularExpressions.Regex
            .Replace(name, @"[\\/:*?""<>|]", "_")
            .Replace(" ", "_");
}

