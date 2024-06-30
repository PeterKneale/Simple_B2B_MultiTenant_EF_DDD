using System.Reflection;
using FluentMigrator.Runner;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Infra.Behaviours;
using Simple.Infra.Database;
using Simple.Infra.Database.Repositories;
using Simple.Infra.DomainEvents;
using MigrationRunner = Simple.Infra.Database.Migrations.MigrationRunner;

namespace Simple.Infra;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetDbConnectionString();

        services.AddMediatR(config =>
        {
            var assembly = Assembly.GetExecutingAssembly();
            config.RegisterServicesFromAssembly(assembly);
            config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
            config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            config.AddOpenBehavior(typeof(TransactionalBehaviour<,>));
        });

        services.AddSingleton(configuration);
        services.AddScoped<MigrationRunner>();

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(runner => runner
                .AddPostgres()
                .WithGlobalConnectionString(connectionString)
                .ScanIn(Assembly.GetExecutingAssembly()).For.Migrations());

        services.AddDbContext<Db>(opt =>
        {
            opt.UseNpgsql(connectionString);
            opt.EnableSensitiveDataLogging(false);
            opt.ConfigureWarnings(c => c.Throw());
            opt.UseSnakeCaseNamingConvention();
        });

        // repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        
        // ddd
        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }
}