namespace LocalBitcoins.Functions.Constants;

public static class GraphQlQuery
{
    public const string GetLatestContactId = @"query query { response:latestContactId }";

    public const string GetMaxClosedAt = @"query query { response:closedTrades(take: 1 order: { closedAt: DEC }) { items { closedAt } }}";

    public const string GetMinClosedAt = @"query query { response:closedTrades(take: 1 order: { closedAt: ASC }) { items { closedAt } }}";

    public const string GetLatestTransactionDate = @"query query { response:trades(take: 1 order: { date: DESC }) { items { date } }}";

    public const string GetMaxTransactionId = @"query query { response:trades(take: 1 order: { transactionId: DESC }) { items { transactionId } }}";

    public const string GetMinTransactionId = @"query query { response:trades(take: 1 order: { transactionId: ASC }) { items { transactionId } }}";
}
