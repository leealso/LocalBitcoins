namespace LocalBitcoins.Functions.Constants;

public static class GraphQlQuery
{
    public const string GetLatestContactId = @"query query { response:latestContactId }";

    public const string GetClosedTrades = @"query query($where: ClosedTradeFilterInput){ response:closedTrades(take: 100 order: { closedAt: DESC } where: $where) { items{ contactId }}}";

    public const string GetMaxTransactionId = @"query query { response:trades(take: 1 order: { transactionId: DESC }) { items { transactionId } }}";

    public const string GetMinTransactionId = @"query query { response:trades(take: 1 order: { transactionId: ASC }) { items { transactionId } }}";
}
