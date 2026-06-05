namespace CSharpPlaywrightAutomation.Support;

public sealed class TestSettings
{
    public string AppBaseUrl { get; init; } =
        Environment.GetEnvironmentVariable("APP_BASE_URL") ?? "https://mock-store.local";

    public string ApiBaseUrl { get; init; } =
        Environment.GetEnvironmentVariable("API_BASE_URL") ?? "https://jsonplaceholder.typicode.com";

    public string BrowserName { get; init; } =
        Environment.GetEnvironmentVariable("BROWSER") ?? "chromium";

    public bool Headless { get; init; } =
        !string.Equals(Environment.GetEnvironmentVariable("HEADLESS"), "false", StringComparison.OrdinalIgnoreCase);

    public int DefaultTimeoutMs { get; init; } =
        int.TryParse(Environment.GetEnvironmentVariable("DEFAULT_TIMEOUT_MS"), out var timeout) ? timeout : 15000;

    public int ViewportWidth { get; init; } =
        int.TryParse(Environment.GetEnvironmentVariable("VIEWPORT_WIDTH"), out var width) ? width : 1440;

    public int ViewportHeight { get; init; } =
        int.TryParse(Environment.GetEnvironmentVariable("VIEWPORT_HEIGHT"), out var height) ? height : 900;
}

