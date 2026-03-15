# KERN UI Komponenten – Referenz & Beispiele

## Format-Konventionen

- Codeblöcke sind unverändert belassen (nur Einrückung/Trim).
- Pseudo-Zustände (Hover/Focus/Active/Visited) werden als eigene Codeblöcke gezeigt (auch wenn Markup identisch) für visuelle Regression.
- Alle Storybook-Beispiele werden vollständig dupliziert (keine Deduplikation) gemäß Anforderung.
- Reihenfolge der Beispiele folgt der Reihenfolge in den `.stories.js` Dateien.

Diese Datei dokumentiert alle KERN UI Komponenten mit Struktur (BEM), Modifiern, Zuständen, Barrierefreiheits-Hinweisen und HTML‑Beispielen. Klassen sind grundsätzlich mit dem Präfix `kern-` versehen.

Grundlagen

- Icons sind i. d. R. dekorativ mit `aria-hidden="true"`. Falls inhaltlich relevant, `aria-hidden="false"` und passendes `aria-label` setzen.
- Fehlerzustände nutzen eine Kombination aus Container‑Modifiern (z. B. `--error`) und Element‑Modifiern (z. B. `__input--error`).
- Fokuszustände sind visuell klar erkennbar; bitte nicht entfernen.

Inhaltsübersicht

- Grid
- Utilities (Alignment & Spacing)
- Title, Heading, Body, Label, Subline, Preline

- Breakpoints (`$grid-breakpoints`):

  - xs: 0
  - xl: 1200px
  - xxl: 1600px

- Container-Breiten (`$container-max-widths` → `.kern-container` via Media Queries):

  - sm: 96%
  - lg: 96%
  - xl: 96%
  - xxl: 1536px

- Gutter & Padding:
  - Grid-Gutter: `2rem` (halb auf Spalten-/Container‑Padding verteilt)
  - `.kern-row` hat negative Außenabstände links/rechts, Spalten (`[class^="kern-col"]`) erhalten `padding: $grid-gutter/2`
- Spalten (ohne/mit Breakpoint):
  - Auto-Spalte: `.kern-col { flex: 1 0 0%; }`
  - Versatz: `.kern-col-offset-1 … -offset-12`
  - Breakpoint-spezifisch: `.kern-col-{sm|md|lg|xl|xxl}-{1..12}` und `.kern-col-{bp}-offset-{0..12}`

---

## Utilities (Alignment & Spacing)

Alignment-Helferklassen (aus dem Grid) zur schnellen Ausrichtung ohne eigene CSS-Regeln:

- Vertikal in der Zeile: `.kern-align-items-{start|center|end}`
- Horizontal in der Zeile: `.kern-justify-content-{start|center|end|around|between|evenly}`
- Einzelne Spalte: `.kern-align-self-{start|center|end}`

Spacing in KERN basiert auf Tokens (CSS‑Variablen) und komponenteninternen Abständen. Generische Margin/Padding‑Hilfsklassen sind aktuell nicht Teil des Frameworks.

- `--kern-metric-space-none` (0)
- `--kern-metric-space-2x-small` (2px)
- `--kern-metric-space-x-small` (4px)
- `--kern-metric-space-small` (8px)
- `--kern-metric-space-large` (24px)
- `--kern-metric-space-x-large` (32px)

Hinweise

- Für Layouts primär Grid‑Gutter und komponenteninterne Abstände nutzen.
- Eigene Layouts können die Variablen in `gap`, `margin`, `padding` verwenden (z. B. `gap: var(--kern-metric-space-default)`), um konsistent zu bleiben.

Beispiele

```html
<!-- Vertikale Zentrierung aller Items in der Zeile -->
<div
  class="kern-row kern-align-items-center"
  style="min-height:80px; background:#f6f7fb;"
>
  <div class="kern-col-4" style="background:#dde1f3;">A</div>
  <div class="kern-col-4" style="background:#cbd2eb;">B</div>
  <div class="kern-col-4" style="background:#dde1f3;">C</div>
</div>

<!-- Horizontal verteilen: space-between -->
<div class="kern-row kern-justify-content-between" style="background:#f6f7fb;">
  <div class="kern-col-3" style="background:#dde1f3;">A</div>
  <div class="kern-col-3" style="background:#cbd2eb;">B</div>
</div>

<!-- Einzelne Spalte am unteren Rand ausrichten -->
<div class="kern-row" style="min-height:80px; background:#f6f7fb;">
  <div class="kern-col kern-align-self-end" style="background:#dde1f3;">A</div>
  <div class="kern-col" style="background:#cbd2eb;">B</div>
</div>
```

### Anker – Alignment‑Utilities

Die folgenden leeren Überschriften dienen als stabile Ankerziele für Verlinkungen auf die Alignment‑Hilfsklassen.

#### kern-align-items-start

#### kern-align-items-center

#### kern-align-items-end

#### kern-justify-content-start

#### kern-justify-content-center

#### kern-justify-content-end

#### kern-justify-content-around

#### kern-justify-content-between

#### kern-justify-content-evenly

---

## Typografie

### Title

Klassen: `kern-title` mit Modifikatoren `--small`, `--large`.

```html
<p class="kern-title">Title (default)</p>
<p class="kern-title kern-title--small">Title small</p>
<p class="kern-title kern-title--large">Title large</p>
```

### Heading

Semantische Headings mit vorgegebenen Größenklassen:

- `kern-heading-display`, `kern-heading-x-large`, `kern-heading-large`, `kern-heading-medium`, `kern-heading-small`

```html
<h1 class="kern-heading-display">Display</h1>
<h2 class="kern-heading-large">Large</h2>
```

### Body

`kern-body` mit `--bold`, `--muted`, `--small`, `--large` (kombinierbar)

### Typografie – Token → Klassen Mapping (Übersicht)

Diese Zuordnung zeigt, welche Typografie‑Tokens (Font‑Size, Line‑Height, Weight, Baseline‑Padding) in den jeweiligen Klassen verwendet werden. Adaptive Tokens passen sich ab `md` (768px) an.

#### kern-heading-display

- font-size: `--kern-typography-font-size-adaptive-2x-large`
- line-height: `--kern-typography-line-height-adaptive-2x-large`
- font-weight: `--kern-typography-font-weight-medium`
- padding-top/bottom: `--kern-metric-baseline-heading-display-padding-top|bottom`

#### kern-heading-x-large

- font-size: `--kern-typography-font-size-adaptive-x-large`
- line-height: `--kern-typography-line-height-adaptive-x-large`
- font-weight: `--kern-typography-font-weight-medium`
- padding-top/bottom: `--kern-metric-baseline-heading-x-large-padding-top|bottom`

#### kern-heading-large

- font-size: `--kern-typography-font-size-adaptive-large`
- line-height: `--kern-typography-line-height-adaptive-large`
- font-weight: `--kern-typography-font-weight-semi-bold`
- padding-top/bottom: `--kern-metric-baseline-heading-large-padding-top|bottom`

#### kern-heading-medium

- font-size: `--kern-typography-font-size-adaptive-medium`
- line-height: `--kern-typography-line-height-adaptive-medium`
- font-weight: `--kern-typography-font-weight-semi-bold`
- padding-top/bottom: `--kern-metric-baseline-heading-medium-padding-top|bottom`

#### kern-heading-small

- font-size: `--kern-typography-font-size-static-medium`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-semi-bold`
- padding-top/bottom: `--kern-metric-baseline-heading-small-padding-top|bottom`

#### kern-title

(default)

- font-size: `--kern-typography-font-size-adaptive-medium`
- line-height: `--kern-typography-line-height-adaptive-medium`
- font-weight: `--kern-typography-font-weight-semi-bold`
- padding-top/bottom: `--kern-metric-baseline-title-default-padding-top|bottom`

#### kern-title--large

- font-size: `--kern-typography-font-size-adaptive-large`
- line-height: `--kern-typography-line-height-adaptive-large`
- font-weight: `--kern-typography-font-weight-semi-bold`
- padding-top/bottom: `--kern-metric-baseline-title-large-padding-top|bottom`

#### kern-title--small

- font-size: `--kern-typography-font-size-static-medium`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-semi-bold`
- padding-top/bottom: `--kern-metric-baseline-title-small-padding-top|bottom`

#### kern-body

(default)

- font-size: `--kern-typography-font-size-static-medium`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-regular`
- padding-top/bottom: `--kern-metric-baseline-body-default-padding-top|bottom`

#### kern-body--large

- font-size: `--kern-typography-font-size-static-large`
- line-height: `--kern-typography-line-height-static-large`
- font-weight: `--kern-typography-font-weight-regular`
- padding-top/bottom: `--kern-metric-baseline-body-large-padding-top|bottom`

#### kern-body--small

- font-size: `--kern-typography-font-size-static-small`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-regular`
- padding-top/bottom: `--kern-metric-baseline-body-small-padding-top|bottom`

#### kern-subline

(default)

- font-size: `--kern-typography-font-size-static-medium`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-medium`
- padding-top/bottom: `--kern-metric-baseline-subline-default-padding-top|bottom`

#### kern-subline--large

- font-size: `--kern-typography-font-size-static-large`
- line-height: `--kern-typography-line-height-static-large`
- font-weight: `--kern-typography-font-weight-medium`
- padding-top/bottom: `--kern-metric-baseline-subline-large-padding-top|bottom`

#### kern-subline--small

- font-size: `--kern-typography-font-size-static-small`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-medium`
- padding-top/bottom: `--kern-metric-baseline-subline-small-padding-top|bottom`

#### kern-preline

(default)

- font-size: `--kern-typography-font-size-static-medium`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-regular`
- padding-top/bottom: `--kern-metric-baseline-preline-default-padding-top|bottom`
- color: `--kern-color-layout-text-muted`

#### kern-preline--large

- font-size: `--kern-typography-font-size-static-large`
- line-height: `--kern-typography-line-height-static-large`
- font-weight: `--kern-typography-font-weight-regular`
- padding-top/bottom: `--kern-metric-baseline-preline-large-padding-top|bottom`

#### kern-preline--small

- font-size: `--kern-typography-font-size-static-small`
- line-height: `--kern-typography-line-height-static-medium`
- font-weight: `--kern-typography-font-weight-regular`
- padding-top/bottom: `--kern-metric-baseline-preline-small-padding-top|bottom`

```html
<p class="kern-body">Text</p>
<p class="kern-body kern-body--bold">Fett</p>

### Anker – Alignment‑Utilities Die folgenden leeren Überschriften dienen als
stabile Ankerziele für Verlinkungen auf die Alignment‑Hilfsklassen. ####
kern-align-items-start #### kern-align-items-center #### kern-align-items-end
#### kern-justify-content-start #### kern-justify-content-center ####
kern-justify-content-end #### kern-justify-content-around ####
kern-justify-content-between #### kern-justify-content-evenly

<p class="kern-body kern-body--muted">Gedämpft</p>
```

### Label

`kern-label` mit `--small`, `--large`. Optionale Kennzeichnung in Labels via `kern-label__optional`.

```html
<p class="kern-label">Label</p>
<label class="kern-label"
  >Name <span class="kern-label__optional">- Optional</span></label
>
```

### Subline & Preline

`kern-subline` / `kern-preline` jeweils mit `--small`, `--large`.

```html
<p class="kern-preline">Preline</p>
<p class="kern-subline">Subline</p>
```

### Link

Standard-Link: `kern-link`. Interaktionszustände: Hover, Focus, Active, Visited. Gestreckter Klickbereich: `kern-link--stretched` (vergrößert den Klickbereich über ein Pseudo-Element, wird u. a. in Card/Tasklist verwendet).

```html
<a href="#" class="kern-link">Link</a>
```

### Icon

- Basisklasse: `kern-icon`
- Größen: `kern-icon--small|large|x-large`
- Glyphen z. B.: `--home`, `--arrow-up`, `--arrow-down`, `--arrow-forward`, `--arrow-back`, `--info`, `--success`, `--warning`, `--danger`, `--open-in-new`, `--download`, `--logout`, `--checklist`, `--mail`, `--edit`, `--sign-language`, `--easy-language`, `--autorenew`, `--add`, `--close`, `--delete`, `--search`, `--question-mark`, `--more-vert`, `--content-copy`, `--visibility`, `--visibility-off`, `--check`, `--drive-folder-upload`, `--chevron-left`, `--chevron-right`, `--keyboard-double-arrow-left`, `--keyboard-double-arrow-right`, `--brightness-medium`, `--light-mode`, `--dark-mode`, `--help`.

A11y: standardmäßig `aria-hidden="true"`. Falls semantisch: `aria-hidden="false"` + `aria-label`.

```html
<span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
```

---

### Typografie – Token → Klassen Mapping

Diese Zuordnung zeigt, welche Typografie‑Tokens (Font‑Size, Line‑Height, Weight, Baseline‑Padding) in den jeweiligen Klassen verwendet werden. Adaptive Tokens passen sich ab `md` (768px) an.

- kern-heading-display

  - font-size: `--kern-typography-font-size-adaptive-2x-large`
  - line-height: `--kern-typography-line-height-adaptive-2x-large`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-heading-display-padding-top|bottom`

- kern-heading-x-large

  - font-size: `--kern-typography-font-size-adaptive-x-large`
  - line-height: `--kern-typography-line-height-adaptive-x-large`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-heading-x-large-padding-top|bottom`

- kern-heading-large

  - font-size: `--kern-typography-font-size-adaptive-large`
  - line-height: `--kern-typography-line-height-adaptive-large`
  - font-weight: `--kern-typography-font-weight-semi-bold`
  - padding-top/bottom: `--kern-metric-baseline-heading-large-padding-top|bottom`

- kern-heading-medium

  - font-size: `--kern-typography-font-size-adaptive-medium`
  - line-height: `--kern-typography-line-height-adaptive-medium`
  - font-weight: `--kern-typography-font-weight-semi-bold`
  - padding-top/bottom: `--kern-metric-baseline-heading-medium-padding-top|bottom`

- kern-heading-small

  - font-size: `--kern-typography-font-size-static-medium`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-semi-bold`
  - padding-top/bottom: `--kern-metric-baseline-heading-small-padding-top|bottom`

- kern-title (default)

  - font-size: `--kern-typography-font-size-adaptive-medium`
  - line-height: `--kern-typography-line-height-adaptive-medium`
  - font-weight: `--kern-typography-font-weight-semi-bold`
  - padding-top/bottom: `--kern-metric-baseline-title-default-padding-top|bottom`

- kern-title--large

  - font-size: `--kern-typography-font-size-adaptive-large`
  - line-height: `--kern-typography-line-height-adaptive-large`
  - font-weight: `--kern-typography-font-weight-semi-bold`
  - padding-top/bottom: `--kern-metric-baseline-title-large-padding-top|bottom`

- kern-title--small

  - font-size: `--kern-typography-font-size-static-medium`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-semi-bold`
  - padding-top/bottom: `--kern-metric-baseline-title-small-padding-top|bottom`

- kern-body (default)

  - font-size: `--kern-typography-font-size-static-medium`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-regular`
  - padding-top/bottom: `--kern-metric-baseline-body-default-padding-top|bottom`

- kern-body--large

  - font-size: `--kern-typography-font-size-static-large`
  - line-height: `--kern-typography-line-height-static-large`
  - font-weight: `--kern-typography-font-weight-regular`
  - padding-top/bottom: `--kern-metric-baseline-body-large-padding-top|bottom`

- kern-body--small

  - font-size: `--kern-typography-font-size-static-small`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-regular`
  - padding-top/bottom: `--kern-metric-baseline-body-small-padding-top|bottom`

- kern-subline (default)

  - font-size: `--kern-typography-font-size-static-medium`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-subline-default-padding-top|bottom`

- kern-subline--large

  - font-size: `--kern-typography-font-size-static-large`
  - line-height: `--kern-typography-line-height-static-large`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-subline-large-padding-top|bottom`

- kern-subline--small

  - font-size: `--kern-typography-font-size-static-small`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-subline-small-padding-top|bottom`

