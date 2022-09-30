using LocalBitcoins.Functions.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public interface ILocalBitcoinsApiGraphClient
{
    Task<TResult> QueryAsync<TResult>(string query, object? variables = null, CancellationToken cancellationToken = default);

    Task<TResult> MutationAsync<TResult>(string query, object? variables = null, CancellationToken cancellationToken = default);
}
