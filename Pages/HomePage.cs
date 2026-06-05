using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class HomePage : BasePage
{
    private readonly ILocator _searchBox;
    private readonly ILocator _searchSubmit;
    private readonly ILocator _cartLink;
    private readonly ILocator _signInLink;
    private readonly ILocator _categoriesHeading;
    private readonly ILocator _welcomeMessage;

    public HomePage(IPage page) : base(page)
    {
        _searchBox = page.Locator("#twotabsearchtextbox");
        _searchSubmit = page.Locator("#nav-search-submit-button");
        _cartLink = page.Locator("#nav-cart");
        _signInLink = page.Locator("#nav-signin");
        _categoriesHeading = page.GetByRole(AriaRole.Heading, new() { Name = "Shop top categories" });
        _welcomeMessage = page.Locator("#welcome-message");
    }

    public async Task LoadAsync()
    {
        await Page.GotoAsync("/");
        await Expect(_searchBox).ToBeVisibleAsync();
    }

    public async Task SearchForAsync(string product)
    {
        await FillAsync(_searchBox, product);
        await ClickAsync(_searchSubmit);
    }

    public async Task SearchWithoutProductAsync()
    {
        await FillAsync(_searchBox, string.Empty);
        await ClickAsync(_searchSubmit);
    }

    public async Task OpenSignInAsync()
    {
        await ClickAsync(_signInLink);
    }

    public async Task OpenCartAsync()
    {
        await ClickAsync(_cartLink);
    }

    public async Task ExpectHeaderNavigationAsync()
    {
        await Expect(_searchBox).ToBeVisibleAsync();
        await Expect(_signInLink).ToBeVisibleAsync();
        await Expect(_cartLink).ToBeVisibleAsync();
    }

    public async Task ExpectShoppingCategoriesAsync()
    {
        await Expect(_categoriesHeading).ToBeVisibleAsync();
    }

    public async Task ExpectWelcomeMessageForAsync(string username)
    {
        await Expect(_welcomeMessage).ToContainTextAsync(username);
    }
}