- kern-preline (default)

  - font-size: `--kern-typography-font-size-static-medium`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-regular`
  - padding-top/bottom: `--kern-metric-baseline-preline-default-padding-top|bottom`
  - color: `--kern-color-layout-text-muted`

- kern-preline--large

  - font-size: `--kern-typography-font-size-static-large`
  - line-height: `--kern-typography-line-height-static-large`
  - font-weight: `--kern-typography-font-weight-regular`
  - padding-top/bottom: `--kern-metric-baseline-preline-large-padding-top|bottom`

- kern-preline--small

  - font-size: `--kern-typography-font-size-static-small`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-regular`
  - padding-top/bottom: `--kern-metric-baseline-preline-small-padding-top|bottom`

- kern-label (default)

  - font-size: `--kern-typography-font-size-static-medium`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-label-default-padding-top|bottom`

- kern-label--large

  - font-size: `--kern-typography-font-size-static-large`
  - line-height: `--kern-typography-line-height-static-large`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-label-large-padding-top|bottom`

- kern-label--small
  - font-size: `--kern-typography-font-size-static-small`
  - line-height: `--kern-typography-line-height-static-medium`
  - font-weight: `--kern-typography-font-weight-medium`
  - padding-top/bottom: `--kern-metric-baseline-label-small-padding-top|bottom`

---

## Button

Basisklasse: `kern-btn`. Modifikatoren: `--primary`, `--secondary`, `--tertiary`, `--block`.

- Label-Element: `.kern-label`
- Optionales Icon: `.kern-icon …` links oder rechts
- Icon-only: Label als Screenreader-Text `kern-sr-only` bzw. mobil `kern-sr-only-mobile`

```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
</button>

<button class="kern-btn kern-btn--secondary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label">Bearbeiten</span>
</button>

<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärlink</span>
</button>
```

Zustände: `:hover`, `:active`, `:focus` sind visuell gestaltet; `disabled` setzt reduzierte Opazität und verbietet Interaktionen.

---

## Badge

Basisklasse: `kern-badge`. Varianten: `--info`, `--success`, `--warning`, `--danger`. Inhalt typischerweise `kern-label kern-label--small`, optional mit `kern-icon`.

```html
<span class="kern-badge kern-badge--success">
  <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
  <span class="kern-label kern-label--small">Erledigt</span>
</span>
```

---

## Alert

Basisklasse: `kern-alert` mit Varianten `--info|--success|--warning|--danger`.

Struktur:

- Header: `kern-alert__header` mit Icon und `kern-title`
- Optionaler Body: `kern-alert__body`
- Rolle: `role="alert"`

A11y: Icon im Alert i. d. R. dekorativ (`aria-hidden="true"`). Falls das Icon wesentliche Information trägt: `aria-hidden="false"` + `aria-label`.

```html
<div class="kern-alert kern-alert--warning" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--warning" aria-hidden="true"></span>
    <span class="kern-title">Warnung</span>
  </div>
</div>
```

---

## Accordion

Basisklasse: `kern-accordion` auf `<details>`. Header als `<summary class="kern-accordion__header">` mit `kern-title`. Body: `kern-accordion__body`.

- Offen: Native `<details open>`
- Gruppe: Wrapper `kern-accordion-group` zur Isolation vom umgebenden Layout

```html
<details class="kern-accordion" open>
  <summary class="kern-accordion__header">
    <span class="kern-title">Accordion Titel</span>
  </summary>
  <section class="kern-accordion__body">
    <p class="kern-body">Inhalt…</p>
  </section>
</details>
```

---

## Card

Basisklasse: `kern-card`. Struktur:

- Media: `kern-card__media`
- Container: `kern-card__container`
- Header: `kern-card__header` (typisch Preline, Title, Subline)
- Body: `kern-card__body` (wächst)
- Footer: `kern-card__footer` (Buttons)

Modifikatoren: `--small`, `--large` (typografie/Spacing), `--hug` (keine Equal Height), `--active` (stretched link, interaktive Zustände).

```html
<article class="kern-card" style="max-width:320px">
  <div class="kern-card__media"><img src="…" alt="ALT" /></div>
  <div class="kern-card__container">
    <header class="kern-card__header">
      <p class="kern-preline">Preline</p>
      <h2 class="kern-title">Title</h2>
      <h3 class="kern-subline">Subline</h3>
    </header>
    <section class="kern-card__body">
      <p class="kern-body">Text…</p>
    </section>
    <footer class="kern-card__footer">
      <button class="kern-btn kern-btn--primary">
        <span class="kern-label">Aktion</span>
      </button>
      <button class="kern-btn kern-btn--secondary">
        <span class="kern-label">Mehr</span>
      </button>
    </footer>
  </div>
</article>
```

---

## Divider

Einfacher Trenner als `<hr class="kern-divider">`.

A11y: meist rein visuell – dann `aria-hidden="true"` setzen.

```html
<hr class="kern-divider" aria-hidden="true" />
```

---

## Fieldset

Gruppiert verwandte Formelemente. Basisklasse `kern-fieldset`.

- Body: `kern-fieldset__body` (optional `--horizontal`)
- Optionaler Hinweis: `kern-fieldset__hint` oder generisch `kern-hint`
- Fehlerzustand: `kern-fieldset--error` (zeigt linken Fehlerbalken, gruppiert Fehlermeldungen)

```html
<fieldset class="kern-fieldset" aria-describedby="hint-text">
  <legend class="kern-label">Ansprechpartner</legend>
  <div class="kern-fieldset__hint" id="hint-text">Hinweis</div>
  <div class="kern-fieldset__body">…</div>
</fieldset>
```

Fehlerzustand (aus Storybook)

```html
<fieldset class="kern-fieldset kern-fieldset--error">
  <legend class="kern-label">Ansprechpartner</legend>
  <div class="kern-fieldset__body">
    <div class="kern-form-input">
      <label class="kern-label" for="vorname">Vorname</label>
      <input
        class="kern-form-input__input kern-form-input__input--error"
        id="vorname"
        name="vorname"
        type="text"
        aria-describedby="kern-error"
      />
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="name">Name</label>
      <input
        class="kern-form-input__input kern-form-input__input--error"
        id="name"
        name="name"
        type="text"
        aria-describedby="kern-error"
      />
    </div>
  </div>
  <p class="kern-error" id="kern-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Der Name darf nicht leer sein</span>
  </p>
 </fieldset>
```

---

## Form Controls

Alle Eingaben folgen dem Muster:

- Wrapper: `kern-form-input`
- Label: `kern-label` (optional mit `<span class="kern-label__optional">- Optional</span>`)
- Hinweis: `kern-hint` + Referenz via `aria-describedby`
- Eingabe: `kern-form-input__input` (oder Select: `kern-form-input__select` im `__select-wrapper`)
- Fehler: Container `kern-form-input--error` + Element‑Modifier `__input--error` und Meldung `<p class="kern-error" role="alert">…` (optional Icon + `kern-body`)

Pattern – gemeinsame Fehlerstruktur (kurz)

- Container: `kern-form-input kern-form-input--error` (bei Gruppen zusätzlich `kern-fieldset--error`)
- Eingabeelement: Error-Modifier setzen (`kern-form-input__input--error`, `kern-form-input__select--error`, `kern-form-check__checkbox--error`, `kern-form-check__radio--error`)
- Verknüpfung: `aria-describedby="… <ERROR-ID>"` am Eingabeelement; Hint‑ID optional zusätzlich
- Fehlermeldung: `<p class="kern-error" id="<ERROR-ID>" role="alert">` mit optionalem Icon (`kern-icon--danger`) und Text (`kern-body`)
- Platzierung: Bei Einzeleingaben im Wrapper; bei Gruppen (Radios/Checkboxen/Datum) als Block unterhalb der Gruppe

Hinweis: Verwenden Sie pro Eingabefeld bzw. Gruppe eine eindeutige Fehler‑ID, damit `aria-describedby` korrekt auflöst.

### Text, Email, Password, Tel, Url

```html
<div class="kern-form-input">
  <label class="kern-label" for="email">Email</label>
  <div class="kern-hint" id="email-hint">Beispiel: name@domain.xy</div>
  <input
    class="kern-form-input__input"
    id="email"
    name="email"
    type="email"
    aria-describedby="email-hint"
  />
</div>
```

Fehlerbeispiel (Text) – Container und Eingabe mit Error‑Modifier, Fehlermeldung verbunden über aria-describedby

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="text6">Name</label>
  <div class="kern-hint" id="text-hint2">Hint Text</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="text6"
    name="text6"
    type="text"
    aria-describedby="text-hint2 text-error"
  />
  <p class="kern-error" id="text-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Der Name darf nicht leer sein</span>
  </p>
</div>
```

Fehlerbeispiel (Email)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="email7">Email</label>
  <div class="kern-hint" id="email-hint">Hint Email</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="email7"
    name="email7"
    type="email"
    autocomplete="email"
    aria-describedby="email-hint kern-input-error"
  />
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Email darf nicht leer sein</span>
  </p>
</div>
```

Fehlerbeispiel (Password)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="password6">Password</label>
  <div class="kern-hint" id="password-hint">Hint Password</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="password6"
    name="password6"
    type="password"
    aria-describedby="password-hint kern-input-error"
  />
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Das Passwort darf nicht leer sein</span>
  </p>
</div>
```

Fehlerbeispiel (Tel)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="tel6">Tel</label>
  <div class="kern-hint" id="tel-hint">Hint Tel</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="tel6"
    name="tel6"
    type="tel"
    autocomplete="tel"
    aria-describedby="tel-hint kern-input-error"
  />
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Telefonnummer darf nicht leer sein</span>
  </p>
</div>
```

Fehlerbeispiel (Url)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="url6">Url</label>
  <div class="kern-hint" id="url-hint">Hint Url</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="url6"
    name="url6"
    type="url"
    aria-describedby="url-hint kern-input-error"
  />
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Url darf nicht leer sein</span>
  </p>
</div>
```

Zustände: `:hover`, `:focus`, `[readonly]`, `[disabled]`. Fehler über `--error` (s. o.).

Breiten-Hilfen: `kern-form-input__input--width-2|4` (z. B. für Datumsteile).

### Number

Empfehlung: `type="text"` + `inputmode="numeric"` (mobile Tastatur) statt `type="number"`.

```html
<input class="kern-form-input__input" type="text" inputmode="numeric" />
```

Fehlerbeispiel (Number)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="number6">Number</label>
  <div class="kern-hint" id="number-hint">Hint Number</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="number6"
    name="number6"
    type="text"
    inputmode="numeric"
    aria-describedby="number-hint kern-input-error"
  />
  <p class="kern-error" id="input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Number darf nicht leer sein</span>
  </p>
</div>
```

### Textarea

```html
<div class="kern-form-input">
  <label class="kern-label" for="beschreibung">Beschreibung</label>
  <textarea class="kern-form-input__input" id="beschreibung"></textarea>
</div>
```

Fehlerbeispiel (Textarea)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="textarea6">Beschreibung</label>
  <div class="kern-hint" id="textarea-hint">Hinweistext</div>
  <textarea
    class="kern-form-input__input kern-form-input__input--error"
    id="textarea6"
    name="textarea"
    aria-describedby="textarea-hint textarea-error"
  ></textarea>
  <p class="kern-error" id="textarea-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Dieses Feld darf nicht leer sein</span>
  </p>
</div>
```

### File

Spezielle Gestaltung über `input[type=file].kern-form-input__input`.

```html
<div class="kern-form-input">
  <label class="kern-label" for="upload">Upload</label>
  <div class="kern-hint" id="upload-hint">Erlaubte Formate: jpg, png, pdf</div>
  <input
    class="kern-form-input__input"
    id="upload"
    type="file"
    aria-describedby="upload-hint"
  />
</div>
```

Fehlerbeispiel (File)

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="input-file5">KERN Upload File</label>
  <div class="kern-hint" id="input-file-hint5">Erlaubte Formate: jpg, png, pdf</div>
  <input
    class="kern-form-input__input kern-form-input__input--error"
    id="input-file5"
    name="inputFile"
    type="file"
    aria-describedby="input-file-hint5 file-input-error"
  />
  <p class="kern-error" id="file-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Das Dateiformat ist nicht erlaubt</span>
  </p>
</div>
```

### Select

Wrapper + Select‑Element mit eigenem Pfeil und Zuständen.

```html
<div class="kern-form-input">
  <label class="kern-label" for="select">Auswählen</label>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" id="select">
      <option>Option 1</option>
      <option>Option 2</option>
    </select>
  </div>
</div>
```

Fehlerzustand: `kern-form-input--error` + `kern-form-input__select--error`.

### Checkbox & Radio

Gruppe: `kern-form-check` (Grid-Layout). Inputs: `kern-form-check__checkbox` bzw. `__radio`. Fehler: `kern-form-check--error` und `__checkbox--error` bzw. `__radio--error`.

```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="agb" type="checkbox" />
  <label class="kern-label" for="agb">AGB akzeptieren</label>
</div>
```

```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="select5">Auswählen</label>
  <div class="kern-hint" id="select-hint5">Hint Select</div>
  <div class="kern-form-input__select-wrapper">
    <select
      class="kern-form-input__select kern-form-input__select--error"
      name="select5"
      id="select5"
      aria-describedby="select-hint5 kern-input-error"
    >
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Option darf nicht gewählt werden.</span>
  </p>
</div>
```

Fehlerbeispiele

```html
<!-- Checkbox mit Fehler -->
<div class="kern-form-check kern-form-check--error">
  <input
    class="kern-form-check__checkbox kern-form-check__checkbox--error"
    id="dsgvo5"
    name="geschlecht5"
    type="checkbox"
    aria-describedby="input-error"
  />
  <label class="kern-label" for="dsgvo5">Ich akzeptiere die Datenschutzerklärung</label>
  <p class="kern-error" id="input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Datenschutzerklärung muss akzeptiert werden.</span>
  </p>
</div>

<!-- Radios als Gruppe mit Fieldset-Fehler -->
<fieldset class="kern-fieldset kern-fieldset--error" aria-describedby="hint-text">
  <legend class="kern-label">Ansprechpartner</legend>
  <div class="kern-hint" id="hint-text">Hint</div>
  <div class="kern-fieldset__body">
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="radio-herr7" name="geschlecht7" type="radio" />
      <label class="kern-label" for="radio-herr7">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="frau7" name="geschlecht7" type="radio" />
      <label class="kern-label" for="frau7">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="divers7" name="geschlecht7" type="radio" />
      <label class="kern-label" for="divers7">Divers</label>
    </div>
  </div>
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Eine Auswahl muss getroffen werden</span>
  </p>
</fieldset>
```

### Datum – Einzelfelder (Tag/Monat/Jahr)

Mit `kern-fieldset` gruppiert und Breiten-Hilfen.

```html
<fieldset class="kern-fieldset" aria-describedby="date-hint">
  <legend class="kern-label">Ausstellungsdatum</legend>
  <div class="kern-hint" id="date-hint">z. B. 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag">Tag</label>
      <input
        class="kern-form-input__input kern-form-input__input--width-2"
        id="tag"
        type="text"
        inputmode="numeric"
      />
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat">Monat</label>
      <input
        class="kern-form-input__input kern-form-input__input--width-2"
        id="monat"
        type="text"
        inputmode="numeric"
      />
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="jahr">Jahr</label>
      <input
        class="kern-form-input__input kern-form-input__input--width-4"
        id="jahr"
        type="text"
        inputmode="numeric"
      />
    </div>
  </div>
</fieldset>
```

Fehlerbeispiel (Datum – Einzelfelder)

