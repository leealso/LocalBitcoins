using LocalBitcoins.Functions.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using LocalBitcoins.Functions.Extensions;
using LocalBitcoins.Functions.Utilities;
using LocalBitcoins.Functions.Constants;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public class LocalBitcoinsApiGraphClient : ILocalBitcoinsApiGraphClient
{
    private readonly GraphQLHttpClient _httpClient;

    private readonly ILogger<LocalBitcoinsApiGraphClient> _logger;

    public LocalBitcoinsApiGraphClient(ILogger<LocalBitcoinsApiGraphClient> logger)
    {
        _httpClient = new GraphQLHttpClient(ApplicationSettingsUtility.Get(ApplicationSettings.LocalBitcoinsGraphQlUrl), new NewtonsoftJsonSerializer());
        _logger = logger;
    }

    public async Task<TResult> QueryAsync<TResult>(string query, object? variables = null, CancellationToken cancellationToken = default)
    {
        var graphQlQuery = new GraphQLRequest
        {
            Query = query,
            Variables = variables
        };

        var route = $"/graphql";
        _logger.LogDebug($"Calling POST {route}");
        var response = await _httpClient.SendMutationAsync<GraphQlResponse<TResult>>(graphQlQuery, cancellationToken);
        response.EnsureNoErrors();
        _logger.LogDebug($"Calling POST /graphql returned no errors");

        return response.Data.Response;
    }
}