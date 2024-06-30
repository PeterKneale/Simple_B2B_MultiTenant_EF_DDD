using JetBrains.Annotations;
using Simple.Domain.Tenants;

namespace Simple.Domain.Tests.Tenants;

[TestSubject(typeof(TenantId))]
public class TenantIdTest
{
    [Fact]
    public void TenantId_equals_TenantId()
    {
        var id = Guid.NewGuid();
        var tenantId1 = new TenantId(id);
        var tenantId2 = new TenantId(id);
        Assert.Equal(tenantId1, tenantId2);
    }

    [Fact]
    public void TenantId_equals_guid()
    {
        var id = Guid.NewGuid();
        var tenantId = new TenantId(id);
        Assert.Equal(id, tenantId);
    }
}