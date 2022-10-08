using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalBitcoinsAPI.Models;

[Table("trades")]
public class Trade
{
    [Key]
    [Column("tid")]
    public int TransactionId { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("price")]
    public decimal Price { get; set; }

    [Column("amount_btc")]
    public decimal AmountBtc { get; set; }

    [Column("amount_fiat")]
    public decimal AmountFiat { get; set; }

    [Column("currency_code")]
    public string CurrencyCode { get; set; }

    [Column("contact_id")]
    public int ContactId { get; set; }

    public Trade()
    {
        
    }
}
