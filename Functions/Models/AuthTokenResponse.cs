using Newtonsoft.Json;

namespace LocalBitcoins.Functions.Models;

public class AuthTokenResponse
{
    [JsonProperty("access_token")]
    public string AccessToken { get; set; }
}
