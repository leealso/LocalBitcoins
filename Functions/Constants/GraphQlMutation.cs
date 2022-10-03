namespace LocalBitcoins.Functions.Constants;

public static class GraphQlMutation
{
    public const string AddExchangeRate = @"mutation mutation($fromCurrencyCode:String! $toCurrencyCode:String! $date:DateTime! $value:Decimal!){ addExchangeRate(fromCurrencyCode: $fromCurrencyCode toCurrencyCode: $toCurrencyCode date: $date value: $value) { id } }";
}
