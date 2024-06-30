namespace Simple.Domain.Tenants;

public class Tenant : IAggregateRoot
{
    private Tenant()
    {
        // Required by EF
    }

    public Tenant(TenantId id, TenantName name)
    {
        TenantId = id;
        TenantName = name;
        CreatedAt = SystemTime.UtcNow();
    }
    
    public TenantId TenantId { get; private init; } = null!;

    public TenantName TenantName { get; private init; } = null!;

    public DateTimeOffset CreatedAt { get; private init; }
}