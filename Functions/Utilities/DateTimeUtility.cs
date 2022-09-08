using System;

namespace LocalBitcoins.Functions.Utilities;

public static class DateTimeUtility 
{
    public static DateTime FromEpoch(long epoch) 
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epoch);
        return dateTimeOffset.DateTime;
    }

    public static string GetNonce()
    {
        var nonce = (long)((DateTime.UtcNow - DateTime.UnixEpoch).TotalMilliseconds * 1000);
        return nonce.ToString();
    }
}