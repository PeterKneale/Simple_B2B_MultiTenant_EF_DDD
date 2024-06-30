using Simple.Domain.Tenants;
using Simple.Domain.Users;

namespace Simple.App.Contracts;

public interface IExecutionContext
{
    TenantId CurrentTenantId { get; }
    UserId CurrentUserId { get; }
    void Set(Guid tenantId, Guid userId);
}

public class ExecutionContext : IExecutionContext
{
    private TenantId? _currentTenantId;
    private UserId? _currentUserId;

    public TenantId CurrentTenantId => _currentTenantId ?? throw new InvalidOperationException("No tenant context set");

    public UserId CurrentUserId => _currentUserId ?? throw new InvalidOperationException("No user context set");

    public void Set(Guid tenantId, Guid userId)
    {
        _currentUserId = new UserId(userId);
        _currentTenantId = new TenantId(tenantId);
    }
}