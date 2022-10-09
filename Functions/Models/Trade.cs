using System;
using CurrencyCodeConstant = LocalBitcoins.Functions.Constants.CurrencyCode;
using LocalBitcoins.Functions.Utilities;

namespace LocalBitcoins.Functions.Models;

public class Trade
{
    public int TransactionId { get; set; }

    public DateTime Date { get; set; }

    public decimal Price { get; set; }

    public decimal AmountBtc { get; set; }

    public decimal AmountFiat { get; set; }

    public string CurrencyCode { get; set; }

    public int? ContactId { get; set; }

    public Trade(LocalBitcoinsTrade localBitcoinsTrade, string currencyCode = CurrencyCodeConstant.CRC)
    {
        TransactionId = localBitcoinsTrade.TId;
        Date = DateTimeUtility.FromEpoch(localBitcoinsTrade.Date);
        Price = localBitcoinsTrade.Price;
        AmountBtc = localBitcoinsTrade.Amount;
        AmountFiat = Price * AmountBtc;
        CurrencyCode = currencyCode;
        ContactId = null;
    }
}
