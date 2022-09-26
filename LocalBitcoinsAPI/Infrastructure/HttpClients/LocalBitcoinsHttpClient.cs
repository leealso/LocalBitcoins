using Newtonsoft.Json;
using LocalBitcoinsAPI.Models.LocalBitcoins;
using LocalBitcoinsAPI.Utilities;

namespace LocalBitcoinsAPI.Infrastructure.HttpClients;

public class LocalBitcoinsHttpClient : ILocalBitcoinsHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly ILogger<LocalBitcoinsHttpClient> _logger;

    public LocalBitcoinsHttpClient(HttpClient httpClient, IConfiguration configuration, ILogger<LocalBitcoinsHttpClient> logger)
    {
        httpClient.BaseAddress = new Uri(configuration.GetValue<string>("LocalBitcoinsApi:Url"));
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IEnumerable<Advertisement>> GetAdsAsync(string tradeType, string countryCode, CancellationToken cancellationToken = default)
    {
        var route = $"/{tradeType.ToLower()}-bitcoins-online/{countryCode}/{LocalBitcoinsUtility.GetCountryName(countryCode)}/.json";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var advertisementsResponse = JsonConvert.DeserializeObject<Response<AdvertisementsResponse>>(responseContent);
        _logger.LogDebug($"Calling GET {route} response body {advertisementsResponse.Data.Advertisements}");

        return advertisementsResponse.Data.Advertisements;
    }
}
