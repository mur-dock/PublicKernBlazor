namespace KernUx.Blazor.Services;

/// <summary>
/// Erzeugt eindeutige IDs für Barrierefreiheits- und Formularverknüpfungen.
/// </summary>
public sealed class IdGeneratorService
{
    public string Create(string? prefix)
    {
        var safePrefix = string.IsNullOrWhiteSpace(prefix) ? "kern" : prefix.Trim();
        return $"{safePrefix}-{Guid.NewGuid():N}";
    }
}

