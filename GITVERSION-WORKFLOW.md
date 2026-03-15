# GitVersion-Workflow (Kurzform)

Dieses Repository verwendet `GitVersion`, um SemVer-Versionen automatisch aus Git-Historie, Branch-Namen und Tags abzuleiten.

## Was ist umgesetzt?

- In `src/PublicKernBlazor.Components/PublicKernBlazor.Components.csproj` ist `GitVersion.MsBuild` eingebunden.
- Die feste `<Version>` im Projekt wurde entfernt, damit die Version zur Build-/Pack-Zeit von GitVersion berechnet wird.
- Im Repository-Root liegt jetzt `GitVersion.yml` als zentrale Versionierungs-Konfiguration.

## Konfiguration (`GitVersion.yml`)

Die Konfiguration orientiert sich an gängigen Best Practices für neue, wachsende Projekte:

- `workflow: GitHubFlow/v1` als pragmatischer Standard für einfache Branch-Modelle.
- `mode: ContinuousDelivery` für fortlaufende, nachvollziehbare Pre-Release-Versionen.
- `next-version: 0.1.0` als Startanker auf Basis des bisherigen Projektstands.
- `tag-prefix: '[vV]?'` akzeptiert Tags wie `v1.2.3` und `1.2.3`.
- `commit-message-incrementing: Disabled`, damit Commit-Nachrichten auf `main` keinen ungeplanten Versionssprung auslösen.
- Branch-Regeln:
  - `main` erzeugt bei normalen Einzel-Commits stabile Patch-Versionen.
  - `main` übernimmt bei Merge aus `feature/*` den Branch-Inkrement und erzeugt damit immer ein Minor-Upgrade.
  - `feature/*` erzeugt `alpha`-Pre-Releases (Minor-Inkrement).
  - `fix/*`, `bugfix/*`, `hotfix/*` erzeugen `beta`-Pre-Releases (Patch-Inkrement).
  - `release/*` erzeugt `rc`-Pre-Releases.
  - PR-Branches erhalten das Label `pr`.

## Kurzablauf

1. Auf einem Branch arbeiten (`feature/*`, `fix/*`, `release/*` etc.).
2. Änderungen committen und bei Releases Tags setzen.
3. `dotnet build` oder `dotnet pack` ausführen.
4. GitVersion setzt daraus die verwendeten Versionswerte (u. a. NuGet-Paketversion).

## Exemplarische Workflows mit Build-Versionen

Beispielannahme für beide Szenarien: Letzter stabiler Tag auf `main` ist `v0.1.0`.

### 1) Feature hinzufügen (`feature/*`)

1. Von `main` einen Branch erstellen, z. B. `feature/kern-table-sortierung`.
2. Feature implementieren und committen.
3. Build auf dem Feature-Branch ausführen (`dotnet build` / `dotnet pack`).
   - Ergebnisversion (Beispiel): `0.2.0-alpha.1`
4. Pull Request erstellen und mit Merge-Commit nach `main` mergen.
5. Build auf `main` nach dem Merge ausführen.
   - Ergebnisversion (Beispiel): `0.2.0`
6. Für den offiziellen Release den Tag setzen, z. B. `v0.2.0`.
   - Build auf dem Tag liefert die Release-Version `0.2.0`.

Ein direkter Einzel-Commit auf `main` (ohne Feature-Merge) bleibt dagegen ein Patch,
z. B. von `0.2.0` auf `0.2.1`.

### 2) Bugfix durchführen (`fix/*`, `bugfix/*`, `hotfix/*`)

1. Von `main` einen Branch erstellen, z. B. `fix/kern-input-aria-describedby`.
2. Fehler beheben und committen.
3. Build auf dem Fix-Branch ausführen.
   - Ergebnisversion (Beispiel): `0.1.1-beta.1`
4. Pull Request erstellen und nach `main` mergen.
5. Build auf `main` nach dem Merge ausführen.
   - Ergebnisversion (Beispiel): `0.1.1`
6. Optional Tag setzen (`v0.1.1`), wenn der Fix als offizieller Release markiert wird.

Hinweis: Die genauen Zähler (`alpha.1`, `beta.1`, ...) hängen von Commit-Anzahl und Historie ab.

Wichtig: Für das erzwungene Minor-Upgrade bei `feature/*`-Übernahmen muss der Merge als
Merge-Commit in `main` landen (kein Squash/Fast-Forward).

## Release-How-To über GitHub (allgemeiner Ablauf)

Dieser Ablauf beschreibt den typischen Weg für ein offizielles Release über GitHub.

1. Änderungen über Pull Request nach `main` mergen (bevorzugt als Merge-Commit).
2. Warten, bis die CI auf `main` erfolgreich ist (Build/Test/Pack).
3. Gewünschte Release-Version festlegen (z. B. `v0.2.0` bei Feature-Release oder `v0.1.1` bei Bugfix-Release).
4. Auf GitHub unter **Releases** ein neues Release erstellen:
   - **Tag**: `vX.Y.Z` (neu erstellen, auf den aktuellen `main`-Commit zeigen lassen)
   - **Target**: `main`
   - **Release title**: z. B. `v0.2.0`
   - **Description**: Kurzbeschreibung/Changelog der Änderungen
5. Release veröffentlichen.
6. Tag- bzw. Release-Workflow in GitHub Actions abwarten.
7. Ergebnis prüfen:
   - erzeugte Paketversion entspricht `X.Y.Z`
   - Artefakte/NuGet-Paket wurden erfolgreich veröffentlicht

Hinweis: Das Tag ist der maßgebliche Punkt für die finale Release-Version. Ohne Tag baut
`main` weiterhin fortlaufende Versionen nach GitVersion-Regeln.

## Wichtige Hinweise für CI

- Der Checkout muss Tags und ausreichend Historie enthalten (`fetch-depth: 0` in GitHub Actions).
- Ohne Tags/Historie kann GitVersion keine erwartbaren Versionsnummern berechnen.

## Offizielle Dokumentation

- Einstieg: https://gitversion.net/docs/
- MSBuild-Integration (`GitVersion.MsBuild`): https://gitversion.net/docs/usage/msbuild
- Konfiguration mit `GitVersion.yml`: https://gitversion.net/docs/reference/configuration
- Verfügbare Variablen: https://gitversion.net/docs/reference/variables


