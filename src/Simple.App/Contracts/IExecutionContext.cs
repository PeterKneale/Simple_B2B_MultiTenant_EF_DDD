using Simple.Domain.Tenants;
using Simple.Domain.Users;

namespace Simple.App.Contracts;

public interface IExecutionContext
{
    TenantId CurrentTenantId { get; }
    UserId CurrentUserId { get; }
    void Set(Guid tenantId, Guid userId);
}