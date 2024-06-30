using Simple.Domain.Surveys;
using Simple.Domain.Users;

namespace Simple.Domain.Tenants;

public partial class Tenant : IAggregateRoot
{
    private Tenant()
    {
        // Required by EF
    }

    public Tenant(TenantId id, TenantName name)
    {
        TenantId = id;
        TenantName = name;
        CreatedAt = SystemTime.UtcNow();
    }
    
    public TenantId TenantId { get; private init; }

    public TenantName TenantName { get; private init; } = null!;

    public DateTimeOffset CreatedAt { get; private init; }

    public virtual ICollection<Survey> Surveys { get; set; } = new List<Survey>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}