using Simple.Domain.Common;
using Simple.Domain.Tenants;
using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Users.Queries;

public static class ListUsers
{
    public record Query(Guid TenantId, int PageNumber, int PageSize) : IRequest<Results>;

    public record Results(IEnumerable<Result> Items);

    public record Result(Guid UserId, string UserName);

    public class Validator : AbstractValidator<Query>;

    public class Handler(IReadRepository<User> repo) : IRequestHandler<Query, Results>
    {
        public async Task<Results> Handle(Query query, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(query.TenantId);
            var page = new Page(query.PageNumber, query.PageSize);
            var results = await repo.ListAsync(new UserListPaginatedSpec(tenantId, page), cancellationToken);
            return new Results(results.Select(t => new Result(t.UserId, t.Name.FullName)));
        }
    }
}