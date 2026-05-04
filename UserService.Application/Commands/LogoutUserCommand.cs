using MediatR;

namespace UserServiceApplication.Commands;

/// <summary>
/// Команда на удаление из БД токена
/// </summary>
public record LogoutUserCommand : IRequest
{
    public required string RefreshToken { get; init; }
}