using System.Net;

namespace CSharpPlaywrightAutomation.MockStore;

public static class MockStoreServer
{
    private const string Host = "mock-store.local";

    public static bool IsMockStoreUrl(string baseUrl)
    {
        return Uri.TryCreate(baseUrl, UriKind.Absolute, out var uri) &&
               string.Equals(uri.Host, Host, StringComparison.OrdinalIgnoreCase);
    }

    public static string ResponseFor(string url)
    {
        var uri = new Uri(url);
        var query = GetQueryValue(uri, "field-keywords");
        var signedInAs = GetQueryValue(uri, "email");

        if (uri.AbsolutePath.StartsWith("/login", StringComparison.OrdinalIgnoreCase))
        {
            return LoginPage();
        }

        if (uri.AbsolutePath.StartsWith("/cart", StringComparison.OrdinalIgnoreCase))
        {
            return CartPage(query);
        }

        if (uri.AbsolutePath.StartsWith("/checkout", StringComparison.OrdinalIgnoreCase))
        {
            return CheckoutPage(query);
        }

        if (uri.AbsolutePath.StartsWith("/payment", StringComparison.OrdinalIgnoreCase))
        {
            return PaymentPage(query);
        }

        if (uri.AbsolutePath.StartsWith("/thank-you", StringComparison.OrdinalIgnoreCase))
        {
            return ThankYouPage();
        }

        if (uri.AbsolutePath.StartsWith("/product", StringComparison.OrdinalIgnoreCase))
        {
            return ProductPage(query);
        }

        if (uri.AbsolutePath.StartsWith("/s", StringComparison.OrdinalIgnoreCase))
        {
            return SearchPage(query);
        }

        return HomePage(signedInAs);
    }

    private static string Layout(string body, string title = "Mock Store")
    {
        return $$"""
        <!doctype html>
        <html lang="en">
          <head>
            <meta charset="utf-8" />
            <title>{{title}}</title>
            <style>
              * { box-sizing: border-box; }
              body { font-family: Arial, sans-serif; margin: 0; background: #eaeded; color: #111; }
              header { background: #131921; color: white; }
              .topbar { display: flex; gap: 16px; align-items: center; padding: 12px 24px; }
              .brand { color: white; font-weight: 700; font-size: 24px; text-decoration: none; }
              .search-form { display: flex; flex: 1; max-width: 760px; }
              .search-form input { flex: 1; padding: 11px; font-size: 16px; border: 0; }
              .search-form button { padding: 10px 20px; border: 0; background: #febd69; cursor: pointer; }
              .nav-link { color: white; text-decoration: none; font-size: 14px; }
              .subnav { background: #232f3e; padding: 8px 24px; font-size: 14px; }
              main { padding: 28px; max-width: 1240px; margin: 0 auto; }
              .hero { background: linear-gradient(180deg, #ffd36f, #eaeded); padding: 42px; margin: -28px -28px 24px; }
              .panel, [data-component-type="s-search-result"], .product, .checkout-panel { background: white; padding: 20px; margin-bottom: 16px; border: 1px solid #d5d9d9; }
              .grid { display: grid; grid-template-columns: 2fr 1fr; gap: 20px; align-items: start; }
              .price { color: #b12704; font-size: 22px; margin: 12px 0; }
              .primary { display: inline-block; padding: 11px 18px; border: 0; border-radius: 20px; background: #ffd814; color: #111; text-decoration: none; cursor: pointer; }
              .secondary { display: inline-block; padding: 10px 16px; border: 1px solid #888; border-radius: 8px; background: #fff; color: #111; text-decoration: none; }
              label { display: block; margin: 12px 0 5px; font-weight: 700; }
              input, select { width: 100%; max-width: 460px; padding: 10px; border: 1px solid #888; border-radius: 4px; }
              a { color: #007185; text-decoration: none; }
              .success { color: #067d62; }
            </style>
          </head>
          <body>
            <header>
              <div class="topbar">
                <a class="brand" href="/">MockShop</a>
                <form class="search-form" action="/s" method="get">
                  <input id="twotabsearchtextbox" name="field-keywords" aria-label="Search MockShop" />
                  <button id="nav-search-submit-button" type="submit">Search</button>
                </form>
                <a id="nav-signin" class="nav-link" href="/login">Hello, sign in</a>
                <a id="nav-cart" class="nav-link" href="/cart">Cart</a>
              </div>
              <div class="subnav">Today's Deals | Customer Service | Registry | Gift Cards | Sell</div>
            </header>
            <main>{{body}}</main>
          </body>
        </html>
        """;
    }

