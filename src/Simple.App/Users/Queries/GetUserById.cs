using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Users.Queries;

public static class GetUserById
{
    public record Query(Guid UserId) : IRequest<Result>;

    public record Result(Guid UserId, string UserName, string FirstName, string LastName);

    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(m => m.UserId).NotEmpty();
        }
    }

    public class Handler(IReadRepository<User> users) : IRequestHandler<Query, Result>
    {
        public async Task<Result> Handle(Query query, CancellationToken cancellationToken)
        {
            var userId = new UserId(query.UserId);
            var user = await users.SingleOrDefaultAsync(new UserByIdSpec(userId), cancellationToken);
            if (user == null)
            {
                PlatformException.ThrowNotFound(userId);
            }

            return new Result(user.UserId, user.Name.FullName, user.Name.First, user.Name.Last);
        }
    }
}