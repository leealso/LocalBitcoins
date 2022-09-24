namespace LocalBitcoinsAPI.Models;

public class DailySummary
{
    public DateTime Date { get; set; }

    public int TransactionCount { get; set; }

    public decimal BtcVolume { get; set; }

    public decimal FiatVolume { get; set; }

    public DailySummary()
    {
        
    }
}
