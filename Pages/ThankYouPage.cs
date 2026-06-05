using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class ThankYouPage : BasePage
{
    private readonly ILocator _heading;
    private readonly ILocator _body;

    public ThankYouPage(IPage page) : base(page)
    {
        _heading = page.Locator("#thank-you-heading");
        _body = page.Locator("body");
    }

    public async Task ExpectOrderConfirmationAsync()
    {
        await Expect(_heading).ToContainTextAsync("Thank you");
    }

    public async Task ExpectConfirmationNumberAsync()
    {
        await Expect(_body).ToContainTextAsync("MOCK-ORDER-1001");
    }

    public async Task ExpectConfirmationEmailMessageAsync()
    {
        await Expect(_body).ToContainTextAsync("test.shopper@example.com");
    }

    public async Task ContinueShoppingAsync()
    {
        await ClickAsync(Page.GetByRole(AriaRole.Link, new() { Name = "Continue shopping" }));
    }
}

