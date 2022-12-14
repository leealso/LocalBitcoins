using Newtonsoft.Json;
using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;
using LocalBitcoins.Functions.Utilities;
using System.Linq;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public class LocalBitcoinsHttpClient : ILocalBitcoinsHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly string _hmacKey;

    private readonly string _hmacSecret;

    private readonly ILogger<LocalBitcoinsHttpClient> _logger;

    public LocalBitcoinsHttpClient(HttpClient httpClient, ILogger<LocalBitcoinsHttpClient> logger)
    {
        _hmacKey = ApplicationSettingsUtility.Get(ApplicationSettings.LocalBitcoinsHmacKey);
        _hmacSecret = ApplicationSettingsUtility.Get(ApplicationSettings.LocalBitcoinsHmacSecret);
        httpClient.BaseAddress = new Uri(ApplicationSettingsUtility.Get(ApplicationSettings.LocalBitcoinsUrl));
        httpClient.DefaultRequestHeaders.Add(LocalBitcoinsHeaders.ApiAuthKey, _hmacKey);
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IList<LocalBitcoinsTrade>> GetTradesAsync(int maxTransactionId = 0, string currencyCode = CurrencyCode.CRC, CancellationToken cancellationToken = default)
    {
        var route = $"/bitcoincharts/{currencyCode}/trades.json?max_tid={maxTransactionId}";
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.GetAsync(route, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var trades = JsonConvert.DeserializeObject<IList<LocalBitcoinsTrade>>(responseContent);
        _logger.LogDebug($"Calling GET {route} response body {responseContent}");

        return trades;
    }

    public async Task<IList<LocalBitcoinsContactData>> GetReleasedTradesAsync(DateTime? startDate = null, CancellationToken cancellationToken = default)
    {
        var route = $"/api/dashboard/released/";
        var nonce = DateTimeUtility.GetNonce();
        var args = new Dictionary<string, string>()
        {
            { "order_by", "-closed_at"}
        };
        if (startDate.HasValue)
            args.Add("start_at", startDate.Value.ToString("yyyy-MM-dd HH:mm:ss+00:00"));
        
        var signature = LocalBitcoinsUtility.GetSignature(_hmacKey, _hmacSecret, route, nonce, args);

        route = $"{route}?{LocalBitcoinsUtility.UrlEncodeParams(args)}";
        using var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_httpClient.BaseAddress, route));
        request.Headers.Add("Apiauth-Nonce", nonce);
        request.Headers.Add("Apiauth-Signature", signature);
        
        _logger.LogDebug($"Calling GET {route}");
        var response = await _httpClient.SendAsync(request, cancellationToken);
        _logger.LogDebug($"Calling GET {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var localBitcoinsResponse = JsonConvert.DeserializeObject<LocalBitcoinsResponse<LocalBitcoinsDashboardClosedResponse>>(responseContent);
        _logger.LogDebug($"Calling GET {route} response body {responseContent}");

        return localBitcoinsResponse.Data.ContactList
            .Select(x => x.Data)
            .ToList();
    }
}
