using System.Text;
using CurrencyConverter.Model;

namespace CurrencyConverter.DataAccess;

public class NBPRemoteRepository : IRemoteRepository
{
    private const string DefaultUrl = "https://static.nbp.pl/dane/kursy/xml/LastA.xml";
    private const string NbpEncoding = "ISO-8859-2";

    // żebym nie czekał tych defaultowych 100s jakby coś było nie tak + uczyli nas że HttpClient otwiera za każdym razem połączenie z nowym socketem jak jest tworzony
    // u pana na wykładzie było using ale te użyte sockety się nie zwalniały i był edge case. (wiem że pewnie pan to wie ale mnie bolało jak na to patrzyłem)
    private static readonly HttpClient Client = new() { Timeout = TimeSpan.FromSeconds(15) };

    private readonly string _url;

    // statyczny konstruktor odpala się raz, przed pierwszym użyciem klasy – ISO-8859-2 wystarczy zarejestrować raz
    static NBPRemoteRepository()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public NBPRemoteRepository(string url = DefaultUrl)
    {
        _url = url;
    }

    public async Task<Result<string>> GetData()
    {
        try
        {
            byte[] bytes = await Client.GetByteArrayAsync(_url);
            return Result<string>.Success(Encoding.GetEncoding(NbpEncoding).GetString(bytes));
        }
        catch (Exception e)
        {
            return Result<string>.Failure($"Żądanie do NBP nie powiodło się: {e.Message}");
        }
    }
}
