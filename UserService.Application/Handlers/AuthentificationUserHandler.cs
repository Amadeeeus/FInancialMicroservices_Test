using MediatR;
using Microsoft.EntityFrameworkCore;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using User.UserService.Domain.Models;
using UserService.Infrastructure.Persistence.Jwt.Interfaces;
using UserService.Infrastructure.Persistence.Persistence;
using UserServiceApplication.Extensions;

namespace UserServiceApplication.Handlers;

/// <summary>
/// Хендлер авторизации
/// </summary>
/// <param name="context"></param>
/// <param name="logger"></param>
public class AuthentificationUserHandler(UserDbContext context,TokensDbContext tokensContext, IJwtTokenGenerator generator,  ILogger<AuthentificationUserHandler> logger) : IRequestHandler<AuthentificationUserCommand,AuthentificationUserOutDto?>
{
    public async Task<AuthentificationUserOutDto?> Handle(AuthentificationUserCommand request, CancellationToken cancellationToken)
    {
        var user = await context.Users
            .Where(x => x.Name == request.Name)
            .FirstAsync(cancellationToken);
        
        //хешируем пароль, который пришел в команде
        var crypted = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        // Верифицируем
        var valid = BCrypt.Net.BCrypt.Verify(crypted, user.Password);
        
        if (!valid)
        {
            return null;
        }
        
        // генерация jwt токенов
        var access = generator.GenerateAccessToken(user);
        var refresh = generator
            .GenerateRefreshToken();

        // добавление refresh в БД
        await tokensContext.Tokens.AddAsync(new TokenEntity
        {
            Id = new Guid(),
            UserId = user.Id,
            RefreshToken =  refresh.HashRefreshToken() //хеширование через экстеншн перед сохранением в базу;
        }, cancellationToken);
        
        await tokensContext.SaveChangesAsync(cancellationToken);

        // возврат токенов
        var tokens = new AuthentificationUserOutDto
        {
            AccessToken = access,
            RefreshToken = refresh
        };
        
        return tokens;
    }
}