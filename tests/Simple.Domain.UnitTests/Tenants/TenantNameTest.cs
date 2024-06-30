using FluentAssertions;
using JetBrains.Annotations;
using Simple.Domain.Tenants;

namespace Simple.Domain.Tests.Tenants;

[TestSubject(typeof(TenantName))]
public class TenantNameTest
{
    [Fact]
    public void Are_equal()
    {
        var value = "x";
        var name1 = TenantName.Create(value);
        var name2 = TenantName.Create(value);
        Assert.Equal(name1, name2);
    }

    [Theory]
    [MemberData(nameof(ValidNames))]
    public void Valid_names_are_accepted(string value)
    {
        var name = TenantName.Create(value);

        name.Value.Should().Be(value.ToLowerInvariant());
    }

    [Theory]
    [MemberData(nameof(InvalidNames))]
    public void Invalid_names_are_not_accepted(string value)
    {
        var act = void () => TenantName.Create(value);

        act.Should()
            .Throw<ArgumentException>()
            .WithMessage(TenantName.InvalidTenantNameMessage);
    }
    
    [Theory]
    [MemberData(nameof(ValidNames))]
    public void Valid_names_are_valid(string value)
    {
        var valid = TenantName.IsValidName(value);
        valid.Should().BeTrue();
    }
    
    [Theory]
    [MemberData(nameof(InvalidNames))]
    public void Invalid_names_are_not_valid(string value)
    {
        var valid = TenantName.IsValidName(value);
        valid.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(Suggestions))]
    public void Suggestions_can_be_made(string value, string expected)
    {
        var suggestion = TenantName.GetSuggestion(value);
        suggestion.Value.Should().Be(expected);
    }

    public static IEnumerable<object[]> Suggestions =>
        new List<object[]>
        {
            new object[] { "a!", "a-" },
            new object[] { "a@", "a-" },
            new object[] { "a#", "a-" },
            new object[] { "a$", "a-" }
        };

    public static IEnumerable<object[]> ValidNames =>
        new List<object[]>
        {
            new object[] { "a" },
            new object[] { "a_b" },
            new object[] { "a-c" },
            new object[] { "a-_-d" }
        };

    public static IEnumerable<object[]> InvalidNames =>
        new List<object[]>
        {
            new object[] { "a!" },
            new object[] { "a@" },
            new object[] { "a#" },
            new object[] { "a$" }
        };
}