using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace LocalBitcoins.Functions.Utilities;

public static class LocalBitcoinsUtility 
{
    public static string GetSignature(string hmacKey, string hmacSecret, string apiCommand, string nonce)
    {
        return GetSignature(hmacKey, hmacSecret, apiCommand, nonce, null);
    }

    public static string GetSignature(string hmacKey, string hmacSecret, string apiCommand, string nonce, Dictionary<string, string> args)
    {
        string paramsStr = null;

        if (args != null && args.Any())
        {
            paramsStr = UrlEncodeParams(args);
        }

        var encoding = new ASCIIEncoding();
        var secretByte = encoding.GetBytes(hmacSecret);
        using (var hmacsha256 = new HMACSHA256(secretByte))
        {
            var message = nonce + hmacKey + apiCommand;
            if (paramsStr != null)
            {
                message += paramsStr;
            }
            var messageByte = encoding.GetBytes(message);

            var signature = ByteToString(hmacsha256.ComputeHash(messageByte));
            return signature;
        }
    }

    private static string UrlEncodeParams(Dictionary<string, string> args)
    {
        var stringBuilder = new StringBuilder();

        var arguments = args
            .Select(x => string.Format(CultureInfo.InvariantCulture, "{0}={1}", UrlEncodeString(x.Key), UrlEncodeString(x.Value)))
            .ToArray();

        stringBuilder.Append(string.Join("&", arguments));
        return stringBuilder.ToString();
    }

    private static string UrlEncodeString(string text)
    {
        return string.IsNullOrEmpty(text)
            ? string.Empty 
            : Uri.EscapeDataString(text).Replace("%20", "+");
    }

    private static string ByteToString(IEnumerable<byte> buffer)
    {
        return buffer.Aggregate("", (current, t) => current + t.ToString("X2", CultureInfo.InvariantCulture));
    }
}
