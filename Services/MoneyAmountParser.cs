using System.Globalization;
using CurrencyConverter.Model;

namespace CurrencyConverter.Services;

public static class MoneyAmountParser 
{
    public static Result<decimal> Parse(string input)
    {
        string normalized = input.Replace(" ", "").Replace(',', '.').Trim();

        if (normalized.Length == 0)
        {
            return Result<decimal>.Failure("Kwota nie może być pusta.");
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
            return Result<decimal>.Failure("Użyj cyfr, opcjonalnie minus oraz pojedyńczy separator do ułamków dziesiętnych, np. -125.50");
        }

        if (!decimal.TryParse(normalized, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal amount))
        {
            return Result<decimal>.Failure("Niepoprawna kwota.");
        }

        return Result<decimal>.Success(amount);
    }
}
