using Newtonsoft.Json;

namespace LocalBitcoinsAPI.LocalBitcoins.Models;

public class Profile
{
    public string Username { get; set; }

    [JsonProperty("trade_count")]
    public string TradeCount { get; set; }

    [JsonProperty("feedback_score")]
    public decimal FeedbackScore { get; set; }

    public string Name { get; set; }

    public Profile()
    {
        
    }
}