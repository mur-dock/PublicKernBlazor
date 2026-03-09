using Bunit;
using KernUx.Blazor.Components.Content;

namespace KernUx.Blazor.Tests.Components.Content;

public sealed class KernDialogTests
{
    [Fact(DisplayName = "Dialog rendert Titel und Close-Button")]
    public void KernDialog_RendertTitelUndCloseButton()
    {
        // BunitContext: virtuelles DOM statt Browser.
        using var context = new BunitContext();

        // JS-Interop muss im Test gestubbt werden, da der Dialog intern
        // kernDialog.showModal/close() über JavaScript aufruft.
        // Die Funktionen sind in wwwroot/js/kern-dialog.js definiert.
        context.JSInterop.SetupVoid("kernDialog.showModal", _ => true);
        context.JSInterop.SetupVoid("kernDialog.close", _ => true);

        // Render<T> erstellt das Component Under Test.
        var cut = context.Render<KernDialog>(parameters => parameters
            .Add(p => p.Title, "Bestätigung")
            .Add(p => p.Open, true)
            .AddChildContent("<p>Wirklich löschen?</p>"));

        // Then
        Assert.Contains("kern-dialog", cut.Markup);
        Assert.Contains("Bestätigung", cut.Markup);
        Assert.Contains("Wirklich löschen?", cut.Markup);
    }

    [Fact(DisplayName = "Dialog löst OnClose beim Schließen-Button aus")]
    public void KernDialog_LoestOnCloseAus()
    {
        // Given
        using var context = new BunitContext();
        context.JSInterop.SetupVoid("kernDialog.showModal", _ => true);
        context.JSInterop.SetupVoid("kernDialog.close", _ => true);

        var closed = false;

        // EventCallback transportiert das Schließen-Event.
        var cut = context.Render<KernDialog>(parameters => parameters
            .Add(p => p.Title, "Dialog")
            .Add(p => p.OnClose, () => closed = true));

        // When – Click auf den Close-Button.
        cut.Find("button[aria-label='Dialog schließen']").Click();

        // Then
        Assert.True(closed);
    }
}

