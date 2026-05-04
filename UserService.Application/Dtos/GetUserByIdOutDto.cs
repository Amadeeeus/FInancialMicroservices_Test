namespace UserServiceApplication.Dtos;

/// <summary>
/// Выходной контракт пользователя по Id
/// </summary>
/// <param name="Id">Id пользователя</param>
/// <param name="Name">Имя</param>
/// <param name="Favourites">Любимые курсы</param>
public record GetUserByIdOutDto(Guid Id, string Name, string? Favourites)
{
    /// <summary>
    /// Id пользователя 
    /// </summary>
    public Guid Id { get; init; } = Id;

    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; init; } = Name;

    /// <summary>
    /// Интересные пользователю курсы
    /// </summary>
    public string? Favourites { get; init; } = Favourites;
}