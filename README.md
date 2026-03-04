# Portfolio.Automation.Framework

Overview
--------
A .NET automation framework covering API and UI testing. The goal of this project is to demonstrate scalable automation patterns, strong test structure, and reusable infrastructure across test types. API tests live under the `API.Tests` project and focus on client-based API validation. UI tests live under the `UI` project and use a decorator-based page object model with structured logging, reusable assertions, and menu-driven navigation.

The framework emphasizes:
- Clear test base inheritance
- Separation of concerns (clients, models, pages, helpers)
- Reusability across test types
- Readable, intention-revealing tests

Project files
-------------
- [`/.gitignore`](.gitignore)
- [`Portfolio.Automation.Framework.sln`](Portfolio.Automation.Framework.sln)
- [`README.md`](README.md)

API.Tests
--------------
- Project file: [`API.Tests/API.Tests.csproj`](API.Tests/API.Tests.csproj)
- Global usings: [`API.Tests.Usings.cs`](API.Tests/Usings.cs)

Clients
API clients responsible for executing HTTP requests and returning strongly typed responses. Each client maps to a single API resource and encapsulates endpoint-specific behavior. Tests interact only with clients (never raw HTTP) to keep test intent clear and decoupled from transport details.

- Albums client:    [`API.Tests.Clients.AlbumsClient`](API.Tests/Clients/AlbumsClient.cs)
- Comments client:  [`API.Tests.Clients.CommentsClient`](API.Tests/Clients/CommentsClient.cs)
- Posts client:     [`API.Tests.Clients.PostsClient`](API.Tests/Clients/PostsClient.cs)

Core
- Base test setup: [`API.Tests.Core.TestBase`](API.Tests/Core/TestBase.cs)
  — Provides shared test setup, configuration access, logging, and HTTP client initialization.
- Albums test base: [`API.Tests.Core.AlbumsTestBase`](API.Tests/Core/AlbumsTestBase.cs)
  — Common setup and AutoFixture helpers for album-related API tests.
- Comments test base: [`API.Tests.Core.CommentsTestBase`](API.Tests/Core/CommentsTestBase.cs)
  — Common setup and AutoFixture helpers for comment-related API tests.
- Posts test base: [`API.Tests.Core.PostsTestBase`](API.Tests/Core/PostsTestBase.cs)
  — Common setup and AutoFixture helpers for post-related API tests.

Logging
- HTTP logging handler: [`API.Tests.Logging.LoggingHttpHandler`](API.Tests/Logging/LoggingHttpHandler.cs)
  — Logs outgoing requests and incoming responses for debugging and visibility.

Models
- Album model: [`API.Tests.Models.Album`](API.Tests/Models/Album.cs)
  — Request/response model for album-related API endpoints.
- Comment model: [`API.Tests.Models.Comment`](API.Tests/Models/Comment.cs)
  — Request/response model for comment-related API endpoints.
- Post model: [`API.Tests.Models.Post`](API.Tests/Models/Post.cs)
  — Request/response model for post-related API endpoints.

Tests
- Album tests: [`API.Tests.Tests.AlbumsTests`](API.Tests/Tests/AlbumsTests.cs)
  — Coverage for album-related API endpoints.
- Comment tests: [`API.Tests.Tests.CommentsTests`](API.Tests/Tests/CommentsTests.cs)
  — Coverage for comment-related API endpoints.
- Post tests: [`API.Tests.Tests.PostsTests`](API.Tests/Tests/PostsTests.cs)
  — Coverage for post-related API endpoints.

Utilities
- Configuration helper: [`API.Tests.Utilities.Config`](API.Tests/Utilities/Config.cs)
- Environment data: [`API.Tests/Utilities/Environment.json`](API.Tests/Utilities/Environment.json)
- Test categories: [`API.Tests.Utilities.Categories`](API.Tests/Utilities/Categories.cs)

Build/artifacts
- Build output: [`API.Tests/bin`](API.Tests/bin/)  
- MSBuild outputs and assets: [`obj folder`](API.Tests/obj/)

UI.Tests
--------------
- Project file: [`UI/UI.csproj`](UI/UI.csproj)
- Global usings: [`UI/Usings.cs`](UI/Usings.cs)

Pages
Page objects and the interfaces and abstractions that support them. Concrete page classes extend `DecoratedBasePage` to inherit automatic logging without any manual decorator setup.

- Page actions interface: [`UI.Pages.IPageActions`](UI/Pages/IPageActions.cs)
  — contract defining all common page interactions; enables the decorator pattern
- Base page actions: [`UI.Pages.BasePageActions`](UI/Pages/BasePageActions.cs)
  — core Selenium implementation of `IPageActions`; handles waits, clicks, text entry, and scrolling
- Decorated base page: [`UI.Pages.DecoratedBasePage`](UI/Pages/DecoratedBasePage.cs)
  — abstract base class that composes `BasePageActions` and `LoggingPageActionsDecorator` automatically; extend this for all page objects
