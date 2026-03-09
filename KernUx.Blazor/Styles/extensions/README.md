# Styles Extensions

Dieses Verzeichnis enthaelt **projekt-spezifische SCSS-Ergaenzungen** fuer `KernUx.Blazor`,
die **nicht Teil des offiziellen KERN-UX Core** sind.

## Warum dieses Verzeichnis?

Dateien in `Styles/core/` stammen aus dem KERN-UX Framework und koennen bei Updates
ausgetauscht werden. Eigene Anpassungen sollten daher **nicht** in `Styles/core/` liegen,
sondern in `Styles/extensions/`.

## Struktur

```text
extensions/
  index.scss
  components/
    _input-currency.scss
```

- `index.scss`: zentraler Einstiegspunkt fuer alle Erweiterungen
- `components/_*.scss`: einzelne, thematisch getrennte Add-ons

## Einbindung

Die Extensions werden **separat** zu einer eigenen CSS-Datei kompiliert:

- SCSS-Quelle: `Styles/extensions/index.scss`
- Kompiliertes CSS: `wwwroot/css/extensions/index.css`

Die Einbindung im Browser erfolgt automatisch über die `KernStyles`-Komponente:

```razor
@* App.razor – im <head> *@
<KernStyles />
```

`KernStyles` rendert zwei `<link>`-Tags:

1. **Theme-CSS**: `css/themes/kern/index.css` (aus KERN-UX Core)
2. **Extensions-CSS**: `css/extensions/index.css` (projektspezifisch)

Falls die Extensions nicht benötigt werden:

```razor
<KernStyles IncludeExtensions="false" />
```

### Warum nicht per `@use` in `themes/kern/index.scss`?

Die Dateien unter `Styles/core/` und `Styles/themes/` sind **Kopien aus dem KERN-UX Repository**
und können bei Updates jederzeit überschrieben werden. Die Extensions dürfen daher **nicht** dort
referenziert werden. Stattdessen wird die separate CSS-Datei über `KernStyles` eingebunden –
das ist update-sicher und erfordert keine Änderung an den KERN-UX-Quelldateien.

## Konventionen für neue Erweiterungen

1. Neue Komponente als eigene Datei unter `components/` anlegen (z. B. `_my-feature.scss`).
2. Datei in `extensions/index.scss` per `@use` registrieren.
3. Bestehende KERN-Core-Dateien in `Styles/core/` nicht direkt veraendern.
4. In der SCSS-Datei kurz kommentieren, **warum** es eine Erweiterung ist.
5. KERN-Token (`var(--kern-...)`) verwenden statt harter Werte, wo moeglich.

## Hinweis

`KernInputCurrency` ist ein Beispiel fuer so eine Erweiterung:

- Komponente: `Components/Forms/KernInputCurrency.razor`
- Styles: `Styles/extensions/components/_input-currency.scss`

So bleibt die Funktionalitaet update-sicher, auch wenn KERN-UX Core-Dateien aktualisiert werden.

