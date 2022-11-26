namespace LocalBitcoins.Functions.Constants;

public static class GraphQlQuery
{
    public const string GetClosedTrades = @"query query { response:closedTrades(take: 25 order: { closedAt: DESC }) { items { contactId } }}";

    public const string GetMaxTransactionId = @"query query { response:trades(take: 1 order: { transactionId: DESC }) { items { transactionId } }}";

    public const string GetMinTransactionId = @"query query { response:trades(take: 1 order: { transactionId: ASC }) { items { transactionId } }}";
}
