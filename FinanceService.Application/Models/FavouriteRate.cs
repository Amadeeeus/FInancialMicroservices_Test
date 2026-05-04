namespace FinanceService.Application.Models;

/// <summary>
/// Курс валют
/// </summary>
public class FavouriteRate
{
    /// <summary>
    /// Id курса
    /// </summary>
    public Guid FavouriteRateId { get; set; }
    
    /// <summary>
    /// Название курса
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Значение курса
    /// </summary>
    public string Rate { get; set; } = string.Empty;
}