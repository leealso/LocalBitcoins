using Newtonsoft.Json;

namespace LocalBitcoinsAPI.Models.LocalBitcoins;

public class Advertisement
{
    public AdvertisementData Data { get; set; }

    public Action Actions { get; set; }

    public Advertisement()
    {
        
    }
}