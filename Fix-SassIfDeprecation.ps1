<#
.SYNOPSIS
    Bereinigt die Sass if()-Deprecation-Warnung durch Unterdrückung via sasscompiler.json.

.DESCRIPTION
    Die neue if(sass(...): ..., else: ...)-Syntax wird von der aktuell in
    AspNetCore.SassCompiler eingebetteten Dart-Sass-Version noch NICHT vollständig
    unterstützt und erzeugt einen Build-Fehler ("expected ')'").

    Dieses Skript wählt daher die offizielle Gegenstrategie:
      → Die Warning wird über --silence-deprecation=if-function unterdrückt.
      → SCSS-Dateien, die bereits auf die neue (fehlerhafte) Syntax migriert wurden,
         werden automatisch auf die alte (funktionierende) Syntax zurückgerollt.

    WANN sollte man stattdessen migrieren?
      Sobald AspNetCore.SassCompiler eine Dart-Sass-Version >= 1.85 mitliefert,
      die else: in if() korrekt parst, kann die neue Syntax genutzt werden.
      Dann dieses Skript mit -Migrate ausführen (ohne Rollback).

.PARAMETER ScssRoot
    Pfad zum Ordner mit den SCSS-Dateien. Standard: ".\KernUxExample.Blazor\Styles"

.PARAMETER SassCompilerJson
    Pfad zur sasscompiler.json. Standard: ".\KernUxExample.Blazor\sasscompiler.json"

.PARAMETER DryRun
    Zeigt Änderungen nur an, speichert aber nichts.

.PARAMETER Migrate
    Statt Rollback: Migriert die alte Syntax zur neuen (nur verwenden wenn Sass-Version
    die neue Syntax bereits unterstützt).

.EXAMPLE
    .\Fix-SassIfDeprecation.ps1 -DryRun
    .\Fix-SassIfDeprecation.ps1
    .\Fix-SassIfDeprecation.ps1 -Migrate -DryRun
#>

