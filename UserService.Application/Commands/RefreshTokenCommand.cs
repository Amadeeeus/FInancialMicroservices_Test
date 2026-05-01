using MediatR;
using UserServiceApplication.Dtos;

namespace User.UserService.Application.Commands;

/// <summary>
/// Команда с поиском токена в БД для замены
/// </summary>
public record RefreshTokenCommand : IRequest<AuthentificationUserOutDto>
{
    public required string RefreshToken { get; init; }
}
