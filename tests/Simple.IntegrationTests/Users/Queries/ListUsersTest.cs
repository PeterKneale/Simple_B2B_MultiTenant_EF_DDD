namespace Simple.IntegrationTests.Users.Queries;

[TestSubject(typeof(ListUsers))]
public class ListUsersTest(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task Should_return_paged_list_of_users()
    {
        // Arrange
        var tenantId = Guid.NewGuid();
        
        // Act
        await CreateTenant(tenantId);

        // Assert
        await Assert(tenantId, 1, 10, 1, 1);
        await Assert(tenantId, 2, 10, 0, 1);
    }

    private async Task Assert(Guid tenantId, int pageNumber, int pageSize, int expectedItems, int expectedTotal)
    {
        var result1 = await Query(new ListUsers.Query(tenantId, pageNumber, pageSize));
        result1.Total.Should().Be(expectedTotal);
        result1.Items.Should().HaveCount(expectedItems);
    }

    private async Task CreateTenant(Guid tenantId)
    {
        var tenant = Fake.Tenant() with
        {
            TenantId = tenantId
        };
        var user = Fake.User();
        await Command(new Register.Command(tenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));
    }
}