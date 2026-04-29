namespace FinanceService.Application.DTOs;

/// <summary>
/// Входная модель получения пользователя с любимой валютой
/// </summary>
public record GetUserWithFavouriteRateDto
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }
}