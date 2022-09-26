namespace LocalBitcoinsAPI.Models.LocalBitcoins;

public class Response<TResponseType>
{
    public TResponseType Data { get; set; }

    public Response()
    {
        
    }
}