using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.UserService.Application.Commands;
using UserService.Infrastructure.Persistence;

namespace UserServiceApplication.Handlers;

/// <summary>
/// Хендлер измененния пользователя
/// </summary>
public class UpdateUserHandler(UserDbContext context, ILogger<UserServiceApplication.Handlers.CreateUserHandler> logger) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken ct)
    {

        var user = await context.Users
                       .FirstOrDefaultAsync(x => x.Id == request.Id, ct) 
                   ?? throw new Exception("User not found");

        
        user.Update(request.Name, request.Password, request.Favourites);
        
        await context.SaveChangesAsync(ct);
    }
}