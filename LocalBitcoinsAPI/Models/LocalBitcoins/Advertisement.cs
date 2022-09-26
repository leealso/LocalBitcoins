using Newtonsoft.Json;

namespace LocalBitcoinsAPI.LocalBitcoins.Models;

public class Advertisement
{
    public AdvertisementData Data { get; set; }

    public Action Actions { get; set; }

    public Advertisement()
    {
        
    }
}