using LocalBitcoinsAPI.Models;
using LocalBitcoinsAPI.Services;
using Microsoft.Extensions.Caching.Memory;

namespace LocalBitcoinsAPI.Extensions;

public static class CacheEntryExtensions
{
    public static void SetExpiration(this ICacheEntry entry, int absoluteExpirationMinutes, int slidingExpirationMinutes)
    {
        entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteExpirationMinutes));
        entry.SetSlidingExpiration(TimeSpan.FromMinutes(slidingExpirationMinutes));
    }
}