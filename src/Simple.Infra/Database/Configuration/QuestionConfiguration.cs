using Simple.Domain.Surveys;
using static Simple.Infra.Database.DbConstants;

namespace Simple.Infra.Database.Configuration;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable(QuestionsTable);

        builder.Property(e => e.QuestionId)
            .ValueGeneratedNever()
            .HasColumnName(QuestionIdColumn)
            .HasConversion<QuestionIdConverter>();
        
        builder.Property(e => e.SurveyId)
            .HasColumnName(SurveyIdColumn)
            .HasConversion<SurveyIdConverter>();
        
        builder.Property(e => e.Type)
            .HasColumnName(InfoColumn)
            .HasColumnType("jsonb")
            .HasConversion<QuestionInfoConverter>();
        
        builder.Property(e => e.CreatedAt)
            .HasColumnName(CreatedAtColumn);
    }
}