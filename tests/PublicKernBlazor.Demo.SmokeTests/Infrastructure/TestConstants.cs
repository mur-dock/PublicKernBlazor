namespace PublicKernBlazor.Demo.SmokeTests.Infrastructure;

/// <summary>
/// Zentrale Konstanten für alle Smoke-Tests: URLs, Timeouts, Artefaktpfade.
/// Änderungen hier wirken sich automatisch auf alle Tests aus.
/// </summary>
internal static class TestConstants
{
    /// <summary>Basis-URL der lokal laufenden Demo-App (entspricht launchSettings.json).</summary>
    internal const string BaseUrl = "https://localhost:7023";

    /// <summary>Maximale Wartezeit in Millisekunden für Playwright-Assertions.</summary>
    internal const int DefaultTimeoutMs = 10_000;

    /// <summary>Verzeichnis für Test-Artefakte (Screenshots, Traces, Videos).</summary>
    internal const string ArtifactDir = "test-results";

    /// <summary>Alle Routen der Demo-App mit erwartetem PageTitle.</summary>
    internal static readonly IReadOnlyList<(string Route, string ExpectedTitle)> AllRoutes =
    [
        ("/",                        "PublicKernBlazor.Components Showcase"),
        ("/components/typography",   "Typografie - PublicKernBlazor.Components Showcase"),
        ("/components/layout",       "Layout - PublicKernBlazor.Components Showcase"),
        ("/components/buttons",      "Buttons - PublicKernBlazor.Components Showcase"),
        ("/components/forms",        "Formulare - PublicKernBlazor.Components Showcase"),
        ("/components/feedback",     "Feedback - PublicKernBlazor.Components Showcase"),
        ("/components/content",      "Content - PublicKernBlazor.Components Showcase"),
        ("/components/navigation",   "Navigation - PublicKernBlazor.Components Showcase"),
        ("/components/icons",        "Icons - PublicKernBlazor.Components Showcase"),
        ("/examples/antrag",         "Antragsstrecke - PublicKernBlazor.Components Showcase"),
        ("/examples/dashboard",      "Dashboard - PublicKernBlazor.Components Showcase"),
        ("/not-found",               "Seite nicht gefunden - PublicKernBlazor.Components Showcase"),
    ];
}

