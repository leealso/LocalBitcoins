using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.LocalBitcoins;

public class Action
{
    [JsonProperty("public_view")]
    public string PublicView { get; set; }
    
    public Action()
    {
        
    }
}