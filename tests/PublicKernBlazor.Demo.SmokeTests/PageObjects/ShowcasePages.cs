namespace PublicKernBlazor.Demo.SmokeTests.PageObjects;

/// <summary>
/// Page Object für die Startseite (<c>/</c>).
/// </summary>
public sealed class HomePage(IPage page) : BasePage(page, "/")
{
    /// <summary>Alle Feature-Cards auf der Startseite.</summary>
    public ILocator FeatureCards => Page.Locator(".kern-card");

    /// <summary>Link zur Antragsstrecke.</summary>
    public ILocator LinkAntrag => Page.Locator("a[href='/examples/antrag']");

    /// <summary>Link zum Dashboard.</summary>
    public ILocator LinkDashboard => Page.Locator("a[href='/examples/dashboard']");

    /// <summary>Navigationslink zur Buttons-Showcase-Seite (im Demo-NavMenu).</summary>
    public ILocator LinkButtons => Page.Locator("a[href='/components/buttons']");
}

/// <summary>Page Object für <c>/components/typography</c>.</summary>
public sealed class TypografieSeite(IPage page) : BasePage(page, "/components/typography");

/// <summary>Page Object für <c>/components/layout</c>.</summary>
public sealed class LayoutSeite(IPage page) : BasePage(page, "/components/layout");

/// <summary>Page Object für <c>/components/buttons</c>.</summary>
public sealed class ButtonSeite(IPage page) : BasePage(page, "/components/buttons")
{
    /// <summary>Alle sichtbaren KERN-Buttons auf der Seite.</summary>
    public ILocator AlleButtons => Page.Locator(".kern-btn");

    /// <summary>Der interaktive Klick-Zähler-Button.</summary>
    public ILocator ZaehlerButton => Page.Locator("button:has-text('Klick mich')");

    /// <summary>Ausgabe des Klick-Zählers.</summary>
    public ILocator ZaehlerAusgabe => Page.Locator("strong", new() { HasTextString = "0" }).First;
}

/// <summary>Page Object für <c>/components/icons</c>.</summary>
public sealed class IconSeite(IPage page) : BasePage(page, "/components/icons")
{
    /// <summary>Alle gerenderten Icon-Elemente in der Galerie.</summary>
    public ILocator IconGalerie => Page.Locator(".kern-icon");
}

/// <summary>Page Object für <c>/components/feedback</c>.</summary>
public sealed class FeedbackSeite(IPage page) : BasePage(page, "/components/feedback")
{
    /// <summary>Alle Alert-Komponenten.</summary>
    public ILocator Alerts => Page.Locator(".kern-alert");

    /// <summary>Alle Badge-Komponenten.</summary>
    public ILocator Badges => Page.Locator(".kern-badge");
}

/// <summary>Page Object für <c>/components/forms</c>.</summary>
public sealed class FormularSeite(IPage page) : BasePage(page, "/components/forms")
{
    /// <summary>Alle Eingabefelder.</summary>
    public ILocator Eingabefelder => Page.Locator(".kern-form-input__input");
}

/// <summary>Page Object für <c>/components/navigation</c>.</summary>
public sealed class NavigationSeite(IPage page) : BasePage(page, "/components/navigation");

/// <summary>Page Object für die 404-Seite (<c>/not-found</c>).</summary>
public sealed class NichtGefundenSeite(IPage page) : BasePage(page, "/not-found")
{
    /// <summary>Link, der zur Startseite zurücknavigiert.</summary>
    public ILocator ZurStartseiteButton => Page.Locator("a:has-text('Zur Startseite')");
}
