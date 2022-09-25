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

    public DailySummary(DateTime date, IQueryable<Trade> trades, IQueryable<Trade> yesterdayTrades)
    {
        Date = date.Date;
        TransactionCount = trades.Count();
        BtcVolume = trades.Sum(x => x.AmountBtc);
        FiatVolume = trades.Sum(x => x.AmountFiat);
        TransactionCountPercentage = ((decimal)TransactionCount / yesterdayTrades.Count()) - 1;
        BtcVolumePercentage = (BtcVolume / yesterdayTrades.Sum(x => x.AmountBtc)) - 1;
        FiatVolumePercentage = (FiatVolume / yesterdayTrades.Sum(x => x.AmountFiat)) - 1;
    }
}
