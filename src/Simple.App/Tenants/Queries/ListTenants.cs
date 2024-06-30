using Simple.Domain.Common;
using Simple.Domain.Tenants;
using Simple.Domain.Tenants.Specifications;

namespace Simple.App.Tenants.Queries;

public static class ListTenants
{
    public record Query(int PageNumber, int PageSize) : IRequest<PaginatedResult<Result>>;

    public record Result(Guid TenantId, string TenantName);

    public class Validator : AbstractValidator<Query>;

    public class Handler(IReadRepository<Tenant> repo) : IRequestHandler<Query, PaginatedResult<Result>>
    {
        public async Task<PaginatedResult<Result>> Handle(Query query, CancellationToken cancellationToken)
        {
            var page = new Page(query.PageNumber, query.PageSize);
            var spec = new TenantListPaginatedSpec(page);
            var results = await repo.ListAsync(spec, cancellationToken);
            var count = await repo.CountAsync(spec, cancellationToken);
            var items = results.Select(t => new Result(t.TenantId, t.TenantName)).ToList();
            return new PaginatedResult<Result>(items, count,query.PageNumber, query.PageSize);
        }
    }
}
