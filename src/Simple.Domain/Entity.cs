namespace Simple.Domain;

public interface IEntity
{
    IEnumerable<IDomainEvent> DomainEvents { get; }
    void AddDomainEvent(IDomainEvent eventItem);
    void ClearDomainEvents();
}

public abstract class Entity : IEntity
{
    private readonly List<IDomainEvent> _domainEvents = [];
    
    public IEnumerable<IDomainEvent> DomainEvents => _domainEvents;
    
    public void AddDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }
    
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}