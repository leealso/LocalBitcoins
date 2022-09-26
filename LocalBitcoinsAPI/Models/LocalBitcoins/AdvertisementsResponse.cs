using Newtonsoft.Json;

namespace LocalBitcoinsAPI.LocalBitcoins.Models;

public class AdvertisementsResponse
{
    [JsonProperty("ad_list")]
    public IList<Advertisement> Advertisements { get; set; }

    [JsonProperty("ad_count")]
    public int AdCount { get; set; }
    
    public AdvertisementsResponse()
    {
        
    }
}