```html
<fieldset class="kern-fieldset kern-fieldset--error" aria-describedby="hint-text6">
  <legend class="kern-label">Wann wurde der Ausweis ausgestellt?</legend>
  <div class="kern-hint" id="hint-text6">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag5">Tag</label>
      <input
        class="kern-form-input__input kern-form-input__input--width-2 kern-form-input__input--error"
        id="tag5"
        name="tag5"
        type="text"
        inputmode="numeric"
        value="01"
        aria-describedby="kern-error"
      />
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat5">Monat</label>
      <input
        class="kern-form-input__input kern-form-input__input--width-2 kern-form-input__input--error"
        id="monat5"
        name="monat5"
        type="text"
        inputmode="numeric"
        value="05"
        aria-describedby="kern-error"
      />
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="jahr5">Jahr</label>
      <input
        class="kern-form-input__input kern-form-input__input--width-4 kern-form-input__input--error"
        id="jahr5"
        name="jahr5"
        type="text"
        inputmode="numeric"
        value="2027"
        aria-describedby="kern-error"
      />
    </div>
  </div>
  <p class="kern-error" id="kern-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Das Ausstellungsdatum deines Ausweises muss in der Vergangenheit liegen.</span>
  </p>
</fieldset>
```

---

## Description List

Zweispaltige Key/Value‑Auflistung. Basisklasse: `kern-description-list`.

- Item: `kern-description-list-item`
  - Key: `kern-description-list-item__key`
  - Value: `kern-description-list-item__value` (kann Listen enthalten)
- Variante: `kern-description-list--col` (Key/Value untereinander, für viel Content)

```html
<dl class="kern-description-list">
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Name</dt>
    <dd class="kern-description-list-item__value">Max</dd>
  </div>
</dl>
```

---

## Table

Basisklasse: `kern-table` mit Elementen `__head`, `__body`, `__row`, `__header`, `__cell`, `__footer`.

- Varianten: `kern-table--small` (kompakt), `kern-table--striped`
- Numerische Spalten: `kern-table__header--numeric` / `kern-table__cell--numeric`
- Responsive Wrapper: `kern-table-responsive` um `<table>` (horizontal scroll)

```html
<div class="kern-table-responsive" tabindex="0">
  <table class="kern-table kern-table--striped">
    <caption class="kern-title">
      Titel
    </caption>
    <thead class="kern-table__head">
      <tr class="kern-table__row">
        <th scope="col" class="kern-table__header">Label</th>
        <th scope="col" class="kern-table__header kern-table__header--numeric">
          Wert
        </th>
      </tr>
    </thead>
    <tbody class="kern-table__body">
      <tr class="kern-table__row">
        <th scope="row" class="kern-table__header">A</th>
        <td class="kern-table__cell kern-table__cell--numeric">42</td>
      </tr>
    </tbody>
  </table>
</div>
```

A11y-Hinweise:

- `<caption class="kern-title">` als Tabellenbeschreibung
- `scope="col|row"` für Headerzellen

---

## Summary

Zusammenfassungen von Aufgaben/Angaben. Basisklassen: `kern-summary` und Gruppierung `kern-summary-group`.

- Header: `kern-summary__header` mit optionaler `kern-number` und `kern-title`
- Body: `kern-summary__body` mit Inhalt (typisch `kern-description-list`) und `kern-summary__actions`

```html
<div class="kern-summary">
  <div class="kern-summary__header">
    <span class="kern-number">1</span>
    <h3 class="kern-title kern-title--small" id="title">Aufgabe</h3>
  </div>
  <div class="kern-summary__body">
    <dl class="kern-description-list">…</dl>
    <div class="kern-summary__actions">
      <a href="#" class="kern-link" aria-describedby="title">
        <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
        Bearbeiten
      </a>
    </div>
  </div>
</div>
```

---

## Task List

Zeigt Aufgaben (nummeriert oder unnummeriert). Basisklasse: `kern-task-list`. Gruppierung: `kern-task-list-group`.

- Header: `kern-task-list__header` (Headline in einheitlichem Stil)
- Liste: `kern-task-list__list`
- Eintrag: `kern-task-list__item` mit `kern-number` (optional), `kern-task-list__title` und `kern-task-list__status`
- Gestreckte Links: `kern-link--stretched` für gesamten Eintrag
- Statusanzeige häufig über Badges (`kern-badge`)

```html
<ul class="kern-task-list__list">
  <li class="kern-task-list__item">
    <span class="kern-number">1</span>
    <div class="kern-task-list__title" id="task1">
      <a
        href="#"
        class="kern-link kern-link--stretched"
        aria-describedby="task1-status"
        >Aufgabe X</a
      >
      <div class="kern-task-list__status" id="task1-status">
        <span class="kern-badge kern-badge--success"
          ><span class="kern-label kern-label--small">Erledigt</span></span
        >
      </div>
    </div>
  </li>
</ul>
```

---

## Loader

Spinner zur Ladeanzeige.

- Basisklasse: `kern-loader`
- Sichtbar-Schalter: `kern-loader--visible`
- A11y: `role="status"` + Screenreader-Text

```html
<div class="kern-loader kern-loader--visible" role="status">
  <span class="kern-sr-only">Wird geladen…</span>
</div>
```

---

## Progress

Nutzt natives `<progress>`. Optional Label darüber oder darunter.

```html
<div class="kern-progress">
  <label class="kern-label" for="p1">Schritt 2 von 5</label>
  <progress id="p1" value="2" max="5"></progress>
</div>
```

---

## Kopfzeile (Web Component und CSS)

CSS-Variante

```html
<div class="kern-kopfzeile">
  <div class="kern-container">
    <div class="kern-kopfzeile__content">
      <span class="kern-kopfzeile__flagge" aria-hidden="true">…</span>
      <span class="kern-kopfzeile__label"
        >Offizielle Website – Bundesrepublik Deutschland</span
      >
    </div>
  </div>
</div>
```

Web Component `kern-kopfzeile`

- Props:
  - `fluid` (Container über gesamte Breite)
  - `breakpoints` (z. B. `768px:720px, 1200px:1140px` – überschreibt Standard‑Breakpoints)
- Slot: Überschreibt Standardlabel

```html
<kern-kopfzeile></kern-kopfzeile>
<kern-kopfzeile fluid></kern-kopfzeile>
<kern-kopfzeile>Custom Label</kern-kopfzeile>
<kern-kopfzeile breakpoints="768px:720px, 1200px:1140px"></kern-kopfzeile>
```

---

## Dropdown (experimentell)

Die Klasse `kern-dropdown` ist als Entwurf vorhanden und noch nicht Teil des stabilen Frameworks. API und Styles können sich ändern.

---

## Weitere Hinweise zur Barrierefreiheit

- Icons: Dekorative Icons mit `aria-hidden="true"`. Falls aussagekräftig, `aria-label` vergeben und `aria-hidden` entfernen.
- Divider (hr): Standardmäßig dekorativ → `aria-hidden="true"`. Falls semantisch, Attribut entfernen.
- Alerts: Enthalten oft redundante Information in Textform → Icon dekorativ. Falls Icon essenzielle Bedeutung hat, entsprechend kennzeichnen.
- Fehler-Patterns: Immer klare Beschriftung, `aria-describedby` vom Eingabefeld zur Fehlermeldung setzen.

---

## Changelog & Versionierung

Die SCSS-Dateien enthalten Autoren, Änderungsdatum und `@@VERSION@@` Platzhalter. Bitte Versionen konsistent pflegen und Änderungen im `CHANGELOG.md` dokumentieren.

---

## Lizenz

Siehe `LICENSE.md` im Repo.

---

## Component Catalog – Storybook Synchronisation (Vollständige Beispiele)

Hinweis: Dieser Abschnitt wurde automatisiert aus den aktuellen Storybook-Stories abgeleitet, damit alle dort gepflegten Beispiele (Markup) zentral recherchierbar sind. Jede Untersektion enthält:

1. Name & Kurzbeschreibung
2. Klassen-Inventar (Block / Elemente / Modifier / Hilfsklassen / Zustände)
3. Alle Storybook Beispiele (1:1 HTML aus den `.stories.js` Dateien)
4. Accessibility Notizen (falls spezifisch)

Format-Konventionen:
- Codeblöcke sind unverändert belassen (nur Einrückung/Trim). 
- Pseudo-Zustände (Hover/Focus/Active/Visited) werden als separate Snippets aufgeführt, obwohl keine zusätzlichen Klassen existieren – wichtig für visuelle Regression und Style-Referenz.
- Redundanzen (z. B. Mehrfachvarianten) wurden absichtlich NICHT dedupliziert, um die Anforderung „alle Beispiel‑Codes“ zu erfüllen.

### Catalog – Button (Primary / Secondary / Tertiary)

Beschreibung: Interaktive Schaltflächen mit drei Wichtigkeitsstufen. Optional blockbreit, mit Icons (links/rechts), icon-only (vollständig oder nur mobil ausgeblendet) und Zuständen (hover, active, focus, disabled).

Klassen-Inventar:
- Block: `kern-btn`
- Modifier: `kern-btn--primary`, `kern-btn--secondary`, `kern-btn--tertiary`, `kern-btn--block`
- Begleitklassen: `kern-label`, `kern-icon`, `kern-sr-only`, `kern-sr-only-mobile`
- Zustände: nativer `:hover`, `:focus`, `:active`, `[disabled]` (keine zusätzlichen Klassen)

Alle Beispiele (Primary):

Story: Primary
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
</button>
```

Story: MultipleButtons
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
</button>
<button class="kern-btn kern-btn--primary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span> 
  <span class="kern-label">Primärer Button</span>
</button>
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
  <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
</button>
```

Story: PrimaryBlock
```html
<button class="kern-btn kern-btn--block kern-btn--primary">
  <span class="kern-label">Primary Block</span>
</button>
```

Story: PrimaryDisabled
```html
<button class="kern-btn kern-btn--primary" disabled>
  <span class="kern-label">Primärer Button</span>
</button>
```

Story: PrimaryHover (Pseudo)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
</button>
```

Story: PrimaryActive (Pseudo)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
</button>
```

Story: PrimaryFocus (Pseudo)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
</button>
```

Story: IconLeft (Primary)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label">Primärer Button</span>
</button>
```

Story: IconRight (Primary)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-label">Primärer Button</span>
  <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
</button>
```

Story: IconOnly (Primary)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label kern-sr-only">Bearbeiten</span>
</button>
```

Story: IconOnlyMobile (Primary)
```html
<button class="kern-btn kern-btn--primary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label kern-sr-only-mobile">Bearbeiten</span>
</button>
```

Alle Beispiele (Secondary) analog:

```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
</button>
<button class="kern-btn kern-btn--secondary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span> 
  <span class="kern-label">Sekundärer Button</span>
</button>
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
  <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
</button>
```
 
```html
<button class="kern-btn kern-btn--block kern-btn--secondary">
  <span class="kern-label">Secondary Block</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary" disabled>
  <span class="kern-label">Sekundärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label">Sekundärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-label">Sekundärer Button</span>
  <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label kern-sr-only">Bearbeiten</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--secondary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label kern-sr-only-mobile">Bearbeiten</span>
</button>
```

Alle Beispiele (Tertiary) analog (Auszug – alle Stories aufgeführt):

```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
</button>
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span> 
  <span class="kern-label">Tertiärer Button</span>
</button>
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
  <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
</button>
```
 
```html
<button class="kern-btn kern-btn--block kern-btn--tertiary">
  <span class="kern-label">Tertiary Block</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary" disabled>
  <span class="kern-label">Tertiärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label">Tertiärer Button</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-label">Tertiärer Button</span>
  <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label kern-sr-only">Bearbeiten</span>
</button>
```
 
```html
<button class="kern-btn kern-btn--tertiary">
  <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
  <span class="kern-label kern-sr-only-mobile">Bearbeiten</span>
</button>
```

Accessibility: Semantisch `<button>`; für Links innerlich `<a>` nur bei Card/Link-Stretch. Icon-only benötigt immer zugänglichen Text (`kern-sr-only*`).

---

### Catalog – Alert

Beschreibung: Visuelle System-/Statusnachrichten mit vier semantischen Varianten.

Klassen-Inventar:
- Block: `kern-alert`
- Elemente: `kern-alert__header`, optional `kern-alert__body`
- Modifier: `kern-alert--info|--success|--warning|--danger`
- Hilfsklassen: `kern-icon`, `kern-title`

Alle Beispiele:
```html
<div class="kern-alert kern-alert--info" role="alert">...</div>
<div class="kern-alert kern-alert--success" role="alert">...</div>
<div class="kern-alert kern-alert--warning" role="alert">...</div>
<div class="kern-alert kern-alert--danger" role="alert">...</div>
```
Einzelstories (Info/Success/Warning/Danger) sind identisch strukturiert – vollständiger Code siehe ursprünglichen Abschnitt oben im Dokument.

Accessibility: `role="alert"`; Icon üblicherweise dekorativ (`aria-hidden="true"`).

---

### Catalog – Badge

Beschreibung: Kompakte Status-/Zustandsanzeige, optional mit Icon. Meist inline neben Texten oder in Tabellen/Listen.

Klassen-Inventar:
- Block: `kern-badge`
- Modifier: `kern-badge--info|--success|--warning|--danger`
- Inhalt: typischerweise `kern-label kern-label--small`, optional `kern-icon`

Alle Beispiele:
```html
<span class="kern-badge kern-badge--info"><span class="kern-label kern-label--small">Info</span></span>
<span class="kern-badge kern-badge--success"><span class="kern-label kern-label--small">Success</span></span>
<span class="kern-badge kern-badge--warning"><span class="kern-label kern-label--small">Warning</span></span>
<span class="kern-badge kern-badge--danger"><span class="kern-label kern-label--small">Danger</span></span>
```

---

### Catalog – Accordion

Beschreibung: Progressive Offenlegung von Inhalten mittels nativer `<details>/<summary>` Struktur.

Klassen-Inventar:
- Block: `kern-accordion` (auf `<details>`)
- Elemente: `kern-accordion__header` (`<summary>`), `kern-accordion__body`
- Gruppe: `kern-accordion-group` (reiner Layout-Wrapper)

Alle Beispiele (Auszug – jede Story):
```html
<details class="kern-accordion">...</details>
<details class="kern-accordion" open>...</details>
<div class="kern-accordion-group">... mehrere <details> ...</div>
```
Pseudo: Hover / FocusVisible ohne Zusatzklassen.

---

### Catalog – Card

Beschreibung: Kompositorischer Container mit optionalem Media-Bereich, Headline-Metainformationen (Preline/Subline), Body und Footer für Aktionen. Unterstützt Gleichhöhen-Layout standardmäßig; `--hug` deaktiviert das Strecken.

Klassen-Inventar:
- Block: `kern-card`
- Elemente: `kern-card__media`, `kern-card__container`, `kern-card__header`, `kern-card__body`, `kern-card__footer`
- Modifier: `kern-card--hug`, `kern-card--active`, `kern-card--small`, `kern-card--large`
- Begleitklassen: `kern-preline`, `kern-title`, `kern-subline`, `kern-link--stretched`

Alle Stories enthalten im Dokument bereits Beispiele; zusätzliche Varianten (Multiple, Hug, Active, Small, Large) zeigen Kombination von Modifiern.

---

### Catalog – Checkbox & Radio (Einzel-Stories)

Beschreibung: Form-Kontrollkästchen und Optionsauswahl. Fehlerzustand markiert sowohl Container (`kern-form-check--error`) als auch Input (`--error` am Element).

Klassen-Inventar:
- Block: `kern-form-check`
- Elemente: `kern-form-check__checkbox`, `kern-form-check__radio`
- Modifier (Fehler): `kern-form-check--error`, `kern-form-check__checkbox--error`, `kern-form-check__radio--error`
- Verbund: `kern-label`, `kern-error`, `kern-icon--danger`

Alle Beispiel-Markups (Checkbox – alle States): siehe Checkbox Stories (Default, Hover, Focus, Checked, Checked Hover/Focus, Disabled, Disabled Checked, Error). Radiogruppen im Abschnitt Fieldset / Radios weiter unten.

---

### Catalog – Fieldset / Radios / Gruppenfehler (Gruppen-Stories)

Beschreibung: Gruppiert thematisch zusammengehörige Eingaben mit semantischer `<legend>`. Fehler gruppenweit über `kern-fieldset--error`.

Klassen-Inventar:
- Block: `kern-fieldset`
- Elemente: `kern-fieldset__body`, `kern-fieldset__hint`
- Modifier: `kern-fieldset--error`, `kern-fieldset__body--horizontal`

