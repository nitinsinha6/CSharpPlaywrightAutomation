using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class CheckoutPage : BasePage
{
    private readonly ILocator _heading;
    private readonly ILocator _fullName;
    private readonly ILocator _addressLine1;
    private readonly ILocator _city;
    private readonly ILocator _zipCode;
    private readonly ILocator _useThisAddressButton;

    public CheckoutPage(IPage page) : base(page)
    {
        _heading = page.GetByRole(AriaRole.Heading, new() { Name = "Checkout" });
        _fullName = page.Locator("#address-name");
        _addressLine1 = page.Locator("#address-line1");
        _city = page.Locator("#address-city");
        _zipCode = page.Locator("#address-zip");
        _useThisAddressButton = page.Locator("#use-this-address");
    }

    public async Task ExpectLoadedAsync()
    {
        await Expect(_heading).ToBeVisibleAsync();
    }

    public async Task SubmitShippingAddressAsync(string fullName, string addressLine1, string city, string zipCode)
    {
        await FillAsync(_fullName, fullName);
        await FillAsync(_addressLine1, addressLine1);
        await FillAsync(_city, city);
        await FillAsync(_zipCode, zipCode);
        await ClickAsync(_useThisAddressButton);
    }
}

