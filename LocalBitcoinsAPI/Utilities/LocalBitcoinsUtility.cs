namespace LocalBitcoinsAPI.Utilities;

public static class LocalBitcoinsUtility
{
    public static string GetCountryName(string countryCode)
    {
        switch (countryCode)
        {
            case "crc":
                return "costa-rica";
            default:
            return string.Empty;
        }
    }
}
