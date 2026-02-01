# Portfolio.Automation.Framework

Overview
--------
A growing .NET automation framework covering API and UI testing. The goal of this project is to demonstrate scalable automation patterns, strong test structure, and reusable infrastructure across test types. API tests live under the `API.Tests` project and focus on client-based API validation. UI tests are being introduced with a shared test base, page abstractions, and reusable helpers to support browser-based automation.

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
- Base page: [`UI.Pages.BasePage`](UI/Pages/BasePage.cs) 
  — common Selenium helpers and wait abstractions
- Text Box page: [`UI.Pages.TextBoxPage`](UI/Pages/TextBoxPage.cs)
  — page object model for the Text Box form

Core
- Test base: [`UI.Core.TestBase`](UI/Core/TestBase.cs)
  — shared WebDriver lifecycle, waits, and configuration
- Text Box test base: [`UI.Core.TextBoxTestBase`](UI/Core/TextBoxTestBase.cs)
  — shared setup and data helpers for Text Box UI tests

Tests
- Test(s): [`UI.Tests.TextBoxTests`](UI/Tests/TextBoxTests.cs)
  — positive and negative form submission scenarios

Utilities
- UI configuration helper: [`UI.Utilities.UiConfig`](UI/Utilities/UiConfig.cs)
- Environment data: [`UI/Utilities/Environment.json`](UI/Utilities/Environment.json)
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
   dotnet test UI.Tests/UI.Tests.csproj   

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