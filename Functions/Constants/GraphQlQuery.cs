namespace LocalBitcoins.Functions.Constants;

public static class GraphQlQuery
{
    public const string GetLatestContactId = @"query query{ response:latestContactId }";

    public const string GetLatestTransactionId = @"query query{ response:latestTransactionId }";
}
