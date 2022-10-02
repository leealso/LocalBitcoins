using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LocalBitcoins.Functions.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<TResult> DeserializeXmlAsync<TResult>(this HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var xmlSerializer = new XmlSerializer(typeof(TResult));
        using var xmlReader = new StringReader(FormatXml(responseContent));
        return (TResult)xmlSerializer.Deserialize(xmlReader);
    }

    private static string FormatXml(string xmlString)
    {
        return xmlString.Replace("<string xmlns=\"http://ws.sdde.bccr.fi.cr\">", string.Empty)
            .Replace("</string>", string.Empty)
            .Replace("&lt;", "<")
            .Replace("&gt;", ">");
    }
}