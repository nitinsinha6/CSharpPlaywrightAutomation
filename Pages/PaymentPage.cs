using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class PaymentPage : BasePage
{
    private readonly ILocator _heading;
    private readonly ILocator _cardNumber;
    private readonly ILocator _cardName;
    private readonly ILocator _expiry;
    private readonly ILocator _cvv;
    private readonly ILocator _placeOrderButton;

    public PaymentPage(IPage page) : base(page)
    {
        _heading = page.GetByRole(AriaRole.Heading, new() { Name = "Payment" });
        _cardNumber = page.Locator("#card-number");
        _cardName = page.Locator("#card-name");
        _expiry = page.Locator("#expiry");
        _cvv = page.Locator("#cvv");
        _placeOrderButton = page.Locator("#place-order");
    }

    public async Task ExpectLoadedAsync()
    {
        await Expect(_heading).ToBeVisibleAsync();
    }

    public async Task PayByCardAsync(string cardNumber, string cardName, string expiry, string cvv)
    {
        await FillAsync(_cardNumber, cardNumber);
        await FillAsync(_cardName, cardName);
        await FillAsync(_expiry, expiry);
        await FillAsync(_cvv, cvv);
        await ClickAsync(_placeOrderButton);
    }
}

