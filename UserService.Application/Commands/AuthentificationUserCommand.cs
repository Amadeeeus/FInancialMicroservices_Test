using MediatR;
using UserServiceApplication.Dtos;

namespace UserServiceApplication.Commands;

/// <summary>
/// Команда авторизации пользователя
/// </summary>
public record AuthentificationUserCommand : IRequest<AuthentificationUserOutDto?>
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