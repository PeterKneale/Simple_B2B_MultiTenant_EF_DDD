namespace Simple.Web.Core;

public static class Constants
{
    public const int MaximumEmailLength = 300;
    public const int MinimumPasswordLength = 15;
    public const int MaximumPasswordLength = 50;

    public const int MaxFirstNameLength = 100;
    public const int MaxLastNameLength = 100;
    
    public const string UserIdClaim = nameof(UserIdClaim);
    public const string TenantIdClaim = nameof(TenantIdClaim);
}