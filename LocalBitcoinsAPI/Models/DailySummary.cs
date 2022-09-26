namespace LocalBitcoinsAPI.Models;

public class DailySummary
{
    public DateTime Date { get; set; }

    public int TransactionCount { get; set; }

    public decimal BtcVolume { get; set; }

    public decimal FiatVolume { get; set; }

    public decimal TransactionCountPercentage { get; set; }

    public decimal BtcVolumePercentage { get; set; }

    public decimal FiatVolumePercentage { get; set; }

    public DailySummary()
    {
        
    }

    public DailySummary(DateTime date, IQueryable<Trade> trades, IQueryable<Trade> lastMonthTrades)
    {
        Date = date.Date;
        TransactionCount = trades.Count();
        BtcVolume = trades.Sum(x => x.AmountBtc);
        FiatVolume = trades.Sum(x => x.AmountFiat);
        TransactionCountPercentage = ((decimal)TransactionCount / (lastMonthTrades.Count() / 30)) - 1;
        BtcVolumePercentage = (BtcVolume / lastMonthTrades.Average(x => x.AmountBtc)) - 1;
        FiatVolumePercentage = (FiatVolume / lastMonthTrades.Average(x => x.AmountFiat) / 30) - 1;
    }
}
