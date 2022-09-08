namespace LocalBitcoins.Functions.Models;

public class LocalBitcoinsResponse<TResponseType>
{
    public TResponseType Data { get; set; }
    
    public LocalBitcoinsPagination Pagination { get; set; }
}
