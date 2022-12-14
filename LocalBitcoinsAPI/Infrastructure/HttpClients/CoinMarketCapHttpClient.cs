using Newtonsoft.Json;
using LocalBitcoinsAPI.Models.CoinMarketCap;

namespace LocalBitcoinsAPI.Infrastructure.HttpClients;

public class CoinMarketCapHttpClient : ICoinMarketCapHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<CoinMarketCapHttpClient> _logger;

    public CoinMarketCapHttpClient(HttpClient httpClient, IConfiguration configuration, ILogger<CoinMarketCapHttpClient> logger)
    {
        httpClient.BaseAddress = new Uri(configuration.GetValue<string>("CoinMarketCapApi:Url"));
        httpClient.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", configuration.GetValue<string>("CoinMarketCapApi:Key"));
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<CryptoCurrency> GetQuoteAsync(string symbol = "BTC", CancellationToken cancellationToken = default)
    {
        var route = $"/v2/cryptocurrency/quotes/latest?symbol={symbol}";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var advertisementsResponse = JsonConvert.DeserializeObject<Response<ResponseData>>(responseContent);
        _logger.LogDebug($"Calling GET {route} response body {advertisementsResponse.Data.Btc}");

        return advertisementsResponse.Data.Btc.FirstOrDefault();
    }
}
