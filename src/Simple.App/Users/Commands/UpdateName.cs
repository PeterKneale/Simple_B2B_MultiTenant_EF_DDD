using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Users.Commands;

public static class UpdateName
{
    public record Command(Guid UserId, string FirstName, string LastName) : IRequest;

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
            RuleFor(m => m.FirstName).NotEmpty();
            RuleFor(m => m.LastName).NotEmpty();
        }
    }

    public class Handler(IRepository<User> users) : IRequestHandler<Command>
    {
        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var userId = new UserId(command.UserId);
            var user = await users.SingleOrDefaultAsync(new UserByIdSpec(userId), cancellationToken);
            if (user == null)
            {
                PlatformException.ThrowNotFound(userId);
            }
            
            var name = Name.Create(command.FirstName, command.LastName);
            user.ChangeName(name);
        }
    }
}