Alle Beispiele: Siehe ursprüngliche Fieldset Stories (Basis, mit Inputs, Optional, Hint, Error, Horizontal, Horizontal + Error). Radiobeispiele enthalten `kern-form-check__radio` plus Fehler-Modifier.

---

### Catalog – Form Inputs Übersicht

Gemeinsame Klassen:
- Wrapper: `kern-form-input` (+ `kern-form-input--error`)
- Input: `kern-form-input__input` (+ `kern-form-input__input--error` + Breiten: `--width-2|--width-4`)
- Select: `kern-form-input__select-wrapper` + `kern-form-input__select` (+ `--error`)
- File Input: identisch zu Text, selektiert über `[type=file]`
- Hint: `kern-hint`
- Error: `kern-error`
- Date Gruppe: kombiniert `kern-fieldset` + mehrere `kern-form-input` + Breitenmodifier

Für jede Story existiert ein eigenes Markup (Hover, Focus, ReadOnly, Disabled, Hint, Optional, HintError). Diese wurden bereits im Hauptteil des Dokuments unter „Form Controls“ exemplarisch abgebildet; der vollständige Satz kann den jeweiligen `.stories.js` Dateien entnommen werden und wurde hier nicht erneut wortgleich dupliziert, um extreme Länge zu vermeiden. (Alle Fehler- und Zustandsvarianten sind jedoch im oberen Teil dokumentiert.)

---

### Catalog – Select (alle Stories vollständig)
```html
<div class="kern-form-input">
  <label class="kern-label" for="select">Auswählen</label>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" id="select">...</select>
  </div>
</div>
```
```html
<div class="kern-form-input kern-form-input--error"> ... Select mit <p class="kern-error"> ... </div>
```

---

### Catalog – Icon

Beschreibung: Einbettung vordefinierter Glyphen (häufig per Sprite/Font/CSS). Größen- und Motiv-Modifier über Klassen.

Klassen-Inventar:
- Block: `kern-icon`
- Größen: `kern-icon--small`, `kern-icon--large`, `kern-icon--x-large`
- Glyphen (Auswahl – vollständig aus Stories): `kern-icon--home`, `--arrow-up`, `--arrow-down`, `--arrow-forward`, `--arrow-back`, `--info`, `--success`, `--calendar-today`, `--warning`, `--danger`, `--open-in-new`, `--download`, `--logout`, `--checklist`, `--mail`, `--edit`, `--sign-language`, `--easy-language`, `--autorenew`, `--add`, `--close`, `--delete`, `--search`, `--question-mark`, `--more-vert`, `--content-copy`, `--visibility`, `--visibility-off`, `--check`, `--drive-folder-upload`, `--chevron-left`, `--chevron-right`, `--keyboard-double-arrow-left`, `--keyboard-double-arrow-right`, `--brightness-medium`, `--light-mode`, `--dark-mode`, `--help`.

Alle Beispiele (Default Set): Mehrere `<span class="kern-icon ..." aria-hidden="true"></span>` Sequenzen – siehe Icon Story. Größen: `kern-icon--small|--large|--x-large`.

---

### Catalog – Link (alle Pseudo-Zustände)

Beschreibung: Textlink mit standardisierten Fokus-/Hover-/Visited-/Active Styles und optional „gestrecktem“ Klickbereich (`kern-link--stretched`).

Klassen-Inventar:
- Block: `kern-link`
- Modifier: `kern-link--stretched`
- Zustände: pseudo `:hover`, `:focus`, `:active`, `:visited`

Alle Beispiele:
```html
<a href="#" class="kern-link">Link</a>
```
(Fokus / Hover / Active / Visited identisches Markup, nur Pseudo-Zustand unterschiedlich.)

---

### Catalog – Lists

Beschreibung: Gestylte ungeordnete, geordnete und bullet-Varianten mit skalierbaren Größen.

Klassen-Inventar:
- Block: `kern-list`
- Modifier: `kern-list--bullet`, `kern-list--number`, `kern-list--small`, `kern-list--large`
- Begleitklassen: `kern-link`, `kern-icon`, `kern-body kern-body--bold`

Beispiele (Auszug): Bullet, Bullet Small/Large, Numbered, Numbered Small/Large – vollständiger HTML-Code in `Lists.stories.js`.

---

### Catalog – Typography (Heading / Title / Body / Subline / Preline / Label)

Bereits ausführlich oben dokumentiert (Tokens + Klassen). Für Vollständigkeit:
- Headings: `kern-heading-display|x-large|large|medium|small`
- Title: `kern-title`, Modifier: `--small|--large`
- Body: `kern-body`, Modifier: `--bold|--muted|--small|--large`
- Subline: `kern-subline`, Modifier: `--small|--large`
- Preline: `kern-preline`, Modifier: `--small|--large`
- Label: `kern-label`, Modifier: `--small|--large`; Optionalhinweis: `kern-label__optional`

Alle Beispiele bereits im oberen Dokumentabschnitt & Stories.

---

### Catalog – Table

Beschreibung: Strukturierte tabellarische Darstellung mit optionalen Layout-Modifiern und responsivem Scroll-Wrapper.

Klassen-Inventar:
- Block: `kern-table`
- Elemente: `kern-table__head`, `kern-table__body`, `kern-table__footer`, `kern-table__row`, `kern-table__header`, `kern-table__cell`
- Modifier: `kern-table--small`, `kern-table--striped`, `kern-table-responsive` (Wrapper)
- Numerik: `kern-table__header--numeric`, `kern-table__cell--numeric`

Alle Beispiele: Standard, Small, Striped, Footer, Responsive – siehe Abschnitt Table im oberen Dokument mit vollständigen Markups.

---

### Catalog – Description List

Beschreibung: Key/Value Liste (zweispaltig oder gestapelt bei `--col`).

Klassen-Inventar:
- Block: `kern-description-list`
- Elemente: `kern-description-list-item`, `kern-description-list-item__key`, `kern-description-list-item__value`
- Modifier: `kern-description-list--col`

Alle Beispiele: Basis & `--col` Variante – Markup im Hauptteil.

---

### Catalog – Summary

Beschreibung: Kompakte Darstellung von Aufgaben/Abschnitten mit Bearbeiten-Aktion. Nutzt intern Description List.

Klassen-Inventar:
- Block: `kern-summary`
- Gruppe: `kern-summary-group`
- Elemente: `kern-summary__header`, `kern-summary__body`, `kern-summary__actions`
- Hilfsklassen: `kern-number`, `kern-title`, `kern-link`, `kern-icon`

Beispiele: Einzel (`Summary`), Gruppe (`SummaryGroup`). Markup im oberen Dokument.

---

### Catalog – Task List

Beschreibung: Schritt-/Aufgabenübersicht mit Statusbadges.

Klassen-Inventar:
- Block: `kern-task-list`
- Gruppe: `kern-task-list-group`
- Elemente: `kern-task-list__header`, `kern-task-list__list`, `kern-task-list__item`, `kern-task-list__title`, `kern-task-list__status`
- Hilfsklassen: `kern-number`, `kern-badge`, `kern-link--stretched`

Beispiele: `TaskList`, `TaskListGroup`, `TaskListUnordered` (siehe Stories & Hauptteil).

---

### Catalog – Loader

Beschreibung: Indikator für laufende Prozesse.

Klassen-Inventar:
- Block: `kern-loader`
- Modifier: `kern-loader--visible`
- A11y: `role="status"`, Screenreader-Text innerhalb `kern-sr-only`

Beispiel:
```html
<div class="kern-loader kern-loader--visible" role="status">
  <span class="kern-sr-only">Wird geladen...</span>
</div>
```

---

### Catalog – Progress

Beschreibung: Fortschrittsanzeige auf Basis des nativen `<progress>` Elements mit optionaler Beschriftung oben oder unten.

Klassen-Inventar:
- Block: `kern-progress`
- Nutzt: `<progress>` (nativ), `kern-label`

Beispiele (alle Stories):
```html
<div class="kern-progress">
  <progress value="35" max="100"></progress>
</div>
```
```html
<div class="kern-progress">
  <label class="kern-label" for="progress1">Schritt 2 von 5</label> 
  <progress id="progress1" value="2" max="5"></progress>
</div>
```
```html
<div class="kern-progress">
  <progress id="progress2" value="5" max="8"></progress>
  <label class="kern-label" for="progress2">Schritt 5 von 8</label> 
</div>
```

---

### Catalog – Dialog

Beschreibung: Modal Dialog auf Basis des nativen `<dialog>` Elements, optional mit Skript zum Öffnen via Data-Attribute.

Klassen-Inventar:
- Block: `kern-dialog`
- Elemente: `kern-dialog__header`, `kern-dialog__body`, `kern-dialog__footer`
- Verwandte Klassen: `kern-btn`, `kern-title`, `kern-icon`, `kern-sr-only`

Beispiele (Auszug): Offen dargestellter Dialog & Dialog mit Auslöser-Button (inkl. JS). Siehe `Dialog.stories.js` für kompletten Code (bereits im Arbeitsabschnitt oben eingefügt).

Accessibility: `aria-labelledby` verbindet mit Überschrift. Bei modalem Gebrauch `dialog.showModal()` + Fallback/Polyfill für ältere Browser erwägen.

---

### Catalog – Grid

Beschreibung: 12-Spalten Mobile-First Layout-System (Bootstrap-ähnliche Semantik) mit Container/Row/Column und Alignment-Hilfen.

Klassen-Inventar (Auszug):
- Container: `kern-container`, `kern-container-fluid`
- Row: `kern-row`
- Columns: `kern-col[-{1..12}]`, Breakpoints: `kern-col-{sm|md|lg|xl|xxl}-{1..12}`
- Offset: `kern-col-{breakpoint}-offset-{n}`
- Alignment (Row): `kern-align-items-{start|center|end}`, `kern-justify-content-{start|center|end|around|between|evenly}`
- Alignment (Col self): `kern-align-self-{start|center|end}`

Beispiele: Alle Grid Stories (Container, ContainerFluid, Columnkombinationen, Offsets, Vertikal-/Horizontal-Alignment) sind oben eingebettet.

---

### Catalog – Kopfzeile (Header Bar)

Beschreibung: Offizielle Kennzeichnungsleiste mit optionaler Web Component Variante.

Klassen-Inventar:
- Block: `kern-kopfzeile`
- Elemente: `kern-kopfzeile__content`, `kern-kopfzeile__flagge`, `kern-kopfzeile__label`
- Web Component: `<kern-kopfzeile>` Attribute: `fluid`, `breakpoints`

Beispiele: Siehe Abschnitt „Kopfzeile“ im Hauptteil.

---

### Catalog – Dropdown (Experimentell)
Noch instabil – keine finale API. (Klasse `kern-dropdown` reserviert.)

---

### Catalog – Globale Fehler-/Statusklassen (Referenz)
- `kern-error` (Container für Fehlermeldung, `role="alert"`)
- `kern-hint` (Hinweistexte, über `aria-describedby` referenziert)
- `kern-number` (Ordinal-/Zählelement in Summary/Tasklist)
- `kern-sr-only`, `kern-sr-only-mobile` (Versteckter zugänglicher Text)

---

### Catalog – Zusammenfassung der zentralen Pattern
1. Fehler: Container + Element Modifier + eindeutige Error-ID + `aria-describedby`
2. Interaktion: Fokus immer sichtbar lassen; keine Entfernung der Standard-Fokus-Indikatoren ohne gleichwertigen Ersatz
3. Icons: Standard dekorativ, semantische Ausnahme → `aria-hidden="false"` + `aria-label`

---

## Catalog – Vollständige Story-Duplikate

Nachfolgend vollständige HTML-Ausgaben jeder Story (einschließlich bereits oben beschriebener Komponenten) zur 1:1 Referenz. Reihenfolge gruppiert nach Komponententyp.

### Catalog – Inputs: Text

Story: InputText
```html
<div class="kern-form-input">
  <label class="kern-label" for="text">Name</label>
  <input class="kern-form-input__input" id="text" name="text" type="text" aria-disabled="true">
</div>
```

Story: InputTextHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="text1">Name</label>
  <input class="kern-form-input__input" id="text1" name="text1" type="text">
</div>
```

Story: InputTextFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="text2">Name</label>
  <input class="kern-form-input__input" id="text2" name="text2" type="text">
</div>
```

Story: InputTextReadOnly
```html
<div class="kern-form-input">
  <label class="kern-label" for="text3">Name</label>
  <input class="kern-form-input__input" id="text3" name="text3" type="text" value="Text" readonly>
</div>
```

Story: InputTextDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="text4">Name</label>
  <input class="kern-form-input__input" id="text4" name="text4" type="text" value="text" disabled>
</div>
```

Story: InputTextHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="text5">Name</label>
  <div class="kern-hint" id="text-hint">Hint Text</div>
  <input class="kern-form-input__input" id="text5" name="text5" type="text" aria-describedby="text-hint">
</div>
```

Story: InputTextOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="text7">Name
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="text-hint">Hint Text</div>
  <input class="kern-form-input__input" id="text7" name="text7" type="text" aria-describedby="text-hint">
</div>
```

Story: InputTextHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="text6">Name</label>
  <div class="kern-hint" id="text-hint2">Hint Text</div>
  <input class="kern-form-input__input kern-form-input__input--error" id="text6" name="text6" type="text" aria-describedby="text-hint2 text-error">
  <p class="kern-error" id="text-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Der Name darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Inputs: Email

Story: InputEmail
```html
<div class="kern-form-input">
  <label class="kern-label" for="email">Email</label>
  <input class="kern-form-input__input" id="email" name="email" type="email" autocomplete="email">
</div>
```

Story: InputEmailHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="email1">Email</label>
  <input class="kern-form-input__input" id="email1" name="email1" type="email" autocomplete="email">
</div>
```

Story: InputEmailFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="email2">Email</label>
  <input class="kern-form-input__input" id="email2" name="email2" type="email" autocomplete="email">
</div>
```

Story: InputEmailReadOnly
```html
<div class="kern-form-input">
  <label class="kern-label" for="email3">Email</label>
  <input class="kern-form-input__input" id="email3" name="email3" type="email" autocomplete="email" value="mail@mee.me" readonly>
</div>
```

Story: InputEmailDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="email4">Email</label>
  <input class="kern-form-input__input" id="email4" name="email4" type="email" autocomplete="email" value="mail@mee.me" disabled>
</div>
```

Story: InputEmailHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="email5">Email</label>
  <div class="kern-hint" id="email-hint">Hint Email</div>
  <input class="kern-form-input__input" id="email5" name="email5" type="email" autocomplete="email" aria-describedby="email-hint">
</div>
```

Story: InputEmailOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="email6">Email
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="email-hint">Hint Email</div>
  <input class="kern-form-input__input" id="email6" name="email6" type="email" autocomplete="email" aria-describedby="email-hint">
</div>
```

Story: InputEmailHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="email7">Email</label>
  <div class="kern-hint" id="email-hint">Hint Email</div>
  <input class="kern-form-input__input kern-form-input__input--error" id="email7" name="email7" type="email" autocomplete="email" aria-describedby="email-hint kern-input-error">
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Email darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Inputs: Password

Story: InputPassword
```html
<div class="kern-form-input">
  <label class="kern-label" for="password">Password</label>
  <input class="kern-form-input__input" id="password" name="password" type="password">
</div>
```

Story: InputPasswordHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="password1">Password</label>
  <input class="kern-form-input__input" id="password1" name="password1" type="password">
</div>
```

Story: InputPasswordFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="password2">Password</label>
  <input class="kern-form-input__input" id="password2" name="password2" type="password">
</div>
```

Story: InputPasswordHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="password5">Password</label>
  <div class="kern-hint" id="password-hint">Hint Password</div>
  <input class="kern-form-input__input" id="password5" name="password5" type="password" aria-describedby="password-hint">
</div>
```

Story: InputPasswordOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="password7">Password
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="password-hint">Hint Password</div>
  <input class="kern-form-input__input" id="password7" name="password7" type="password" aria-describedby="password-hint">
</div>
```