param(
    [string]$ScssRoot          = ".\KernUxExample.Blazor\Styles",
    [string]$SassCompilerJson  = ".\KernUxExample.Blazor\sasscompiler.json",
    [switch]$DryRun,
    [switch]$Migrate
)

Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# ─────────────────────────────────────────────────────────────────
# Hilfsfunktion: Extrahiert die 3 Argumente eines if()-Aufrufs,
# wobei verschachtelte Klammern und Strings korrekt behandelt werden.
# Gibt $null zurück wenn kein gültiges 3-Argument-if() gefunden.
# ─────────────────────────────────────────────────────────────────
function Get-IfArguments {
    param([string]$inner)

    $args3   = @()
    $depth   = 0
    $current = [System.Text.StringBuilder]::new()
    $inStr   = $false
    $strChar = ''

    for ($i = 0; $i -lt $inner.Length; $i++) {
        $ch = $inner[$i]

        if (-not $inStr -and ($ch -eq "'" -or $ch -eq '"')) {
            $inStr = $true; $strChar = $ch
            [void]$current.Append($ch); continue
        }
        if ($inStr -and $ch -eq $strChar) {
            $inStr = $false
            [void]$current.Append($ch); continue
        }
        if ($inStr) { [void]$current.Append($ch); continue }

        if     ($ch -eq '(') { $depth++ }
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
# Funktion: Erkennt ob eine Zeile bereits die NEUE Syntax enthält
#   if(sass(condition): trueVal, else: falseVal)
# und gibt sie ZURÜCKGEROLLT zurück (→ alte Syntax).
# ─────────────────────────────────────────────────────────────────
function Rollback-SassIfLine {
    param([string]$line)

    # Muster: if(sass(...): ..., else: ...)
    # Wir parsen zeichenweise um verschachtelte Klammern zu unterstützen
    $result = [System.Text.StringBuilder]::new()
    $pos    = 0

    while ($pos -lt $line.Length) {
        $idx = $line.IndexOf('if(sass(', $pos)
        if ($idx -lt 0) {
            [void]$result.Append($line.Substring($pos))
            break
        }

        # Prüfen ob davor ein Bezeichner steht (dann nicht anfassen)
        $charBefore = if ($idx -gt 0) { $line[$idx - 1] } else { ' ' }
        if ($charBefore -match '[a-zA-Z0-9_-]') {
            [void]$result.Append($line.Substring($pos, $idx - $pos + 8))
            $pos = $idx + 8
            continue
        }

        # Äußere Klammer von if(...) finden
        $outerStart = $idx + 3        # nach "if("
        $depth      = 1
        $end        = $outerStart
        $inStr      = $false
        $strChar    = ''

        while ($end -lt $line.Length -and $depth -gt 0) {
            $ch = $line[$end]
            if (-not $inStr -and ($ch -eq "'" -or $ch -eq '"')) { $inStr = $true; $strChar = $ch }
            elseif ($inStr -and $ch -eq $strChar)               { $inStr = $false }
            elseif (-not $inStr) {
                if     ($ch -eq '(') { $depth++ }
                elseif ($ch -eq ')') { $depth-- }
            }
            $end++
        }

        if ($depth -ne 0) {
            [void]$result.Append($line.Substring($pos, $idx - $pos + 8))
            $pos = $idx + 8
            continue
        }

        $innerFull = $line.Substring($outerStart, $end - $outerStart - 1)
        # innerFull sieht so aus: sass(condition): trueVal, else: falseVal

        # sass(...) extrahieren
        if (-not $innerFull.StartsWith('sass(')) {
            [void]$result.Append($line.Substring($pos, $end - $pos))
            $pos = $end
            continue
        }

        # Inhalt von sass() finden
        $sassStart = 5   # nach "sass("
        $dInner    = 1
        $si        = $sassStart
        $inStr2    = $false
        $strChar2  = ''

        while ($si -lt $innerFull.Length -and $dInner -gt 0) {
            $ch = $innerFull[$si]
            if (-not $inStr2 -and ($ch -eq "'" -or $ch -eq '"')) { $inStr2 = $true; $strChar2 = $ch }
            elseif ($inStr2 -and $ch -eq $strChar2)               { $inStr2 = $false }
            elseif (-not $inStr2) {
                if     ($ch -eq '(') { $dInner++ }
                elseif ($ch -eq ')') { $dInner-- }
            }
            $si++
        }

        $condition = $innerFull.Substring($sassStart, $si - $sassStart - 1).Trim()

        # Nach "sass(condition): " kommt "trueVal, else: falseVal"
        $afterSass = $innerFull.Substring($si).TrimStart()
        if (-not $afterSass.StartsWith(':')) {
            [void]$result.Append($line.Substring($pos, $end - $pos))
            $pos = $end
            continue
        }
        $afterColon = $afterSass.Substring(1).TrimStart()

        # trueVal und "else: falseVal" trennen (auf Komma auf Tiefe 0)
        $dSplit  = 0
        $inStr3  = $false
        $strCh3  = ''
        $splitAt = -1

        for ($i = 0; $i -lt $afterColon.Length; $i++) {
            $ch = $afterColon[$i]
            if (-not $inStr3 -and ($ch -eq "'" -or $ch -eq '"')) { $inStr3 = $true; $strCh3 = $ch }
            elseif ($inStr3 -and $ch -eq $strCh3)                { $inStr3 = $false }
            elseif (-not $inStr3) {
                if     ($ch -eq '(') { $dSplit++ }
                elseif ($ch -eq ')') { $dSplit-- }
                elseif ($ch -eq ',' -and $dSplit -eq 0) { $splitAt = $i; break }
            }
        }

        if ($splitAt -lt 0) {
            [void]$result.Append($line.Substring($pos, $end - $pos))
            $pos = $end
            continue
        }

        $trueVal  = $afterColon.Substring(0, $splitAt).Trim()
        $elseRest = $afterColon.Substring($splitAt + 1).Trim()

        if (-not $elseRest.StartsWith('else:')) {
            [void]$result.Append($line.Substring($pos, $end - $pos))
            $pos = $end
            continue
        }

        $falseVal = $elseRest.Substring(5).Trim()

        # Alte Syntax zusammenbauen
        $oldIf = "if($condition, $trueVal, $falseVal)"
        [void]$result.Append($line.Substring($pos, $idx - $pos))
        [void]$result.Append($oldIf)
        $pos = $end
    }

    return $result.ToString()
}

# ─────────────────────────────────────────────────────────────────
# Funktion: Migriert ALTE Syntax → NEUE Syntax (für künftige Nutzung)
# ─────────────────────────────────────────────────────────────────
function Migrate-SassIfLine {
    param([string]$line)

    $result = [System.Text.StringBuilder]::new()
    $pos    = 0

    while ($pos -lt $line.Length) {
        $idx = $line.IndexOf('if(', $pos)
        if ($idx -lt 0) {
            [void]$result.Append($line.Substring($pos))
            break
        }

        $charBefore  = if ($idx -gt 0) { $line[$idx - 1] } else { ' ' }
        $isDirective = ($charBefore -eq '@') -or ($charBefore -match '[a-zA-Z0-9_-]')

        if ($isDirective) {
            [void]$result.Append($line.Substring($pos, $idx - $pos + 3))
            $pos = $idx + 3
            continue
        }

        # Bereits neue Syntax? (sass() drin)
        if ($line.Substring($idx).StartsWith('if(sass(')) {
            [void]$result.Append($line.Substring($pos, $idx - $pos + 3))
            $pos = $idx + 3
            continue
        }

        $start   = $idx + 3
        $depth   = 1
        $end     = $start
        $inStr   = $false
        $strChar = ''

        while ($end -lt $line.Length -and $depth -gt 0) {
            $ch = $line[$end]
            if (-not $inStr -and ($ch -eq "'" -or $ch -eq '"')) { $inStr = $true; $strChar = $ch }
            elseif ($inStr -and $ch -eq $strChar)               { $inStr = $false }
            elseif (-not $inStr) {
                if     ($ch -eq '(') { $depth++ }
                elseif ($ch -eq ')') { $depth-- }
            }
            $end++
        }

        if ($depth -ne 0) {
            [void]$result.Append($line.Substring($pos, $idx - $pos + 3))
            $pos = $idx + 3
            continue
        }

        $innerContent = $line.Substring($start, $end - $start - 1)
        $arguments    = Get-IfArguments -inner $innerContent

        if ($null -eq $arguments) {
            [void]$result.Append($line.Substring($pos, $end - $pos))
            $pos = $end
            continue
        }

        $newIf = "if(sass($($arguments[0])): $($arguments[1]), else: $($arguments[2]))"
        [void]$result.Append($line.Substring($pos, $idx - $pos))
        [void]$result.Append($newIf)
        $pos = $end
    }

    return $result.ToString()
}

# ─────────────────────────────────────────────────────────────────
# sasscompiler.json: --silence-deprecation=if-function verwalten
# ─────────────────────────────────────────────────────────────────
function Update-SassCompilerJson {
    param(
        [string]$JsonPath,
        [bool]$AddSilence   # true = hinzufügen, false = entfernen
    )

    if (-not (Test-Path $JsonPath)) {
        Write-Warning "sasscompiler.json nicht gefunden: $JsonPath"
        return $false
    }

    $content = Get-Content $JsonPath -Raw -Encoding UTF8
    $flag    = '--silence-deprecation=if-function'
    $changed = $false

    if ($AddSilence) {
        if ($content -notmatch [regex]::Escape($flag)) {
            # Flag zu Arguments hinzufügen
            if ($content -match '"Arguments"\s*:\s*"([^"]*)"') {
                $oldArgs = $Matches[1]
                $newArgs = if ($oldArgs.Trim() -eq '') { $flag } else { "$oldArgs $flag" }
                $content = $content -replace [regex]::Escape('"Arguments": "' + $oldArgs + '"'), ('"Arguments": "' + $newArgs + '"')
                $changed = $true
                Write-Host ""
                Write-Host "  📋 sasscompiler.json" -ForegroundColor Cyan
                Write-Host "    + Arguments: `"$newArgs`"" -ForegroundColor Green
            } else {
                Write-Warning "  Kein 'Arguments'-Feld in sasscompiler.json gefunden."
            }
        } else {
            Write-Host "  ✅ sasscompiler.json: Flag bereits vorhanden." -ForegroundColor DarkGray
        }
    } else {
        if ($content -match [regex]::Escape($flag)) {
            $content = $content -replace (" " + [regex]::Escape($flag)), ''
            $content = $content -replace ([regex]::Escape($flag) + " "), ''
            $content = $content -replace [regex]::Escape($flag), ''
            $changed = $true
            Write-Host ""
            Write-Host "  📋 sasscompiler.json  – Flag entfernt" -ForegroundColor Cyan
        }
    }

    if ($changed -and -not $DryRun) {
        $content | Set-Content -Path $JsonPath -Encoding UTF8 -NoNewline
    }
    return $changed
}

# ─────────────────────────────────────────────────────────────────
# SCSS-Dateien verarbeiten
# ─────────────────────────────────────────────────────────────────
if (-not (Test-Path $ScssRoot)) {
    Write-Error "SCSS-Ordner nicht gefunden: $ScssRoot"
    exit 1
}

$mode = if ($Migrate) { "MIGRATION (alt → neu)" } else { "ROLLBACK (neu → alt)" }
Write-Host ""
Write-Host "  Modus : $mode" -ForegroundColor Yellow
if ($DryRun) {
    Write-Host "  Modus : DRY RUN – keine Dateien werden verändert" -ForegroundColor Yellow
}

$scssFiles    = Get-ChildItem -Path $ScssRoot -Recurse -Filter "*.scss"
$totalFiles   = 0
$totalChanges = 0

foreach ($file in $scssFiles) {
    $lines       = Get-Content -Path $file.FullName -Encoding UTF8
    $newLines    = @()
    $fileChanged = $false
    $lineNumber  = 0

    foreach ($line in $lines) {
        $lineNumber++
        $converted = if ($Migrate) {
            Migrate-SassIfLine  -line $line
        } else {
            Rollback-SassIfLine -line $line
        }

        if ($converted -ne $line) {
            $fileChanged = $true
            $totalChanges++
            $relPath = $file.FullName.Replace((Resolve-Path $ScssRoot).Path + '\', '')
            Write-Host ""
            Write-Host "  📄 $relPath  (Zeile $lineNumber)" -ForegroundColor Cyan
            Write-Host "    - : $($line.Trim())"      -ForegroundColor Red
            Write-Host "    + : $($converted.Trim())" -ForegroundColor Green
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

# sasscompiler.json anpassen
if ($Migrate) {
    # Bei Migration: Flag nicht mehr nötig → entfernen
    $jsonChanged = Update-SassCompilerJson -JsonPath $SassCompilerJson -AddSilence $false
} else {
    # Bei Rollback: Flag setzen um Warning zu unterdrücken
    $jsonChanged = Update-SassCompilerJson -JsonPath $SassCompilerJson -AddSilence $true
}

# ─────────────────────────────────────────────────────────────────
# Zusammenfassung
# ─────────────────────────────────────────────────────────────────
Write-Host ""
Write-Host "─────────────────────────────────────────────────────" -ForegroundColor DarkGray
if ($DryRun) {
    Write-Host "  DRY RUN – keine Dateien wurden verändert" -ForegroundColor Yellow
}
Write-Host "  SCSS-Dateien mit Änderungen : $totalFiles"   -ForegroundColor White
Write-Host "  SCSS-Ersetzungen gesamt     : $totalChanges" -ForegroundColor White
Write-Host "─────────────────────────────────────────────────────" -ForegroundColor DarkGray

if ($totalChanges -eq 0 -and -not $jsonChanged) {
    Write-Host "  ✅ Keine Änderungen notwendig." -ForegroundColor Green
} elseif ($DryRun) {
    Write-Host "  ℹ️  Ohne -DryRun werden die Änderungen gespeichert." -ForegroundColor Yellow
} else {
    if ($Migrate) {
        Write-Host "  ✅ Migration abgeschlossen. sasscompiler.json bereinigt." -ForegroundColor Green
    } else {
        Write-Host "  ✅ Rollback abgeschlossen." -ForegroundColor Green
        Write-Host "  ✅ sasscompiler.json: --silence-deprecation=if-function gesetzt." -ForegroundColor Green
        Write-Host ""
        Write-Host "  ℹ️  Sobald AspNetCore.SassCompiler eine neuere Dart-Sass-Version" -ForegroundColor DarkGray
        Write-Host "      mitliefert, kannst du mit -Migrate zur neuen Syntax wechseln." -ForegroundColor DarkGray
    }
}
Write-Host ""

