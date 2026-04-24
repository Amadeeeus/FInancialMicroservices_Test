using MediatR;

namespace User.UserService.Application.Commands;

/// <summary>
/// Команда изменения/создания пользователя
/// </summary>
public record UpdateUserCommand :  IRequest
{
    /// <summary>
    /// Id пользователя 
    /// </summary>
    public required Guid Id { get; init; }
    
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