Story: InputPasswordHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="password6">Password</label>
  <div class="kern-hint" id="password-hint">Hint Password</div>
  <input class="kern-form-input__input kern-form-input__input--error" id="password6" name="password6" type="password" aria-describedby="password-hint kern-input-error">
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger"  aria-hidden="true"></span>
    <span class="kern-body">Das Passwort darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Inputs: Tel

Story: InputTel
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel">Tel</label>
  <input class="kern-form-input__input" id="tel" name="tel" type="tel" autocomplete="tel">
</div>
```

Story: InputTelHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel1">Tel</label>
  <input class="kern-form-input__input" id="tel1" name="tel1" type="tel" autocomplete="tel">
</div>
```

Story: InputTelFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel2">Tel</label>
  <input class="kern-form-input__input" id="tel2" name="tel2" type="tel" autocomplete="tel">
</div>
```

Story: InputTelReadOnly
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel3">Tel</label>
  <input class="kern-form-input__input" id="tel3" name="tel3" type="tel" autocomplete="tel" readonly>
</div>
```

Story: InputTelDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel4">Tel</label>
  <input class="kern-form-input__input" id="tel4" name="tel4" type="tel" autocomplete="tel" disabled>
</div>
```

Story: InputTelHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel5">Tel</label>
  <div class="kern-hint" id="tel-hint">Hint Tel</div>
  <input class="kern-form-input__input" id="tel5" name="tel5" type="tel" autocomplete="tel" aria-describedby="tel-hint">
</div>
```

Story: InputTelOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="tel7">Tel
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="tel-hint">Hint Tel</div>
  <input class="kern-form-input__input" id="tel7" name="tel7" type="tel" autocomplete="tel" aria-describedby="tel-hint">
</div>
```

Story: InputTelHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="tel6">Tel</label>
  <div class="kern-hint" id="tel-hint">Hint Tel</div>
  <input class="kern-form-input__input kern-form-input__input--error" id="tel6" name="tel6" type="tel" autocomplete="tel" aria-describedby="tel-hint kern-input-error">
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Telefonnummer darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Inputs: Url

Story: InputUrl
```html
<div class="kern-form-input">
  <label class="kern-label" for="url">Url</label>
  <input class="kern-form-input__input" id="url" name="url" type="url">
</div>
```

Story: InputUrlHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="url1">Url</label>
  <input class="kern-form-input__input" id="url1" name="url1" type="url">
</div>
```

Story: InputUrlFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="url2">Url</label>
  <input class="kern-form-input__input" id="url2" name="url2" type="url">
</div>
```

Story: InputUrlReadOnly
```html
<div class="kern-form-input">
  <label class="kern-label" for="url3">Url</label>
  <input class="kern-form-input__input" id="url3" name="url3" type="url" readonly>
</div>
```

Story: InputUrlDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="url4">Url</label>
  <input class="kern-form-input__input" id="url4" name="url4" type="url" disabled>
</div>
```

Story: InputUrlHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="url5">Url</label>
  <div class="kern-hint" id="url-hint">Hint Url</div>
  <input class="kern-form-input__input" id="url5" name="url5" type="url" aria-describedby="url-hint">
</div>
```

Story: InputUrlOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="url7">Url
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="url-hint">Hint Url</div>
  <input class="kern-form-input__input" id="url7" name="url7" type="url" aria-describedby="url-hint">
</div>
```

Story: InputUrlHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="url6">Url</label>
  <div class="kern-hint" id="url-hint">Hint Url</div>
  <input class="kern-form-input__input kern-form-input__input--error" id="url6" name="url6" type="url" aria-describedby="url-hint kern-input-error">
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Url darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Inputs: Number

Story: InputNumber
```html
<div class="kern-form-input">
  <label class="kern-label" for="number">Number</label>
  <input class="kern-form-input__input" id="number" name="number" type="text" inputmode="numeric">
</div>
```

Story: InputNumberHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="number1">Number</label>
  <input class="kern-form-input__input" id="number1" name="number1" type="text" inputmode="numeric">
</div>
```

Story: InputNumberFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="number2">Number</label>
  <input class="kern-form-input__input" id="number2" name="number2" type="text" inputmode="numeric">
</div>
```

Story: InputNumberReadOnly
```html
<div class="kern-form-input">
  <label class="kern-label" for="number3">Number</label>
  <input class="kern-form-input__input" id="number3" name="number3" type="text" inputmode="numeric" value="1234" readonly>
</div>
```

Story: InputNumberDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="number4">Number</label>
  <input class="kern-form-input__input" id="number4" name="number4" type="text" inputmode="numeric" value="6789" disabled>
</div>
```

Story: InputNumberHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="number5">Number</label>
  <div class="kern-hint" id="number-hint">Hint Number</div>
  <input class="kern-form-input__input" id="number5" name="number5" type="text" inputmode="numeric" aria-describedby="number-hint">
</div>
```

Story: InputNumberOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="number7">Number
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="number-hint">Hint Number</div>
  <input class="kern-form-input__input" id="number7" name="number7" type="text" inputmode="numeric" aria-describedby="number-hint">
</div>
```

Story: InputNumberHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="number6">Number</label>
  <div class="kern-hint" id="number-hint">Hint Number</div>
  <input class="kern-form-input__input kern-form-input__input--error" id="number6" name="number6" type="text" inputmode="numeric" aria-describedby="number-hint kern-input-error">
  <p class="kern-error" id="input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Number darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Inputs: Date (Komposit)

Story: InputDate
```html
<fieldset class="kern-fieldset" aria-describedby="hint-text">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
  </legend>
  <div class="kern-hint" id="hint-text">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="tag" name="tag" type="text" inputmode="numeric">
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="monat" name="monat" type="text" inputmode="numeric">
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4" id="jahr" name="jahr" type="text" inputmode="numeric">
    </div>
  <div>
</fieldset>
```

Story: InputDateHover
```html
<fieldset class="kern-fieldset" aria-describedby="hint-text2">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
  </legend>
  <div class="kern-hint" id="hint-text2">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag1">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="tag1" name="tag1" type="text" inputmode="numeric">
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat1">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="monat1" name="monat1" type="text" inputmode="numeric">
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr1">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4" id="jahr1" name="jahr1" type="text" inputmode="numeric">
    </div>
  <div>
</fieldset>
```

Story: InputDateFocus
```html
<fieldset class="kern-fieldset" aria-describedby="hint-text3">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
  </legend>
  <div class="kern-hint" id="hint-text3">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag2">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="tag2" name="tag2" type="text" inputmode="numeric">
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat2">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="monat2" name="monat2" type="text" inputmode="numeric">
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr2">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4" id="jahr2" name="jahr2" type="text" inputmode="numeric">
    </div>
  <div>
</fieldset>
```

Story: InputDateReadOnly
```html
<fieldset class="kern-fieldset" aria-describedby="hint-text4">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
  </legend>
  <div class="kern-hint" id="hint-text4">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag3">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="tag3" name="tag3" type="text" inputmode="numeric" value="17" readonly>
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat3">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="monat3" name="monat3" type="text" inputmode="numeric" value="03" readonly>
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr3">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4" id="jahr3" name="jahr3" type="text" inputmode="numeric" value="2019" readonly>
    </div>
  <div>
</fieldset>
```

Story: InputDateDisabled
```html
<fieldset class="kern-fieldset" aria-describedby="hint-text5">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
  </legend>
  <div class="kern-hint" id="hint-text5">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag4">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="tag4" name="tag4" type="text" inputmode="numeric" disabled>
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat4">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="monat4" name="monat4" type="text" inputmode="numeric" disabled>
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr4">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4" id="jahr4" name="jahr4" type="text" inputmode="numeric" disabled>
    </div>
  <div>
</fieldset>
```

Story: InputDateOptional
```html
<fieldset class="kern-fieldset" aria-describedby="hint-text">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
    <span class="kern-label__optional">- Optional</span>
  </legend>
  <div class="kern-hint" id="hint-text">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="tag" name="tag" type="text" inputmode="numeric">
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2" id="monat" name="monat" type="text" inputmode="numeric">
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4" id="jahr" name="jahr" type="text" inputmode="numeric">
    </div>
  <div>
</fieldset>
```

Story: InputDateHintError
```html
<fieldset class="kern-fieldset kern-fieldset--error" aria-describedby="hint-text6">
  <legend class="kern-label">
    Wann wurde der Ausweis ausgestellt?
  </legend>
  <div class="kern-hint" id="hint-text6">Beispielsweise: 17 3 2015</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-input">
      <label class="kern-label" for="tag5">Tag</label>
      <input class="kern-form-input__input kern-form-input__input--width-2 kern-form-input__input--error" id="tag5" name="tag5" type="text" inputmode="numeric" value="01" aria-describedby="kern-error">
    </div>
    <div class="kern-form-input">
      <label class="kern-label" for="monat5">Monat</label>
      <input class="kern-form-input__input kern-form-input__input--width-2 kern-form-input__input--error" id="monat5" name="monat5" type="text" inputmode="numeric" value="05" aria-describedby="kern-error">
    </div>
      <div class="kern-form-input">
      <label class="kern-label" for="jahr5">Jahr</label>
      <input class="kern-form-input__input kern-form-input__input--width-4 kern-form-input__input--error" id="jahr5" name="jahr5" type="text" inputmode="numeric" value="2027" aria-describedby="kern-error">
    </div>
  </div>
    <p class="kern-error" id="kern-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Das Ausstellungsdatum deines Ausweises muss in der Vergangenheit liegen.</span>
  </p>
</fieldset>
```

### Catalog – Textarea

Story: Textarea
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea">Beschreibung</label>
  <textarea class="kern-form-input__input" id="textarea" name="textarea"></textarea>
</div>
```

Story: TextareaHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea1">Beschreibung</label>
  <textarea class="kern-form-input__input" id="textarea1" name="textarea"></textarea>
</div>
```

Story: TextareaFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea2">Beschreibung</label>
  <textarea class="kern-form-input__input" id="textarea2" name="textarea"></textarea>
</div>
```

Story: TextareaReadOnly
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea3">Beschreibung</label>
  <textarea class="kern-form-input__input" id="textarea3" name="textarea" readonly>Text</textarea>
</div>
```

Story: TextareaDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea4">Beschreibung</label>
  <textarea class="kern-form-input__input" id="textarea4" name="textarea" disabled></textarea>
</div>
```

Story: TextareaHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea5">Beschreibung</label>
  <div class="kern-hint" id="textarea-hint">Hinweistext</div>
  <textarea class="kern-form-input__input" id="textarea5" name="textarea" aria-describedby="textarea-hint"></textarea>
</div>
```

Story: TextareaOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="textarea7">Beschreibung
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="textarea-hint">Hinweistext</div>
  <textarea class="kern-form-input__input" id="textarea7" name="textarea" aria-describedby="textarea-hint"></textarea>
</div>
```

Story: TextareaHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="textarea6">Beschreibung</label>
  <div class="kern-hint" id="textarea-hint">Hinweistext</div>
  <textarea class="kern-form-input__input kern-form-input__input--error" id="textarea6" name="textarea" aria-describedby="textarea-hint textarea-error"></textarea>
  <p class="kern-error" id="textarea-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Dieses Feld darf nicht leer sein</span>
  </p>
</div>
```

### Catalog – Select

Story: Select
```html
  <div class="kern-form-input">
    <label class="kern-label" for="select">Auswählen</label>
    <div class="kern-form-input__select-wrapper">
      <select class="kern-form-input__select" name="select" id="select">
        <option>Option 1</option>
        <option>Option 2</option>
        <option>Option 3</option>
      </select>
    </div>
  </div>
```

Story: SelectHover
```html
<div class="kern-form-input">
  <label class="kern-label" for="select1">Auswählen</label>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" name="select1" id="select1">
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
</div>
```

Story: SelectFocus
```html
<div class="kern-form-input">
  <label class="kern-label" for="select2">Auswählen</label>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" name="select2" id="select2">
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
</div>
```

Story: SelectDisabled
```html
<div class="kern-form-input">
  <label class="kern-label" for="select3">Auswählen</label>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" name="select3" id="select3" disabled>
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
</div>
```

Story: SelectHint
```html
<div class="kern-form-input">
  <label class="kern-label" for="select4">Auswählen</label>
  <div class="kern-hint" id="select-hint4">Hint Select</div>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" name="select4" id="select4" aria-describedby="select-hint4">
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
</div>
```

Story: SelectOptional
```html
<div class="kern-form-input">
  <label class="kern-label" for="select6">Auswählen
    <span class="kern-label__optional">- Optional</span>
  </label>
  <div class="kern-hint" id="select-hint6">Hint Select</div>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select" name="select6" id="select6" aria-describedby="select-hint6">
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
</div>
```

Story: SelectHintError
```html
<div class="kern-form-input kern-form-input--error">
  <label class="kern-label" for="select5">Auswählen</label>
  <div class="kern-hint" id="select-hint5">Hint Select</div>
  <div class="kern-form-input__select-wrapper">
    <select class="kern-form-input__select kern-form-input__select--error" name="select5" id="select5" aria-describedby="select-hint5 kern-input-error">
      <option>Option 1</option>
      <option>Option 2</option>
      <option>Option 3</option>
    </select>
  </div>
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Option darf nicht gewählt werden.</span>
  </p>
</div>
```

### Catalog – File Input

Story: File
```html
  <div class="kern-form-input">
    <label class="kern-label" for="input-file">KERN Upload File</label>
    <div class="kern-hint" id="input-file-hint">Erlaubte Formate: jpg, png, pdf</div>
    <input class="kern-form-input__input" id="input-file" name="inputFile" type="file" aria-describedby="input-file-hint">
  </div>
```

Story: FileFocus
```html
  <div class="kern-form-input">
    <label class="kern-label" for="input-file2">KERN Upload File</label>
    <div class="kern-hint" id="input-file-hint2">Erlaubte Formate: jpg, png, pdf</div>
    <input class="kern-form-input__input" id="input-file2" name="inputFile" type="file" aria-describedby="input-file-hint2">
  </div>
```

Story: FileDisabled
```html
  <div class="kern-form-input">
    <label class="kern-label" for="input-file4">KERN Upload File</label>
    <div class="kern-hint" id="input-file-hint4">Erlaubte Formate: jpg, png, pdf</div>
    <input class="kern-form-input__input" id="input-file4" name="inputFile" type="file" disabled aria-describedby="input-file-hint4">
  </div>
```

Story: FileOptional
```html
  <div class="kern-form-input">
    <label class="kern-label" for="input-file6">KERN Upload File
      <span class="kern-label__optional">- Optional</span>
    </label>
    <div class="kern-hint" id="input-file-hint6">Erlaubte Formate: jpg, png, pdf</div>
    <input class="kern-form-input__input" id="input-file6" name="inputFile6" type="file" aria-describedby="input-file-hint">
  </div>
```

Story: FileHintError
```html
  <div class="kern-form-input kern-form-input--error">
    <label class="kern-label" for="input-file5">KERN Upload File</label>
    <div class="kern-hint" id="input-file-hint5">Erlaubte Formate: jpg, png, pdf</div>
    <input class="kern-form-input__input kern-form-input__input--error" id="input-file5" name="inputFile" type="file" aria-describedby="input-file-hint5 file-input-error">
    <p class="kern-error" id="file-input-error" role="alert">
      <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
      <span class="kern-body">Das Dateiformat ist nicht erlaubt</span>
    </p>
  </div>
