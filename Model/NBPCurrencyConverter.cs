namespace CurrencyConverter.Model;

public class NBPCurrencyConverter : ICurrencyConverter
{
    public decimal Convert(ICurrency source, ICurrency target, decimal amount)
    {
        decimal amountInPln = amount * source.UnitRate;
        return amountInPln / target.UnitRate;
    }
}
