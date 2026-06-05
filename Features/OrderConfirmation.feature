@ui @order @regression
Feature: Order confirmation
  As a shopper
  I want confirmation after payment
  So that I know my order was placed

  Scenario: Thank you page displays confirmation details
    Given the shopper is on the thank you page
    Then the thank you page should confirm the order
    And the confirmation number should be displayed
    And the confirmation email message should be displayed

