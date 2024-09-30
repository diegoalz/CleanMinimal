using System.Text.RegularExpressions;

namespace CleanMinimal.Domain.ValueObjects;

public partial record PhoneNumber
{
    private const int MinimumLength = 12;
    private const int MaximumLength = 13;

    private const string Pattern =  @"^\d{2,3}(\d{2,3})(\d{4})(\d{4})$";

    public string Value { get; init; }

    private PhoneNumber(string value) => Value = value;

    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneNumberRegex();

    public static PhoneNumber? Create(string value)
    {
        if(
            string.IsNullOrEmpty(value) || !PhoneNumberRegex().IsMatch(value) ||
            (value.Length < MinimumLength && value.Length > MaximumLength)
        )
        {
            return null;
        }

        return new PhoneNumber(value);
    }
}