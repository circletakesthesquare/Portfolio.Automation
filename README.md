# Portfolio.Automation.Framework

Overview
--------
A small .NET API automation framework for testing API endpoints. Tests and framework code live under the `API.Tests` project. The framework contains an API client, request/response models, logging helpers, test base classes, and configuration utilities.

Project files
-------------
- [/.gitignore](.gitignore)
- [Portfolio.Automation.Framework.sln](Portfolio.Automation.Framework.sln)
- [README.md](README.md)

API.Tests
--------------
- Project file: [API.Tests/API.Tests.csproj](API.Tests/API.Tests.csproj)
- Global usings: [API.Tests/Usings.cs](API.Tests/Usings.cs)

Clients
- API client: [`API.Tests.Clients.PostsClient`](API.Tests/Clients/PostsClient.cs) — [API.Tests/Clients/PostsClient.cs](API.Tests/Clients/PostsClient.cs)

Core
- Test base: [`API.Tests.Core.TestBase`](API.Tests/Core/TestBase.cs) — [API.Tests/Core/TestBase.cs](API.Tests/Core/TestBase.cs)
- Posts test base: [`API.Tests.Core.PostsTestBase`](API.Tests/Core/PostsTestBase.cs) — [PostsTestBase.cs](API.Tests/Core/PostsTestBase.cs)

Logging
- HTTP logging handler: [`API.Tests.Logging.LoggingHttpHandler`](API.Tests/Logging/LoggingHttpHandler.cs) — [API.Tests/Logging/LoggingHttpHandler.cs](API.Tests/Logging/LoggingHttpHandler.cs)

Models
- Post model: [`API.Tests.Models.PostModel`](API.Tests/Models/PostModel.cs) — [API.Tests/Models/PostModel.cs](API.Tests/Models/PostModel.cs)

Tests
- Test(s): [`API.Tests.Tests.PostsTests`](API.Tests/Tests/PostsTests.cs) — [API.Tests/Tests/PostsTests.cs](API.Tests/Tests/PostsTests.cs)

Utilities
- Configuration helper: [`API.Tests.Utilities.Config`](API.Tests/Utilities/Config.cs) — [API.Tests/Utilities/Config.cs](API.Tests/Utilities/Config.cs)
- Environment data: [API.Tests/Utilities/Environment.json](API.Tests/Utilities/Environment.json)
- Test categories: [`API.Tests.Utilities.Categories`](API.Tests/Utilities/Categories.cs) — [Categories.cs](API.Tests/Utilities/Categories.cs)

Build/artifacts
- Build output: [API.Tests/bin](API.Tests/bin/)  
- MSBuild outputs and assets: [obj folder](API.Tests/obj/)

Getting started
---------------
1. Restore and build:
   ```bash
   dotnet restore Portfolio.Automation.Framework.sln
   dotnet build Portfolio.Automation.Framework.sln -c Debug

2. Run all tests:
   dotnet test API.Tests/API.Tests.csproj -c Debug

3. Run a single test (example):
   dotnet test API.Tests/API.Tests.csproj --filter FullyQualifiedName~API.Tests.Tests.PostsTests

4. Run tests by category (using the Category trait):
   # Run only positive tests
   dotnet test --filter "Category=Positive"

   # Run only negative tests
   dotnet test --filter "Category=Negative"

   # Combine multiple traits
   dotnet test --filter "Category=Positive&Category=Get"

   Test categories are defined in Categories.cs
   and used via [Trait("Category", Categories.XYZ)].

Notes
---------------
- Tests rely on configuration in API.Tests/Utilities/Environment.json and helpers in API.Tests.Utilities.Config.
- The HTTP logging behavior is implemented in API.Tests.Logging.LoggingHttpHandler.
- The API client lives at API.Tests.Clients.PostsClient and uses models like API.Tests.Models.PostModel.
- Randomized test data is generated using AutoFixture via API.Tests.Tests.PostsTestBase.
- You can group/filter tests by category in Visual Studio Test Explorer or via CLI.
