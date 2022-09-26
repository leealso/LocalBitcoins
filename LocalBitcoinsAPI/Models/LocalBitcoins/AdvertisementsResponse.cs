using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.LocalBitcoins;

public class AdvertisementsResponse
{
    [JsonProperty("ad_list")]
    public IEnumerable<Advertisement> Advertisements { get; set; }

    [JsonProperty("ad_count")]
    public int AdCount { get; set; }
    
    public AdvertisementsResponse()
    {
        
    }
}