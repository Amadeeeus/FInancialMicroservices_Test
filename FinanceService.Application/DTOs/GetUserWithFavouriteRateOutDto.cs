using FinanceService.Application.Models;

namespace FinanceService.Application.DTOs;

/// <summary>
/// Выходная модель пользователя с курсами
/// </summary>
public record GetUserWithFavouriteRateOutDto
{
    /// <summary>
    /// Id пользователя 
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; init; } = string.Empty;
    
    /// <summary>
    /// Любимые курсы
    /// </summary>
    public List<FavouriteRate>?  FavouriteRates { get; set; }
}