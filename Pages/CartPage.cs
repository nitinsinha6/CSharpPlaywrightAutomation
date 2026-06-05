using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class CartPage : BasePage
{
    private readonly ILocator _cartItemTitle;
    private readonly ILocator _proceedToCheckoutButton;
    private readonly ILocator _emptyCartHeading;
    private readonly ILocator _body;

    public CartPage(IPage page) : base(page)
    {
        _cartItemTitle = page.Locator("#cart-item-title");
        _proceedToCheckoutButton = page.Locator("#proceed-to-checkout");
        _emptyCartHeading = page.Locator("#empty-cart-heading");
        _body = page.Locator("body");
    }

    public async Task ExpectProductInCartAsync(string product)
    {
        await Expect(_cartItemTitle).ToContainTextAsync(product);
    }

    public async Task ExpectSubtotalAsync(string subtotal)
    {
        await Expect(_body).ToContainTextAsync($"Subtotal: {subtotal}");
    }

    public async Task ExpectEmptyAsync()
    {
        await Expect(_emptyCartHeading).ToBeVisibleAsync();
    }

    public async Task ProceedToCheckoutAsync()
    {
        await ClickAsync(_proceedToCheckoutButton);
    }
}

