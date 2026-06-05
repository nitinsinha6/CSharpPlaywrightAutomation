using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class ProductPage : BasePage
{
    private readonly ILocator _title;
    private readonly ILocator _addToCartButton;
    private readonly ILocator _buyNowButton;
    private readonly ILocator _body;

    public ProductPage(IPage page) : base(page)
    {
        _title = page.Locator("#productTitle");
        _addToCartButton = page.Locator("#add-to-cart-button");
        _buyNowButton = page.Locator("#buy-now-button");
        _body = page.Locator("body");
    }

    public async Task ExpectLoadedAsync()
    {
        await Expect(_title).ToBeVisibleAsync();
    }

    public async Task ExpectPriceAndAvailabilityAsync()
    {
        await Expect(_body).ToContainTextAsync("$899.99");
        await Expect(_body).ToContainTextAsync("In Stock");
    }

    public async Task AddToCartAsync()
    {
        await ClickAsync(_addToCartButton);
    }

    public async Task BuyNowAsync()
    {
        await ClickAsync(_buyNowButton);
    }
}

