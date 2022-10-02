using LocalBitcoins.Functions.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using GraphQL.Client.Http;
using LocalBitcoins.Functions.Extensions;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public class LocalBitcoinsApiGraphClient : ILocalBitcoinsApiGraphClient
{
    private readonly GraphQLHttpClient _httpClient;

    private readonly ILogger<LocalBitcoinsApiGraphClient> _logger;

    public LocalBitcoinsApiGraphClient(GraphQLHttpClient httpClient, ILogger<LocalBitcoinsApiGraphClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<TResult> QueryAsync<TResult>(string query, object? variables = null, CancellationToken cancellationToken = default)
    {
        var graphQlQuery = new GraphQLHttpRequest
        {
            Query = query,
            Variables = variables
        };
        _logger.LogDebug($"Calling POST /graphql");
        var response = await _httpClient.SendQueryAsync<GraphQlResponse<TResult>>(graphQlQuery, cancellationToken);
        response.EnsureNoErrors();
        _logger.LogDebug($"Calling POST /graphql returned no errors");

        return response.Data.Response;
    }

    public async Task<TResult> MutationAsync<TResult>(string query, object? variables = null, CancellationToken cancellationToken = default)
    {
        var graphQlQuery = new GraphQLHttpRequest
        {
            Query = query,
            Variables = variables
        };
        _logger.LogDebug($"Calling POST /graphql");
        var response = await _httpClient.SendMutationAsync<GraphQlResponse<TResult>>(graphQlQuery, cancellationToken);
        response.EnsureNoErrors();
        _logger.LogDebug($"Calling POST /graphql returned no errors");

        return response.Data.Response;
    }
}
