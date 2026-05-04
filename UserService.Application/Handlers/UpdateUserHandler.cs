using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Infrastructure.Persistence;
using UserServiceApplication.Commands;

namespace UserServiceApplication.Handlers;

/// <summary>
/// Хендлер измененния пользователя
/// </summary>
public class UpdateUserHandler(UserDbContext context, ILogger<UserServiceApplication.Handlers.CreateUserHandler> logger) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken ct)
    {
        logger.LogInformation("Updating user | UserId: {UserId}", request.Id);
        
        var user = await context.Users
                       .FirstOrDefaultAsync(x => x.Id == request.Id, ct) 
                   ?? throw new Exception("User not found");

        
        user.Update(request.Name, request.Password, request.Favourites);
        
        logger.LogInformation("User updated | UserId: {UserId}", request.Id);
        
        await context.SaveChangesAsync(ct);
    }
}