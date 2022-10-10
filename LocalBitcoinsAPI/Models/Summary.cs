namespace LocalBitcoinsAPI.Models;

public class Summary
{
    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TransactionCount { get; set; }

    public decimal BtcVolume { get; set; }

    public decimal FiatVolume { get; set; }

    public decimal Price => BtcVolume <= 0 
        ? 0 
        : Math.Round(FiatVolume / BtcVolume, 2);

    public int ClosedTransactionCount { get; set; }

    public decimal ClosedBtcVolume { get; set; }

    public decimal ClosedFiatVolume { get; set; }

    public decimal ClosedPrice => ClosedBtcVolume <= 0 
        ? 0 
        : Math.Round((decimal)ClosedFiatVolume / ClosedBtcVolume, 2);

    public decimal ClosedTransactionCountPercentage => TransactionCount <= 0 
        ? 0 
        : Math.Round((decimal)ClosedTransactionCount / TransactionCount, 4);

    public decimal ClosedBtcVolumePercentage => BtcVolume <= 0 
        ? 0 
        : Math.Round(ClosedBtcVolume / BtcVolume, 4);

    public decimal ClosedFiatVolumePercentage => FiatVolume <= 0 
        ? 0 
        : Math.Round(ClosedFiatVolume / FiatVolume, 4);

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