- Base page: [`UI.Pages.BasePage`](UI/Pages/BasePage.cs)
  — legacy base page retained for reference; superseded by `DecoratedBasePage`
- Navigation helper: [`UI.Pages.NavigationHelper`](UI/Pages/NavigationHelper.cs)
  — utility for navigating the site via menu clicks rather than direct URLs
- Text Box page: [`UI.Pages.TextBoxPage`](UI/Pages/TextBoxPage.cs)
  — page object model for the Text Box form; extends `DecoratedBasePage`

Decorators
- Logging decorator: [`UI.Decorators.LoggingPageActionsDecorator`](UI/Decorators/LoggingPageActionsDecorator.cs)
  — wraps any `IPageActions` instance to add method entry/exit logging, execution timing, exception details, and sensitive data masking

Core
- Test base: [`UI.Core.TestBase`](UI/Core/TestBase.cs)
  — shared WebDriver lifecycle, logger, config initialization, and teardown for all UI tests
- Driver factory: [`UI.Core.DriverFactory`](UI/Core/DriverFactory.cs)
  — creates and configures `IWebDriver` instances based on `UiConfig` settings
- UI config: [`UI.Core.UiConfig`](UI/Core/UiConfig.cs)
  — loads browser configuration from `appsettings.json` (base URL, browser, headless mode, timeout)

Logging
- Logger config: [`UI.Logging.LoggerConfig`](UI/Logging/LoggerConfig.cs)
  — centralized Serilog setup; enriches log entries with the current test name

Tests
- Text Box tests: [`UI.Tests.TextBox.TextBoxTests`](UI/Tests/TextBox/TextBoxTests.cs)
  — positive, negative, and edge case scenarios for form submission

Utilities
- Assertions: [`UI.Utilities.Assertions.TextBoxPageAssertions`](UI/Utilities/Assertions/TextBoxPageAssertions.cs)
  — structured assertion helper for `TextBoxPage` output; uses NUnit assertion scopes with logging
- Test categories: [`UI.Utilities.Categories`](UI/Utilities/Categories.cs)

Build/artifacts
- Build output: [`UI/bin`](UI/bin/)
- MSBuild outputs and assets: [`obj folder`](UI/obj/)

Getting started
---------------
1. Restore and build:
   ```bash
   dotnet restore Portfolio.Automation.Framework.sln
   dotnet build Portfolio.Automation.Framework.sln -c Debug

2. Run API tests:
   dotnet test API.Tests/API.Tests.csproj -c Debug

3. Run UI tests:
   dotnet test UI/UI.csproj

4. Run a single test (example):
   dotnet test API.Tests/API.Tests.csproj --filter FullyQualifiedName~API.Tests.Tests.PostsTests

5. Run tests by category (using the Category trait):
   # Run only positive tests
   dotnet test --filter "Category=Positive"

   # Run only negative tests
   dotnet test --filter "Category=Negative"

   # Combine multiple traits
   dotnet test --filter "Category=Positive&Category=Get"

   Test categories are defined in Categories.cs
   and used via [Trait("Category", Categories.XYZ)].

Notes & Design Decisions
---------------

- API and UI tests are intentionally separated to avoid cross-contamination of concerns.
- API tests emphasize client reuse and data-driven assertions.
- Environment-specific configuration is centralized in `API.Tests/Utilities/Environment.json` and accessed through `API.Tests.Utilities.Config` to keep test logic free of hard-coded values.
- HTTP request/response logging is handled via a custom delegating handler (`API.Tests.Logging.LoggingHttpHandler`) to provide visibility without polluting test assertions.
- API interactions are encapsulated in domain-specific clients (e.g. `PostsClient`, `AlbumsClient`, `CommentsClient`) to enforce separation between transport concerns and test intent.
- Request and response payloads are modeled explicitly (e.g. `PostModel`, `AlbumModel`, `CommentModel`) to maintain strong typing and reduce brittle assertion logic.
- Randomized but controlled test data is generated using AutoFixture via domain test base classes (e.g. `PostsTestBase`), enabling expressive tests while avoiding duplicated setup code.
- Test base classes provide shared setup, helpers, and conventions, keeping individual tests focused on behavior rather than orchestration.
- Tests are categorized consistently and can be filtered via Visual Studio Test Explorer or the `dotnet test` CLI to support targeted execution (e.g. smoke, regression, negative paths).
- UI page interactions are decoupled from logging via the decorator pattern. `BasePageActions` handles raw Selenium operations; `LoggingPageActionsDecorator` wraps it to add logging, timing, and error detail. `DecoratedBasePage` composes this chain automatically, so concrete page objects inherit full observability by extension alone — no logging code in the pages themselves.
- UI logging is handled by Serilog via `LoggerConfig`, which enriches all entries with the current test name for easy filtering across parallel or sequential runs.
- Page-level assertions are extracted into dedicated assertion classes (e.g. `TextBoxPageAssertions`) to keep test methods focused on behavior rather than assertion mechanics.