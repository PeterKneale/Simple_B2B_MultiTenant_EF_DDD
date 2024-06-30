using MartinCostello.Logging.XUnit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Simple.Infra;
using Simple.Infra.Database.Migrations;

namespace Simple.IntegrationTests.Fixtures;

public class ServiceFixture : IDisposable, ITestOutputHelperAccessor
{
    private readonly ServiceProvider _provider;
    private readonly IConfigurationRoot _config;

    public ServiceFixture()
    {
        _config = new ConfigurationBuilder()
            .AddJsonFile("testsettings.json", false)
            .AddEnvironmentVariables()
            .Build();
        _provider = new ServiceCollection()
            .AddApplication()
            .AddInfra(_config)
            .AddLogging(builder => builder.AddXUnit(this, c =>
            {
                c.Filter = (category, level) => true;
            }))
            .BuildServiceProvider();
        ResetDatabase();
    }

    public void ResetDatabase()
    {
        _provider.ApplyDatabaseMigrations(true);
    }

    public IServiceProvider ServiceProvider => _provider;
    
    public IConfiguration Configuration => _config;

    public void Dispose()
    {
        _provider.Dispose();
    }

    public ITestOutputHelper? OutputHelper { get; set; }
    
}