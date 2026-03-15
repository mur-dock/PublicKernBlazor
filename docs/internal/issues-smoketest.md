# Smoke-Test Issues – Analyse der fehlgeschlagenen Tests

**Testlauf (initial):** 27 Tests gesamt, 8 bestanden, 19 fehlgeschlagen  
**Testlauf (aktuell):** 27 Tests gesamt, 27 bestanden, 0 fehlgeschlagen  
**Datum:** 2026-03-08  
**Umgebung:** .NET 10, Playwright 1.50, NUnit 4.3.2, Windows 11, PowerShell 5.1

---

## Zusammenfassung

| Kategorie                            | Anzahl | Ursache                                                                          |
|--------------------------------------|:------:|----------------------------------------------------------------------------------|
| PageTitle nicht seitenspezifisch     |   11   | `<PageTitle>` wird von Blazor SSR-Seiten nicht an den Browser übergeben          |
| Theme-Toggle funktioniert nicht      |   3    | `ThemeService.Toggle()` im SSR-Modus ohne SignalR-Verbindung wirkungslos         |
| Antragsstrecke Validierung/Durchlauf |   2    | Selektoren im Test passen nicht zur tatsächlichen DOM-Struktur                   |
| Dashboard Fortschrittslabel          |   1    | CSS-Klasse `.kern-progress__label` existiert nicht – tatsächlich `.kern-label`   |
| Responsive Overflow auf Mobil        |   1    | Antragsstrecke hat horizontalen Scrollbalken auf 390px Viewport                  |
| 404-Button navigiert nicht           |   1    | `NavigationManager.NavigateTo("/")` funktioniert im SSR-Modus nicht clientseitig |

---

## Issue 1 – PageTitle wird bei SSR-Seiten nicht aktualisiert

**Tests betroffen:** 11 (alle Routen außer `/`)

**Symptom:**  
Alle Seiten zeigen den Titel `"PublicKernBlazor.Components Showcase"` (aus `App.razor` `<title>`) statt
des seitenspezifischen Titels (z.B. `"Typografie - PublicKernBlazor.Components Showcase"`).

**Ursache:**  
Die `<PageTitle>`-Komponente in Blazor aktualisiert den Browser-Titel erst, wenn die Seite als
**InteractiveServer** oder **InteractiveWebAssembly** gerendert wird. Bei Static SSR-Seiten
(ohne `@rendermode`) wird `<PageTitle>` serverseitig in die `<head>`-Sektion gestreamt, aber:

1. `App.razor` enthält ein statisches `<title>PublicKernBlazor.Components Showcase</title>` (Zeile 8)
2. `<HeadOutlet />` steht **nach** dem `<title>`-Tag
3. Playwright wartet auf `NetworkIdle`, liest dann den Titel – aber der Browser verwendet
   den statischen `<title>`, weil `<HeadOutlet>` bei SSR die Aktualisierung über JS-Interop
   benötigt, die bei Static SSR nicht verfügbar ist

**Fix-Optionen (Demo-App):**

| Option                                                    | Aufwand    | Beschreibung                                                                   |
|-----------------------------------------------------------|------------|--------------------------------------------------------------------------------|
| A: `<title>` aus `App.razor` entfernen                    | ⭐ Niedrig  | Nur `<HeadOutlet />` behalten – überlässt `<PageTitle>` die volle Kontrolle    |
| B: Alle Seiten auf `@rendermode InteractiveServer` setzen | Mittel     | Dann funktioniert `<PageTitle>` korrekt, aber erhöht Server-Last               |
| C: Test-Erwartung anpassen                                | Workaround | `Contains`-Check statt `Equals` – behandelt aber nicht das eigentliche Problem |

**Empfehlung:** Option A – `<title>` in `App.razor` durch `<HeadOutlet />` ersetzen lassen.

**Entscheidung:** Bitte Option A umsetzen, damit die Seitentitel korrekt angezeigt werden, ohne alle Seiten auf
InteractiveServer umzustellen.

---

## Issue 2 – Theme-Toggle hat keine Wirkung (SSR-Seiten)

**Tests betroffen:** 3 (`ThemeToggle_WechseltAttributAufDark`, `ThemeToggle_SetztkernThemeCookie`,
`ThemeToggle_ZweimalKlick_WechseltZurueckAufLight`)

**Symptom:**

- `data-kern-theme` bleibt nach Klick auf Toggle bei `"light"`
- Cookie `kern-theme` wird nicht gesetzt

**Ursache:**  
Die Startseite (`Home.razor`) hat **kein** `@rendermode InteractiveServer`. Damit wird der
Theme-Toggle-Button in `MainLayout.razor` ebenfalls als Static SSR gerendert:

