using System.Globalization;
using System.Text;
using System.Xml.Serialization;
using BackgroundRateService.Infrastructure.External.Interfaces;
using BackgroundRateService.Infrastructure.External.XML.Models;

namespace BackgroundRateService.Infrastructure.External;

/// <summary>
/// Провайдер получения курсов
/// </summary>
public class ExchangeRateProvider(HttpClient client) : IExchangeRateProvider
{
    public async Task<Dictionary<string, decimal>> GetExchangeRatesAsync(CancellationToken ct)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        var stream = await client.GetStreamAsync("http://www.cbr.ru/scripts/XML_daily.asp", ct);

        var serializer = new XmlSerializer(typeof(ValCursXml));
        var result = (ValCursXml)serializer.Deserialize(stream)!;

        var culture = new CultureInfo("ru-RU");

        return result.Valutes.ToDictionary(
            v => v.Name, v =>
            {
                var value = decimal.Parse(v.Value, culture);
                return value / v.Nominal;
            }
        );
    }
}