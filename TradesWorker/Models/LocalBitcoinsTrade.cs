namespace TradesWorker.Models;

public record LocalBitcoinsTrade
{
    public int TId { get; set; }

    public long Date { get; set; }

    public decimal Price { get; set; }

    public decimal Amount { get; set; }

    public LocalBitcoinsTrade(int tId, long date, decimal price, decimal amount) =>
        (TId, Date, Price, Amount) = (tId, date, price, amount);
}
