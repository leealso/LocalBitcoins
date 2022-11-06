using System.Threading.Tasks;
using System.Threading;

namespace LocalBitcoins.Functions.Infrastructure.HttpClients;

public interface IAuthHttpClient
{
    Task<string> GetAccessTokenAsync(CancellationToken cancellationToken = default);
}
