using Simple.App.Contracts;
using Simple.Domain.Tenants.DomainEvents;

namespace Simple.App.Tenants.DomainEvents;

public class TenantCreatedHandler(ILogger<TenantCreatedHandler> log) : IDomainEventHandler<TenantCreatedEvent>
{
    public Task Handle(TenantCreatedEvent domainEvent)
    {
        log.LogInformation("Tenant Created: {Tenant}", domainEvent.Tenant);
        return Task.CompletedTask;
    }
}
