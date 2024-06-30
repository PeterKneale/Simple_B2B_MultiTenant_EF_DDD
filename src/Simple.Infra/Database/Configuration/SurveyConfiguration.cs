using Simple.Domain.Surveys;
using static Simple.Infra.Database.DbConstants;

namespace Simple.Infra.Database.Configuration;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.ToTable(SurveysTable);

        builder.Property(e => e.SurveyId)
            .ValueGeneratedNever()
            .HasColumnName(SurveyIdColumn)
            .HasConversion<SurveyIdConverter>();

        builder.Property(e => e.TenantId)
            .HasColumnName(TenantIdColumn)
            .HasConversion<TenantIdConverter>();

        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName(NameColumn)
            .HasConversion<SurveyNameConverter>();

        builder.Property(e => e.CreatedAt)
            .HasColumnName(CreatedAtColumn);
    }
}