﻿namespace Simple.Domain.Users;

public record Name
{
    private Name(string first, string last)
    {
        First = first;
        Last = last;
    }

    public string FullName => $"{First} {Last}";

    public string First { get; }

    public string Last { get; }

    public static Name Create(string first, string last)
    {
        if (string.IsNullOrWhiteSpace(first)) throw new ArgumentException("First name cannot be empty");
        if (string.IsNullOrWhiteSpace(last)) throw new ArgumentException("Last name cannot be empty");
        return new Name(first, last);
    }

    public static implicit operator string(Name x) => x.FullName;

    public override string ToString() => FullName;
}