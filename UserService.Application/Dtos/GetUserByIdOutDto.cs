namespace UserServiceApplication.Dtos;

/// <summary>
/// Выходной контракт пользователя по Id
/// </summary>
/// <param name="id">Id пользователя</param>
/// <param name="name">Имя</param>
/// <param name="favourites">Любимые курсы</param>
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