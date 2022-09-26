namespace LocalBitcoinsAPI.Utilities;

public static class LocalBitcoinsUtility
{
    public static string GetCountryName(string countryCode)
    {
        switch (countryCode.ToLower())
        {
            case "cr":
                return "costa-rica";
            default:
            return string.Empty;
        }
    }
}
