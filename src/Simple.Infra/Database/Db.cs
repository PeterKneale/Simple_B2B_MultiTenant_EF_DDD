using System.Diagnostics.CodeAnalysis;
using Simple.Domain.Surveys;
using Simple.Domain.Tenants;
using Simple.Domain.Users;
using Simple.Infra.Database.Configuration;
using Simple.Infra.IntegrationEvents;

namespace Simple.Infra.Database;

[ExcludeFromCodeCoverage]
public class Db(DbContextOptions<Db> options) : DbContext(options)
{
    public virtual DbSet<Tenant> Tenants { get; init; }

    public virtual DbSet<User> Users { get; init; }

    public virtual DbSet<Survey> Surveys { get; init; }
    
    public virtual DbSet<Question> Questions { get; init; }
    
    public virtual DbSet<IntegrationEvent> IntegrationEvent { get; init; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new TenantConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new SurveyConfiguration());
        builder.ApplyConfiguration(new QuestionConfiguration());
        builder.ApplyConfiguration(new IntegrationEventConfiguration());
    }
}
