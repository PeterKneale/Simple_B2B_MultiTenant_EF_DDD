using Simple.Domain.Tenants.DomainEvents;

namespace Simple.Domain.Tenants;

public class Tenant : Entity, IAggregateRoot
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
        AddDomainEvent(new TenantCreatedEvent(this));
    }
    
    public TenantId TenantId { get; private init; } = null!;

    public TenantName TenantName { get; private init; } = null!;

    public DateTimeOffset CreatedAt { get; private init; }
}