using System.Threading.Tasks;
using System.Threading;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public interface ILocalBitcoinsApiGraphClient
{
    Task<TResult> QueryAsync<TResult>(string query, object? variables, CancellationToken cancellationToken = default);

    Task<TResult> QueryAsync<TResult>(string query, CancellationToken cancellationToken = default)
        => QueryAsync<TResult>(query, default, cancellationToken);

    Task<TResult> MutationAsync<TResult>(string query, object? variables = null, CancellationToken cancellationToken = default)
        => QueryAsync<TResult>(query, variables, cancellationToken);
}
