namespace Simple.IntegrationTests.Tenants.Queries;

// We need to remove all tenants so that those created in other tests
// don't affect the pagination results.
public class ListTenantsTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output, resetDatabase:true)
{
    [Fact]
    public async Task Paging_returns_correct_item_count_and_total()
    {
        // Create 1 tenant (1 in total)
        await CreateTenants(1);
        // page size is 1.
        await Assert(1, 1, 1, 1);
        await Assert(2, 1, 0, 1);
        await Assert(3, 1, 0, 1);
        // page size is 10.
        await Assert(1, 10, 1, 1);
        await Assert(2, 10, 0, 1);
        await Assert(3, 10, 0, 1);
        // page size is 100.
        await Assert(1, 100, 1, 1);
        await Assert(2, 100, 0, 1);
        await Assert(3, 100, 0, 1);

        // Create 1 more tenant (2 in total)
        await CreateTenants(1);
        // page size is 1.
        await Assert(1, 1, 1, 2);
        await Assert(2, 1, 1, 2);
        await Assert(3, 1, 0, 2);
        // page size is 10.
        await Assert(1, 10, 2, 2);
        await Assert(2, 10, 0, 2);
        await Assert(3, 10, 0, 2);
        // page size is 100
        await Assert(1, 100, 2, 2);
        await Assert(2, 100, 0, 2);
        await Assert(3, 100, 0, 2);

        // Create tenants (9 in total)
        await CreateTenants(7);
        // page size is 5.
        await Assert(1, 5, 5, 9);
        await Assert(2, 5, 4, 9);
        await Assert(3, 5, 0, 9);
        // page size is 10
        await Assert(1, 10, 9, 9);
        await Assert(2, 10, 0, 9);
        await Assert(3, 10, 0, 9);
        // page size is 100
        await Assert(1, 100, 9, 9);
        await Assert(2, 100, 0, 9);
        await Assert(3, 100, 0, 9);

        // Create 1 more tenant (10 in total)
        await CreateTenants(1);
        // page size is 5.
        await Assert(1, 5, 5, 10);
        await Assert(2, 5, 5, 10);
        await Assert(3, 5, 0, 10);
        // page size is 10
        await Assert(1, 10, 10, 10);
        await Assert(2, 10, 0, 10);
        await Assert(3, 10, 0, 10);
        // page size is 100
        await Assert(1, 100, 10, 10);
        await Assert(2, 100, 0, 10);
        await Assert(3, 100, 0, 10);

        // Create 1 more tenant (11 in total)
        await CreateTenants(1);
        // page size is 5.
        await Assert(1, 5, 5, 11);
        await Assert(2, 5, 5, 11);
        await Assert(3, 5, 1, 11);
        // page size is 10
        await Assert(1, 10, 10, 11);
        await Assert(2, 10, 1, 11);
        await Assert(3, 10, 0, 11);

        // Create more tenants (21 in total)
        await CreateTenants(10);
        // page size is 5.
        await Assert(1, 5, 5, 21);
        await Assert(2, 5, 5, 21);
        await Assert(3, 5, 5, 21);
        await Assert(4, 5, 5, 21);
        await Assert(5, 5, 1, 21);
        // page size is 10
        await Assert(1, 10, 10, 21);
        await Assert(2, 10, 10, 21);
        await Assert(3, 10, 1, 21);
    }

    private async Task Assert(int pageNumber, int pageSize, int expectedItems, int expectedTotal)
    {
        var results = await List(pageNumber, pageSize);
        results.Items.Should().HaveCount(expectedItems);
        results.Total.Should().Be(expectedTotal);
    }

    private async Task<PaginatedResult<ListTenants.Result>> List(int pageNumber, int pageSize)
    {
        Log($"Listing page {pageNumber}/{pageSize}");
        return await Query(new ListTenants.Query(pageNumber, pageSize));
    }

    private async Task CreateTenants(int count)
    {
        Log($"Creating {count} tenants");
        for (var i = 0; i < count; i++)
        {
            var tenant = Fake.Tenant();
            var user = Fake.User();
            await Command(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));
        }
    }
}