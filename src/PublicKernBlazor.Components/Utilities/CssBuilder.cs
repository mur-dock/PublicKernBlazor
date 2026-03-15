using System.Globalization;

namespace PublicKernBlazor.Components.Utilities;

/// <summary>
/// Hilft beim sicheren Zusammensetzen von CSS-Klassen für KERN-Blazor-Komponenten.
/// </summary>
public sealed class CssBuilder
{
    private readonly List<string> _classes = [];

    /// <summary>Initialisiert einen leeren <see cref="CssBuilder"/>.</summary>
    public CssBuilder()
    {
    }

    /// <summary>Initialisiert einen <see cref="CssBuilder"/> mit einer Basis-CSS-Klasse.</summary>
    /// <param name="value">Die initiale CSS-Klasse (darf <c>null</c> oder leer sein).</param>
    public CssBuilder(string? value)
    {
        AddClass(value);
    }

    /// <summary>
    /// Fügt eine oder mehrere CSS-Klassen hinzu, wenn die Bedingung erfüllt ist.
    /// Duplikate werden ignoriert.
    /// </summary>
    /// <param name="value">CSS-Klasse(n), durch Leerzeichen getrennt.</param>
    /// <param name="when">Bedingung – Standard <c>true</c>.</param>
    /// <returns>Dieselbe Instanz für Method-Chaining.</returns>
    public CssBuilder AddClass(string? value, bool when = true)
    {
        if (!when || string.IsNullOrWhiteSpace(value))
        {
            return this;
        }

        foreach (var cssClass in value.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
        {
            if (!_classes.Contains(cssClass, StringComparer.Ordinal))
            {
                _classes.Add(cssClass);
            }
        }

        return this;
    }

    /// <summary>
    /// Übernimmt den <c>class</c>-Wert aus einem <c>AdditionalAttributes</c>-Dictionary,
    /// wie es von <c>[Parameter(CaptureUnmatchedValues = true)]</c> erzeugt wird.
    /// </summary>
    /// <param name="additionalAttributes">Das Attribut-Dictionary der Komponente.</param>
    /// <returns>Dieselbe Instanz für Method-Chaining.</returns>
    public CssBuilder AddClassFromAttributes(IReadOnlyDictionary<string, object>? additionalAttributes)
    {
        if (additionalAttributes is null)
        {
            return this;
        }

        if (additionalAttributes.TryGetValue("class", out var classValue))
        {
            AddClass(Convert.ToString(classValue, CultureInfo.InvariantCulture));
        }

        return this;
    }

    /// <summary>Gibt die zusammengesetzte CSS-Klassen-Zeichenkette zurück.</summary>
    /// <returns>Leerzeichengetrennter String aller gesammelten CSS-Klassen.</returns>
    public string Build() => string.Join(" ", _classes);
}
