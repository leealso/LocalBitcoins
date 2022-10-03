namespace LocalBitcoinsAPI.Constants;

public static class CurrencyCode
{
    public const string CRC = "CRC";

    public const string USD = "USD";

    public static bool IsCrc(string currencyCode) => CRC.Equals(currencyCode.ToUpper());

    public static bool IsUsd(string currencyCode) => USD.Equals(currencyCode.ToUpper());
}
