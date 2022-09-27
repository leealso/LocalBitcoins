namespace LocalBitcoinsAPI.Models.CoinMarketCap;

public class Response<TResponseType>
{
    public TResponseType Data { get; set; }

    public Response()
    {
        
    }
}

public class ResponseData
{
    public IList<CryptoCurrency> Btc {get;set;}
}