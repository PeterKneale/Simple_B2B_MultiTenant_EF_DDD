namespace Simple.App.Contracts;

public interface IDomainEventHandler<in T> where T: IDomainEvent
{
    Task Handle(T domainEvent);
}