- `ThemeService.Toggle()` wird aufgerufen – aber nur auf dem **Server**
- Der Server hat keine SignalR-Verbindung zum Client → kein Re-Render
- `data-kern-theme` auf dem `<div>` (in `KernThemeProvider`) wird nicht aktualisiert
- Der Cookie wird nicht gesetzt (kein JS-Interop, kein HTTP-Redirect)

Das Anti-FOUC-Script in `App.razor` setzt `data-kern-theme` auf dem `<html>`-Element,
aber `KernThemeProvider` setzt es auf einem inneren `<div>`. Die Tests prüfen `<html>`,
das Anti-FOUC-Script ist also korrekt – aber der Blazor-Toggle wirkt nur auf den
`<div>` im `KernThemeProvider`.

**Fix-Optionen (Demo-App / Library):**

| Option                                             | Aufwand    | Beschreibung                                                                             |
|----------------------------------------------------|------------|------------------------------------------------------------------------------------------|
| A: `MainLayout.razor` auf InteractiveServer setzen | ⭐ Niedrig  | `@rendermode InteractiveServer` im Layout → alle Seiten interaktiv                       |
| B: Theme-Toggle via JS-Interop                     | Mittel     | Cookie setzen + `data-kern-theme` auf `<html>` per JS ändern (funktioniert auch bei SSR) |
| C: Tests anpassen: `<div>` statt `<html>` prüfen   | Workaround | Prüft den `KernThemeProvider`-Container statt das Root-Element                           |

**Empfehlung:** Option A für die Demo, Option B langfristig für die Library.

**Entscheidung:** Bitte Option B umsetzen, aber JS-Interop minimal halten (nur Cookie + `data-kern-theme` auf `<html>`),
damit der Toggle auch bei SSR funktioniert, ohne alle Seiten auf InteractiveServer umzustellen.

---

## Issue 3 – Antragsstrecke: Validierungsfehler nicht sichtbar (falscher Selektor)

**Test betroffen:** `Antrag_PflichtfeldLeer_ZeigtValidierungsfehler`

**Symptom:**

```
Locator(".kern-form-input__error").First – element(s) not found
```

**Ursache:**  
Der Test sucht nach `.kern-form-input__error`, aber die tatsächliche CSS-Klasse für
Fehlermeldungen in der Library ist **`.kern-error`** (siehe `KernFormField.razor`, Zeile 25):

```razor
<p class="kern-error" id="@ResolvedErrorId" role="alert">
```

**Fix (Smoke-Tests):**  
Selektor in `InteractiveSmokeTests.cs` ändern: `.kern-form-input__error` → `.kern-error`

---

## Issue 4 – Antragsstrecke: Zusammenfassung nicht gefunden

**Test betroffen:** `Antrag_VollstaendigerDurchlauf_ZeigtErfolgsmeldung`

**Symptom:**

```
Locator(".kern-summary") – element(s) not found (nach Schritt 2)
```

**Ursache:**  
Der Test füllt Schritt 1 und 2 per `FuelleSchritt1Async()` / `FuelleSchritt2Async()` aus.
Die Selektoren in den `Fill`-Methoden verwenden generische Locatoren wie
`Page.Locator("input[type='text']").Nth(0)` – diese sind fragil und treffen möglicherweise
nicht die richtigen Felder, wenn sich die DOM-Reihenfolge ändert oder zusätzliche Felder
(z.B. RadioGroup, Geburtsdatum-Selects) vorhanden sind.

Zudem gibt es eine Validierung in `NextStep()` – wenn die Pflichtfelder nicht korrekt
befüllt werden, bleibt der Wizard auf dem aktuellen Schritt stehen.

**Fix-Optionen:**

| Option                                  | Aufwand     | Beschreibung                                                                   |
|-----------------------------------------|-------------|--------------------------------------------------------------------------------|
| A: `data-testid`-Attribute in der Demo  | ⭐ Empfohlen | Eindeutige Selektoren: `data-testid="vorname"`, `data-testid="nachname"`, etc. |
| B: Selektoren im Page Object verfeinern | Mittel      | Label-basierte Locatoren: `Page.GetByLabel("Vorname")`                         |
| C: Test auf Schritt-1-only reduzieren   | Workaround  | Nur prüfen, dass Schritt 1 korrekt angezeigt wird                              |

**Empfehlung:** Option A – `data-testid`-Attribute in `AntragExample.razor` hinzufügen.

**Entscheidung:** Im Sinne der Barrierfreiheit sollte Option B umgesetzt werden, damit die Tests auch ohne spezielle
Test-IDs robust bleiben. Bitte die Selektoren in den `Fill`-Methoden auf label-basierte Locatoren umstellen (z.B.
`Page.GetByLabel("Vorname")`), damit sie semantisch korrekt und weniger anfällig für DOM-Änderungen sind.

