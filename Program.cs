using CurrencyConverter.DataAccess;
using CurrencyConverter.DataAccess.Parsers;
using CurrencyConverter.Model;
using CurrencyConverter.Services;
using CurrencyConverter.View;

namespace CurrencyConverter;

static class Program
{
    [STAThread]
    static int Main()
    {
        ApplicationConfiguration.Initialize();

        IRemoteRepository repository = new NBPRemoteRepository();
        IParser<ICurrencyCollection> parser = new NBPXmlParser();
        ICurrencyConverter converter = new NBPCurrencyConverter();

        var service = new CurrencyService(repository, parser, converter);

        Application.Run(new CurrencyConverterView(service));
        return 0;
    }
}
