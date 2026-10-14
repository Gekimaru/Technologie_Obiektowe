namespace CurrencyConverter.Model;

public interface ICurrency
{
    string Name { get; }
    string Code { get; }

    decimal Rate { get; }

    decimal Multiplier { get; }

    decimal UnitRate { get; }
}
