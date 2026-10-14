namespace CurrencyConverter.Model;

public interface ICurrencyCollection
{
    void Add(ICurrency currency);
    void Remove(string code);
    ICurrency? Get(string code);
    ICurrency[] GetAll();
}
