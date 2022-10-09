using System.Collections.Generic;

namespace LocalBitcoins.Functions.Models;

public class GraphQlPagination<TType>
{
    public IList<TType> Items { get; set; }
}
