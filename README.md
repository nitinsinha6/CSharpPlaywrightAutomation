# C# Playwright BDD Automation Framework

Scalable C#/.NET automation framework using Playwright, NUnit, and Reqnroll for Cucumber-style BDD tests.

## What It Covers

- Amazon-like mock shopping store.
- Login page.
- Product search.
- Product detail page.
- Add to cart.
- Cart validation.
- Checkout/shipping.
- Payment.
- Thank-you/order confirmation.
- API tests using Playwright `APIRequestContext`.
- GitHub Actions CI with cross-browser matrix and artifact upload.

## Prerequisites

- .NET 8 SDK
- PowerShell

## Setup

```powershell
cd CSharpPlaywrightAutomation
dotnet restore
pwsh bin/Debug/net8.0/playwright.ps1 install
```

If the Playwright install script is not generated yet, run:

```powershell
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install
```

## Run Tests

Run all tests:

```powershell
dotnet test
```

Run by tag/category:

```powershell
dotnet test --filter "TestCategory=ui"
dotnet test --filter "TestCategory=api"
dotnet test --filter "TestCategory=checkout"
dotnet test --filter "TestCategory=smoke"
```

Run headed:

```powershell
$env:HEADLESS = "false"
dotnet test --filter "TestCategory=ui"
```

Run specific browser:

```powershell
$env:BROWSER = "chromium"
dotnet test

$env:BROWSER = "firefox"
dotnet test

$env:BROWSER = "webkit"
dotnet test
```

## HTML/Test Reports

Generate TRX test output:

```powershell
dotnet test --logger "trx;LogFileName=test-results.trx" --results-directory reports
```

In GitHub Actions, TRX reports, Playwright failure screenshots, traces, and videos are uploaded as artifacts.

## Mock Store

The mock store is fulfilled inside Playwright request routing at:

```text
https://mock-store.local
```

This avoids public Amazon bot blocking while preserving realistic Amazon-style test flows.

## GitHub Actions

Workflow:

```text
.github/workflows/dotnet-playwright-ci.yml
```

The pipeline:

- Restores dependencies.
- Builds the project.
- Installs Playwright browsers.
- Runs tests across Chromium, Firefox, and WebKit.
- Uploads TRX reports and Playwright artifacts.
- Shows pass/fail status in GitHub Actions.

