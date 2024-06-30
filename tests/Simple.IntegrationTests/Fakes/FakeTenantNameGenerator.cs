namespace Simple.IntegrationTests.Fakes;

public static class FakeTenantNameGenerator
{
    public static string GenerateValidTenantName()
    {
        return Guid.NewGuid().ToString();
    }
    
    public static string GenerateInValidTenantName()
    {
        return Guid.NewGuid().ToString() + "$";
    }
}