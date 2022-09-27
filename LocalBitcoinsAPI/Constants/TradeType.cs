namespace LocalBitcoinsAPI.Constants;

public static class TradeType
{
    public const string Buy = "BUY";

    public const string Sell = "SELL";

    public static bool IsBuy(string tradeType) => Buy.Equals(tradeType.ToUpper());

    public static bool IsSell(string tradeType) => Sell.Equals(tradeType.ToUpper());
}
