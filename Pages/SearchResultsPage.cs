using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Pages;

public sealed class SearchResultsPage : BasePage
{
    private readonly ILocator _results;
    private readonly ILocator _body;
    private readonly ILocator _emptySearchHeading;

    public SearchResultsPage(IPage page) : base(page)
    {
        _results = page.Locator("[data-component-type='s-search-result']");
        _body = page.Locator("body");
        _emptySearchHeading = page.Locator("#search-empty-heading");
    }

    public async Task ExpectResultsForAsync(string product)
    {
        await Expect(_results.First).ToBeVisibleAsync();
        await Expect(_body).ToContainTextAsync(product);
    }

    public async Task ExpectEmptySearchMessageAsync()
    {
        await Expect(_emptySearchHeading).ToBeVisibleAsync();
    }

    public async Task OpenFirstResultAsync()
    {
        await ClickAsync(_results.First.Locator("h2 a").First);
    }
}

