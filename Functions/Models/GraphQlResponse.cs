using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TradeTypeConstant = LocalBitcoins.Functions.Constants.TradeType;

namespace LocalBitcoins.Functions.Models;

public class GraphQlResponse<TResponse>
{
    public TResponse Response { get;set; }
}
