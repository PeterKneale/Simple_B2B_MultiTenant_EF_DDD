using System.Text.RegularExpressions;

namespace Simple.Domain.Tenants;

public record TenantName
{
    private static readonly Regex ValidTenantNameRegex = new("^[a-zA-Z0-9_-]+$", RegexOptions.Compiled);
    private static readonly Regex InvalidCharsRegex = new("[^a-zA-Z0-9_-]", RegexOptions.Compiled);
    public const string InvalidTenantNameMessage = "Tenant name must contain only letters, numbers, underscores, and hyphens.";

    private TenantName(string value)
    {
        Value = value;
    }

    public static TenantName Create(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        if (!ValidTenantNameRegex.IsMatch(value))
        {
            throw new ArgumentException(InvalidTenantNameMessage);
        }

        value = value.ToLowerInvariant();

        return new TenantName(value);
    }

    public static bool IsValidName(string value) =>
        ValidTenantNameRegex.IsMatch(value);

    public static TenantName GetSuggestion(string value)
    {
        var suggestion = InvalidCharsRegex.Replace(value, "-");
        return Create(suggestion);
    }

    public string Value { get; }

    public static implicit operator string(TenantName tenantName) => tenantName.Value;
}