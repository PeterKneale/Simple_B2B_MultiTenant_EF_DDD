namespace Simple.App.Contracts;

public interface IIntegrationEventPublisher
{
    Task Publish(IIntegrationEvent integrationEvent, CancellationToken token);
}