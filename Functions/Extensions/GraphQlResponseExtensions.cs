using System.Linq;
using System.Net.Http;
using GraphQL;

namespace LocalBitcoins.Functions.Extensions;

public static class GraphQlResponseExtensions
{
    public static void EnsureNoErrors<TResult>(this GraphQLResponse<TResult> graphQlResponse)
    {
        if (graphQlResponse.Errors != null && graphQlResponse.Errors.Any())
            throw new HttpRequestException("Unhandled GraphQL exception");
    }
}