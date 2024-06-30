namespace Simple.IntegrationTests.Tenants.Queries;

public class IsNameAvailableTests(ServiceFixture service, ITestOutputHelper output) : BaseTest(service, output)
{
    [Fact]
    public async Task Should_Return_Available_For_Valid_And_Available_Name()
    {
        // Arrange
        var name = "abc" + Guid.NewGuid();
        
        // Act
        var result = await Execute(new IsNameAvailable.Query(name));

        // Assert
        result.IsNameValid.Should().BeTrue();
        result.IsNameAvailable.Should().BeTrue();
    }
    
    [Fact]
    public async Task Should_Return_Unavailable_For_Valid_But_Unavailable_Name()
    {
        // Arrange
        var value = "abc" + Guid.NewGuid();
        var tenant = Fake.Tenant()
            with { TenantName = value };
        var user = Fake.User();
        
        // Act
        await Execute(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password));   
        var result = await Execute(new IsNameAvailable.Query(value));

        // Assert
        result.IsNameValid.Should().BeTrue();
        result.IsNameAvailable.Should().BeFalse();
    }
    
    [Fact]
    public async Task Should_Return_Available_For_Invalid_But_Available_Suggested_Name()
    {
        // Arrange
        var unique = Guid.NewGuid();
        var name = "abc$def" + unique;
        var expected = "abc-def" + unique;
    
        // Act
        var result = await Execute(new IsNameAvailable.Query(name));
    
        // Assert
        result.IsNameValid.Should().BeFalse();
        result.IsNameAvailable.Should().BeNull();
        result.Suggestion.Should().Be(expected);
        result.IsSuggestionAvailable.Should().BeTrue();
    }
    
    
    [Fact]
    public async Task Should_Return_Unavailable_For_Invalid_And_Unavailable_Suggested_Name()
    {
        // Arrange
        var unique = Guid.NewGuid();
        var name = "abc$def" + unique;
        var expected = "abc-def" + unique;
        var tenant = Fake.Tenant()
            with { TenantName = expected };
        var user = Fake.User();
    
        // Act
        await Execute(new Register.Command(tenant.TenantId, tenant.TenantName, user.UserId, user.FirstName, user.LastName, user.Email, user.Password)); 
        var result = await Execute(new IsNameAvailable.Query(name));
    
        // Assert
        result.IsNameValid.Should().BeFalse();
        result.IsNameAvailable.Should().BeNull();
        result.Suggestion.Should().Be(expected);
        result.IsSuggestionAvailable.Should().BeFalse();
    }
}