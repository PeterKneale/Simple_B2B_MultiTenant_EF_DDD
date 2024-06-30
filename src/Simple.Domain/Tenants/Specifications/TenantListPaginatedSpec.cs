using Ardalis.Specification;
using Simple.Domain.Users.Specifications;

namespace Simple.Domain.Tenants.Specifications;

public class TenantListPaginatedSpec : PaginatedSpec<Tenant>
{
    public TenantListPaginatedSpec(int page, int pageSize) : base(page, pageSize)
    {
        Query
            .OrderBy(x => x.TenantName)
            .Skip(Skip)
            .Take(Take)
            .AsNoTracking();
    }
}