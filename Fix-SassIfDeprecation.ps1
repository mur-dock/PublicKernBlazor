<#
.SYNOPSIS
    Ersetzt die veraltete Sass if()-Syntax durch die neue CSS-kompatible Syntax.

.DESCRIPTION
    Sass hat die alte if(condition, true, false)-Syntax als deprecated markiert.
    Dieses Skript sucht alle .scss-Dateien im angegebenen Ordner und ersetzt
    alle Vorkommen durch die neue Syntax:
        if(sass(condition): trueValue, else: falseValue)

    WICHTIG: Sass @if-Direktiven (z.B. "@if", "@else if") werden NICHT angefasst.
             Nur Funktionsaufrufe wie "if(...)" in Ausdrücken werden migriert.

.PARAMETER ScssRoot
    Pfad zum Ordner, der die SCSS-Dateien enthält. Standard: "./Styles"

.PARAMETER DryRun
    Wenn gesetzt, werden Änderungen nur angezeigt, aber NICHT gespeichert.

.EXAMPLE
    .\Fix-SassIfDeprecation.ps1 -DryRun
    .\Fix-SassIfDeprecation.ps1 -ScssRoot ".\Styles"
#>

param(
    [string]$ScssRoot = ".\KernUxExample.Blazor\Styles",
    [switch]$DryRun
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# ─────────────────────────────────────────────────────────────────
# Hilfsfunktion: Extrahiert die 3 Argumente eines if()-Aufrufs,
# wobei verschachtelte Klammern korrekt berücksichtigt werden.
# Gibt $null zurück, wenn kein gültiges 3-Argument-if() gefunden.
# ─────────────────────────────────────────────────────────────────
function Get-IfArguments {
    param([string]$inner) # Inhalt ZWISCHEN den äußeren if( ... )

    $args3 = @()
    $depth = 0
    $current = [System.Text.StringBuilder]::new()
    $inString = $false
    $stringChar = ''

    for ($i = 0; $i -lt $inner.Length; $i++) {
        $ch = $inner[$i]

        # Einfache String-Erkennung (Sass nutzt ' und ")
        if (-not $inString -and ($ch -eq "'" -or $ch -eq '"')) {
            $inString = $true
            $stringChar = $ch
            [void]$current.Append($ch)
            continue
        }
        if ($inString -and $ch -eq $stringChar) {
            $inString = $false
            [void]$current.Append($ch)
            continue
        }
        if ($inString) {
            [void]$current.Append($ch)
            continue
        }

        if ($ch -eq '(') { $depth++ }
        elseif ($ch -eq ')') { $depth-- }

        if ($ch -eq ',' -and $depth -eq 0) {
            $args3 += $current.ToString().Trim()
            [void]$current.Clear()
        } else {
            [void]$current.Append($ch)
        }
    }
    $args3 += $current.ToString().Trim()

    if ($args3.Count -ne 3) { return $null }
    return $args3
}

# ─────────────────────────────────────────────────────────────────
# Hauptfunktion: Transformiert EINE Zeile
# Findet alle if(...) Aufrufe (keine @if-Direktiven)
# und ersetzt sie durch die neue Syntax.
# ─────────────────────────────────────────────────────────────────
function Convert-SassIfLine {
    param([string]$line)

    $result = [System.Text.StringBuilder]::new()
    $pos = 0

    while ($pos -lt $line.Length) {
        # Suche nächstes "if(" das NICHT von "@" oder Buchstabe/- davor steht
        $idx = $line.IndexOf('if(', $pos)

        if ($idx -lt 0) {
            # Kein weiteres if() → Rest anhängen und fertig
            [void]$result.Append($line.Substring($pos))
            break
        }

        # Prüfen ob es ein Sass @if / @else if / regulärer Bezeichner ist
        $charBefore = if ($idx -gt 0) { $line[$idx - 1] } else { ' ' }
        $isDirective = ($charBefore -eq '@') -or
                       ($charBefore -match '[a-zA-Z0-9_-]')

        if ($isDirective) {
            # Nicht ersetzen – bis hinter "if(" springen und weitersuchen
            [void]$result.Append($line.Substring($pos, $idx - $pos + 3))
            $pos = $idx + 3
            continue
        }

        # Inhalt der Klammern extrahieren (mit Tiefenzähler)
        $start = $idx + 3  # Position nach "if("
        $depth = 1
        $end = $start
        $inString = $false
        $stringChar = ''

        while ($end -lt $line.Length -and $depth -gt 0) {
            $ch = $line[$end]

            if (-not $inString -and ($ch -eq "'" -or $ch -eq '"')) {
                $inString = $true; $stringChar = $ch
            } elseif ($inString -and $ch -eq $stringChar) {
                $inString = $false
            } elseif (-not $inString) {
                if ($ch -eq '(') { $depth++ }
                elseif ($ch -eq ')') { $depth-- }
            }
            $end++
        }

        if ($depth -ne 0) {
            # Unbalancierte Klammer – unverändert lassen
            [void]$result.Append($line.Substring($pos, $idx - $pos + 3))
            $pos = $idx + 3
            continue
        }

        $innerContent = $line.Substring($start, $end - $start - 1)
        $arguments = Get-IfArguments -inner $innerContent

        if ($null -eq $arguments) {
            # Kein 3-Argument if() – z.B. CSS if() → unverändert lassen
            [void]$result.Append($line.Substring($pos, $end - $pos))
            $pos = $end
            continue
        }

        $condition = $arguments[0]
        $trueVal   = $arguments[1]
        $falseVal  = $arguments[2]

        # Neue Syntax zusammenbauen
        $newIf = "if(sass($condition): $trueVal, else: $falseVal)"

        # Alles bis vor das if() + neues if() anhängen
        [void]$result.Append($line.Substring($pos, $idx - $pos))
        [void]$result.Append($newIf)
        $pos = $end
    }

    return $result.ToString()
}

# ─────────────────────────────────────────────────────────────────
# Dateien verarbeiten
# ─────────────────────────────────────────────────────────────────

if (-not (Test-Path $ScssRoot)) {
    Write-Error "Ordner nicht gefunden: $ScssRoot"
    exit 1
}

$scssFiles = Get-ChildItem -Path $ScssRoot -Recurse -Filter "*.scss"

if ($scssFiles.Count -eq 0) {
    Write-Warning "Keine .scss-Dateien in '$ScssRoot' gefunden."
    exit 0
}

$totalFiles   = 0
$totalChanges = 0

foreach ($file in $scssFiles) {
    $lines      = Get-Content -Path $file.FullName -Encoding UTF8
    $newLines   = @()
    $fileChanged = $false
    $lineNumber  = 0

    foreach ($line in $lines) {
        $lineNumber++
        $converted = Convert-SassIfLine -line $line

        if ($converted -ne $line) {
            $fileChanged = $true
            $totalChanges++

            $relPath = $file.FullName.Replace((Resolve-Path $ScssRoot).Path + '\', '')
            Write-Host ""
            Write-Host "  📄 $relPath  (Zeile $lineNumber)" -ForegroundColor Cyan
            Write-Host "    - ALT : $($line.Trim())"  -ForegroundColor Red
            Write-Host "    + NEU : $($converted.Trim())" -ForegroundColor Green
        }
        $newLines += $converted
    }

    if ($fileChanged) {
        $totalFiles++
        if (-not $DryRun) {
            $newLines | Set-Content -Path $file.FullName -Encoding UTF8
        }
    }
}

# ─────────────────────────────────────────────────────────────────
# Zusammenfassung
# ─────────────────────────────────────────────────────────────────
Write-Host ""
Write-Host "─────────────────────────────────────────" -ForegroundColor DarkGray
if ($DryRun) {
    Write-Host "  DRY RUN – keine Dateien wurden verändert" -ForegroundColor Yellow
}
Write-Host "  Dateien mit Änderungen : $totalFiles" -ForegroundColor White
Write-Host "  Ersetzungen gesamt     : $totalChanges" -ForegroundColor White
Write-Host "─────────────────────────────────────────" -ForegroundColor DarkGray

if ($totalChanges -eq 0) {
    Write-Host "  ✅ Keine deprecated if()-Aufrufe gefunden." -ForegroundColor Green
} elseif ($DryRun) {
    Write-Host "  ℹ️  Führe das Skript ohne -DryRun aus, um die Änderungen zu speichern." -ForegroundColor Yellow
} else {
    Write-Host "  ✅ Alle Änderungen wurden gespeichert." -ForegroundColor Green
}
Write-Host ""