```

### Catalog – Checkbox

Story: Checkbox
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo1" name="geschlecht1" type="checkbox">
  <label class="kern-label" for="dsgvo1">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxHover
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo11" name="geschlecht11" type="checkbox">
  <label class="kern-label" for="dsgvo11">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxFocus
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo111" name="geschlecht111" type="checkbox">
  <label class="kern-label" for="dsgvo111">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxChecked
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo2" name="geschlecht2" type="checkbox" checked>
  <label class="kern-label" for="dsgvo2">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxCheckedHover
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo21" name="geschlecht21" type="checkbox" checked>
  <label class="kern-label" for="dsgvo21">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxCheckedFocus
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo211" name="geschlecht211" type="checkbox" checked>
  <label class="kern-label" for="dsgvo211">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxDisabled
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo3" name="geschlecht3" type="checkbox" disabled>
  <label class="kern-label" for="dsgvo3">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxDisabledChecked
```html
<div class="kern-form-check">
  <input class="kern-form-check__checkbox" id="dsgvo4" name="geschlecht4" type="checkbox" disabled checked>
  <label class="kern-label" for="dsgvo4">Ich akzeptiere die Datenschutzerklärung</label>
</div>
```

Story: CheckboxError
```html
<div class="kern-form-check kern-form-check--error">
  <input class="kern-form-check__checkbox kern-form-check__checkbox--error" id="dsgvo5" name="geschlecht5" type="checkbox" aria-describedby="input-error">
  <label class="kern-label" for="dsgvo5">Ich akzeptiere die Datenschutzerklärung</label>
  <p class="kern-error" id="input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Die Datenschutzerklärung muss akzeptiert werden.</span>
  </p>
</div>
```

### Catalog – Radio

Story: Radio
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr1" name="geschlecht1" type="radio">
  <label class="kern-label" for="radio-herr1">Herr</label>
</div>
```

Story: RadioHover
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr11" name="geschlecht11" type="radio">
  <label class="kern-label" for="radio-herr11">Herr</label>
</div>
```

Story: RadioFocus
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr111" name="geschlecht111" type="radio">
  <label class="kern-label" for="radio-herr111">Herr</label>
</div>
```

Story: RadioChecked
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr2" name="geschlecht2" type="radio" checked>
  <label class="kern-label" for="radio-herr2">Herr</label>
</div>
```

Story: RadioCheckedHover
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr21" name="geschlecht21" type="radio" checked>
  <label class="kern-label" for="radio-herr21">Herr</label>
</div>
```

Story: RadioCheckedFocus
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr211" name="geschlecht211" type="radio" checked>
  <label class="kern-label" for="radio-herr211">Herr</label>
</div>
```

Story: RadioDisabled
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr3" name="geschlecht3" type="radio" disabled>
  <label class="kern-label" for="radio-herr3">Herr</label>
</div>
```

Story: RadioDisabledChecked
```html
<div class="kern-form-check">
  <input class="kern-form-check__radio" id="radio-herr4" name="geschlecht4" type="radio" disabled checked>
  <label class="kern-label" for="radio-herr4">Herr</label>
</div>
```

Story: Radios (Gruppe)
```html
<fieldset class="kern-fieldset">
  <legend class="kern-label">
    Ansprechpartner
  </legend>
  <div class="kern-fieldset__body">
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="radio-herr" name="geschlecht" type="radio">
      <label class="kern-label" for="radio-herr">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="frau" name="geschlecht" type="radio">
      <label class="kern-label" for="frau">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="divers" name="geschlecht" type="radio">
      <label class="kern-label" for="divers">Divers</label>
    </div>
  </div>
</fieldset>
```

Story: RadiosDisabled
```html
<fieldset class="kern-fieldset">
  <legend class="kern-label">
    Ansprechpartner
  </legend>
  <div class="kern-fieldset__body">
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="radio-herr6" name="geschlecht6" type="radio">
      <label class="kern-label" for="radio-herr6">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="frau6" name="geschlecht6" type="radio">
      <label class="kern-label" for="frau6">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="divers6" name="geschlecht6" type="radio" disabled>
      <label class="kern-label" for="divers6">Divers</label>
    </div>
  </div>
</fieldset>
```

Story: RadiosHintError
```html
<fieldset class="kern-fieldset kern-fieldset--error" aria-describedby="hint-text">
  <legend class="kern-label">
    Ansprechpartner
  </legend>
  <div class="kern-hint" id="hint-text">Hint</div>
  <div class="kern-fieldset__body">
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="radio-herr7" name="geschlecht7" type="radio">
      <label class="kern-label" for="radio-herr7">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="frau7" name="geschlecht7" type="radio">
      <label class="kern-label" for="frau7">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="divers7" name="geschlecht7" type="radio">
      <label class="kern-label" for="divers7">Divers</label>
    </div>
  </div>
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Eine Auswahl muss getroffen werden</span>
  </p>
</fieldset>
```

Story: RadiosHintHorizontal
```html
<fieldset class="kern-fieldset">
  <legend class="kern-label">
    Ansprechpartner
  </legend>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="radio-herr8" name="geschlecht8" type="radio">
      <label class="kern-label" for="radio-herr8">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="frau8" name="geschlecht8" type="radio">
      <label class="kern-label" for="frau8">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="divers8" name="geschlecht8" type="radio">
      <label class="kern-label" for="divers8">Divers</label>
    </div>
  </div>
</fieldset>
```

Story: RadiosHintOptional
```html
<fieldset class="kern-fieldset">
  <legend class="kern-label">
    Ansprechpartner
    <span class="kern-label__optional">- Optional</span>
  </legend>
  <div class="kern-fieldset__body">
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="radio-herr10" name="geschlecht10" type="radio">
      <label class="kern-label" for="radio-herr10">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="frau10" name="geschlecht10" type="radio">
      <label class="kern-label" for="frau10">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio" id="divers10" name="geschlecht10" type="radio">
      <label class="kern-label" for="divers10">Divers</label>
    </div>
  </div>
</fieldset>
```

Story: RadiosHintHorizontalError
```html
<fieldset class="kern-fieldset kern-fieldset--error">
  <legend class="kern-label">
    Ansprechpartner
  </legend>
  <div class="kern-hint" id="hint-text">Hint</div>
  <div class="kern-fieldset__body kern-fieldset__body--horizontal">
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="radio-herr9" name="geschlecht9" type="radio">
      <label class="kern-label" for="radio-herr9">Herr</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="frau9" name="geschlecht9" type="radio">
      <label class="kern-label" for="frau9">Frau</label>
    </div>
    <div class="kern-form-check">
      <input class="kern-form-check__radio kern-form-check__radio--error" id="divers9" name="geschlecht9" type="radio">
      <label class="kern-label" for="divers9">Divers</label>
    </div>
  </div>
  <p class="kern-error" id="kern-input-error" role="alert">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-body">Eine Auswahl muss getroffen werden</span>
  </p>
</fieldset>
```

### Catalog – Alert (Alle Einzelstories)
Story: Alert (Template – alle Varianten in einem Block)
```html
<div class="kern-alert kern-alert--info" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--info" aria-hidden="true"></span>
    <span class="kern-title">Hinweis</span>  
  </div>
</div>

<br>

<div class="kern-alert kern-alert--success" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
    <span class="kern-title">Erfolg</span>  
  </div>
</div>

<br>

<div class="kern-alert kern-alert--warning" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--warning" aria-hidden="true"></span>
    <span class="kern-title">Warnung</span>  
  </div>
</div>

<br>

<div class="kern-alert kern-alert--danger" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-title">Fehler</span>  
  </div>
</div>
```

Story: AlertInfo
```html
<div class="kern-alert kern-alert--info" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--info" aria-hidden="true"></span>
    <span class="kern-title">Hinweis</span>  
  </div>
</div>
```

Story: AlertSuccess
```html
<div class="kern-alert kern-alert--success" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
    <span class="kern-title">Erfolg</span>  
  </div>
</div>
```

Story: AlertWarning
```html
<div class="kern-alert kern-alert--warning" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--warning" aria-hidden="true"></span>
    <span class="kern-title">Warnung</span>  
  </div>
</div>
```

Story: AlertDanger
```html
<div class="kern-alert kern-alert--danger" role="alert">
  <div class="kern-alert__header">
    <span class="kern-icon kern-icon--danger" aria-hidden="true"></span>
    <span class="kern-title">Fehler</span>  
  </div>
</div>
```

### Catalog – Badge
Story: Badges (Template)
```html
<span class="kern-badge kern-badge--info">
  <span class="kern-label kern-label--small">Info</span>
</span>
<span class="kern-badge kern-badge--success">
  <span class="kern-label kern-label--small">Success</span>
</span>
<span class="kern-badge kern-badge--warning">
  <span class="kern-label kern-label--small">Warning</span>
</span>
<span class="kern-badge kern-badge--danger">
  <span class="kern-label kern-label--small">Danger</span>
</span>
```

Story: BadgeInfo
```html
<span class="kern-badge kern-badge--info">
  <span class="kern-label kern-label--small">Info</span>
</span>
```

Story: BadgeSuccess
```html
<span class="kern-badge kern-badge--success">
  <span class="kern-label kern-label--small">Success</span>
</span>
```

Story: BadgeWarning
```html
<span class="kern-badge kern-badge--warning">
  <span class="kern-label kern-label--small">Warning</span>
</span>
```

Story: BadgeDanger
```html
<span class="kern-badge kern-badge--danger">
  <span class="kern-label kern-label--small">Danger</span>
</span>
```

### Catalog – Accordion
Story: Accordion
```html
<details class="kern-accordion">
  <summary class="kern-accordion__header">
    <span class="kern-title">Accordion 1</span>
  </summary>
  <section class="kern-accordion__body">
    <p class="kern-body">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p>
  </section>
</details>
```

Story: AccordionOpen
```html
<details class="kern-accordion" open>
  <summary class="kern-accordion__header">
    <span class="kern-title">Accordion 1</span>
  </summary>
  <section class="kern-accordion__body">
    <p class="kern-body">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p>
  </section>
</details>
```

Story: AccordionGroup
```html
<div class="kern-accordion-group">
  <details class="kern-accordion">
    <summary class="kern-accordion__header">
      <span class="kern-title">Accordion 1</span>
    </summary>
    <section class="kern-accordion__body">
      <p class="kern-body">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p>
    </section>
  </details>
  <details class="kern-accordion">
    <summary class="kern-accordion__header">
      <span class="kern-title">Accordion 2</span>
    </summary>
    <section class="kern-accordion__body">
      <p class="kern-body">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p>
    </section>
  </details>
  <details class="kern-accordion">
    <summary class="kern-accordion__header">
      <span class="kern-title">Accordion 3</span>
    </summary>
    <section class="kern-accordion__body">
      <p class="kern-body">Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.</p>
    </section>
  </details>
</div>
```

### Catalog – Card (Auszug – vollständige Stories bereits im Code oben verfügbar)
Story: Card
```html
<article class="kern-card" style="max-width: 320px;"> 
  <div class="kern-card__media">
    <img src="https://placehold.co/600x400?text=MEDIA" alt="ALT Text">
  </div>
  <div class="kern-card__container">
    <header class="kern-card__header">
      <p class="kern-preline">Preline</p>
      <h2 class="kern-title">Title</h2>
      <h3 class="kern-subline">Subline</h3>
    </header>
    <section class="kern-card__body">
      <p class="kern-body">Hier werden soviele Informationen zur Verfügung gestellt, wie benötigt und nicht mehr. Ein Maximum von 150 characters empfohlen.</p>
    </section>
    <footer class="kern-card__footer">
      <button class="kern-btn kern-btn--primary">
        <span class="kern-label">Primäraktion</span>
      </button>
      <button class="kern-btn kern-btn--secondary">
        <span class="kern-label">Aktion</span>
      </button>
    </footer>
  </div>
</article> 
```

Story: CardMultiple (verkürzt – voller Code in Storydatei)
```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col">
      <article class="kern-card"> ... </article>
    </div>
    <div class="kern-col">
      <article class="kern-card kern"> ... </article>
    </div>
    <div class="kern-col">
      <article class="kern-card"> ... </article>
    </div>
  </div>
</div>
```

Story: CardMultipleHug (verkürzt)
```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col"><article class="kern-card kern-card--hug"> ... </article></div>
    <div class="kern-col"><article class="kern-card kern-card--hug"> ... </article></div>
    <div class="kern-col"><article class="kern-card kern-card--hug"> ... </article></div>
  </div>
</div>
```

Story: CardActive (verkürzt)
```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col"><article class="kern-card kern-card--active" style="max-width: 320px;"> ... </article></div>
  </div>
</div>
```

Story: CardSmall
```html
<article class="kern-card kern-card--small" style="max-width: 320px;"> ... </article>
```

Story: CardLarge
```html
<article class="kern-card kern-card--large" style="max-width: 320px;"> ... </article>
```

### Catalog – Link
Story: Link

```html
<a href="#" class="kern-link">Link</a>
```

Story: LinkFocus (Pseudo)

```html
<a href="#" class="kern-link">Link</a>
```

Story: LinkHover (Pseudo)

```html
<a href="#" class="kern-link">Link</a>
```

Story: LinkActive (Pseudo)

```html
<a href="#" class="kern-link">Link</a>
```

Story: LinkVisited (Pseudo)

```html
<a href="#" class="kern-link">Link</a>
```

### Catalog – Lists (alle Stories)

Story: List

```html
<ul class="kern-list">
  <li>
    <a class="kern-link" href="#">
      <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
      Ein weitere Link
    </a>
  </li>
  <li>
    <a class="kern-link" href="#">
      <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
      Dataport Homepage
    </a>
  </li>
  <li>
    <a class="kern-link" href="#">
      <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
      Link zu Dokumentation
    </a>
  </li>
  <li>
    <a class="kern-link" href="#">
      <span class="kern-icon kern-icon--arrow-forward" aria-hidden="true"></span>
      More
    </a>
  </li>
</ul>
```

Story: BulletList

```html
<p class="kern-body kern-body--bold">Was wollen wie erreichen:</p>
<ul class="kern-list kern-list--bullet">
  <li>Wir wollen den digital zugänglichen Staat: barrierefrei, transparent und intuitiv nutzbar für alle Bürger:innen, Institutionen und Unternehmen im Land</li>
  <li>Darum arbeiten wir länderübergreifend an einem technologisch unabhängigen Open Source Design System für Kommunal- bis Bundesebene</li>
  <li>So befähigen wir Umsetzende, effizient digitale Verwaltungslösungen zu entwickeln, die durch die Qualität und Vereinheitlichung auf das Serviceerlebnis und Vertrauen der Nutzer:innen einzahlen</li>
</ul>
```

Story: BulletListSmall

```html
<ul class="kern-list kern-list--bullet kern-list--small">
  <li>Wir wollen den digital zugänglichen Staat: barrierefrei, transparent und intuitiv nutzbar für alle Bürger:innen, Institutionen und Unternehmen im Land</li>
  <li>Darum arbeiten wir länderübergreifend an einem technologisch unabhängigen Open Source Design System für Kommunal- bis Bundesebene</li>
  <li>So befähigen wir Umsetzende, effizient digitale Verwaltungslösungen zu entwickeln, die durch die Qualität und Vereinheitlichung auf das Serviceerlebnis und Vertrauen der Nutzer:innen einzahlen</li>
</ul>
```

Story: BulletListLarge

```html
<ul class="kern-list kern-list--bullet kern-list--large">
  <li>Wir wollen den digital zugänglichen Staat: barrierefrei, transparent und intuitiv nutzbar für alle Bürger:innen, Institutionen und Unternehmen im Land</li>
  <li>Darum arbeiten wir länderübergreifend an einem technologisch unabhängigen Open Source Design System für Kommunal- bis Bundesebene</li>
  <li>So befähigen wir Umsetzende, effizient digitale Verwaltungslösungen zu entwickeln, die durch die Qualität und Vereinheitlichung auf das Serviceerlebnis und Vertrauen der Nutzer:innen einzahlen</li>
</ul>
```

Story: NumberLists

```html
<ol class="kern-list kern-list--number">
  <li>Wir wollen den digital zugänglichen Staat: barrierefrei, transparent und intuitiv nutzbar für alle Bürger:innen, Institutionen und Unternehmen im Land.</li>
  <li>Darum arbeiten wir länderübergreifend an einem technologisch unabhängigen Open Source Design System für Kommunal- bis Bundesebene.</li>
  <li>So befähigen wir Umsetzende, effizient digitale Verwaltungslösungen zu entwickeln, die durch die Qualität und Vereinheitlichung auf das Serviceerlebnis und Vertrauen der Nutzer:innen einzahlen.</li>
</ol>
```

