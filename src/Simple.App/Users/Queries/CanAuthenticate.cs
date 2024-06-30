using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Users.Queries;

public static class CanAuthenticate
{
    public record Query(string Email, string Password) : IRequest<Result>;

    public record Result(bool Success, Guid? UserId = null, Guid? TenantId = null);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty().MaximumLength(50);
        }
    }

    public class Handler(IReadRepository<User> users, ILogger<Handler> log) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken token)
        {
            var email = EmailAddress.Create(query.Email);
            var user = await users.SingleOrDefaultAsync(new UserByEmailSpec(email), token);
            if (user == null)
            {
                log.LogInformation("User {Email} failed to authenticate (not found)", email);
                return new Result(false);
            }

            if (!user.Password.Verify(query.Password, (supplied, hashed) => BCrypt.Net.BCrypt.Verify(supplied, hashed)))
            {
                log.LogInformation("User {Email} failed to authenticate (incorrect password)", email);
                return new Result(false);
            }

            return new Result(true, user.UserId, user.TenantId);
        }
    }
}