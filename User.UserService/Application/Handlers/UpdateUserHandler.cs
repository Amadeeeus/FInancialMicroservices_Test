using MediatR;
using Microsoft.EntityFrameworkCore;
using User.UserService.Application.Commands;
using User.UserService.Infrastructure.Persistence;

namespace User.UserService.Application.Handlers;

/// <summary>
/// Хендлер измененния пользователя
/// </summary>
public class UpdateUserHandler(UserDbContext context, ILogger<CreateUserHandler> logger) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken ct)
    {

        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == request.Id, ct) 
                   ?? throw new Exception("User not found");

        
        user.Update(request.Name, request.Password, request.Favourites);
        
        await context.SaveChangesAsync(ct);
    }
}