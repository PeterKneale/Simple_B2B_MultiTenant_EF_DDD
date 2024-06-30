using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Users.Queries;

public static class GetUserByEmail
{
    public record Query(string Email) : IRequest<Result>;

    public record Result(Guid UserId, string UserName);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.Email).NotEmpty();
        }
    }

    public class Handler(IReadRepository<User> users) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
        {
            var email = EmailAddress.Create(query.Email);
            var user = await users.SingleOrDefaultAsync(new UserByEmailSpec(email), cancellationToken);
            if (user == null)
            {
                PlatformException.ThrowNotFound(email);
            }

            return new Result(user.UserId, user.Name.FullName);
        }
    }
}