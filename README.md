// ...existing code...
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

Logging
- HTTP logging handler: [`SDET.API.Tests.Logging.LoggingHttpHandler`](SDET.API.Tests/Logging/LoggingHttpHandler.cs) — [SDET.API.Tests/Logging/LoggingHttpHandler.cs](SDET.API.Tests/Logging/LoggingHttpHandler.cs)

Models
- Post model: [`SDET.API.Tests.Models.PostModel`](SDET.API.Tests/Models/PostModel.cs) — [SDET.API.Tests/Models/PostModel.cs](SDET.API.Tests/Models/PostModel.cs)

Tests
- Test(s): [`SDET.API.Tests.Tests.PostsTests`](SDET.API.Tests/Tests/PostsTests.cs) — [SDET.API.Tests/Tests/PostsTests.cs](SDET.API.Tests/Tests/PostsTests.cs)

Utilities
- Configuration helper: [`SDET.API.Tests.Utilities.Config`](SDET.API.Tests/Utilities/Config.cs) — [SDET.API.Tests/Utilities/Config.cs](SDET.API.Tests/Utilities/Config.cs)
- Environment data: [SDET.API.Tests/Utilities/Environment.json](SDET.API.Tests/Utilities/Environment.json)

Build/artifacts
- Build output: [SDET.API.Tests/bin](SDET.API.Tests/bin/)
- MSBuild outputs and assets: [SDET.API.Tests/obj/project.assets.json](SDET.API.Tests/obj/project.assets.json), [SDET.API.Tests/obj/SDET.API.Tests.csproj.nuget.dgspec.json](SDET.API.Tests/obj/SDET.API.Tests.csproj.nuget.dgspec.json), [SDET.API.Tests/obj/SDET.API.Tests.csproj.nuget.g.props](SDET.API.Tests/obj/SDET.API.Tests.csproj.nuget.g.props), [SDET.API.Tests/obj/SDET.API.Tests.csproj.nuget.g.targets](SDET.API.Tests/obj/SDET.API.Tests.csproj.nuget.g.targets), [SDET.API.Tests/obj/Debug/net8.0](SDET.API.Tests/obj/Debug/net8.0/)

Getting started
---------------
1. Restore and build:
   ```bash
   dotnet restore Portfolio.Automation.Framework.sln
   dotnet build Portfolio.Automation.Framework.sln -c Debug

   Run all tests:
   dotnet test SDET.API.Tests/SDET.API.Tests.csproj -c Debug

Run a single test (example):
dotnet test SDET.API.Tests/SDET.API.Tests.csproj --filter FullyQualifiedName~SDET.API.Tests.Tests.PostsTests

Notes
Tests rely on configuration in SDET.API.Tests/Utilities/Environment.json and helpers in SDET.API.Tests.Utilities.Config.
The HTTP logging behavior is implemented in SDET.API.Tests.Logging.LoggingHttpHandler.
The API client lives at SDET.API.Tests.Clients.PostsClient and uses models like SDET.API.Tests.Models.PostModel.
