using System.Globalization;
using System.Xml;
using System.Xml.Linq;
using CurrencyConverter.Model;

namespace CurrencyConverter.DataAccess.Parsers;

public class NBPXmlParser : IParser<ICurrencyCollection>
{
    private static readonly NumberFormatInfo NbpNumberFormat = new() { NumberDecimalSeparator = "," };

    public Result<ICurrencyCollection> Parse(string dataString)
    {
        if(dataString is null)
        {
            return Result<ICurrencyCollection>.Failure($"{nameof(dataString)} jest nullem");
        }

        XElement? root;
        try
        {
            root = XDocument.Parse(dataString).Root;
        }
        catch (XmlException e)
        {
            return Result<ICurrencyCollection>.Failure($"Nieprawidłowy XML: {e.Message}");
        }

        if (root == null)
        {
            return Result<ICurrencyCollection>.Failure("Dokument NBP nie ma głównego elementu (roota).");
        }

        ICurrencyCollection currencies = new CurrencyCollection();

        int parsed = 0;

        foreach (XElement position in root.Elements("pozycja"))
        {
            Result<Currency> currency = ParseCurrency(position);
            if (!currency.IsSuccess)
            {
                return Result<ICurrencyCollection>.Failure(currency.Error);
            }
            currencies.Add(currency.Value);
            parsed++;
        }

        if (parsed == 0)
        {
            return Result<ICurrencyCollection>.Failure("Dokument NBP nie zawiera żadnych walut/kursów.");
        }

        return Result<ICurrencyCollection>.Success(currencies);
    }

    private static Result<Currency> ParseCurrency(XElement position)
    {
        string? name = GetNodeChildByName(position, "nazwa_waluty");
        string? code = GetNodeChildByName(position, "kod_waluty");
        string? rateText = GetNodeChildByName(position, "kurs_sredni");
        string? multiplierText = GetNodeChildByName(position, "przelicznik");

        if (name == null || code == null || rateText == null || multiplierText == null)
        {
            return Result<Currency>.Failure($"Niekompletny Node <pozycja> w Dokumencie NBP{(code != null ? $", kod waluty: ({code})" : "")}.");
        }

        if (!TryParseNumber(rateText, out decimal rate))
        {
            return Result<Currency>.Failure($"Nieprawidłowa stawka: '{rateText}' dla {code}.");
        }

        if (!TryParseNumber(multiplierText, out decimal multiplier))
        {
            return Result<Currency>.Failure($"Nieprawidłowwy mnożnik: '{multiplierText}' dla {code}.");
        }

        return Currency.Create(name, code, rate, multiplier);
    }

    private static bool TryParseNumber(string text, out decimal value)
    {
        return decimal.TryParse(text, NumberStyles.AllowDecimalPoint, NbpNumberFormat, out value);
    }

    private static string? GetNodeChildByName(XElement parent, string name)
    {
        return parent.Element(name)?.Value.Trim();
    }
}
