namespace Simple.Domain.Users;

public record UserId(Guid Value)
{
    public static implicit operator Guid(UserId id) => id.Value;
}