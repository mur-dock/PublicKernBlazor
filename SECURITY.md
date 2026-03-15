# Sicherheitsrichtlinie

## Unterstützte Versionen

Sicherheitsupdates werden für folgende Versionen bereitgestellt:

| Version | Unterstützt |
|---------|-------------|
| 0.1.x   | ✅ Ja        |
| < 0.1   | ❌ Nein      |

## Sicherheitslücke melden

**Bitte melde Sicherheitslücken niemals als öffentliches GitHub-Issue.**

Verwende stattdessen die **GitHub Security Advisories** dieses Repositories:

1. Navigiere zur Seite „Security" des Repositories
2. Klicke auf „Report a vulnerability"
3. Fülle das Formular mit allen verfügbaren Details aus

Alternativ kann eine verschlüsselte E-Mail an die Maintainer gesendet werden –
die Kontaktdaten sind im Repository-Profil hinterlegt.

### Was du in deinem Bericht angeben solltest

Um uns eine schnelle Bearbeitung zu ermöglichen, bitten wir um folgende Angaben:

- **Beschreibung** des Problems und der potenziellen Auswirkungen
- **Betroffene Komponente(n)** (z. B. `KernDialog`, `ThemeService`)
- **Reproduktionsschritte** (minimales Beispiel, falls möglich)
- **Betroffene Version(en)**
- **Mögliche Gegenmaßnahmen**, sofern du welche kennst

## Reaktionszeit

| Schritt                       | Zeitrahmen                 |
|-------------------------------|----------------------------|
| Eingangsbestätigung           | Innerhalb von 5 Werktagen  |
| Erstbewertung und Rückmeldung | Innerhalb von 10 Werktagen |
| Patch-Veröffentlichung        | Abhängig vom Schweregrad   |

Bei kritischen Lücken (CVSS ≥ 9.0) wird ein Hotfix priorisiert.

## Scope

### Im Scope

- Bibliotheks-Code in `PublicKernBlazor.Components/` (C#, Razor-Komponenten)
- Statische Assets (`wwwroot/css/`, `wwwroot/js/`)
- Services (`ThemeService`, `IdGeneratorService`)

### Außerhalb des Scope

- **KERN-UX-Upstream** (`Styles/core/`) – Lücken dort bitte direkt an
  [kern-ux.de](https://www.kern-ux.de) oder das
  [GitLab-Repository](https://gitlab.opencode.de/kern-ux/kern-ux-plain) melden
- Demo-Anwendung (`PublicKernBlazor.Demo/`) – enthält keine produktiv eingesetzten Geheimnisse
- Smoke-Tests (`PublicKernBlazor.Demo.SmokeTests/`)

## Bekanntmachung von Sicherheitslücken

Nach der Behebung einer Sicherheitslücke wird ein **GitHub Security Advisory** veröffentlicht.
Die Veröffentlichung erfolgt koordiniert – in der Regel nach dem Release des Patches,
sodass Nutzer:innen Zeit haben, ihre Abhängigkeit zu aktualisieren.

