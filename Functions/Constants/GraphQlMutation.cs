namespace LocalBitcoins.Functions.Constants;

public static class GraphQlMutation
{
    public const string AddClosedTrades = @"mutation mutation($closedTrades:[ClosedTradeInput!]!){addClosedTrades(closedTrades:$closedTrades){contactId}}";

    public const string AddExchangeRate = @"mutation mutation($closedTrade:ClosedTradeInput!){addClosedTrade(closedTrade:$closedTrade){contactId}}";
}
