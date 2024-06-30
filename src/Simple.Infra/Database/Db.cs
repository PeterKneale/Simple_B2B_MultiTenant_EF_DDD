using System.Diagnostics.CodeAnalysis;
using Simple.Domain.Surveys;
using Simple.Domain.Tenants;
using Simple.Domain.Users;
using Simple.Infra.Database.Configuration;

namespace Simple.Infra.Database;

[ExcludeFromCodeCoverage]
public class Db(DbContextOptions<Db> options) : DbContext(options)
{
    public virtual DbSet<Tenant> Tenants { get; init; }

    public virtual DbSet<User> Users { get; init; }

    public virtual DbSet<Survey> Surveys { get; init; }
    
    public virtual DbSet<Question> Questions { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TenantConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new SurveyConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
    }
}
