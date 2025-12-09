using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Ordering.Domain.ValueObjects;

public record EmailAddress
{
    public string Value { get; }
    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                                               RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private EmailAddress(string value)
    {
        Value = value;
    }

    public static EmailAddress Of(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

        if (!EmailRegex.IsMatch(value))
            throw new ArgumentException($"Invalid email format: '{value}'", nameof(value));

        return new EmailAddress(value);
    }
}
