@ui @search @smoke
Feature: Product search
  As an online shopper
  I want to search for products
  So that I can view relevant product results

  Scenario: Search for a product
    Given the shopper is on the store home page
    When the shopper searches for "laptop"
    Then product search results for "laptop" should be displayed

  @regression
  Scenario Outline: Search supported product categories
    Given the shopper is on the store home page
    When the shopper searches for "<search_term>"
    Then product search results for "<search_term>" should be displayed

    Examples:
      | search_term         |
      | laptop              |
      | wireless headphones |
      | books               |
      | backpacks           |

  @negative
  Scenario: Empty search shows helpful message
    Given the shopper is on the store home page
    When the shopper searches without entering a product
    Then the empty search message should be displayed

  @regression
  Scenario: Open product detail from search results
    Given the shopper is on the store home page
    When the shopper searches for "wireless headphones"
    And the shopper opens the first search result
    Then the product detail page should be displayed
    And the product price and availability should be displayed

