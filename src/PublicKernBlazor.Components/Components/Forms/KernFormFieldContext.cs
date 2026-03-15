namespace PublicKernBlazor.Components.Components.Forms;

/// <summary>
/// Transportiert berechnete IDs und ARIA-Werte vom gemeinsamen Formular-Wrapper
/// in die eigentlichen Eingabeelemente.
/// </summary>
public sealed record KernFormFieldContext(
    string InputId,
    string? HintId,
    string? ErrorId,
    string? AriaDescribedBy,
    bool HasError,
    bool Disabled,
    bool ReadOnly
);

