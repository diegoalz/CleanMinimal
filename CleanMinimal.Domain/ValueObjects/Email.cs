using System.Text.RegularExpressions;

namespace CleanMinimal.Domain.ValueObjects;

public partial record Email
{
    private const string Pattern = @"^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$";

    public string Value { get; init; }

    private Email(string value) => Value = value;

    [GeneratedRegex(Pattern)]
    private static partial Regex EmailRegex();

    public static Email? Create(string value)
    {
        if(string.IsNullOrEmpty(value) || !EmailRegex().IsMatch(value))
        {
            return null;
        }
        
        return new Email(value);
    }
}