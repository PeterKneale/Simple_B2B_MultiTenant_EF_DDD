using Ardalis.Specification;

namespace Simple.Domain.Tenants.Specifications;

public class TenantByNameSpec : SingleResultSpecification<Tenant>
{
    public TenantByNameSpec(TenantName name)
    {   
        Query.Where(x => x.TenantName.Equals(name));
    }
}