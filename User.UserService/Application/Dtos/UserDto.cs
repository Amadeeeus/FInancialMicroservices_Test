namespace User.UserService.Application.Dtos;

/// <summary>
/// Выходной DTO пользователя
/// </summary>
public class UserDto(Guid id, string name, string password, string? favourites)
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
    /// Пароль
    /// </summary>
    public string Password { get; init; } = password;

    /// <summary>
    /// Интересные пользователю курсы
    /// </summary>
    public string? Favourites { get; init; } = favourites;
}