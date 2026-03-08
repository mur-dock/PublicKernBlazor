# Run-SmokeTests.ps1
# Startet die KernUx.Blazor.Demo-App, führt alle Playwright-Smoke-Tests aus,
# gibt die Ergebnisse aus und beendet sich mit dem Test-Exit-Code.
#
# Verwendung:
#   .\Run-SmokeTests.ps1                          # headless, Debug
#   .\Run-SmokeTests.ps1 -Headless $false         # Browser sichtbar (lokal)
#   .\Run-SmokeTests.ps1 -Configuration Release   # Release-Build

param(
    [ValidateSet("Debug", "Release")]
    [string] $Configuration = "Debug",

    [bool]   $Headless       = $true
)

[Console]::OutputEncoding = [System.Text.Encoding]::UTF8
$ErrorActionPreference = "Stop"

# ── Pfade ─────────────────────────────────────────────────────────────────────
$SolutionRoot = Resolve-Path (Join-Path $PSScriptRoot ".")
$DemoProject  = Join-Path $SolutionRoot "KernUx.Blazor.Demo\KernUx.Blazor.Demo.csproj"
$TestProject  = Join-Path $SolutionRoot "KernUx.Blazor.Demo.SmokeTests\KernUx.Blazor.Demo.SmokeTests.csproj"
$ResultsDir   = Join-Path $SolutionRoot "test-results"
$ResultsFile  = Join-Path $SolutionRoot "smoke-test-results.txt"
# HTTPS-URL für Playwright-Tests (Chromium vertraut dem Dev-Zertifikat automatisch)
$AppUrl       = "https://localhost:7023"
# HTTP-URL für den Health-Check im Skript.
# Explizit 127.0.0.1 statt "localhost": PowerShell 5.1 löst "localhost" unter
# modernem Windows häufig auf ::1 (IPv6) auf, während Kestrel auf IPv4 bindet.
$AppHealthUrl = "http://127.0.0.1:5076"
$AppProcess   = $null
$TestExitCode = 1

function Write-Step([string] $Message) {
    Write-Host ""
    Write-Host "==> $Message" -ForegroundColor Cyan
}

