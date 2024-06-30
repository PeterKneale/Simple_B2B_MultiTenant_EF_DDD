namespace Simple.IntegrationTests.Tenants.Queries;

public class GetTenantByNameTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task Get_tenant_by_name_that_does_not_exist()
    {
        var name = "x";

        Func<Task> act = async () => await Execute(new GetTenantByName.Query(name));

        await act.Should()
            .ThrowAsync<PlatformException>()
            .WithMessage("*not found*");
    }
}