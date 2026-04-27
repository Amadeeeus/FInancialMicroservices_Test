using MediatR;
using Microsoft.EntityFrameworkCore;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using User.UserService.Application.Extensions;
using User.UserService.Domain.Models;
using User.UserService.Infrastructure.Jwt.Interfaces;
using User.UserService.Infrastructure.Persistence;

namespace User.UserService.Application.Handlers;
/// <summary>
/// Хендлер замены токена
/// </summary>
/// <param name="context">Контекст БД токена</param>
/// <param name="userContext">Контекст БД пользователя</param>
/// <param name="generator">Генератор токенов</param>
/// <param name="logger">Логирование</param>
/// <returns>Access и Refresh токены</returns>>
public class RefreshTokenHandler(TokensDbContext context, UserDbContext userContext, IJwtTokenGenerator generator, ILogger<RefreshTokenHandler> logger) : IRequestHandler<RefreshTokenCommand,  AuthentificationUserOutDto>
{
    public async Task<AuthentificationUserOutDto> Handle(RefreshTokenCommand request, CancellationToken ct)
    {
        var hash = request.RefreshToken.HashRefreshToken();
         
        var token = await context.GetValidRefreshToken(hash, ct);
        
        if (token != null)
        {
            token.IsRevoked = true;
            token.Revoked = DateTime.UtcNow;
             
            await context.SaveChangesAsync(ct);
        }

        // Получаем пользователя по Id из БД users
        var user = await userContext.Users.FirstOrDefaultAsync(x => x.Id == token!.UserId, ct);
        
        //генерируем токены
        var access = generator.GenerateAccessToken(user);
        var refresh = generator
            .GenerateRefreshToken();

        //Добавляем новый токен в бд
        await context.Tokens.AddAsync(new TokenEntity
        {
            Id = new Guid(),
            UserId = user!.Id,
            RefreshToken = refresh.HashRefreshToken(),
        }, ct);

        return new AuthentificationUserOutDto
        {
            AccessToken = access,
            RefreshToken = refresh
        };
    }
}