    private static string HomePage(string signedInAs = "")
    {
        var welcome = string.IsNullOrWhiteSpace(signedInAs)
            ? string.Empty
            : $"""<p id="welcome-message">Hello, {Html(signedInAs)}</p>""";

        return Layout($$"""
          <section class="hero">
            <h1>MockShop</h1>
            <p>Online shopping for automation practice with realistic retail flows.</p>
            {{welcome}}
          </section>
          <section class="panel">
            <h2>Shop top categories</h2>
            <p>Search for laptop, wireless headphones, books, or backpacks.</p>
          </section>
        """);
    }

    private static string LoginPage()
    {
        return Layout("""
          <section class="checkout-panel">
            <h1>Sign in</h1>
            <form id="login-form" action="/" method="get">
              <label for="ap_email">Email or mobile phone number</label>
              <input id="ap_email" name="email" type="text" value="test.shopper@example.com" />
              <label for="ap_password">Password</label>
              <input id="ap_password" name="password" type="password" value="Password123" />
              <button id="signInSubmit" class="primary" type="submit">Sign in</button>
            </form>
          </section>
        """, "Sign in");
    }

    private static string SearchPage(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Layout("""
              <section class="panel">
                <h1 id="search-empty-heading">Enter a search term</h1>
                <p>Please type a product name in the search box to see matching items.</p>
              </section>
            """, "Search");
        }

