namespace Simple.Domain.Users.Specifications;

public class UserByIdSpec : SingleResultSpecification<User>
{
    public UserByIdSpec(UserId userId)
    {
        Query.Where(x => x.UserId.Equals(userId));
    }
}