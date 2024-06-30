using Simple.Domain.Tenants;

namespace Simple.Domain.Users.Specifications;

public class UserListPaginatedSpec : Specification<User>
{
    public UserListPaginatedSpec(TenantId tenantId, Page page)
    {
        Query
            .Where(x => x.TenantId.Equals(tenantId))
            .OrderBy(x => x.Name.Last).ThenBy(x => x.Name.First)
            .Skip(page.Skip)
            .Take(page.Take)
            .AsNoTracking();
    }
}