        var safeQuery = Html(query);
        var encodedQuery = Url(query);
        return Layout($$"""
          <h1>Search results for {{safeQuery}}</h1>
          <div data-component-type="s-search-result">
            <h2><a href="/product/1?field-keywords={{encodedQuery}}">{{Title(query)}} Pro 15</a></h2>
            <p class="price">$899.99</p>
            <p>Reliable test product for browser automation.</p>
          </div>
          <div data-component-type="s-search-result">
            <h2><a href="/product/2?field-keywords={{encodedQuery}}">Budget {{Title(query)}}</a></h2>
            <p class="price">$249.99</p>
            <p>Second result for list assertions.</p>
          </div>
        """, $"{safeQuery} results");
    }

    private static string ProductPage(string query)
    {
        var safeQuery = Html(query);
        var encodedQuery = Url(query);
        return Layout($$"""
          <div class="grid">
            <section class="product">
              <h1 id="productTitle">{{Title(safeQuery)}} Pro 15</h1>
              <p>Mock product detail page for stable cross-browser tests.</p>
              <p class="price">$899.99</p>
              <p>In Stock</p>
              <p>FREE delivery tomorrow with MockShop Prime.</p>
            </section>
            <aside class="panel">
              <p class="price">$899.99</p>
              <p>Ships from MockShop</p>
              <a id="add-to-cart-button" class="primary" href="/cart?field-keywords={{encodedQuery}}">Add to Cart</a>
              <br /><br />
              <a id="buy-now-button" class="secondary" href="/checkout?field-keywords={{encodedQuery}}">Buy Now</a>
            </aside>
          </div>
        """, $"{safeQuery} product");
    }

    private static string CartPage(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return Layout("""
              <section class="panel">
                <h1 id="empty-cart-heading">Your MockShop Cart is empty</h1>
                <p>Shop today's deals or search for products to add items to your cart.</p>
                <a class="primary" href="/">Continue shopping</a>
              </section>
            """, "Shopping Cart");
        }

        var safeQuery = Html(query);
        var encodedQuery = Url(query);
        return Layout($$"""
          <div class="grid">
            <section class="panel">
              <h1>Shopping Cart</h1>
              <h2 id="cart-item-title">{{Title(safeQuery)}} Pro 15</h2>
              <p class="price">$899.99</p>
              <p>Quantity: 1</p>
            </section>
            <aside class="panel">
              <h2>Subtotal: $899.99</h2>
              <a id="proceed-to-checkout" class="primary" href="/checkout?field-keywords={{encodedQuery}}">Proceed to checkout</a>
            </aside>
          </div>
        """, "Shopping Cart");
    }

    private static string CheckoutPage(string query)
    {
        var safeQuery = Html(query);
        return Layout($$"""
          <section class="checkout-panel">
            <h1>Checkout</h1>
            <h2>Shipping address</h2>
            <form id="checkout-form" action="/payment" method="get">
              <input type="hidden" name="field-keywords" value="{{safeQuery}}" />
              <label for="address-name">Full name</label>
              <input id="address-name" name="name" value="Test Shopper" />
              <label for="address-line1">Address line 1</label>
              <input id="address-line1" name="address" value="123 Automation Way" />
              <label for="address-city">City</label>
              <input id="address-city" name="city" value="Seattle" />
              <label for="address-state">State</label>
              <select id="address-state" name="state"><option>WA</option><option>CA</option></select>
              <label for="address-zip">ZIP code</label>
              <input id="address-zip" name="zip" value="98101" />
              <p>Order item: {{Title(safeQuery)}} Pro 15</p>
              <button id="use-this-address" class="primary" type="submit">Use this address</button>
            </form>
          </section>
        """, "Checkout");
    }

    private static string PaymentPage(string query)
    {
        var safeQuery = Html(query);
        return Layout($$"""
          <section class="checkout-panel">
            <h1>Payment</h1>
            <form id="payment-form" action="/thank-you" method="get">
              <input type="hidden" name="field-keywords" value="{{safeQuery}}" />
              <label for="card-number">Card number</label>
              <input id="card-number" name="card" value="4111111111111111" />
              <label for="card-name">Name on card</label>
              <input id="card-name" name="cardName" value="Test Shopper" />
              <label for="expiry">Expiration date</label>
              <input id="expiry" name="expiry" value="12/30" />
              <label for="cvv">CVV</label>
              <input id="cvv" name="cvv" value="123" />
              <h2>Order total: $899.99</h2>
              <button id="place-order" class="primary" type="submit">Place your order</button>
            </form>
          </section>
        """, "Payment");
    }

    private static string ThankYouPage()
    {
        return Layout("""
          <section class="checkout-panel">
            <h1 id="thank-you-heading" class="success">Thank you, your order has been placed.</h1>
            <p>Confirmation number: MOCK-ORDER-1001</p>
            <p>An email confirmation has been sent to test.shopper@example.com.</p>
            <a class="primary" href="/">Continue shopping</a>
          </section>
        """, "Thank you");
    }

    private static string GetQueryValue(Uri uri, string key)
    {
        var query = uri.Query.TrimStart('?').Split('&', StringSplitOptions.RemoveEmptyEntries);
        foreach (var part in query)
        {
            var pieces = part.Split('=', 2);
            if (pieces.Length == 2 && string.Equals(WebUtility.UrlDecode(pieces[0]), key, StringComparison.OrdinalIgnoreCase))
            {
                return WebUtility.UrlDecode(pieces[1]);
            }
        }

        return string.Empty;
    }

    private static string Html(string value)
    {
        return WebUtility.HtmlEncode(value);
    }

    private static string Url(string value)
    {
        return WebUtility.UrlEncode(value);
    }

    private static string Title(string value)
    {
        return string.Join(" ", value.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => char.ToUpperInvariant(word[0]) + word[1..].ToLowerInvariant()));
    }
}

