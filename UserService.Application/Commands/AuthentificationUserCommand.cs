using MediatR;
using User.UserService.Application.Dtos;
using UserServiceApplication.Dtos;

namespace User.UserService.Application.Commands;

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