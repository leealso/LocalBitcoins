using System;
using TradeTypeConstant = LocalBitcoins.Functions.Constants.TradeType;

namespace LocalBitcoins.Functions.Models;

public class ClosedTrade
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

    public ClosedTrade()
    {
        
    }

    public ClosedTrade(LocalBitcoinsContactData localBitcoinsContact)
    {
        ContactId = localBitcoinsContact.ContactId;
        Buyer = localBitcoinsContact.Buyer.Username;
        Seller = localBitcoinsContact.Seller.Username;
        TradeType = localBitcoinsContact.IsBuying
            ? TradeTypeConstant.Buy
            : TradeTypeConstant.Sell;
        CurrencyCode = localBitcoinsContact.Currency;
        AmountBtc = localBitcoinsContact.AmountBtc;
        AmountFiat = localBitcoinsContact.Amount;
        FeeBtc = localBitcoinsContact.FeeBtc;
        ClosedAt = localBitcoinsContact.ClosedAt;
    }
}
