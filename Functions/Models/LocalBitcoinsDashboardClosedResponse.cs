using System.Collections.Generic;
using Newtonsoft.Json;

namespace LocalBitcoins.Functions.Models;

public class LocalBitcoinsDashboardClosedResponse
{
    [JsonProperty("contact_list")]
    public IList<LocalBitcoinsContact> ContactList { get; set; }
    
    [JsonProperty("contact_count")]
    public int ContactCount { get; set; }
}
