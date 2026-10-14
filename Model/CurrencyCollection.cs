namespace CurrencyConverter.Model;

public class CurrencyCollection : ICurrencyCollection
{
    // ignore case samo się tłumaczy
    private readonly Dictionary<string, ICurrency> _currencies;

    public CurrencyCollection()
    {
        this._currencies = new(StringComparer.OrdinalIgnoreCase);
        Add(Currency.Pln);
    }

    public void Add(ICurrency currency)
    {
        if (!this._currencies.TryAdd(currency.Code, currency))
        {
            this._currencies[currency.Code] = currency;
        }
    }

    public void Remove(string code)
    {
        _currencies.Remove(code.Trim());
    }

    public ICurrency? Get(string code)
    {
        return _currencies.GetValueOrDefault(code.Trim());
    }

    public ICurrency[] GetAll()
    {
        return _currencies.Values.OrderBy(c => c.Code, StringComparer.Ordinal).ToArray();
    }
}
