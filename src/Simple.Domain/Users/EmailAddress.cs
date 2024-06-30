namespace Simple.Domain.Users;

public record EmailAddress 
{
    private EmailAddress(string value)
    {
        Value = value;
    }

    public string Value { get; }
    
    public static EmailAddress Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Email address cannot be empty");

        if (!value.Contains('@')) throw new ArgumentException("The email address is not valid, no '@' found");

        if (!value.Contains('.')) throw new ArgumentException("The email address is not valid, no '.' found");

        return new EmailAddress(value);
    }

    public static implicit operator string(EmailAddress x) => x.Value;
}