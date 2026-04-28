using MediatR;
using User.UserService.Application.Commands;
using UserService.Infrastructure.Persistence.Persistence;
using UserServiceApplication.Extensions;

namespace UserServiceApplication.Handlers;

/// <summary>
/// Хендлер logout
/// </summary>
/// <param name="context">Контекст БД токена</param>
/// <param name="logger">Логирование</param>
public class LogoutUserHandler(TokensDbContext context, ILogger<LogoutUserHandler> logger) : IRequestHandler<LogoutUserCommand>
{
     public async Task Handle(LogoutUserCommand request, CancellationToken ct)
     {
         //Обращаемся к методам extensions
         var hash = request.RefreshToken.HashRefreshToken();
         
         var token = await context.GetValidRefreshToken(hash, ct);

         if (token != null)
         {
             token.IsRevoked = true;
             token.Revoked = DateTime.UtcNow;
             
             await context.SaveChangesAsync(ct);
         }
     }
}