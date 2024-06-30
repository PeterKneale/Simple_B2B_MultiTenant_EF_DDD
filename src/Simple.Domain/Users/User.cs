using Simple.Domain.Tenants;

namespace Simple.Domain.Users;

public class User : IAggregateRoot
{
    private User()
    {
        // Required by EF
    }
    
    public User(TenantId tenantId, UserId userId, Name name, EmailAddress email, Password password)
    {
        TenantId = tenantId;
        UserId = userId;
        Name = name;
        Email = email;
        Password = password;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void ChangeName(Name name)
    {
        Name = name;
    }
    
    public UserId UserId { get; init; }

    public TenantId TenantId { get; init; }

    public Name Name { get; private set; } = null!;

    public EmailAddress Email { get; init; } = null!;

    public Password Password { get; init; } = null!;

    public DateTime CreatedAt { get; init; }
}