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

Die Extensions werden im Theme-Entry eingebunden:

- `Styles/themes/kern/index.scss`

Aktuell:

```scss
@use "../../core/index.scss";
@use "../../extensions/index.scss" as kernext;
```

Der Alias (`as kernext`) verhindert Namespace-Konflikte bei Sass-Modulen.

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

