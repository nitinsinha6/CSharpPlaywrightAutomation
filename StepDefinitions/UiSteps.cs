using CSharpPlaywrightAutomation.Pages;
using CSharpPlaywrightAutomation.Support;
using Reqnroll;

namespace CSharpPlaywrightAutomation.StepDefinitions;

[Binding]
public sealed class UiSteps
{
    private readonly TestContext _context;

    public UiSteps(TestContext context)
    {
        _context = context;
    }

    [Given("the shopper is on the store home page")]
    public async Task GivenTheShopperIsOnTheStoreHomePage()
    {
        await HomePage.LoadAsync();
    }

    [Given("the shopper opens the login page")]
    public async Task GivenTheShopperOpensTheLoginPage()
    {
        await LoginPage.LoadAsync();
    }

    [Given("the shopper logs in with email {string} and password {string}")]
    public async Task GivenTheShopperLogsIn(string email, string password)
    {
        await LoginPage.LoadAsync();
        await LoginPage.SignInAsync(email, password);
        await HomePage.ExpectShoppingCategoriesAsync();
    }

    [Given("the shopper has opened the first result for {string}")]
    public async Task GivenTheShopperHasOpenedTheFirstResultFor(string searchTerm)
    {
        await OpenFirstSearchResultAsync(searchTerm);
    }

    [Given("the shopper has added {string} to the cart")]
    public async Task GivenTheShopperHasAddedProductToTheCart(string searchTerm)
    {
        await OpenFirstSearchResultAsync(searchTerm);
        await ProductPage.AddToCartAsync();
        await CartPage.ExpectProductInCartAsync(searchTerm);
    }

    [Given("the shopper is checking out {string}")]
    public async Task GivenTheShopperIsCheckingOut(string searchTerm)
    {
        await GivenTheShopperHasAddedProductToTheCart(searchTerm);
        await CartPage.ProceedToCheckoutAsync();
        await CheckoutPage.ExpectLoadedAsync();
    }

    [Given("the shopper is on the payment page for {string}")]
    public async Task GivenTheShopperIsOnThePaymentPageFor(string searchTerm)
    {
        await GivenTheShopperIsCheckingOut(searchTerm);
        await SubmitDefaultShippingAddressAsync();
        await PaymentPage.ExpectLoadedAsync();
    }

    [Given("the shopper is on the thank you page")]
    public async Task GivenTheShopperIsOnTheThankYouPage()
    {
        await Page.GotoAsync("/thank-you");
    }

    [When("the shopper signs in with email {string} and password {string}")]
    public async Task WhenTheShopperSignsIn(string email, string password)
    {
        await LoginPage.SignInAsync(email, password);
    }

    [When("the shopper searches for {string}")]
    public async Task WhenTheShopperSearchesFor(string searchTerm)
    {
        _context.SearchTerm = searchTerm;
        await HomePage.SearchForAsync(searchTerm);
    }

    [When("the shopper searches without entering a product")]
    public async Task WhenTheShopperSearchesWithoutEnteringAProduct()
    {
        _context.SearchTerm = string.Empty;
        await HomePage.SearchWithoutProductAsync();
    }

    [When("the shopper opens the first search result")]
    public async Task WhenTheShopperOpensTheFirstSearchResult()
    {
        await SearchResultsPage.OpenFirstResultAsync();
    }

    [When("the shopper opens sign in from the header")]
    public async Task WhenTheShopperOpensSignInFromTheHeader()
    {
        await HomePage.OpenSignInAsync();
    }

    [When("the shopper opens the cart from the header")]
    public async Task WhenTheShopperOpensTheCartFromTheHeader()
    {
        await HomePage.OpenCartAsync();
    }

    [When("the shopper adds the product to the cart")]
    public async Task WhenTheShopperAddsTheProductToTheCart()
    {
        await ProductPage.AddToCartAsync();
    }

    [When("the shopper proceeds to checkout from the cart")]
    public async Task WhenTheShopperProceedsToCheckoutFromTheCart()
    {
        await CartPage.ProceedToCheckoutAsync();
    }

    [When("the shopper buys the product now")]
    public async Task WhenTheShopperBuysTheProductNow()
    {
        await ProductPage.BuyNowAsync();
    }

    [When("the shopper submits the shipping address")]
    public async Task WhenTheShopperSubmitsTheShippingAddress()
    {
        await SubmitDefaultShippingAddressAsync();
    }

    [When("the shopper pays with test card details")]
    public async Task WhenTheShopperPaysWithTestCardDetails()
    {
        await PaymentPage.PayByCardAsync("4111111111111111", "Test Shopper", "12/30", "123");
    }

