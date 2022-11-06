using LocalBitcoins.Functions.Constants;
using LocalBitcoins.Functions.Models;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Threading;
using LocalBitcoins.Functions.Utilities;
using Newtonsoft.Json;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public class AuthHttpClient : IAuthHttpClient
{
    private readonly HttpClient _httpClient;

    private readonly string _tenantId;

    private readonly string _clientId;

    private readonly string _clientSecret;

    private readonly string _resource;

    private readonly ILogger<AuthHttpClient> _logger;

    public AuthHttpClient(HttpClient httpClient, ILogger<AuthHttpClient> logger)
    {
        _tenantId = ApplicationSettingsUtility.Get(ApplicationSettings.AuthTenantId);
        _clientId = ApplicationSettingsUtility.Get(ApplicationSettings.AuthClientId);
        _clientSecret = ApplicationSettingsUtility.Get(ApplicationSettings.AuthClientSecret);
        _resource = ApplicationSettingsUtility.Get(ApplicationSettings.AuthResource);
        httpClient.BaseAddress = new Uri(ApplicationSettingsUtility.Get(ApplicationSettings.AuthUrl));
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var route = $"/{_tenantId}/oauth2/token";
        _logger.LogDebug($"Calling POST {route}");
        var data = new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credential"),
            new KeyValuePair<string, string>("client_id", _clientId),
            new KeyValuePair<string, string>("client_secret", _clientSecret),
            new KeyValuePair<string, string>("resource", _resource)
        };
        var response = await _httpClient.PostAsync(route, new FormUrlEncodedContent(data), cancellationToken);
        _logger.LogDebug($"Calling POST {route} returned {response.StatusCode} {response.ReasonPhrase}");
        response.EnsureSuccessStatusCode();

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var authTokenResponse = JsonConvert.DeserializeObject<AuthTokenResponse>(responseContent);
        _logger.LogDebug($"Calling POST {route} response body {responseContent}");

        return authTokenResponse.AccessToken;
    }
}
