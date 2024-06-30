using Simple.App.Contracts;
using Simple.Infra.Database;

namespace Simple.Infra.IntegrationEvents;

internal class IntegrationEventPublisher(Db db) : IIntegrationEventPublisher
{
    public async Task Publish(IIntegrationEvent integrationEvent, CancellationToken token)
    {
        await db.IntegrationEvent.AddAsync(IntegrationEvent.CreateFrom(integrationEvent), token);
    }
}