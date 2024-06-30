namespace Simple.Domain.Tenants.DomainEvents;

public class TenantCreatedEvent(Tenant tenant) : IDomainEvent
{
    public Tenant Tenant { get; } = tenant;
}