namespace PublicKernBlazor.Demo.SmokeTests.Infrastructure;

/// <summary>
/// Globale NUnit-Einrichtung: installiert Playwright-Browser einmalig pro Testlauf.
/// <para>
/// <c>[SetUpFixture]</c> mit <c>[OneTimeSetUp]</c> wird von NUnit genau einmal vor allen
/// Tests im gesamten Assembly ausgeführt – unabhängig von der Anzahl der Testklassen.
/// </para>
/// </summary>
[SetUpFixture]
public sealed class PlaywrightGlobalSetup
{
    /// <summary>
    /// Installiert Chromium, falls noch nicht vorhanden.
    /// Playwright sucht den Browser im lokalen Benutzerverzeichnis (~/.cache/ms-playwright).
    /// </summary>
    [OneTimeSetUp]
    public static void InstallBrowser() =>
        Microsoft.Playwright.Program.Main(["install", "chromium"]);
}

