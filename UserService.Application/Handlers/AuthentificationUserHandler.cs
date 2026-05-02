using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.UserService.Application.Commands;
using UserService.Domain.Entities;
using UserService.Infrastructure.Jwt.Interfaces;
using UserService.Infrastructure.Persistence;
using UserServiceApplication.Commands;
using UserServiceApplication.Dtos;
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
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            logger.LogWarning("User not found");
            
            throw new KeyNotFoundException();
        }

        logger.LogInformation("Authenticating user | ID: {Id}", user.Id);
        
        // Верифицируем
        var valid = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
        
        if (!valid)
        {
            logger.LogWarning("Invalid password");
            
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

        logger.LogInformation("User authenticated | ID: {ID}", user.Id);
        
        return tokens;
    }
}