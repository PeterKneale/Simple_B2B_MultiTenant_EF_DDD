using Simple.Domain.Common;
using Simple.Domain.Tenants;
using Simple.Domain.Users;
using Simple.Domain.Users.Specifications;

namespace Simple.App.Users.Queries;

public static class ListUsers
{
    public record Query(Guid TenantId, int PageNumber, int PageSize) : IRequest<PaginatedResult<Result>>;

    public record Result(Guid UserId, string UserName);

    public class Validator : AbstractValidator<Query>;

    public class Handler(IReadRepository<User> repo) : IRequestHandler<Query, PaginatedResult<Result>>
    {
        public async Task<PaginatedResult<Result>> Handle(Query query, CancellationToken cancellationToken)
        {
            var tenantId = new TenantId(query.TenantId);
            var page = new Page(query.PageNumber, query.PageSize);
            var spec = new UserListPaginatedSpec(tenantId, page);
            var list = await repo.ListAsync(spec, cancellationToken);
            var count = await repo.CountAsync(spec, cancellationToken);
            var items = list.Select(t => new Result(t.UserId, t.Name.FullName)).ToList();
            return new PaginatedResult<Result>(items, count, query.PageNumber, query.PageSize);
        }
    }
}