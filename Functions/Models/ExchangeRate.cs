using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalBitcoins.Functions.Models;

[Table("exchange_rates")]
public class ExchangeRate
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("from_currency_id")]
    public int FromCurrencyId { get; set; }

    [Column("to_currency_id")]
    public int ToCurrencyId { get; set; }

    [Column("exchange_rate")]
    public decimal Value { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    public ExchangeRate()
    {
        
    }
}
