using Ardalis.Specification;
using Simple.Domain.Tenants;

namespace Simple.Domain.Users.Specifications;

public class UserListPaginatedSpec : PaginatedSpec<User>
{
    public UserListPaginatedSpec(TenantId tenantId, int page, int pageSize) : base(page, pageSize)
    {
        Query
            .Where(x => x.TenantId.Equals(tenantId))
            .OrderBy(x => x.Name.Last).ThenBy(x=>x.Name.First)
            .Skip(Skip)
            .Take(Take)
            .AsNoTracking();
    }
}