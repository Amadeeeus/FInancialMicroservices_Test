namespace FinanceService.Application.DTOs;

public class GetUserByIdOutDto(Guid id, string name, string? favourites)
{
    /// <summary>
    /// Id пользователя 
    /// </summary>
    public Guid Id { get; init; } = id;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; init; } = name;

    /// <summary>
    /// Интересные пользователю курсы
    /// </summary>
    public string? Favourites { get; init; } = favourites;
}