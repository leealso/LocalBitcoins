using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace LocalBitcoins.Functions.Models;

[Serializable]
public class BccrIndicatorResponse
{
    [XmlArray("Datos_de_INGC011_CAT_INDICADORECONOMIC")] 
    [XmlArrayItem("INGC011_CAT_INDICADORECONOMIC")]
    public IList<Indicator> Data { get; set; }
}
