namespace LocalBitcoins.Functions.Constants;

public static class GraphQlMutation
{
    public const string AddClosedTrades = @"mutation mutation($closedTrades:[ClosedTradeInput!]!){ response:addClosedTrades(closedTrades:$closedTrades) { contactId } }";

    public const string AddExchangeRate = @"mutation mutation($fromCurrencyCode:String! $toCurrencyCode:String! $date:DateTime! $value:Decimal!){ response:addExchangeRate(fromCurrencyCode: $fromCurrencyCode toCurrencyCode: $toCurrencyCode date: $date value: $value) { id } }";
}
