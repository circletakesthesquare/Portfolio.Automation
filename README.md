# Portfolio.Automation.Framework

Overview
--------
A small .NET API automation framework for testing API endpoints. Tests and framework code live under the `SDET.API.Tests` project. The framework contains an API client, request/response models, logging helpers, test base classes, and configuration utilities.

Project files
-------------
- [/.gitignore](.gitignore)
- [Portfolio.Automation.Framework.sln](Portfolio.Automation.Framework.sln)
- [README.md](README.md)

SDET.API.Tests
--------------
- Project file: [SDET.API.Tests/SDET.API.Tests.csproj](SDET.API.Tests/SDET.API.Tests.csproj)
- Global usings: [SDET.API.Tests/Usings.cs](SDET.API.Tests/Usings.cs)

Clients
- API client: [`SDET.API.Tests.Clients.PostsClient`](SDET.API.Tests/Clients/PostsClient.cs) — [SDET.API.Tests/Clients/PostsClient.cs](SDET.API.Tests/Clients/PostsClient.cs)

Core
- Test base: [`SDET.API.Tests.Core.TestBase`](SDET.API.Tests/Core/TestBase.cs) — [SDET.API.Tests/Core/TestBase.cs](SDET.API.Tests/Core/TestBase.cs)
- Posts test base: [`SDET.API.Tests.Core.PostsTestBase`](SDET.API.Tests/Core/PostsTestBase.cs) — [PostsTestBase.cs](SDET.API.Tests/Core/PostsTestBase.cs)

Logging
- HTTP logging handler: [`SDET.API.Tests.Logging.LoggingHttpHandler`](SDET.API.Tests/Logging/LoggingHttpHandler.cs) — [SDET.API.Tests/Logging/LoggingHttpHandler.cs](SDET.API.Tests/Logging/LoggingHttpHandler.cs)

Models
- Post model: [`SDET.API.Tests.Models.PostModel`](SDET.API.Tests/Models/PostModel.cs) — [SDET.API.Tests/Models/PostModel.cs](SDET.API.Tests/Models/PostModel.cs)

Tests
- Test(s): [`SDET.API.Tests.Tests.PostsTests`](SDET.API.Tests/Tests/PostsTests.cs) — [SDET.API.Tests/Tests/PostsTests.cs](SDET.API.Tests/Tests/PostsTests.cs)

Utilities
- Configuration helper: [`SDET.API.Tests.Utilities.Config`](SDET.API.Tests/Utilities/Config.cs) — [SDET.API.Tests/Utilities/Config.cs](SDET.API.Tests/Utilities/Config.cs)
- Environment data: [SDET.API.Tests/Utilities/Environment.json](SDET.API.Tests/Utilities/Environment.json)
- Test categories: [`SDET.API.Tests.Utilities.Categories`](SDET.API.Tests/Utilities/Categories.cs) — [Categories.cs](SDET.API.Tests/Utilities/Categories.cs)

Build/artifacts
- Build output: [SDET.API.Tests/bin](SDET.API.Tests/bin/)  
- MSBuild outputs and assets: [obj folder](SDET.API.Tests/obj/)

Getting started
---------------
1. Restore and build:
   ```bash
   dotnet restore Portfolio.Automation.Framework.sln
   dotnet build Portfolio.Automation.Framework.sln -c Debug

2. Run all tests:
   dotnet test SDET.API.Tests/SDET.API.Tests.csproj -c Debug

3. Run a single test (example):
   dotnet test SDET.API.Tests/SDET.API.Tests.csproj --filter FullyQualifiedName~SDET.API.Tests.Tests.PostsTests

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
- Tests rely on configuration in SDET.API.Tests/Utilities/Environment.json and helpers in SDET.API.Tests.Utilities.Config.
- The HTTP logging behavior is implemented in SDET.API.Tests.Logging.LoggingHttpHandler.
- The API client lives at SDET.API.Tests.Clients.PostsClient and uses models like SDET.API.Tests.Models.PostModel.
- Randomized test data is generated using AutoFixture via SDET.API.Tests.Tests.PostsTestBase.
- You can group/filter tests by category in Visual Studio Test Explorer or via CLI.
