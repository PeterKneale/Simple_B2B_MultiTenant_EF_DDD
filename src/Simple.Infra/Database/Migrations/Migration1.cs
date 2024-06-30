using FluentMigrator;
using static Simple.Infra.Database.DbConstants;

namespace Simple.Infra.Database.Migrations;

[Migration(1)]
public class Migration1 : Migration
{
    public override void Up()
    {
        Create.Table(TenantsTable)
            .WithColumn(TenantIdColumn).AsGuid().PrimaryKey()
            .WithColumn(NameColumn).AsString(NameMaxLength)
            .WithColumn(CreatedAtColumn).AsDateTimeOffset();

        Create.Table(UsersTable)
            .WithColumn(UserIdColumn).AsGuid().PrimaryKey()
            .WithColumn(TenantIdColumn).AsGuid().ForeignKey(TenantsTable, TenantIdColumn)
            .WithColumn(FirstNameColumn).AsString(NameMaxLength)
            .WithColumn(LastNameColumn).AsString(NameMaxLength)
            .WithColumn(EmailColumn).AsString(EmailMaxLength).Unique()
            .WithColumn(PasswordColumn).AsString(MaxPasswordLength)
            .WithColumn(CreatedAtColumn).AsDateTimeOffset();

        Create.Table(SurveysTable)
            .WithColumn(SurveyIdColumn).AsGuid().PrimaryKey()
            .WithColumn(TenantIdColumn).AsGuid().ForeignKey(TenantsTable, TenantIdColumn)
            .WithColumn(NameColumn).AsString(NameMaxLength)
            .WithColumn(CreatedAtColumn).AsDateTimeOffset();

        Create.Table(QuestionsTable)
            .WithColumn(QuestionIdColumn).AsGuid().PrimaryKey()
            .WithColumn(SurveyIdColumn).AsGuid().ForeignKey(SurveysTable, SurveyIdColumn)
            .WithColumn(InfoColumn).AsCustom("jsonb")
            .WithColumn(CreatedAtColumn).AsDateTimeOffset();
        
        Create.Table(IntegrationEventTable)
            .WithColumn(IdColumn).AsGuid().PrimaryKey()
            .WithColumn(TypeColumn).AsString()
            .WithColumn(DataColumn).AsString()
            .WithColumn(CreatedAtColumn).AsDateTimeOffset()
            .WithColumn(ProcessedAt).AsDateTimeOffset().Nullable();
    }

    public override void Down()
    {
        Delete.Table(IntegrationEventTable).IfExists();
        Delete.Table(QuestionsTable).IfExists();
        Delete.Table(SurveysTable).IfExists();
        Delete.Table(UsersTable).IfExists();
        Delete.Table(TenantsTable).IfExists();
    }
}