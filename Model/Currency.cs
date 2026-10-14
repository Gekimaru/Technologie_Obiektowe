namespace CurrencyConverter.Model;

public class Currency : ICurrency
{
    public static readonly Currency Pln = new("złoty polski", "PLN", 1m, 1m);

    public string Name { get; }
    public string Code { get; }
    public decimal Rate { get; }
    public decimal Multiplier { get; }
    public decimal UnitRate => Rate / Multiplier;

    private Currency(string name, string code, decimal rate, decimal multiplier)
    {
        Name = name;
        Code = code;
        Rate = rate;
        Multiplier = multiplier;
    }

    public static Result<Currency> Create(string name, string code, decimal rate, decimal multiplier)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return Result<Currency>.Failure("Oznaczenie waluty nie może byc puste.");
        }

        if (rate <= 0M)
        {
            return Result<Currency>.Failure($"Kurs waluty: {code} musi być pozytywny a wyniósł: {rate}.");
        }

        if (multiplier <= 0M)
        {
            return Result<Currency>.Failure($"Mnożnik waluty {code} musi byc pozytywny a wyniósł: {multiplier}.");
        }

        return Result<Currency>.Success(new Currency(name, code.Trim().ToUpper(), rate, multiplier));
    }

    public override string ToString()
    {
        return $"{Code}  {Multiplier,6} {Name,-30} = {Rate,10} PLN";
    }
}
