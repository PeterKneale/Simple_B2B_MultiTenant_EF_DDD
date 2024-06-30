using MediatR;

namespace Simple.Domain;

/// <summary>
/// https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/domain-events-design-implementation
/// </summary>
public interface IDomainEvent : INotification;