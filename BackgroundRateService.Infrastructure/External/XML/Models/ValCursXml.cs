using System.Xml.Serialization;

namespace BackgroundRateService.Infrastructure.External.XML.Models;

/// <summary>
/// Проекция Xml на сущнос
/// </summary>
[XmlRoot("ValCurs")]
public class ValCursXml
{
    [XmlElement("Valute")]
    public List<ValuteXml> Valutes { get; set; } = new();
}