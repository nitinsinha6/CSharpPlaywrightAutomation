using Microsoft.Playwright;
using NUnit.Framework;

namespace CSharpPlaywrightAutomation.Pages;

public abstract class BasePage
{
    protected BasePage(IPage page)
    {
        Page = page;
    }

    protected IPage Page { get; }

    protected async Task ClickAsync(ILocator locator)
    {
        await Expect(locator).ToBeVisibleAsync();
        await locator.ClickAsync();
    }

    protected async Task FillAsync(ILocator locator, string value)
    {
        await Expect(locator).ToBeVisibleAsync();
        await locator.FillAsync(value);
    }

    protected static ILocatorAssertions Expect(ILocator locator)
    {
        return Assertions.Expect(locator);
    }

    protected static IPageAssertions Expect(IPage page)
    {
        return Assertions.Expect(page);
    }

    protected static void AssertContains(string actual, string expected)
    {
        Assert.That(actual, Does.Contain(expected).IgnoreCase);
    }
}

