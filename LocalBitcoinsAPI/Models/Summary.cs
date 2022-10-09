namespace LocalBitcoinsAPI.Models;

public class Summary
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TransactionCount { get; set; }

    public decimal BtcVolume { get; set; }

    public decimal FiatVolume { get; set; }

    public decimal Price => FiatVolume / BtcVolume;

    public int ClosedTransactionCount { get; set; }

    public decimal ClosedBtcVolume { get; set; }

    public decimal ClosedFiatVolume { get; set; }

    public decimal ClosedPrice => ClosedFiatVolume / ClosedBtcVolume;

    public int ClosedTransactionCountPercentage => ClosedTransactionCount / TransactionCount;

    public decimal ClosedBtcVolumePercentage => ClosedBtcVolume / BtcVolume;

    public decimal ClosedFiatVolumePercentage => ClosedFiatVolume / FiatVolume;

    public Summary()
    {
        
    }

    public Summary(DateTime startDate, DateTime endDate, IQueryable<Trade> trades)
    {
        StartDate = startDate;
        EndDate = endDate;
        TransactionCount = trades.Count();
        BtcVolume = trades.Sum(x => x.AmountBtc);
        FiatVolume = trades.Sum(x => x.AmountFiat);
        var closedTrades = trades.Where(c => c.ContactId.HasValue);
        ClosedTransactionCount = closedTrades.Count();
        ClosedBtcVolume = closedTrades.Sum(x => x.AmountBtc);
        ClosedFiatVolume = closedTrades.Sum(x => x.AmountFiat);
    }
}
