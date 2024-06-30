global using FluentAssertions;
global using Simple.App;
global using Simple.App.Surveys.Commands;
global using Simple.App.Surveys.Queries;
global using Simple.App.Tenants.Commands;
global using Simple.App.Tenants.Queries;
global using Simple.App.Users.Queries;
global using Simple.IntegrationTests.Fakes;
global using Simple.IntegrationTests.Fixtures;
global using Xunit;
global using Xunit.Abstractions;
using System.Diagnostics.CodeAnalysis;

[assembly:ExcludeFromCodeCoverage]
[assembly: AssemblyTrait("Type", "Integration")]