---

## Issue 5 – Dashboard: Fortschrittslabel nicht gefunden (falscher CSS-Selektor)

**Test betroffen:** `Dashboard_SimulierenButton_AktualisiertFortschritt`

**Symptom:**

```
Timeout 10000ms exceeded – waiting for Locator(".kern-progress").Locator(".kern-progress__label")
```

**Ursache:**  
Die `KernProgress`-Komponente rendert das Label als `<label class="kern-label">`, nicht als
`.kern-progress__label`:

```razor
<!-- KernProgress.razor (tatsächliches Rendering) -->
<label class="kern-label" for="@ResolvedId">@Label</label>
```

Der Test-Selektor `.kern-progress__label` existiert nicht im DOM.

**Fix (Smoke-Tests):**  
Selektor in `DashboardPage.cs` ändern:

```csharp
// Vorher (falsch)
public Task<string> GetFortschrittsLabelAsync() =>
    Fortschritt.Locator(".kern-progress__label").InnerTextAsync();

// Nachher (korrekt)
public Task<string> GetFortschrittsLabelAsync() =>
    Fortschritt.Locator(".kern-label").InnerTextAsync();
```

---

## Issue 6 – Responsive: Horizontaler Scrollbalken auf Mobil (390px)

**Test betroffen:** `Antragsstrecke_RendertAufMobilOhneHorizontalenScrollbalken`

**Symptom:**

```
scrollWidth > clientWidth auf 390px Viewport
```

**Ursache:**  
Die Antragsstrecke verwendet `KernContainer` → `KernRow` → `KernCol` mit festen `Span`-Werten.
Bei 390px Breite können bestimmte Elemente (z.B. die Button-Reihe mit `KernCol Span="6"`)
über den Viewport hinausragen, besonders wenn:

- Padding/Margin des Containers nicht für Mobil optimiert ist
- Inputs mit fester `min-width` oder das Grid nicht auf `Span="12"` bei kleinen Viewports umschaltet

Mögliche Verursacher:

- `KernFieldset` mit horizontaler RadioGroup
- Die Button-Reihe mit `Span="6" SpanMd="4"` → bei Mobil noch `Span="6"` statt `Span="12"`

**Fix-Optionen (Demo-App):**

| Option                                        | Aufwand         | Beschreibung                                    |
|-----------------------------------------------|-----------------|-------------------------------------------------|
| A: Responsive Spans korrigieren               | ⭐ Niedrig       | `Span="12"` als Basis, `SpanMd="6"` für Tablet+ |
| B: `overflow-x: hidden` auf `.kern-container` | Nicht empfohlen | Versteckt das Problem nur                       |
| C: Test-Toleranz einbauen                     | Workaround      | Erst ab Abweichung > 5px als Fehler werten      |

**Empfehlung:** Option A – Responsive Spans in `AntragExample.razor` prüfen und anpassen.

**Entscheidung:** Bitte Option A umsetzen, damit die Antragsstrecke auch auf Mobilgeräten ohne Scrollbalken korrekt
dargestellt wird. Insbesondere die Button-Reihe sollte bei kleinen Viewports auf `Span="12"` umschalten, damit sie nicht
über den Bildschirm hinausragt.

---

## Issue 7 – NotFound: „Zur Startseite"-Button navigiert nicht

**Test betroffen:** `NichtGefundenSeite_ZurStartseiteButton_NavigiertZurueck`

**Symptom:**

```
Page URL expected to be 'https://localhost:7023/'
But was: 'https://localhost:7023/not-found'
```

**Ursache:**  
`NotFound.razor` hat **kein** `@rendermode InteractiveServer`. Der Button-Klick ruft
`NavigationManager.NavigateTo("/")` auf – aber in Static SSR hat die Seite keine
SignalR-Verbindung, sodass `NavigateTo()` keinen clientseitigen Redirect auslöst.

Die Seite wird als pures HTML ausgeliefert; der `@onclick`-Handler wird nicht verdrahtet.

**Fix (Demo-App):**  
`@rendermode InteractiveServer` in `NotFound.razor` hinzufügen, oder den Button durch
einen normalen `<a href="/">` Link ersetzen (funktioniert auch bei SSR).

---

## Status je Issue

