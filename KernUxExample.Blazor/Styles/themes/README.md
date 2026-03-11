# 📁 themes

## Inhalt

Dieser Ordner enthält nur das Theme-Verzeichnis für KERN, weil die Library aktuell ausschließlich das KERN-Theme als Standard-Theme ausliefert.

- **[kern]** - Enthält das Theme für KERN (Dachmarke).

Projektspezifische Zusatz-Themes werden nicht über `KernUx.Blazor` ausgeliefert. Wenn ein konsumierendes Projekt ein eigenes Theme benötigt, muss es dieses selbst als statisches Asset bereitstellen und explizit einbinden.

## Theming-Mechanismus
KERN Design System nutzt einen robusten Theming-Mechanismus, der auf CSS Custom Properties (Variablen) und der modernen OKLCH-Farbnotation basiert.  
Dies ermöglicht dynamisches, barrierefreies und überschreibbares Theming.

## 1. Das Kernprinzip: Semantische Variablen

Alle Komponenten des KERN Design Systems verwenden sogenannte semantische Variablen (z.B. `--kern-color-accent`, `--kern-color-action-default`).  
**Themes** funktionieren, indem sie diese semantischen Variablen auf dem :root-Element oder einem spezifischen Theme-Selektor neu definieren.

### Aufbau der Variablen
KERN Variablen sind in drei Ebenen unterteilt, was eine einfache Verwaltung von Licht- und Dunkelmodi (Light/Dark Mode) ermöglicht:

1. Token-Basis (Kernpaletten): Speichert die OKLCH-Werte als separate Komponenten (Lightness, Chroma, Hue).

Beispiel: `--kern-darkblue-700-lightness`, `--kern-darkblue-700-chroma`, `--kern-darkblue-700-hue`;

2. Semantische Variable: Verwendet die Token-Basis, um eine konkrete, nutzungsbezogene Farbe zu definieren.

Beispiel: `--kern-color-action-default: oklch(var(--kern-darkblue-700-lightness) var(--kern-darkblue-700-chroma) var(--kern-darkblue-700-hue));`

3. Endgültige Anwendung: Die Komponente verwendet die semantische Variable.

Beispiel: `color: var(--kern-color-action-default);`

## 2. Automatische Themenerkennung (`prefers-color-scheme`)

Der Standardmechanismus basiert auf der Media Query `@media (prefers-color-scheme:...)`, wodurch das Theme automatisch an die Systempräferenzen des Nutzers angepasst wird.

### Implementierung im Projekt
```css
/* Standard: Wenn das System den Light Mode bevorzugt */
@media (prefers-color-scheme: light) {
  :root {
    --kern-color-action-default: oklch(var(--kern-darkblue-700-lightness) var(--kern-darkblue-700-chroma) var(--kern-darkblue-700-hue));
    --kern-color-action-on-default: oklch(var(--kern-black-lightness) var(--kern-black-chroma) var(--kern-black-hue));
    /* ... weitere Light Mode Farben ... */
  }
}

/* Standard: Wenn das System den Dark Mode bevorzugt */
@media (prefers-color-scheme: dark) {
  :root {
    --kern-color-action-default: oklch(var(--kern-darkblue-300-lightness) var(--kern-darkblue-300-chroma) var(--kern-darkblue-300-hue));
    --kern-color-action-on-default: oklch(var(--kern-white-lightness) var(--kern-white-chroma) var(--kern-white-hue));
    /* ... weitere Dark Mode Farben ... */
  }
}
```

## 3. Manuelle Theme-Umschaltung (Override)

Um Benutzern das manuelle Umschalten des Themes zu ermöglichen (z.B. über einen Button im Einstellungsmenü), verwenden Sie Attribute-Selektoren und Klassen.

### A. Umschaltung per `data-kern-theme` Attribut

Definieren Sie die Theme-Variablen für die Attribute `[data-kern-theme="light"]` und `[data-kern-theme="dark"]`.

**Anwendung:** Setzen Sie das Attribut auf das `<html>`-Element oder den gewünschten Container:
```html
<body data-kern-theme="dark">
</body>
```

**CSS-Definition:**
```css
/* Definiert das Light Theme, wenn das data-Attribut gesetzt ist */
[data-kern-theme="light"],
.kern-light {
  /* Beispiel: Light Mode Farben überschreiben */
    --kern-color-action-default: oklch(var(--kern-darkblue-700-lightness) var(--kern-darkblue-700-chroma) var(--kern-darkblue-700-hue));
  /* ... */
}

/* Definiert das Dark Theme, wenn das data-Attribut gesetzt ist */
[data-kern-theme="dark"],
.kern-dark {
  /* Beispiel: Dark Mode Farben überschreiben */
    --kern-color-action-default: oklch(var(--kern-darkblue-300-lightness) var(--kern-darkblue-300-chroma) var(--kern-darkblue-300-hue));
  /* ... */
}
```

### B. Umschaltung per CSS-Klasse

Es können alternativ die Klassen .kern-light und .kern-dark verwenden. Diese sind mit den Attributen gruppiert (`[data-kern-theme=light], .kern-light`).

**Anwendung:**
```html
<div class="kern-dark">
</div>
```

## 4. Definieren eines eigenen Themes (Custom Theme)
Um ein eigenes, benutzerdefiniertes Theme zu erstellen (z.B. für Branding-Zwecke), folgen Sie diesen Schritten:

1. Kopieren Sie das gesamte Variablen-Set eines bestehenden Themes (z.B. Light Mode).

2. Überschreiben Sie die gewünschten semantischen Variablen mit Ihren eigenen Token-Basis-Werten.

**Beispiel:** Custom Theme (Vorlage) für das Automatische Themenerkennung und das Manuelle Theme-Umschaltung
```css
@media (prefers-color-scheme: dark) {
  :root {
    /* brand */
    --kern-color-accent: oklch(var(--kern-red-450-lightness) var(--kern-red-450-chroma) var(--kern-red-450-hue));
    /* ... */
  }
}

@media (prefers-color-scheme: light) {
  :root {
    /* brand */
    --kern-color-accent: oklch(var(--kern-red-550-lightness) var(--kern-red-550-chroma) var(--kern-red-550-hue));
    /* ... */
  }
}

[data-kern-theme=light],
.kern-light {
  /* brand */
  --kern-color-accent: oklch(var(--kern-red-550-lightness) var(--kern-red-550-chroma) var(--kern-red-550-hue));
  /* ... */
}

[data-kern-theme=dark],
.kern-dark {
  /* brand */
  --kern-color-accent: oklch(var(--kern-red-450-lightness) var(--kern-red-450-chroma) var(--kern-red-450-hue));
  /* ... */
}
```