using CSharpPlaywrightAutomation.MockStore;
using Microsoft.Playwright;
using Reqnroll;

namespace CSharpPlaywrightAutomation.Support;

[Binding]
public sealed class Hooks
{
    private readonly TestContext _context;
    private readonly ScenarioContext _scenarioContext;

    public Hooks(TestContext context, ScenarioContext scenarioContext)
    {
        _context = context;
        _scenarioContext = scenarioContext;
    }

    [BeforeScenario(Order = 0)]
    public async Task BeforeScenarioAsync()
    {
        _context.Playwright = await Playwright.CreateAsync();
        _context.Browser = await LaunchBrowserAsync(_context.Playwright, _context.Settings);
        _context.BrowserContext = await _context.Browser.NewContextAsync(new BrowserNewContextOptions
        {
            BaseURL = _context.Settings.AppBaseUrl,
            IgnoreHTTPSErrors = true,
            ViewportSize = new ViewportSize
            {
                Width = _context.Settings.ViewportWidth,
                Height = _context.Settings.ViewportHeight
            },
            RecordVideoDir = "reports/playwright/videos"
        });

        _context.BrowserContext.SetDefaultTimeout(_context.Settings.DefaultTimeoutMs);
        await _context.BrowserContext.Tracing.StartAsync(new TracingStartOptions
        {
            Screenshots = true,
            Snapshots = true,
            Sources = true
        });

        if (MockStoreServer.IsMockStoreUrl(_context.Settings.AppBaseUrl))
        {
            await _context.BrowserContext.RouteAsync("**/*", async route =>
            {
                await route.FulfillAsync(new RouteFulfillOptions
                {
                    Status = 200,
                    ContentType = "text/html; charset=utf-8",
                    Body = MockStoreServer.ResponseFor(route.Request.Url)
                });
            });
        }

        _context.Page = await _context.BrowserContext.NewPageAsync();
        _context.ApiRequestContext = await _context.Playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
        {
            BaseURL = _context.Settings.ApiBaseUrl,
            IgnoreHTTPSErrors = true,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                ["Accept"] = "application/json"
            }
        });
    }

    [AfterScenario(Order = 100)]
    public async Task AfterScenarioAsync()
    {
        Directory.CreateDirectory("reports/playwright");

        if (_scenarioContext.TestError is not null && _context.Page is not null)
        {
            var screenshotName = SafeName(_scenarioContext.ScenarioInfo.Title) + ".png";
            await _context.Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = Path.Combine("reports", "playwright", screenshotName),
                FullPage = true
            });
        }

        if (_context.BrowserContext is not null)
        {
            Directory.CreateDirectory(Path.Combine("reports", "playwright", "traces"));
            var traceName = SafeName(_scenarioContext.ScenarioInfo.Title) + ".zip";
            await _context.BrowserContext.Tracing.StopAsync(new TracingStopOptions
            {
                Path = Path.Combine("reports", "playwright", "traces", traceName)
            });
            await _context.BrowserContext.CloseAsync();
        }

        if (_context.ApiRequestContext is not null)
        {
            await _context.ApiRequestContext.DisposeAsync();
        }

        if (_context.Browser is not null)
        {
            await _context.Browser.CloseAsync();
        }

        _context.Playwright?.Dispose();
    }

    private static Task<IBrowser> LaunchBrowserAsync(IPlaywright playwright, TestSettings settings)
    {
        var launchOptions = new BrowserTypeLaunchOptions
        {
            Headless = settings.Headless
        };

        return settings.BrowserName.ToLowerInvariant() switch
        {
            "firefox" => playwright.Firefox.LaunchAsync(launchOptions),
            "webkit" => playwright.Webkit.LaunchAsync(launchOptions),
            _ => playwright.Chromium.LaunchAsync(launchOptions)
        };
    }

    private static string SafeName(string value)
    {
        return string.Join("_", value.Select(character => char.IsLetterOrDigit(character) ? character : '_'))
            .Trim('_')
            .ToLowerInvariant();
    }
}
