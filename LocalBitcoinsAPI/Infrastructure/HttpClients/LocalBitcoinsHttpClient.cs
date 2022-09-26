using Newtonsoft.Json;
using LocalBitcoinsAPI.LocalBitcoins.Models;
using LocalBitcoinsAPI.Utilities;

namespace LocalBitcoinsAPI.Infrastructure.HttpClients;

public class LocalBitcoinsHttpClient : ILocalBitcoinsHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<LocalBitcoinsHttpClient> _logger;

    public LocalBitcoinsHttpClient(HttpClient httpClient, ILogger<LocalBitcoinsHttpClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IList<Advertisement>> GetAdsAsync(string tradeType, string countryCode, CancellationToken cancellationToken = default)
    {
        var route = $"/{tradeType.ToLower()}-bitcoins-online/{countryCode}/{LocalBitcoinsUtility.GetCountryName(countryCode)}/.json";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var advertisementsResponse = JsonConvert.DeserializeObject<AdvertisementsResponse>(responseContent);
        _logger.LogDebug($"Calling GET {route} response body {advertisementsResponse.Advertisements}");

        return advertisementsResponse.Advertisements;
    }
}
