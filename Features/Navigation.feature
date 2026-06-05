@ui @navigation @regression
Feature: Store navigation
  As a shopper
  I want header navigation to work
  So that I can move through the store quickly

  Scenario: Home page displays shopping entry points
    Given the shopper is on the store home page
    Then the store header should display search, sign in, and cart links
    And the home page should display shopping categories

  Scenario: Shopper opens sign in from the header
    Given the shopper is on the store home page
    When the shopper opens sign in from the header
    Then the login page should be displayed

  Scenario: Shopper opens an empty cart from the header
    Given the shopper is on the store home page
    When the shopper opens the cart from the header
    Then the cart should be empty

  Scenario: Shopper continues shopping from thank you page
    Given the shopper is on the thank you page
    When the shopper continues shopping
    Then the home page should display shopping categories

