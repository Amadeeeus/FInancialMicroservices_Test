using MediatR;

namespace User.UserService.Application.Commands;

/// <summary>
/// Команда изменения/создания пользователя
/// </summary>
public record CreateUserCommand :  IRequest
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