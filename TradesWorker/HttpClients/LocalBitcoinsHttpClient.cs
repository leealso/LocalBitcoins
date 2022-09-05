using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TradesWorker.Configurations;
using TradesWorker.Constants;
using TradesWorker.Models;

namespace TradesWorker.HttpClients;

public class LocalBitcoinsHttpClient : ILocalBitcoinsHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<LocalBitcoinsHttpClient> _logger;

    public LocalBitcoinsHttpClient(HttpClient httpClient, IOptions<LocalBitcoinsConfiguration> configuration, ILogger<LocalBitcoinsHttpClient> logger)
    {
        httpClient.BaseAddress = new Uri(configuration.Value.Url);
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IList<LocalBitcoinsTrade>> GetTradesAsync(int maxTransactionId = 0, string currencyCode = Default.CurrencyCode, CancellationToken cancellationToken = default)
    {
        var route = $"/bitcoincharts/{currencyCode}/trades.json?max_tid={maxTransactionId}";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var trades = JsonConvert.DeserializeObject<IList<LocalBitcoinsTrade>>(responseContent);
        _logger.LogDebug($"Calling GET {route} response body {responseContent}");

        return trades ?? new List<LocalBitcoinsTrade>();
    }
}
