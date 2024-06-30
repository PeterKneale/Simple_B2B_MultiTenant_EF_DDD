using Ardalis.Specification;

namespace Simple.Domain.Tenants.Specifications;

public class TenantByIdSpec : SingleResultSpecification<Tenant>
{
    public TenantByIdSpec(TenantId tenantId)
    {
        Query
            .Where(x => x.TenantId.Equals(tenantId));
    }
}