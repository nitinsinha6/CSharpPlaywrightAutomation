using System.Text.Json;
using CSharpPlaywrightAutomation.Api;
using Microsoft.Playwright;
using NUnit.Framework;
using Reqnroll;
using AutomationTestContext = CSharpPlaywrightAutomation.Support.TestContext;

namespace CSharpPlaywrightAutomation.StepDefinitions;

[Binding]
public sealed class ApiSteps
{
    private readonly AutomationTestContext _context;
    private IAPIResponse? _response;

    public ApiSteps(AutomationTestContext context)
    {
        _context = context;
    }

    [When("the API client requests product {int}")]
    public async Task WhenTheApiClientRequestsProduct(int productId)
    {
        _response = await ProductsClient.GetProductAsync(productId);
    }

    [When("the API client creates product {string}")]
    public async Task WhenTheApiClientCreatesProduct(string title)
    {
        _response = await ProductsClient.CreateProductAsync(new Dictionary<string, object>
        {
            ["title"] = title,
            ["body"] = "Created by C# Playwright API automation",
            ["userId"] = 1
        });
    }

    [Then("the API response should be successful")]
    public void ThenTheApiResponseShouldBeSuccessful()
    {
        Assert.That(Response.Ok, Is.True, $"Expected successful response, got {Response.Status}");
    }

    [Then("the API response status should be 200 or 201")]
    public void ThenTheApiResponseStatusShouldBe200Or201()
    {
        Assert.That(Response.Status, Is.AnyOf(200, 201));
    }

    [Then("the product id should be {int}")]
    public async Task ThenTheProductIdShouldBe(int expectedId)
    {
        var product = await DeserializeProductAsync();
        Assert.That(product.Id, Is.EqualTo(expectedId));
    }

    [Then("the created product title should be {string}")]
    public async Task ThenTheCreatedProductTitleShouldBe(string expectedTitle)
    {
        var product = await DeserializeProductAsync();
        Assert.That(product.Title, Is.EqualTo(expectedTitle));
    }

    private ProductsClient ProductsClient => new(
        _context.ApiRequestContext ?? throw new InvalidOperationException("API request context is not initialized."));

    private IAPIResponse Response => _response ?? throw new InvalidOperationException("API response is not set.");

    private async Task<Product> DeserializeProductAsync()
    {
        var json = await Response.TextAsync();
        return JsonSerializer.Deserialize<Product>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        }) ?? throw new InvalidOperationException("Could not deserialize product response.");
    }
}
