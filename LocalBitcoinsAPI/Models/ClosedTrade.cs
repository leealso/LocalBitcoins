using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalBitcoinsAPI.Models;

[Table("closed_trades")]
public class ClosedTrade
{
    [Key]
    [Column("contact_id")]
    public int ContactId { get; set; }

    [Column("buyer")]
    public string Buyer { get; set; }

    [Column("seller")]
    public string Seller { get; set; }

    [Column("trade_type")]
    public string TradeType { get; set; }

    [Column("currency_code")]
    public string CurrencyCode { get; set; }

    [Column("amount_btc")]
    public decimal AmountBtc { get; set; }

    [Column("amount_fiat")]
    public decimal AmountFiat { get; set; }

    [Column("fee_btc")]
    public decimal FeeBtc { get; set; }

    [Column("closed_at")]
    public DateTime ClosedAt { get; set; }

    public ClosedTrade()
    {
        
    }
}
