using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TradesWorker.Constants;
using TradesWorker.Utilities;

namespace TradesWorker.Models;

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

    public Trade()
    {
        
    }

    public Trade(LocalBitcoinsTrade localBitcoinsTrade, string currencyCode = Default.CurrencyCode)
    {
        TransactionId = localBitcoinsTrade.TId;
        Date = DateTimeUtility.FromEpoch(localBitcoinsTrade.Date);
        Price = localBitcoinsTrade.Price;
        AmountBtc = localBitcoinsTrade.Amount;
        AmountFiat = Price * AmountBtc;
        CurrencyCode = currencyCode;
    }
}