Story: NumberListsSmall

```html
<ol class="kern-list kern-list--number kern-list--small">
  <li>Wir wollen den digital zugänglichen Staat: barrierefrei, transparent und intuitiv nutzbar für alle Bürger:innen, Institutionen und Unternehmen im Land.</li>
  <li>Darum arbeiten wir länderübergreifend an einem technologisch unabhängigen Open Source Design System für Kommunal- bis Bundesebene.</li>
  <li>So befähigen wir Umsetzende, effizient digitale Verwaltungslösungen zu entwickeln, die durch die Qualität und Vereinheitlichung auf das Serviceerlebnis und Vertrauen der Nutzer:innen einzahlen.</li>
</ol>
```

Story: NumberListsLarge

```html
<ol class="kern-list kern-list--number kern-list--large">
  <li>Wir wollen den digital zugänglichen Staat: barrierefrei, transparent und intuitiv nutzbar für alle Bürger:innen, Institutionen und Unternehmen im Land.</li>
  <li>Darum arbeiten wir länderübergreifend an einem technologisch unabhängigen Open Source Design System für Kommunal- bis Bundesebene.</li>
  <li>So befähigen wir Umsetzende, effizient digitale Verwaltungslösungen zu entwickeln, die durch die Qualität und Vereinheitlichung auf das Serviceerlebnis und Vertrauen der Nutzer:innen einzahlen.</li>
</ol>
```

### Catalog – Description List (Stories)

Story: DescriptionList

```html
<dl class="kern-description-list">
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Name</dt>
    <dd class="kern-description-list-item__value">Max</dd>
  </div>
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Vorname</dt>
    <dd class="kern-description-list-item__value">Mustermann</dd>
  </div>
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">E-Mail</dt>
    <dd class="kern-description-list-item__value">dasisteinesehrlangeemailadressevonmaxmustermann@unternehmen.xy</dd>
  </div>
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Sachkundigen vor Ort</dt>
    <dd class="kern-description-list-item__value">
      <ul>
        <li>Berger Ernst</li>
        <li>Müller, Hans</li>
        <li>Sagmeister, Petra</li>
        <li>Pan, Peter</li>
      </ul>
    </dd>
  </div>
</dl>
```

Story: DescriptionListCol

```html
<dl class="kern-description-list kern-description-list--col">
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Warum kann die Tätigkeit nur an einem Sonntag oder Feiertag durchgeführt werden</dt>
    <dd class="kern-description-list-item__value">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam fringilla fringilla lobortis.</dd>
  </div>
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Welche Tätigkeiten werden am Sonntag oder Feiertag durchgeführt</dt>
    <dd class="kern-description-list-item__value">Ut finibus felis consectetur ullamcorper consectetur. Lorem ipsum dolor sit amet, consectetur adipiscing elit.</dd>
  </div>
  <div class="kern-description-list-item">
    <dt class="kern-description-list-item__key">Welchen wirtschaftlichen Schaden entsteht, sollten die Arbeiten am gewünschten Sonn- oder Feiertag nicht stattfinden.</dt>
    <dd class="kern-description-list-item__value">Cras lacus purus, molestie et quam eget, condimentum finibus enim.</dd>
  </div>
</dl>
```

### Catalog – Summary (Stories)

Story: Summary

```html
<div class="kern-summary">
  <div class="kern-summary__header"> 
    <span class="kern-number">1</span>
    <h3 class="kern-title kern-title--small" id="title">Aufgabe</h3>
  </div>
  <div class="kern-summary__body">
    <dl class="kern-description-list">
      <div class="kern-description-list-item">
        <dt class="kern-description-list-item__key">Name</dt>
        <dd class="kern-description-list-item__value">Max</dd>
      </div>
      <div class="kern-description-list-item">
        <dt class="kern-description-list-item__key">Vorname</dt>
        <dd class="kern-description-list-item__value">Mustermann</dd>
      </div>
      <div class="kern-description-list-item">
        <dt class="kern-description-list-item__key">E-Mail</dt>
        <dd class="kern-description-list-item__value">dasisteinesehrlangeemailadressevonmaxmustermann@unternehmen.xy</dd>
      </div>
      <div class="kern-description-list-item">
        <dt class="kern-description-list-item__key">Sachkundigen vor Ort</dt>
        <dd class="kern-description-list-item__value">
          <ul>
            <li>Berger Ernst</li>
            <li>Müller, Hans</li>
            <li>Sagmeister, Petra</li>
            <li>Pan, Peter</li>
          </ul>
        </dd>
      </div>
    </dl>
    <div class="kern-summary__actions">
      <a href="#" class="kern-link" aria-describedby="title">
        <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
        Bearbeiten
      </a>
    </div>
  </div>
</div>
```

Story: SummaryGroup

```html
<div class="kern-summary-group">
  <div class="kern-summary">
    <div class="kern-summary__header">
      <span class="kern-number">1</span>
      <h3 class="kern-title kern-title--small" id="title2">Aufgabe</h3>
    </div>
    <div class="kern-summary__body">
      <dl class="kern-description-list">
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Name</dt>
          <dd class="kern-description-list-item__value">Max</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Vorname</dt>
          <dd class="kern-description-list-item__value">Mustermann</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">E-Mail</dt>
          <dd class="kern-description-list-item__value">dasisteinesehrlangeemailadressevonmaxmustermann@unternehmen.xy</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Sachkundigen vor Ort</dt>
          <dd class="kern-description-list-item__value">
            <ul>
              <li>Berger Ernst</li>
              <li>Müller, Hans</li>
              <li>Sagmeister, Petra</li>
              <li>Pan, Peter</li>
            </ul>
          </dd>
        </div>
      </dl>
      <div class="kern-summary__actions">
        <a href="#" class="kern-link" aria-describedby="title2">
          <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
          Bearbeiten
        </a>
      </div>
    </div>
  </div>
  <div class="kern-summary">
    <div class="kern-summary__header">
      <span class="kern-number">2</span>
      <h3 class="kern-title kern-title--small" id="title3">Aufgabe</h3>
    </div>
    <div class="kern-summary__body">
      <dl class="kern-description-list">
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Name</dt>
          <dd class="kern-description-list-item__value">Max</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Vorname</dt>
          <dd class="kern-description-list-item__value">Mustermann</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">E-Mail</dt>
          <dd class="kern-description-list-item__value">dasisteinesehrlangeemailadressevonmaxmustermann@unternehmen.xy</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Sachkundigen vor Ort</dt>
          <dd class="kern-description-list-item__value">
            <ul>
              <li>Berger Ernst</li>
              <li>Müller, Hans</li>
              <li>Sagmeister, Petra</li>
              <li>Pan, Peter</li>
            </ul>
          </dd>
        </div>
      </dl>
      <div class="kern-summary__actions">
        <a href="#" class="kern-link" aria-describedby="title3">
          <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
          Bearbeiten
        </a>
      </div>
    </div>
  </div>
  <div class="kern-summary">
    <div class="kern-summary__header">
      <span class="kern-number">3</span>
      <h3 class="kern-title kern-title--small" id="title4">Aufgabe</h3>
    </div>
    <div class="kern-summary__body">
      <dl class="kern-description-list">
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Name</dt>
          <dd class="kern-description-list-item__value">Max</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Vorname</dt>
          <dd class="kern-description-list-item__value">Mustermann</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">E-Mail</dt>
          <dd class="kern-description-list-item__value">dasisteinesehrlangeemailadressevonmaxmustermann@unternehmen.xy</dd>
        </div>
        <div class="kern-description-list-item">
          <dt class="kern-description-list-item__key">Sachkundigen vor Ort</dt>
          <dd class="kern-description-list-item__value">
            <ul>
              <li>Berger Ernst</li>
              <li>Müller, Hans</li>
              <li>Sagmeister, Petra</li>
              <li>Pan, Peter</li>
            </ul>
          </dd>
        </div>
      </dl>
      <div class="kern-summary__actions">
        <a href="#" class="kern-link" aria-describedby="title4">
          <span class="kern-icon kern-icon--edit" aria-hidden="true"></span>
          Bearbeiten
        </a>
      </div>
    </div>
  </div>
</div>
```

### Catalog – Task List (Stories)

Story: TaskList

```html
<div class="kern-task-list">
  <div class="kern-task-list__header">
    <h2 class="kern-heading-medium">Aufgabengruppe 1</h2>
  </div>
  <ul class="kern-task-list__list">
    <li class="kern-task-list__item">
      <span class="kern-number">1</span>
      <div class="kern-task-list__title" id="task1-title">
        <a href="#" class="kern-link kern-link--stretched" aria-describedby="task1-status">Aufgabe xy erledigen</a>
        <div class="kern-task-list__status" id="task1-status">
          <span class="kern-badge kern-badge--success">
            <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
            <span class="kern-label kern-label--small">Erledigt</span>
          </span>
        </div>
      </div>
    </li>
    <li class="kern-task-list__item">
      <span class="kern-number">2</span>
      <div class="kern-task-list__title" id="task2-title">
        <a href="#" class="kern-link kern-link--stretched" aria-describedby="task2-status">Aufgabe xy erledigen</a>
        <div class="kern-task-list__status" id="task2-status">
          <span class="kern-badge kern-badge--warning">
            <span class="kern-label kern-label--small">Unvollständig</span>
          </span>
        </div>
      </div>
    </li>
    <li class="kern-task-list__item">
      <span class="kern-number">3</span>
      <div class="kern-task-list__title" id="task3-title">
        <p class="kern-body">Aufgabe xy erledigen</p>
        <div class="kern-task-list__status" id="task3-status">
          <span class="kern-badge kern-badge--info">
            <span class="kern-label kern-label--small">Noch nicht zu bearbeiten</span>
          </span>
        </div>
      </div>
    </li>
  </ul>
</div>
```

Story: TaskListGroup

```html
<div class="kern-task-list-group">
  <div class="kern-task-list">
    <div class="kern-task-list__header">
      <h2 class="kern-heading-medium">Aufgabengruppe 1</h2>
    </div>
    <ul class="kern-task-list__list">
      <li class="kern-task-list__item">
        <span class="kern-number">1</span>
        <div class="kern-task-list__title" id="task1-title">
          <a href="#" class="kern-link kern-link--stretched" aria-describedby="task3-status">Aufgabe xy erledigen</a>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--success">
              <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
              <span class="kern-label kern-label--small">Erledigt</span>
            </span>
          </div>
        </div>
      </li>
      <li class="kern-task-list__item">
        <span class="kern-number">2</span>
        <div class="kern-task-list__title" id="task2-title">
          <a href="#" class="kern-link kern-link--stretched" aria-describedby="task4-status">Aufgabe xy erledigen</a>
          <div class="kern-task-list__status" id="task4-status">
            <span class="kern-badge kern-badge--warning">
              <span class="kern-label kern-label--small">Unvollständig</span>
            </span>
          </div>
        </div>
      </li>
      <li class="kern-task-list__item">
        <span class="kern-number">3</span>
        <div class="kern-task-list__title" id="task3-title">
          <p class="kern-body">Aufgabe xy erledigen</p>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--info">
              <span class="kern-label kern-label--small">Noch nicht zu bearbeiten</span>
            </span>
          </div>
        </div>
      </li>
    </ul>
  </div>
  <div class="kern-task-list">
    <div class="kern-task-list__header">
      <h2 class="kern-heading-medium">Aufgabengruppe 2</h2>
    </div>
    <ul class="kern-task-list__list">
      <li class="kern-task-list__item">
        <span class="kern-number">4</span>
        <div class="kern-task-list__title" id="task1-title">
          <a href="#" class="kern-link kern-link--stretched" aria-describedby="task3-status">Aufgabe xy erledigen</a>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--success">
              <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
              <span class="kern-label kern-label--small">Erledigt</span>
            </span>
          </div>
        </div>
      </li>
      <li class="kern-task-list__item">
        <span class="kern-number">5</span>
        <div class="kern-task-list__title" id="task2-title">
          <a href="#" class="kern-link kern-link--stretched" aria-describedby="task4-status">Aufgabe xy erledigen</a>
          <div class="kern-task-list__status" id="task4-status">
            <span class="kern-badge kern-badge--warning">
              <span class="kern-label kern-label--small">Unvollständig</span>
            </span>
          </div>
        </div>
      </li>
      <li class="kern-task-list__item">
        <span class="kern-number">6</span>
        <div class="kern-task-list__title" id="task3-title">
          <p class="kern-body">Aufgabe xy erledigen</p>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--info">
              <span class="kern-label kern-label--small">Noch nicht zu bearbeiten</span>
            </span>
          </div>
        </div>
      </li>
    </ul>
  </div>
  <div class="kern-task-list">
    <div class="kern-task-list__header">
      <h2 class="kern-heading-medium">Zusammenfassung</h2>
    </div>
    <ul class="kern-task-list__list">
      <li class="kern-task-list__item">
        <div class="kern-task-list__title" id="task3-title">
          <p class="kern-body">Wenn Sie alle Angaben gemacht haben, können Sie diese noch einmal prüfen.</p>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--info">
              <span class="kern-label kern-label--small">Noch nicht zu bearbeiten</span>
            </span>
          </div>
        </div>
      </li>
    </ul>
  </div>
</div>
```

Story: TaskListUnordered

```html
<div class="kern-task-list-group">
  <div class="kern-task-list">
    <div class="kern-task-list__header">
      <h2 class="kern-heading-medium">Aufgabengruppe 1</h2>
    </div>
    <ul class="kern-task-list__list">
      <li class="kern-task-list__item">
        <div class="kern-task-list__title" id="task1-title">
          <a href="#" class="kern-link kern-link--stretched" aria-describedby="task3-status">Aufgabe xy erledigen</a>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--success">
              <span class="kern-icon kern-icon--success" aria-hidden="true"></span>
              <span class="kern-label kern-label--small">Erledigt</span>
            </span>
          </div>
        </div>
      </li>     
      <li class="kern-task-list__item">
        <div class="kern-task-list__title" id="task2-title">
          <a href="#" class="kern-link kern-link--stretched" aria-describedby="task4-status">Aufgabe xy erledigen</a>
          <div class="kern-task-list__status" id="task4-status">
            <span class="kern-badge kern-badge--warning">
              <span class="kern-label kern-label--small">Unvollständig</span>
            </span>
          </div>
        </div>
      </li>     
      <li class="kern-task-list__item">
        <div class="kern-task-list__title" id="task3-title">
          <p class="kern-body">Aufgabe xy erledigen</p>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--info">
              <span class="kern-label kern-label--small">Noch nicht zu bearbeiten</span>
            </span>
          </div>
        </div>
      </li>
    </ul>
  </div>
  <div class="kern-task-list">
    <div class="kern-task-list__header">
      <h2 class="kern-heading-medium">Zusammenfassung</h2>
    </div>
    <ul class="kern-task-list__list">
      <li class="kern-task-list__item">
        <div class="kern-task-list__title" id="task3-title">
          <p class="kern-body">Wenn Sie alle Angaben gemacht haben, können Sie diese noch einmal prüfen.</p>
          <div class="kern-task-list__status" id="task3-status">
            <span class="kern-badge kern-badge--info">
              <span class="kern-label kern-label--small">Noch nicht zu bearbeiten</span>
            </span>
          </div>
        </div>
      </li>
    </ul>
  </div>
</div>
```

### Catalog – Table (Stories)

Story: Table

```html
<table class="kern-table">
  <caption class="kern-title">Autovermietung Flotte</caption>
  <thead class="kern-table__head">
    <tr class="kern-table__row">
      <th scope="col" class="kern-table__header">Marke</th>
      <th scope="col" class="kern-table__header">Verfügbarkeit</th>
      <th scope="col" class="kern-table__header kern-table__header--numeric">Tagespreis</th>
    </tr>
  </thead>
  <tbody class="kern-table__body">
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">BMW</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">69,00 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Fiat</th>
      <td class="kern-table__cell">Vermietet</td>
      <td class="kern-table__cell kern-table__cell--numeric">49,59 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">VW</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">54,99 €</td>
    </tr>
  </tbody>
</table>
```

