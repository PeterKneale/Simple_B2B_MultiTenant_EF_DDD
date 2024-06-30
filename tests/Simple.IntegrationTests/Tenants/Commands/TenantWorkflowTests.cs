namespace Simple.IntegrationTests.Tenants.Commands;

public class TenantWorkflowTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task RegistrationTest()
    {
        var name = "abc" + Guid.NewGuid();
        var tenant = Fake.Tenant() with {TenantName = name};
        var user = Fake.User();

        await Command(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));

        var idResult = await Query(new GetTenantById.Query(tenant.TenantId));
        Log(idResult);
        idResult.TenantId.Should().Be(tenant.TenantId);
        idResult.TenantName.Should().Be(tenant.TenantName);
        
        var nameResult = await Query(new GetTenantByName.Query(tenant.TenantName));
        Log(nameResult);
        nameResult.TenantId.Should().Be(tenant.TenantId);
        nameResult.TenantName.Should().Be(tenant.TenantName);
        
        var emailResult = await Query(new GetUserByEmail.Query(user.Email));
        Log(emailResult);
        emailResult.UserId.Should().Be(user.UserId);
        
        var authResult = await Query(new CanAuthenticate.Query(user.Email, user.Password));
        Log(authResult);
        authResult.Success.Should().BeTrue();
        authResult.UserId.Should().Be(user.UserId);

        var listResult = await Query(new ListTenants.Query(1, 10));
        Log(listResult);
        listResult.Items.Should().ContainSingle(t => t.TenantId == tenant.TenantId && t.TenantName == tenant.TenantName);
        
        var listUsersResult = await Query(new ListUsers.Query(tenant.TenantId, 1, 10));
        Log(listUsersResult);
        listUsersResult.Items.Should().ContainSingle(u => u.UserId == user.UserId);
    }
}