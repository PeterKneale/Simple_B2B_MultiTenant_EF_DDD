namespace Simple.Domain.Tenants;

public record TenantId(Guid Value)
{
    public static implicit operator Guid(TenantId id) => id.Value;
}