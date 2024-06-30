using System.Collections.Immutable;
using Simple.Infra.Database;

namespace Simple.Infra.DomainEvents;

public interface IDomainEventDispatcher
{
    Task Publish();
}

internal class DomainEventDispatcher(Db db, IPublisher mediator, ILogger<DomainEventDispatcher> log) : IDomainEventDispatcher
{
    public async Task Publish()
    {
        var entities = db.ChangeTracker.Entries<Entity>()
            .Select(po => po.Entity)
            .Where(po => po.DomainEvents.Any())
            .ToArray();

        foreach (var entity in entities)
        {
            log.LogDebug($"Publishing domain events from {entity.GetType().Name}");
            foreach (var domainEvent in entity.DomainEvents)
            {
                log.LogDebug($"Publishing domain event {domainEvent.GetType().Name}");
                await mediator.Publish(domainEvent);
            }
            entity.ClearDomainEvents();
        }
    }
}