namespace UserServiceApplication.Dtos;

/// <summary>
/// Входная модель создания/обновления пользователя
/// </summary>
public record CreateUserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; init; } 

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; init; } 
    
    /// <summary>
    /// Интересные пользователю курсы
    /// </summary>
    public string? Favourites { get; init; }
}