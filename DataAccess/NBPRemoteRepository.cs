using System.Text;
using CurrencyConverter.Model;

namespace CurrencyConverter.DataAccess;

public class NBPRemoteRepository : IRemoteRepository
{
    private const string DefaultUrl = "https://static.nbp.pl/dane/kursy/xml/LastA.xml";
    private const string NbpEncoding = "ISO-8859-2";

    // ¿ebym nie czeka³ tych deafultowych 100s jakby coœ by³o nie tak + uczyli nas ¿e HttpClient otwiera za ka¿dym razem po³¹czenie z nowym socketem jak jest tworzony
    // u pana na wyk³adzie by³o using ale te u¿yte sockety siê nie zwalnia³y i by³ edge case. (wiem ¿e pewnie pan to wie ale mnie bola³o jak na to patrzy³em)
    private static readonly HttpClient Client = new() { Timeout = TimeSpan.FromSeconds(15) };

    private readonly string _url;

    public NBPRemoteRepository(string url = DefaultUrl)
    {
        _url = url;
    }

    public async Task<Result<string>> GetData()
    {
        // to mo¿na w sumie przenieœæ do statycznego konstruktora, ale nie jestem tego pewien wiêc zostawie tak
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        try
        {
            byte[] bytes = await Client.GetByteArrayAsync(_url);
            return Result<string>.Success(Encoding.GetEncoding(NbpEncoding).GetString(bytes));
        }
        catch (Exception e)
        {
            return Result<string>.Failure($"NBP request failed: {e.Message}");
        }
    }
}
