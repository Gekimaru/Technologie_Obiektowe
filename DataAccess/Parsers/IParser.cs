using CurrencyConverter.Model;

namespace CurrencyConverter.DataAccess.Parsers;

public interface IParser<T>
{
    Result<T> Parse(string data);
}
