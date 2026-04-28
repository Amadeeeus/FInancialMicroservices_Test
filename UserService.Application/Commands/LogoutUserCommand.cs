using MediatR;

namespace User.UserService.Application.Commands;

/// <summary>
/// Команда на удаление из БД токена
/// </summary>
public record LogoutUserCommand : IRequest
{
    public required string RefreshToken { get; init; }
}