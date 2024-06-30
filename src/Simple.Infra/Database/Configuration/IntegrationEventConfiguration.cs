using Simple.Infra.IntegrationEvents;
using static Simple.Infra.Database.DbConstants;

namespace Simple.Infra.Database.Configuration;

public class IntegrationEventConfiguration : IEntityTypeConfiguration<IntegrationEvent>
{
    public void Configure(EntityTypeBuilder<IntegrationEvent> builder)
    {
        builder.ToTable(IntegrationEventTable);
        
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd()
            .IsRequired()
            .HasColumnName(IdColumn);

        builder.Property(e => e.Type)
            .IsRequired()
            .HasMaxLength(256)
            .HasColumnName(TypeColumn);

        builder.Property(e => e.Data)
            .IsRequired()
            .HasColumnName(DataColumn);

        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasColumnName(CreatedAtColumn);

        builder.Property(e => e.ProcessedAt)
            .HasColumnName(ProcessedAt);
    }
}