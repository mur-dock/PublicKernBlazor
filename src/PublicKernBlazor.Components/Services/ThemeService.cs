using PublicKernBlazor.Components.Enums;
using PublicKernBlazor.Components.Utilities;

namespace PublicKernBlazor.Components.Services;

/// <summary>
/// Verwaltet das aktive KERN-UX-Theme appweit als Blazor-Service.
/// Das Theme wird über <c>data-kern-theme</c> auf dem umschließenden Element gesetzt.
/// </summary>
public sealed class ThemeService
{
    private KernTheme _current = KernTheme.Light;

    /// <summary>
    /// Wird ausgelöst, wenn das Theme gewechselt wurde.
    /// </summary>
    public event Action<KernTheme>? ThemeChanged;

    /// <summary>
    /// Das aktuell aktive Theme.
    /// </summary>
    public KernTheme Current
    {
        get => _current;
        private set
        {
            if (_current == value)
            {
                return;
            }

            _current = value;
            ThemeChanged?.Invoke(_current);
        }
    }

    /// <summary>
    /// Der HTML-Attributwert für <c>data-kern-theme</c>.
    /// </summary>
    public string AttributeValue => Current.ToKebabCase();

    /// <summary>
    /// Wechselt zwischen Light und Dark.
    /// </summary>
    public void Toggle()
    {
        Current = Current == KernTheme.Light ? KernTheme.Dark : KernTheme.Light;
    }

    /// <summary>
    /// Setzt das Theme explizit.
    /// </summary>
    public void Set(KernTheme theme)
    {
        Current = theme;
    }

    /// <summary>
    /// Initialisiert das Theme anhand eines gespeicherten Wertes.
    /// </summary>
    public void Initialize(string? storedValue)
    {
        _current = string.Equals(storedValue, KernTheme.Dark.ToKebabCase(), StringComparison.OrdinalIgnoreCase)
            ? KernTheme.Dark
            : KernTheme.Light;
    }
}

