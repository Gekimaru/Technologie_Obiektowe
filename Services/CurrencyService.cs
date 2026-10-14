using CurrencyConverter.DataAccess;
using CurrencyConverter.DataAccess.Parsers;
using CurrencyConverter.Model;

namespace CurrencyConverter.Services;

public class CurrencyService
{
    private IRemoteRepository _remoteRepository;
    private IParser<ICurrencyCollection> _parser;
    private ICurrencyConverter _converter;

    public CurrencyService(IRemoteRepository repository, IParser<ICurrencyCollection> parser, ICurrencyConverter converter)
    {
        _remoteRepository = repository;
        _parser = parser;
        _converter = converter;
    }

    public async Task<Result<ICurrencyCollection>> LoadAsync()
    {
        var data = await _remoteRepository.GetData();
        if (!data.IsSuccess)
        {
            return Result<ICurrencyCollection>.Failure(data.Error);
        }

        return _parser.Parse(data.Value);
    }

    public Result<decimal> Convert(ICurrency? source, ICurrency? target, string amountText)
    {
        if (source == null || target == null)
        {
            return Result<decimal>.Failure("Wybierz obie waluty.");
        }

        if (source.Code == target.Code)
        {
            return Result<decimal>.Failure("Wybierz dwie różne waluty.");
        }

        Result<decimal> amount = MoneyAmountParser.Parse(amountText);
        if (!amount.IsSuccess)
        {
            return amount;
        }

        return Result<decimal>.Success(_converter.Convert(source, target, amount.Value));
    }
}
