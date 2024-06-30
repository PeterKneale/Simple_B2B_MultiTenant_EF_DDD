using Simple.Domain.Tenants;
using static Simple.Infra.Database.DbConstants;

namespace Simple.Infra.Database.Configuration;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.ToTable(TenantsTable);

        builder.Property(e => e.TenantId)
            .ValueGeneratedNever()
            .HasColumnName(TenantIdColumn)
            .HasConversion(new TenantIdConverter());
        
        builder
            .Property(e => e.CreatedAt)
            .HasColumnName(CreatedAtColumn);
        
        builder.Property(e => e.TenantName)
            .HasMaxLength(NameMaxLength)
            .HasColumnName(NameColumn)
            .HasConversion(new TenantNameConverter());

        builder.Ignore(x => x.DomainEvents);
    }
}