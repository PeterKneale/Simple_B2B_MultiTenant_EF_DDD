namespace Simple.IntegrationTests.Tenants.Queries;

public class GetTenantByIdTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task Get_tenant_that_does_not_exist()
    {
        var id = Guid.NewGuid();

        Func<Task> act = async () => await Query(new GetTenantById.Query(id));

        await act.Should()
            .ThrowAsync<PlatformException>()
            .WithMessage("*not found*");
    }
}