try {
    # ── Schritt 1: Testprojekt prüfen ─────────────────────────────────────────
    if (-not (Test-Path $TestProject)) {
        throw "Testprojekt nicht gefunden: $TestProject`n" +
              "Bitte zuerst 'KernUx.Blazor.Demo.SmokeTests' anlegen (siehe playwright-smoketest-options.md)."
    }

    # ── Schritt 2: Solution bauen ─────────────────────────────────────────────
    Write-Step "Solution wird gebaut ($Configuration)..."
    dotnet build "$SolutionRoot\KernUxExample.slnx" `
        --configuration $Configuration `
        --nologo -q 2>&1 | Out-File -FilePath (Join-Path $SolutionRoot "build-output.txt") -Encoding utf8
    if ($LASTEXITCODE -ne 0) {
        Get-Content (Join-Path $SolutionRoot "build-output.txt") -Encoding utf8
        throw "Build fehlgeschlagen (Exit $LASTEXITCODE)."
    }
    Write-Host "   Build erfolgreich." -ForegroundColor Green

    # ── Schritt 3: Playwright-Browser installieren ────────────────────────────
    Write-Step "Playwright-Browser prüfen / installieren..."
    $PlaywrightScript = Join-Path $SolutionRoot `
        "KernUx.Blazor.Demo.SmokeTests\bin\$Configuration\net10.0\playwright.ps1"

    if (Test-Path $PlaywrightScript) {
        # Playwright gibt über Node.js Deprecation-Warnungen auf stderr aus
        # (z.B. DEP0169 url.parse). Diese sind harmlos, würden aber bei
        # $ErrorActionPreference = "Stop" als Fehler gewertet werden.
        # Daher: stderr unterdrücken, Erfolg über $LASTEXITCODE prüfen.
        $ErrorActionPreference = "Continue"
        & pwsh $PlaywrightScript install chromium 2>$null
        $ErrorActionPreference = "Stop"

        if ($LASTEXITCODE -ne 0) {
            Write-Warning "Playwright-Browser-Installation hat Exit-Code $LASTEXITCODE zurückgegeben."
        } else {
            Write-Host "   Chromium bereit." -ForegroundColor Green
        }
    } else {
        Write-Warning "playwright.ps1 nicht gefunden – Browser evtl. bereits installiert."
    }

    # ── Schritt 4: Demo-App im Hintergrund starten ────────────────────────────
    Write-Step "Demo-App wird gestartet..."

    # Stdout und Stderr der Demo-App in Logdateien umleiten für Diagnostik
    $AppStdOut = Join-Path $SolutionRoot "demo-app-stdout.txt"
    $AppStdErr = Join-Path $SolutionRoot "demo-app-stderr.txt"

    # --urls bindet Kestrel explizit auf beide Ports.
    # "localhost" in Kestrel bindet auf sowohl IPv4 (127.0.0.1) als auch IPv6 (::1).
    $AppProcess = Start-Process "dotnet" `
        -ArgumentList "run --project `"$DemoProject`" --configuration $Configuration --no-build --urls `"https://localhost:7023;http://localhost:5076`"" `
        -PassThru `
        -WindowStyle Hidden `
        -RedirectStandardOutput $AppStdOut `
        -RedirectStandardError  $AppStdErr

    Write-Host "   PID: $($AppProcess.Id)"
    Write-Host "   HTTPS: $AppUrl (für Playwright)"
    Write-Host "   HTTP:  $AppHealthUrl (für Health-Check)"

    # Warten bis die App antwortet (max. 45 Sekunden)
    # Health-Check läuft über HTTP, damit kein TLS-Zertifikatsproblem
    # in PowerShell 5.1 auftreten kann.
    $Timeout = 45
    $Elapsed = 0
    $Ready   = $false
    Write-Host "   Warte auf Bereitschaft ($AppHealthUrl)..." -NoNewline

    # $ErrorActionPreference = "Stop" kann in PowerShell 5.1 dazu führen, dass
    # .NET-Exceptions (z.B. TcpClient.Connect) nicht korrekt im try/catch landen.
    # Daher für den Health-Check-Loop temporär auf "Continue" setzen.
    $ErrorActionPreference = "Continue"

    while ($Elapsed -lt $Timeout) {
        Start-Sleep -Seconds 1
        $Elapsed++
        Write-Host "." -NoNewline

        # Prüfen, ob der Prozess noch läuft
        if ($AppProcess.HasExited) {
            Write-Host ""
            Write-Host "   App-Prozess hat sich unerwartet beendet (Exit $($AppProcess.ExitCode))." -ForegroundColor Red
            if (Test-Path $AppStdErr) {
                Write-Host "   --- stderr ---" -ForegroundColor Yellow
                Get-Content $AppStdErr -Encoding utf8 | Select-Object -First 20 | ForEach-Object { Write-Host "   $_" }
            }
            if (Test-Path $AppStdOut) {
                Write-Host "   --- stdout ---" -ForegroundColor Yellow
                Get-Content $AppStdOut -Encoding utf8 | Select-Object -First 20 | ForEach-Object { Write-Host "   $_" }
            }
            throw "Demo-App-Prozess beendet mit Exit-Code $($AppProcess.ExitCode)."
        }

        # TCP-Port-Check: zuverlässig in jeder PowerShell-Version,
        # kein TLS- oder DNS-Problem wie bei Invoke-WebRequest.
        # Prüfe IPv4 und IPv6, da Kestrel mit "localhost" je nach OS-Konfiguration
        # nur auf einer der beiden Adressen binden kann.
        $Connected = $false
        foreach ($addr in @("127.0.0.1", "::1")) {
            try {
                $tcp = New-Object System.Net.Sockets.TcpClient
                $tcp.Connect($addr, 5076)
                if ($tcp.Connected) {
                    $tcp.Close()
                    $Connected = $true
                    break
                }
                $tcp.Close()
            } catch {
                # Diese Adresse nicht erreichbar – nächste versuchen
            }
        }
        if ($Connected) {
            $Ready = $true
            break
        }
    }
    Write-Host ""

    # ErrorActionPreference wiederherstellen
    $ErrorActionPreference = "Stop"

    if (-not $Ready) {
        Write-Host "   Diagnostik:" -ForegroundColor Yellow
        Write-Host "   PowerShell-Version: $($PSVersionTable.PSVersion)" -ForegroundColor Yellow
        Write-Host "   App-PID: $($AppProcess.Id), läuft: $(-not $AppProcess.HasExited)" -ForegroundColor Yellow
        if (Test-Path $AppStdErr) {
            Write-Host "   --- stderr (letzte 10 Zeilen) ---" -ForegroundColor Yellow
            Get-Content $AppStdErr -Encoding utf8 | Select-Object -Last 10 | ForEach-Object { Write-Host "   $_" }
        }
        if (Test-Path $AppStdOut) {
            Write-Host "   --- stdout (letzte 10 Zeilen) ---" -ForegroundColor Yellow
            Get-Content $AppStdOut -Encoding utf8 | Select-Object -Last 10 | ForEach-Object { Write-Host "   $_" }
        }
        throw "Demo-App hat innerhalb von $Timeout Sekunden nicht geantwortet ($AppHealthUrl)."
    }
    Write-Host "   App bereit nach $Elapsed Sekunde(n)." -ForegroundColor Green

    # ── Schritt 5: Smoke-Tests ausführen ─────────────────────────────────────
    Write-Step "Smoke-Tests werden ausgeführt (Headless: $Headless)..."

    # Playwright liest PLAYWRIGHT_HEADLESS aus der Umgebung
    $Env:PLAYWRIGHT_HEADLESS = if ($Headless) { "1" } else { "0" }

    New-Item -ItemType Directory -Force -Path $ResultsDir | Out-Null

    # dotnet test schreibt Fehlermeldungen auf stderr. In PowerShell 5.1 mit
    # $ErrorActionPreference = "Stop" würden diese als terminierende Fehler
    # geworfen, bevor $LASTEXITCODE gelesen werden kann. Daher: "Continue".
    $ErrorActionPreference = "Continue"

    $TestOutput = dotnet test "$TestProject" `
        --configuration $Configuration `
        --no-build `
        --logger "console;verbosity=normal" `
        --results-directory "$ResultsDir" `
        2>&1

    $TestExitCode = $LASTEXITCODE
    $ErrorActionPreference = "Stop"

    # Output auf Konsole ausgeben
    $TestOutput | ForEach-Object { Write-Host $_ }

    # Output in Datei schreiben (UTF-8)
    $TestOutput | Out-File -FilePath $ResultsFile -Encoding utf8

    # ── Schritt 6: Ergebnis-Zusammenfassung ausgeben ─────────────────────────
    Write-Step "Ergebnis-Zusammenfassung"
    $Content = Get-Content $ResultsFile -Encoding utf8
    $Summary = $Content | Where-Object {
        $_ -match "Bestanden|Fehler|übersprungen|Passed|Failed|Skipped|gesamt|total"
    } | Select-Object -Last 5

    if ($Summary) {
        $Summary | ForEach-Object { Write-Host "   $_" }
    } else {
        Write-Host "   (Keine Zusammenfassung gefunden – vollständige Ausgabe in $ResultsFile)"
    }

    Write-Host ""
    if ($TestExitCode -eq 0) {
        Write-Host "Ergebnis: ALLE TESTS BESTANDEN" -ForegroundColor Green
    } else {
        Write-Host "Ergebnis: MINDESTENS EIN TEST FEHLGESCHLAGEN" -ForegroundColor Red
        Write-Host "Artefakte: $ResultsDir" -ForegroundColor Yellow
        Write-Host "Log:       $ResultsFile" -ForegroundColor Yellow
    }

} catch {
    Write-Host ""
    Write-Host "Fehler beim Ausführen der Smoke-Tests:" -ForegroundColor Red
    Write-Host "  $_" -ForegroundColor Red

    # Falls Testausgabe bereits in Datei geschrieben wurde, anzeigen
    if ((Test-Path $ResultsFile) -and (Get-Item $ResultsFile).Length -gt 0) {
        Write-Host ""
        Write-Host "  --- Test-Output (letzte 30 Zeilen) ---" -ForegroundColor Yellow
        Get-Content $ResultsFile -Encoding utf8 | Select-Object -Last 30 | ForEach-Object { Write-Host "  $_" }
    }

    $TestExitCode = 1

} finally {
    # ── Schritt 7: Demo-App stoppen ───────────────────────────────────────────
    if ($null -ne $AppProcess -and -not $AppProcess.HasExited) {
        Write-Step "Demo-App wird gestoppt (PID $($AppProcess.Id))..."
        Stop-Process -Id $AppProcess.Id -Force -ErrorAction SilentlyContinue
        Write-Host "   Gestoppt." -ForegroundColor Green
    }

    # Diagnostik-Dateien anzeigen, falls vorhanden und Test fehlgeschlagen
    if ($TestExitCode -ne 0) {
        $diagStdOut = Join-Path $SolutionRoot "demo-app-stdout.txt"
        $diagStdErr = Join-Path $SolutionRoot "demo-app-stderr.txt"
        if ((Test-Path $diagStdOut) -or (Test-Path $diagStdErr)) {
            Write-Host "   Diagnostik-Logs: demo-app-stdout.txt, demo-app-stderr.txt" -ForegroundColor Yellow
        }
    }

    Write-Host ""
}

exit $TestExitCode

