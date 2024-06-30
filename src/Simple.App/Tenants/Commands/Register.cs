using Simple.Domain.Tenants;
using Simple.Domain.Tenants.Specifications;
using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Tenants.Commands;

public static class Register
{
    public record Command(Guid TenantId, string TenantName, Guid UserId, string FirstName, string LastName, string Email, string Password) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.TenantId).NotEmpty();
            RuleFor(m => m.TenantName).NotEmpty();
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty();
            RuleFor(m => m.LastName).NotEmpty();
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty();
        }
    }

    public class Handler(IRepository<Tenant> tenants, IRepository<User> users, ILogger<Handler> log) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            log.LogInformation("Registering tenant {Name} with owner {Email}", command.TenantName, command.Email);

            var tenantId = new TenantId(command.TenantId);
            if (await tenants.SingleOrDefaultAsync(new TenantByIdSpec(tenantId), cancellationToken) != null)
                PlatformException.ThrowAlreadyExists(tenantId);
            
            var userId = new UserId(command.UserId);
            if (await users.SingleOrDefaultAsync(new UserByIdSpec(userId), cancellationToken) != null)
                PlatformException.ThrowAlreadyExists(userId);

            var tenantName = TenantName.Create(command.TenantName);
            if (await tenants.SingleOrDefaultAsync(new TenantByNameSpec(tenantName), cancellationToken) != null)
                PlatformException.ThrowAlreadyExists(tenantName);
            
            var tenant = new Tenant(tenantId, tenantName);
            
            var name = Name.Create(command.FirstName, command.LastName);
            var email = EmailAddress.Create(command.Email);
            var password = Password.Encrypt(command.Password, BCrypt.Net.BCrypt.HashPassword);
            var user = new User(tenantId, userId, name, email, password);
            
            await tenants.AddAsync(tenant, cancellationToken);
            await users.AddAsync(user, cancellationToken);
        }
    }
}