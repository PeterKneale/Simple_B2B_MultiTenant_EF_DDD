namespace Simple.Domain.Users;

public record Password
{
    private Password(string hashed)
    {
        Hashed = hashed;
    }

    public string Hashed { get; }

    public static Password Encrypt(string supplied, Func<string, string> encrypt) => new(encrypt(supplied));

    public bool Verify(string supplied, Func<string, string, bool> verify) => verify(supplied, Hashed);

    public static Password Create(string value)
    {
        return new Password(value);
    }
}