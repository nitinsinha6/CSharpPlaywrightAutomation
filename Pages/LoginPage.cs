using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class LoginPage : BasePage
{
    private readonly ILocator _email;
    private readonly ILocator _password;
    private readonly ILocator _signInButton;

    public LoginPage(IPage page) : base(page)
    {
        _email = page.Locator("#ap_email");
        _password = page.Locator("#ap_password");
        _signInButton = page.Locator("#signInSubmit");
    }

    public async Task LoadAsync()
    {
        await Page.GotoAsync("/login");
        await ExpectLoadedAsync();
    }

    public async Task ExpectLoadedAsync()
    {
        await Expect(_email).ToBeVisibleAsync();
        await Expect(_password).ToBeVisibleAsync();
        await Expect(_signInButton).ToBeVisibleAsync();
    }

    public async Task SignInAsync(string email, string password)
    {
        await FillAsync(_email, email);
        await FillAsync(_password, password);
        await ClickAsync(_signInButton);
    }
}

