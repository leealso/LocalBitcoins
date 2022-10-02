using System;
using System.Xml.Serialization;

namespace LocalBitcoins.Functions.Models;

public class Indicator
{
    [XmlElement("COD_INDICADORINTERNO")]
    public int Code { get; set; }

    [XmlElement("DES_FECHA")]
    public DateTime Date { get; set; }

    [XmlElement("NUM_VALOR")]
    public decimal Value { get; set; }

    public Indicator()
    {
        
    }
}
