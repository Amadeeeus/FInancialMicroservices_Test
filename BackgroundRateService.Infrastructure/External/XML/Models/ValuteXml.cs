namespace BackgroundRateService.Infrastructure.External.XML.Models;

/// <summary>
/// Сущность курса валют
/// </summary>
public class ValuteXml
{
    /// <summary>
    /// Название валюты
    /// </summary>
    public string Name { get; set; } =  string.Empty;
    
    /// <summary>
    /// Номинал
    /// </summary>
    public int Nominal { get; set; }
    
    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; set; } = string.Empty;
}