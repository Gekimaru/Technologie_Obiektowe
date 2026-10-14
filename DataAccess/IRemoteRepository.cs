using CurrencyConverter.Model;

namespace CurrencyConverter.DataAccess;

public interface IRemoteRepository
{
    Task<Result<string>> GetData();
}
