namespace LocalBitcoinsAPI.Models;

public class ClosedTradeInput
{
    public int ContactId { get; set; }

    public string Buyer { get; set; }

    public string Seller { get; set; }

    public string TradeType { get; set; }

    public string CurrencyCode { get; set; }

    public decimal AmountBtc { get; set; }

    public decimal AmountFiat { get; set; }

    public decimal FeeBtc { get; set; }

    public DateTime ClosedAt { get; set; }
}
