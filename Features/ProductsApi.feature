@api @smoke
Feature: Products API
  As a test automation engineer
  I want reusable API checks
  So that UI and service behavior can be validated in one framework

  Scenario: Get product by id
    When the API client requests product 1
    Then the API response should be successful
    And the product id should be 1

  @regression
  Scenario: Create product
    When the API client creates product "playwright framework item"
    Then the API response status should be 200 or 201
    And the created product title should be "playwright framework item"

