using Simple.App.Contracts;

namespace Simple.App.Tenants.IntegrationEvents;

public class TenantCreatedIntegrationEvent : IIntegrationEvent
{
    public Guid TenantId { get; init; }
    public string TenantName { get; init; }
    public Guid UserId { get; init; }
    public string Email { get; init; }
}