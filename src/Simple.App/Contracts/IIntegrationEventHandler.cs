namespace Simple.App.Contracts;

public interface IIntegrationEventHandler
{
    Task Handle(IIntegrationEvent integrationEvent, CancellationToken token);
}