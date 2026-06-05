@ui @cart @checkout @regression
Feature: Cart and checkout
  As a shopper
  I want to manage my cart and complete checkout
  So that I can place an order

  Scenario: Add searched product to cart
    Given the shopper has opened the first result for "laptop"
    When the shopper adds the product to the cart
    Then the cart should contain "laptop"
    And the cart subtotal should be "$899.99"

  Scenario: Proceed from cart to checkout
    Given the shopper has added "laptop" to the cart
    When the shopper proceeds to checkout from the cart
    Then the checkout page should be displayed

  Scenario: Buy now opens checkout directly
    Given the shopper has opened the first result for "wireless headphones"
    When the shopper buys the product now
    Then the checkout page should be displayed

  Scenario: Shipping address opens payment
    Given the shopper is checking out "laptop"
    When the shopper submits the shipping address
    Then the payment page should be displayed

  Scenario: Payment places order
    Given the shopper is on the payment page for "laptop"
    When the shopper pays with test card details
    Then the thank you page should confirm the order

  @smoke
  Scenario: Login, search product, add to cart, checkout, pay, and see thank you page
    Given the shopper logs in with email "test.shopper@example.com" and password "Password123"
    When the shopper searches for "laptop"
    And the shopper opens the first search result
    And the shopper adds the product to the cart
    And the shopper proceeds to checkout from the cart
    And the shopper submits the shipping address
    And the shopper pays with test card details
    Then the thank you page should confirm the order

