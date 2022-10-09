using System;
using Newtonsoft.Json;

namespace LocalBitcoins.Functions.Models;

public class LocalBitcoinsContactData
{
    [JsonProperty("contact_id")]
    public int ContactId { get; set; }
    
    public LocalBitcoinsUser Buyer { get; set; }

    public LocalBitcoinsUser Seller { get; set; }

    [JsonProperty("is_buying")]
    public bool IsBuying { get; set; }

    [JsonProperty("is_selling")]
    public bool IsSelling { get; set; }

    public string Currency { get; set; }

    public decimal Amount { get; set; }

    [JsonProperty("amount_btc")]
    public decimal AmountBtc { get; set; }

    [JsonProperty("fee_btc")]
    public decimal FeeBtc { get; set; }

    [JsonProperty("closed_at")]
    public DateTime ClosedAt { get; set; }
}
