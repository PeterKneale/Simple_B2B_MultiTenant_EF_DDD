namespace Simple.Infra.Database;

public static class DbConstants
{
    public const string SchemaName = "public";
    public const string TenantsTable = "tenants";
    public const string UsersTable = "users";
    public const string SurveysTable = "surveys";
    public const string QuestionsTable = "questions";

    // Common Column Names
    public const string QuestionIdColumn = "question_id";
    public const string CreatedAtColumn = "created_at";
    public const string EmailColumn = "email";
    public const string FirstNameColumn = "first_name";
    public const string TitleColumn = "title";
    public const string MandatoryColumn = "mandatory";
    public const string InfoColumn = "info";
    public const string TypeColumn = "type";
    public const string IdColumn = "id";
    public const string IsVerified = "is_verified";
    public const string KeyColumn = "key";
    public const string LastNameColumn = "last_name";
    public const string NameColumn = "name";
    public const string PasswordColumn = "password";
    public const string RegisteredAt = "registered_at";
    public const string UserIdColumn = "user_id";
    public const string TenantIdColumn = "tenant_id";
    public const string VerifiedAt = "verified_at";
    public const string VerifiedToken = "verification_token";
    public const string ForgotToken = "forgot_token";
    public const string ForgotPasswordTokenExpiry = "forgot_token_expiry";

    public const string SurveyIdColumn = "survey_id";

    // Other Constants
    public const int NameMaxLength = 100;
    public const int KeyMaxLength = 100;
    public const int EmailMaxLength = 200;
    public const int MaxPasswordLength = 100;
}