using Bogus;

namespace Simple.IntegrationTests.Fakes;

public static class Fake
{
    public static FakeTenant Tenant() =>
        new Faker<FakeTenant>()
            .CustomInstantiator(f =>
            {
                var id = Guid.NewGuid();
                var name = id.ToString();
                return new FakeTenant(id, name);
            }).Generate();
    
    public static FakeUser User() =>
        new Faker<FakeUser>()
            .CustomInstantiator(f =>
            {
                var id = Guid.NewGuid();
                var firstName = f.Person.FirstName;
                var lastName = f.Person.LastName;
                var email = f.Person.Email;
                var password = f.Internet.Password();
                return new FakeUser(id, firstName, lastName, email, password);
            }).Generate();
}