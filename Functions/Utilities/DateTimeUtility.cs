using System;

namespace LocalBitcoins.Functions.Utilities;

public static class DateTimeUtility 
{
    public static DateTime FromEpoch(long epoch) 
    {
        DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(epoch);
        return dateTimeOffset.DateTime;
    }
}