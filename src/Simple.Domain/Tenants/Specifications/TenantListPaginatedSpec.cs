namespace Simple.Domain.Tenants.Specifications;

public class TenantListPaginatedSpec : Specification<Tenant>
{
    public TenantListPaginatedSpec(Page page) 
    {
        Query
            .OrderBy(x => x.TenantName)
            .Skip(page.Skip)
            .Take(page.Take)
            .AsNoTracking();
    }
}