    [When("the shopper continues shopping")]
    public async Task WhenTheShopperContinuesShopping()
    {
        await ThankYouPage.ContinueShoppingAsync();
    }

    [Then("the login page should be displayed")]
    public async Task ThenTheLoginPageShouldBeDisplayed()
    {
        await LoginPage.ExpectLoadedAsync();
    }

    [Then("the login page should display email, password, and sign in controls")]
    public async Task ThenTheLoginPageShouldDisplayRequiredControls()
    {
        await LoginPage.ExpectLoadedAsync();
    }

    [Then("the store header should display search, sign in, and cart links")]
    public async Task ThenTheStoreHeaderShouldDisplayNavigation()
    {
        await HomePage.ExpectHeaderNavigationAsync();
    }

    [Then("the home page should display shopping categories")]
    public async Task ThenTheHomePageShouldDisplayShoppingCategories()
    {
        await HomePage.ExpectShoppingCategoriesAsync();
    }

    [Then("product search results for {string} should be displayed")]
    public async Task ThenProductSearchResultsShouldBeDisplayed(string searchTerm)
    {
        await SearchResultsPage.ExpectResultsForAsync(searchTerm);
    }

    [Then("the empty search message should be displayed")]
    public async Task ThenTheEmptySearchMessageShouldBeDisplayed()
    {
        await SearchResultsPage.ExpectEmptySearchMessageAsync();
    }

    [Then("the product detail page should be displayed")]
    public async Task ThenTheProductDetailPageShouldBeDisplayed()
    {
        await ProductPage.ExpectLoadedAsync();
    }

    [Then("the product price and availability should be displayed")]
    public async Task ThenTheProductPriceAndAvailabilityShouldBeDisplayed()
    {
        await ProductPage.ExpectPriceAndAvailabilityAsync();
    }

    [Then("the cart should be empty")]
    public async Task ThenTheCartShouldBeEmpty()
    {
        await CartPage.ExpectEmptyAsync();
    }

    [Then("the cart should contain {string}")]
    public async Task ThenTheCartShouldContain(string product)
    {
        await CartPage.ExpectProductInCartAsync(product);
    }

    [Then("the cart subtotal should be {string}")]
    public async Task ThenTheCartSubtotalShouldBe(string subtotal)
    {
        await CartPage.ExpectSubtotalAsync(subtotal);
    }

    [Then("the checkout page should be displayed")]
    public async Task ThenTheCheckoutPageShouldBeDisplayed()
    {
        await CheckoutPage.ExpectLoadedAsync();
    }

    [Then("the payment page should be displayed")]
    public async Task ThenThePaymentPageShouldBeDisplayed()
    {
        await PaymentPage.ExpectLoadedAsync();
    }

    [Then("the thank you page should confirm the order")]
    public async Task ThenTheThankYouPageShouldConfirmTheOrder()
    {
        await ThankYouPage.ExpectOrderConfirmationAsync();
    }

    [Then("the confirmation number should be displayed")]
    public async Task ThenTheConfirmationNumberShouldBeDisplayed()
    {
        await ThankYouPage.ExpectConfirmationNumberAsync();
    }

    [Then("the confirmation email message should be displayed")]
    public async Task ThenTheConfirmationEmailMessageShouldBeDisplayed()
    {
        await ThankYouPage.ExpectConfirmationEmailMessageAsync();
    }

    private Microsoft.Playwright.IPage Page =>
        _context.Page ?? throw new InvalidOperationException("Playwright page is not initialized.");

    private HomePage HomePage => new(Page);
    private LoginPage LoginPage => new(Page);
    private SearchResultsPage SearchResultsPage => new(Page);
    private ProductPage ProductPage => new(Page);
    private CartPage CartPage => new(Page);
    private CheckoutPage CheckoutPage => new(Page);
    private PaymentPage PaymentPage => new(Page);
    private ThankYouPage ThankYouPage => new(Page);

    private async Task OpenFirstSearchResultAsync(string searchTerm)
    {
        await HomePage.LoadAsync();
        _context.SearchTerm = searchTerm;
        await HomePage.SearchForAsync(searchTerm);
        await SearchResultsPage.OpenFirstResultAsync();
        await ProductPage.ExpectLoadedAsync();
    }

    private async Task SubmitDefaultShippingAddressAsync()
    {
        await CheckoutPage.SubmitShippingAddressAsync(
            fullName: "Test Shopper",
            addressLine1: "123 Automation Way",
            city: "Seattle",
            zipCode: "98101");
    }
}