| Issue | Kurzbeschreibung                    | Entscheidung                                           | Erledigt |
|-------|-------------------------------------|--------------------------------------------------------|:--------:|
| 1     | PageTitle bei SSR-Seiten            | Option A (`<title>` in `App.razor` entfernen)          |    ✅     |
| 2     | Theme-Toggle ohne Wirkung bei SSR   | Option B (minimales JS für Cookie + `data-kern-theme`) |    ✅     |
| 3     | Falscher Selektor für Fehlermeldung | `.kern-form-input__error` → `.kern-error`              |    ✅     |
| 4     | Fragile Antrag-Selektoren           | Option B (label-/role-basierte Locatoren)              |    ✅     |
| 5     | Falscher Progress-Selektor          | `.kern-progress__label` → `.kern-label`                |    ✅     |
| 6     | Horizontaler Overflow auf Mobil     | Option A (responsive `Span="12"` + `SpanMd=...`)       |    ✅     |
| 7     | NotFound-Navigation im SSR-Modus    | SSR-sicherer `<a href="/">` Link                       |    ✅     |

---

## Post-Fix

Nach Umsetzung der Entscheidungen aus allen 7 Issues wurden Demo und Smoke-Tests erneut verifiziert.

### Ergebnis

- **Smoke-Tests:** `27/27` bestanden, `0` fehlgeschlagen
- **Build Demo:** erfolgreich, `0` Warnungen, `0` Fehler
- **Build SmokeTests:** erfolgreich, `0` Warnungen, `0` Fehler

### Validierte Korrekturen (Kurz)

- Seitentitel werden pro Route korrekt gesetzt (SSR + `HeadOutlet`)
- Theme-Toggle funktioniert auch auf SSR-Seiten (Cookie + `data-kern-theme`)
- Antrag- und Dashboard-Selektoren sind stabil (semantisch statt fragiler DOM-Indizes)
- NotFound-Navigation funktioniert ohne Interaktivitätsmodus
- Mobile-Layout der Antragsstrecke ohne horizontalen Scrollbalken

### Offene Punkte

- Keine offenen Smoke-Test-Issues aus diesem Durchlauf

---

## Historie (initialer Stand)

### ✅ Bestanden (8)

| Test                                                 | Testklasse            |
|------------------------------------------------------|-----------------------|
| `AntiFouc_ThemeAttributIstVorBlazorHydrationGesetzt` | ThemeSmokeTests       |
| `Navigation_EnthaeltAlleShowcaseLinks`               | NavigationSmokeTests  |
| `Seite_LaeadtMitKorrektemTitelOhneFehler(/)`         | NavigationSmokeTests  |
| `Desktop (1280×800)`                                 | ResponsiveSmokeTests  |
| `Tablet (768×1024)`                                  | ResponsiveSmokeTests  |
| `Mobil (390×844)`                                    | ResponsiveSmokeTests  |
| `ButtonSeite_KlickZaehler_ErhoehtsichBeiKlick`       | InteractiveSmokeTests |
| `ContentSeite_Dialog_OeffnetUndSchliessst`           | InteractiveSmokeTests |

### ❌ Fehlgeschlagen (19)

| Test                                                         | Issue   | Fix-Ort                  |
|--------------------------------------------------------------|---------|--------------------------|
| PageTitle für 10 Routen (außer `/`)                          | Issue 1 | Demo: `App.razor`        |
| `ThemeToggle_WechseltAttributAufDark`                        | Issue 2 | Demo: `MainLayout.razor` |
| `ThemeToggle_SetztkernThemeCookie`                           | Issue 2 | Demo: `MainLayout.razor` |
| `ThemeToggle_ZweimalKlick_WechseltZurueckAufLight`           | Issue 2 | Demo: `MainLayout.razor` |
| `Antrag_PflichtfeldLeer_ZeigtValidierungsfehler`             | Issue 3 | Tests: Selektor          |
| `Antrag_VollstaendigerDurchlauf_ZeigtErfolgsmeldung`         | Issue 4 | Tests + Demo             |
| `Dashboard_SimulierenButton_AktualisiertFortschritt`         | Issue 5 | Tests: Selektor          |
| `Antragsstrecke_RendertAufMobilOhneHorizontalenScrollbalken` | Issue 6 | Demo: Layout             |
| `NichtGefundenSeite_ZurStartseiteButton_NavigiertZurueck`    | Issue 7 | Demo: SSR-Modus          |

---

## Priorisierte Fix-Reihenfolge (initial)

1. **Issue 1** – `<title>` aus `App.razor` entfernen → löst 11 Tests
2. **Issue 2** – `MainLayout.razor` auf `@rendermode InteractiveServer` → löst 3 Tests
3. **Issue 3 + 5** – Selektoren in Tests korrigieren → löst 2 Tests
4. **Issue 7** – NotFound-Button als `<a href="/">` → löst 1 Test
5. **Issue 4** – `data-testid`-Attribute + Selektor-Verfeinerung → löst 1 Test
6. **Issue 6** – Responsive Spans prüfen → löst 1 Test

**Geschätzte Auswirkung nach Fix 1–4:** 17 von 19 Tests sollten bestehen (28 von 27 → kein Test mehr offen).
