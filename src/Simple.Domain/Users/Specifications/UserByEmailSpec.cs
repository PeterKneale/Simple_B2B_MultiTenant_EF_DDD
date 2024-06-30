namespace Simple.Domain.Users.Specifications;

public class UserByEmailSpec : SingleResultSpecification<User>
{
    public UserByEmailSpec(EmailAddress email)
    {
        Query.Where(x => x.Email.Equals(email));
    }
}