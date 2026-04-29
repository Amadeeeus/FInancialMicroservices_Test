namespace FinanceService.Domain.Entities;

/// <summary>
/// Сущность курса
/// </summary>
public class ExchangeRateEntity
{
    /// <summary>
    /// Id курса
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название курса
    /// </summary>
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Значение курса
    /// </summary>
    public decimal Rate { get; set; }
}