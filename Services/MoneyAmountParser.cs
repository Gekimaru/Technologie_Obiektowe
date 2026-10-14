using System.Globalization;
using CurrencyConverter.Model;

namespace CurrencyConverter.Services;

public static class MoneyAmountParser 
{
    public static Result<decimal> Parse(string input)
    {
        string normalized = input.Replace(" ", "").Replace(',', '.');

        if (normalized.Length == 0)
        {
            return Result<decimal>.Failure("Amount cannot be empty.");
        }

        // nie chce mi się bawić w scientific notation więc nie sprawdzam tu E
        if (normalized.Count(c => c == '.') > 1 ||
            normalized.LastIndexOf('-') > 0 ||
            !normalized.All(c => 
                {
                    return char.IsAsciiDigit(c) || c == '.' || c == '-';
                })
            )
        {
            return Result<decimal>.Failure("Use digits, an optional leading minus and a single decimal separator only, e.g. -125.50");
        }

        if (!decimal.TryParse(normalized, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal amount))
        {
            return Result<decimal>.Failure("Not a valid number.");
        }

        return Result<decimal>.Success(amount);
    }
}
