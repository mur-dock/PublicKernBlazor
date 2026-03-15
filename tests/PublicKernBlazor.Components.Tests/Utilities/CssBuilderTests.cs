using PublicKernBlazor.Components.Utilities;

namespace PublicKernBlazor.Components.Tests.Utilities;

public sealed class CssBuilderTests
{
    [Fact(DisplayName = "Doppelte Klassen werden nicht mehrfach eingefügt")]
    public void Build_VermeidetDoppelteKlassen()
    {
        // Given – Builder mit einer Basisklasse initialisieren
        var builder = new CssBuilder("kern-icon")
            .AddClass("kern-icon--info")
            // dieselbe Klasse ein zweites Mal – darf kein Duplikat erzeugen
            .AddClass("kern-icon kern-icon--info kern-icon--large");

        // When
        var result = builder.Build();

        // Then – jede Klasse genau einmal, Reihenfolge wie zuerst hinzugefügt
        Assert.Equal("kern-icon kern-icon--info kern-icon--large", result);
    }

    [Fact(DisplayName = "Klasse aus HTML-Attribut-Dictionary wird übernommen")]
    public void Build_ÜbernimmtKlasseAusAdditionalAttributes()
    {
        // Given – Attribut-Dictionary simuliert @attributes="..." in Razor
        var attributes = new Dictionary<string, object>
        {
            ["class"] = "custom-a custom-b"
        };
        var builder = new CssBuilder("kern-btn")
            .AddClassFromAttributes(attributes);

        // When
        var result = builder.Build();

        // Then – eigene Klasse und zusätzliche Klassen gemeinsam vorhanden
        Assert.Equal("kern-btn custom-a custom-b", result);
    }
}
