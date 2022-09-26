using Newtonsoft.Json;

namespace LocalBitcoinsAPI.LocalBitcoins.Models;

public class Action
{
    [JsonProperty("public_view")]
    public string PublicView { get; set; }
    
    public Action()
    {
        
    }
}