using System.Collections.Generic;
using System.Xml.Serialization;

namespace LocalBitcoins.Functions.Models;

[XmlRoot(ElementName = "Datos_de_INGC011_CAT_INDICADORECONOMIC")]
public class BccrIndicatorResponse
{
    [XmlElement(ElementName = "INGC011_CAT_INDICADORECONOMIC")]
    public List<BccrIndicator> Indicators { get; set; }
}
