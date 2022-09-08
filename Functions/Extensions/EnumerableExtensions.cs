using System.Collections.Generic;
using System.Threading.Tasks;

namespace LocalBitcoins.Functions.Extensions;

public static class EnumerableExtensions
{
    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(this IEnumerable<T> list)
    {
        foreach (var item in list)
        {
            await Task.Delay(1);
            yield return item;
        }
    }
}