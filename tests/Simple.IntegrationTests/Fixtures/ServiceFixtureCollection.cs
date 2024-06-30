namespace Simple.IntegrationTests.Fixtures;

[CollectionDefinition(nameof(ServiceFixtureCollection), DisableParallelization = true)]
public class ServiceFixtureCollection : ICollectionFixture<ServiceFixture>
{
}