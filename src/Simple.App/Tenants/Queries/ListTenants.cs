using Simple.Domain.Tenants;
using Simple.Domain.Tenants.Specifications;

namespace Simple.App.Tenants.Queries;

public static class ListTenants
{
    public record Query(int? Page = null, int? PageSize = null) : IRequest<Results>;

    public record Results(IEnumerable<Result> Items);

    public record Result(Guid TenantId, string TenantName);

    public class Validator : AbstractValidator<Query>;

    public class Handler(IReadRepository<Tenant> repo) : IRequestHandler<Query, Results>
    {
        public async Task<Results> Handle(Query query, CancellationToken cancellationToken)
        {
            var results = await repo.ListAsync(new TenantListPaginatedSpec(query.Page ?? 1, query.PageSize ?? 10), cancellationToken);
            return new Results(results.Select(t => new Result(t.TenantId, t.TenantName)));
        }
    }
}