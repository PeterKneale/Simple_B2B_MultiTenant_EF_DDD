namespace Simple.IntegrationTests;

public class SetupTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task Setup_test_user()
    {
        var tenant = Fake.Tenant()
            with { TenantName = "Tenant_Name" };
        
        var user = Fake.User()
            with { Email = "testuser@example.org", Password = "PasswordPassword" };

        await Command(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));
    }
}