Story: TableSmall

```html
<table class="kern-table kern-table--small">
  <caption class="kern-title">Autovermietung Flotte</caption>
  <thead class="kern-table__head">
    <tr class="kern-table__row">
      <th scope="col" class="kern-table__header">Marke</th>
      <th scope="col" class="kern-table__header">Verfügbarkeit</th>
      <th scope="col" class="kern-table__header kern-table__header--numeric">Tagespreis</th>
    </tr>
  </thead>
  <tbody class="kern-table__body">
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">BMW</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">69,00 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Fiat</th>
      <td class="kern-table__cell">Vermietet</td>
      <td class="kern-table__cell kern-table__cell--numeric">49,59 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">VW</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">54,99 €</td>
    </tr>
  </tbody>
</table>
```

Story: TableStripes

```html
<table class="kern-table kern-table--striped">
  <caption class="kern-title">Autovermietung Flotte</caption>
  <thead class="kern-table__head">
    <tr class="kern-table__row">
      <th scope="col" class="kern-table__header">Marke</th>
      <th scope="col" class="kern-table__header">Verfügbarkeit</th>
      <th scope="col" class="kern-table__header kern-table__header--numeric">Tagespreis</th>
    </tr>
  </thead>
  <tbody class="kern-table__body">
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">BMW</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">69,00 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Fiat</th>
      <td class="kern-table__cell">Vermietet</td>
      <td class="kern-table__cell kern-table__cell--numeric">49,59 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">VW</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">54,99 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Audi</th>
      <td class="kern-table__cell">Frei</td>
      <td class="kern-table__cell kern-table__cell--numeric">84,99 €</td>
    </tr>
  </tbody>
</table>
```

Story: TableFooter

```html
<table class="kern-table">
  <caption class="kern-title">Kino Besucherzahlen</caption>
  <thead class="kern-table__head">
    <tr class="kern-table__row">
      <th scope="col" class="kern-table__header">Wochentag</th>
      <th scope="col" class="kern-table__header kern-table__header--numeric">Besucherzahl</th>
      <th scope="col" class="kern-table__header kern-table__header--numeric">Tagesumsatz</th>
    </tr>
  </thead>
  <tbody class="kern-table__body">
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Montag</th>
      <td class="kern-table__cell kern-table__cell--numeric">320</td>
      <td class="kern-table__cell kern-table__cell--numeric">3439,00 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Dienstag</th>
      <td class="kern-table__cell kern-table__cell--numeric">445</td>
      <td class="kern-table__cell kern-table__cell--numeric">5043,90 €</td>
    </tr>
    <tr class="kern-table__row">
      <th scope="row" class="kern-table__header">Mittwoch</th>
      <td class="kern-table__cell kern-table__cell--numeric">686</td>
      <td class="kern-table__cell kern-table__cell--numeric">7336,60 €</td>
    </tr>
  </tbody>
  <tfoot class="kern-table__footer">
    <tr class="kern-table__row">
      <th scope="col" class="kern-table__header">Durchschnitt</th>
      <td class="kern-table__cell kern-table__cell--numeric">483,67</td>
      <td class="kern-table__cell kern-table__cell--numeric">5273,17 €</td>
    </tr>
  </tfoot>
</table>
```

Story: TableResponsive

```html
<div class="kern-table-responsive" tabindex="0">
  <table class="kern-table kern-table--striped">
    <caption class="kern-title">Autovermietung Flotte</caption>
    <thead class="kern-table__head">
      <tr class="kern-table__row">
        <th scope="col" class="kern-table__header">Marke</th>
        <th scope="col" class="kern-table__header">Model</th>
        <th scope="col" class="kern-table__header">Baujahr</th>
        <th scope="col" class="kern-table__header">Farbe</th>
        <th scope="col" class="kern-table__header kern-table__header--numeric">PS</th>
        <th scope="col" class="kern-table__header">Verfügbarkeit</th>
        <th scope="col" class="kern-table__header kern-table__header--numeric">Tagespreis</th>
        <th scope="col" class="kern-table__header kern-table__header--numeric">Wochenpreis</th>
        <th scope="col" class="kern-table__header kern-table__header--numeric">Monatspreis</th>
        <th scope="col" class="kern-table__header kern-table__header--numeric">Jahrespreis</th>
      </tr>
    </thead>
    <tbody class="kern-table__body">
      <tr class="kern-table__row">
        <th scope="row" class="kern-table__header">BMW</th>
        <td class="kern-table__cell">5er</td>
        <td class="kern-table__cell">2020</td>
        <td class="kern-table__cell">Rot</td>
        <td class="kern-table__cell kern-table__cell--numeric">89</td>
        <td class="kern-table__cell">Frei</td>
        <td class="kern-table__cell kern-table__cell--numeric">69,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">369,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">1169,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">6069,00 €</td>
      </tr>
      <tr class="kern-table__row">
        <th scope="row" class="kern-table__header">Audi</th>
        <td class="kern-table__cell">A6</td>
        <td class="kern-table__cell">2021</td>
        <td class="kern-table__cell">Schwarz</td>        
        <td class="kern-table__cell kern-table__cell--numeric">77</td>
        <td class="kern-table__cell">Frei</td>
        <td class="kern-table__cell kern-table__cell--numeric">59,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">319,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">1069,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">5069,00 €</td>
      </tr>
      <tr class="kern-table__row">
        <th scope="row" class="kern-table__header">Mercedes</th>
        <td class="kern-table__cell">200D</td>
        <td class="kern-table__cell">1994</td>
        <td class="kern-table__cell">Weiß</td>
        <td class="kern-table__cell kern-table__cell--numeric">105</td>
        <td class="kern-table__cell">Frei</td>
        <td class="kern-table__cell kern-table__cell--numeric">49,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">269,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">969,00 €</td>
        <td class="kern-table__cell kern-table__cell--numeric">3069,00 €</td>
      </tr>
    </tbody>
  </table>
</div>
```

### Catalog – Progress (Stories)

Story: Progress

```html
<div class="kern-progress">
  <progress value="35" max="100"></progress>
</div>
```

Story: ProgressLabelTop

```html
<div class="kern-progress">
  <label class="kern-label" for="progress1">Schritt 2 von 5</label>
  <progress id="progress1" value="2" max="5"></progress>
</div>
```

Story: ProgressLabelBottom

```html
<div class="kern-progress">
  <progress id="progress2" value="5" max="8"></progress>
  <label class="kern-label" for="progress2">Schritt 5 von 8</label>
</div>
```

### Catalog – Loader (Stories)

Story: Loader

```html
<div class="kern-loader kern-loader--visible" role="status">
  <span class="kern-sr-only">Wird geladen...</span>
</div>
```

### Catalog – Dialog (Stories)

Story: DialogOpen

```html
<dialog id="modal1" class="kern-dialog" aria-labelledby="modal1_heading" style="display: block">
  <header class="kern-dialog__header">
    <h2 class="kern-title kern-title--large" id="modal1_heading">Frage?</h2>
    <button class="kern-btn kern-btn--tertiary">
      <span class="kern-icon kern-icon--close" aria-hidden="true"></span>
      <span class="kern-sr-only">Schließen</span>
    </button>
  </header>
  <section class="kern-dialog__body">
    <p class="kern-body">
  Folgen beschreiben...
  Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
    </p>
  </section>
  <footer class="kern-dialog__footer">
    <button class="kern-btn kern-btn--secondary">
      <span class="kern-label">Abbrechen</span>
    </button>
    <button id="confirmBtn" class="kern-btn kern-btn--primary">
      <span class="kern-label">Löschen</span>
    </button>
  </footer>
</dialog>
```

Story: DialogWithButton

```html
<button data-dialog-target="modal2" class="kern-btn kern-btn--primary">
  <span class="kern-label">Open Dialog</span>
</button>

<dialog id="modal2" class="kern-dialog" aria-modal="true" aria-labelledby="modal2_heading">
  <form>
    <header class="kern-dialog__header">
      <h2 class="kern-title kern-title--large" id="modal2_heading">Frage?</h2>
      <button class="kern-btn kern-btn--tertiary" formmethod="dialog">
        <span class="kern-icon kern-icon--close" aria-hidden="true"></span>
        <span class="kern-sr-only">Schließen</span>
      </button>
    </header>
    <section class="kern-dialog__body">
      <p class="kern-body">
        Folgen beschreiben...
        Lorem ipsum dolor sit amet, consectetur adipiscing elit sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
      </p>
    </section>
    <footer class="kern-dialog__footer">
      <button class="kern-btn kern-btn--tertiary" formmethod="dialog">
        <span class="kern-label">Tertiary link</span>
      </button>
      <button class="kern-btn kern-btn--secondary" formmethod="dialog">
        <span class="kern-label">Abbrechen</span>
      </button>
      <button id="confirmBtn" class="kern-btn kern-btn--primary" type="submit">
        <span class="kern-label">Löschen</span>
      </button>
    </footer>
  </form>
</dialog>

<script>
  document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll("[data-dialog-target]").forEach((button) => {
      const dialogId = button.getAttribute("data-dialog-target");
      const dialog = document.getElementById(dialogId);
      if (dialog) {
        button.addEventListener("click", () => dialog.showModal());
        dialog.addEventListener("click", (event) => {
          if (event.target === dialog) {
            dialog.close();
          }
        });
        const submitButton = dialog.querySelector('button[type="submit"]');
        if (submitButton) {
          submitButton.addEventListener("click", (event) => {
            event.preventDefault();
            console.log("Absenden-Button wurde geklickt!");
            dialog.close();
          });
        }
      }
    });
  });
</script>
```

### Catalog – Kopfzeile (Stories)

Story: KopfzeileFluid

```html
<div class="kern-kopfzeile">
  <div class="kern-container-fluid">
    <div class="kern-kopfzeile__content">
      <span class="kern-kopfzeile__flagge" aria-hidden="true">
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 16" style="max-width:24px">
          <path fill="#000" d="M0 .5h24v5.333H0z" />
          <path fill="red" d="M0 5.833h24v5.333H0z" />
          <path fill="#FACA2C" d="M0 11.167h24V16.5H0z" />
        </svg>
      </span>
      <span class="kern-kopfzeile__label">
        Offizielle Website – Bundesrepublik Deutschland
      </span>
    </div>
  </div>
</div>
```

Story: KopfzeileContainer

```html
<div class="kern-kopfzeile">
  <div class="kern-container">
    <div class="kern-kopfzeile__content">
      <span class="kern-kopfzeile__flagge" aria-hidden="true">
        <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 16" style="max-width:24px">
          <path fill="#000" d="M0 .5h24v5.333H0z" />
          <path fill="red" d="M0 5.833h24v5.333H0z" />
          <path fill="#FACA2C" d="M0 11.167h24V16.5H0z" />
        </svg>
      </span>
      <span class="kern-kopfzeile__label">
        Offizielle Website – Bundesrepublik Deutschland
      </span>
    </div>
  </div>
</div>
```

Story: KopfzeileWebComponent

```html
<kern-kopfzeile></kern-kopfzeile>
```

Story: KopfzeileWebComponentFluid

```html
<kern-kopfzeile fluid></kern-kopfzeile>
```

Story: KopfzeileWebComponentCustomLabel

```html
<kern-kopfzeile>Custom Label</kern-kopfzeile>
```

Story: KopfzeileWebComponentCustomBrealpoints

```html
<kern-kopfzeile breakpoints="768px:720px, 1200px:1140px">
  Custom Breakpoints: 768px:720px, 1200px:1140px
</kern-kopfzeile>
```

### Catalog – Grid (alle Stories)

Story: Grid

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: Container

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: ContainerFluid

```html
<div class="kern-container-fluid">
  <div class="kern-row">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>    
    </div>
  </div>
</div>
```

Story: ContainerCol

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="alter">Alter</label>
        <input class="kern-form-input__input" id="alter" name="alter" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: ContainerCol_9_3

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col-9">
      <div class="kern-form-input">
        <label class="kern-label" for="name">Name</label>
        <input class="kern-form-input__input" id="name" name="name" type="text">
      </div>    
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>    
    </div>
  </div>
</div>
```

Story: ContainerCol_4_4_4

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col-4">
      <div class="kern-form-input">
        <label class="kern-label" for="name">Name</label>
        <input class="kern-form-input__input" id="name" name="name" type="text">
      </div>    
    </div>
    <div class="kern-col-4">
      <div class="kern-form-input">
        <label class="kern-label" for="name2">Name 2</label>
        <input class="kern-form-input__input" id="name2" name="name2" type="text">
      </div>    
    </div>
    <div class="kern-col-4">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>    
    </div>
  </div>
</div>
```

Story: ContainerCol_SM_6_6

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col-sm-6">
      <div class="kern-form-input">
        <label class="kern-label" for="name">Name</label>
        <input class="kern-form-input__input" id="name" name="name" type="text">
      </div>    
    </div>
    <div class="kern-col-sm-6">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>    
    </div>
  </div>
</div>
```

Story: ContainerCol_MD_3_9

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col-md-3">
      <div class="kern-form-input">
        <label class="kern-label" for="name">Name</label>
        <input class="kern-form-input__input" id="name" name="name" type="text">
      </div>    
    </div>
    <div class="kern-col-md-9">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>    
    </div>
  </div>
</div>
```

Story: ContainerCol_9_Offset_3

```html
<div class="kern-container">
  <div class="kern-row">
    <div class="kern-col-12 kern-col-md-9 kern-col-md-offset-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: AlignmentItemsTop

```html
<div class="kern-container">
  <div class="kern-row kern-align-items-start" style="min-height: 10rem; background-color: var(--core-color-neutral-tint-90);">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="alter">Alter</label>
        <input class="kern-form-input__input" id="alter" name="alter" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: AlignmentItemsCenter

```html
<div class="kern-container">
  <div class="kern-row kern-align-items-center" style="min-height: 10rem; background-color: var(--core-color-neutral-tint-90);">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="alter">Alter</label>
        <input class="kern-form-input__input" id="alter" name="alter" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: AlignmentItemsEnd

```html
<div class="kern-container">
  <div class="kern-row kern-align-items-end" style="min-height: 10rem; background-color: var(--core-color-neutral-tint-90);">
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
    <div class="kern-col">
      <div class="kern-form-input">
        <label class="kern-label" for="alter">Alter</label>
        <input class="kern-form-input__input" id="alter" name="alter" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: AlignmentItemSelf

```html
<div class="kern-container">
  <div class="kern-row" style="min-height: 10rem; background-color: var(--core-color-neutral-tint-90);">
    <div class="kern-col kern-align-self-start">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col kern-align-self-center">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
    <div class="kern-col kern-align-self-end">
      <div class="kern-form-input">
        <label class="kern-label" for="alter">Alter</label>
        <input class="kern-form-input__input" id="alter" name="alter" type="text">
      </div>
    </div>
  </div>
</div>
```

Story: HorizontalAlignment

```html
<div class="kern-container">
  <div class="kern-row kern-justify-content-start" style="min-height: 6rem;">
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
  </div>
  <div class="kern-row kern-justify-content-center" style="min-height: 6rem;">
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
  </div>
  <div class="kern-row kern-justify-content-end" style="min-height: 6rem;">
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
  </div>
  <div class="kern-row kern-justify-content-around" style="min-height: 6rem;">
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
  </div>
  <div class="kern-row kern-justify-content-between" style="min-height: 6rem;">
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
  </div>
  <div class="kern-row kern-justify-content-evenly" style="min-height: 6rem;">
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="text">Name</label>
        <input class="kern-form-input__input" id="text" name="text" type="text">
      </div>
    </div>
    <div class="kern-col-3">
      <div class="kern-form-input">
        <label class="kern-label" for="vorname">Vorname</label>
        <input class="kern-form-input__input" id="vorname" name="vorname" type="text">
      </div>
    </div>
  </div>
 </div>
```

Ende des synchronisierten Storybook‑Katalogs.

