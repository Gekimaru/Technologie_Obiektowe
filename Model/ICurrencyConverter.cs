namespace CurrencyConverter.Model;

public interface ICurrencyConverter
{
    decimal Convert(ICurrency source, ICurrency target, decimal amount);
}
