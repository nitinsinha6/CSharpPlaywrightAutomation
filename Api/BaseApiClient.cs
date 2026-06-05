using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Api;

public abstract class BaseApiClient
{
    protected BaseApiClient(IAPIRequestContext requestContext)
    {
        RequestContext = requestContext;
    }

    protected IAPIRequestContext RequestContext { get; }
}

