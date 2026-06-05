@ui @login @regression
Feature: Shopper authentication
  As a shopper
  I want to sign in before checkout
  So that my order can be associated with my account

  Scenario: Login page displays required fields
    Given the shopper opens the login page
    Then the login page should display email, password, and sign in controls

  @smoke
  Scenario: Shopper signs in successfully
    Given the shopper opens the login page
    When the shopper signs in with email "test.shopper@example.com" and password "Password123"
    Then the home page should display shopping categories

