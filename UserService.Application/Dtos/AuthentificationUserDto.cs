namespace User.UserService.Application.Dtos;

public record AuthentificationUserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; init; } 

    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; init; } 
}