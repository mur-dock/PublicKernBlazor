namespace PublicKernBlazor.Components.Services;

/// <summary>
/// Erzeugt eindeutige IDs für Barrierefreiheits- und Formularverknüpfungen.
/// </summary>
public sealed class IdGeneratorService
{
    /// <summary>
    /// Erstellt eine neue, innerhalb der Seite eindeutige ID.
    /// </summary>
    /// <param name="prefix">Optionaler lesbarer Präfix (z.B. <c>"kern-input"</c>). Leer ergibt <c>"kern"</c>.</param>
    /// <returns>Eine eindeutige ID der Form <c>{prefix}-{guid}</c>.</returns>
    public string Create(string? prefix)
    {
        var safePrefix = string.IsNullOrWhiteSpace(prefix) ? "kern" : prefix.Trim();
        return $"{safePrefix}-{Guid.NewGuid():N}";
    }
}
