using System.Globalization;

namespace KernUx.Blazor.Utilities;

/// <summary>
/// Hilft beim sicheren Zusammensetzen von CSS-Klassen für KERN-Blazor-Komponenten.
/// </summary>
public sealed class CssBuilder
{
    private readonly List<string> _classes = [];

    public CssBuilder()
    {
    }

    public CssBuilder(string? value)
    {
        AddClass(value);
    }

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

    public string Build() => string.Join(" ", _classes);
}
