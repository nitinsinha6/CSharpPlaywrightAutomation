using Microsoft.Playwright;

namespace CSharpPlaywrightAutomation.Api;

public sealed class ProductsClient : BaseApiClient
{
    public ProductsClient(IAPIRequestContext requestContext) : base(requestContext)
    {
    }

    public Task<IAPIResponse> GetProductAsync(int productId)
    {
        return RequestContext.GetAsync($"/posts/{productId}");
    }

    public Task<IAPIResponse> CreateProductAsync(object payload)
    {
        return RequestContext.PostAsync("/posts", new APIRequestContextOptions
        {
            DataObject = payload
        });
    }
}

