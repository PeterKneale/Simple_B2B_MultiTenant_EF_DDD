using Microsoft.Extensions.DependencyInjection;

namespace Simple.Infra.Database.Migrations;

public static class MigrationExtensions
{
    
    public static IServiceProvider ApplyDatabaseMigrations(this IServiceProvider app, bool reset = false)
    {
        var runner = app.GetRequiredService<MigrationRunner>();
        runner.Run(reset);
        return app;
    }
}