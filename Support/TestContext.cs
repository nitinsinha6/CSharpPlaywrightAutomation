using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Support;

public sealed class TestContext
{
    public TestSettings Settings { get; init; } = new();
    public IPlaywright? Playwright { get; set; }
    public IBrowser? Browser { get; set; }
    public IBrowserContext? BrowserContext { get; set; }
    public IPage? Page { get; set; }
    public IAPIRequestContext? ApiRequestContext { get; set; }
    public string SearchTerm { get; set; } = string.Empty;
}
