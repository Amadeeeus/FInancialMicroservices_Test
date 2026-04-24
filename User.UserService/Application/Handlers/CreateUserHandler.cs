using MediatR;
using User.UserService.Application.Commands;
using User.UserService.Infrastructure.Persistence;

namespace User.UserService.Application.Handlers;

/// <summary>
/// Хендлер создания пользователя
/// </summary>
public class CreateUserHandler(UserDbContext context, ILogger<CreateUserHandler> logger) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken ct)
    {
        var user = new Domain.Models.User(
            new Guid(),
            request.Name, 
            request.Password,
            request.Favourites!
            );
        
        await context.Users.AddAsync(user, ct);
        
        await context.SaveChangesAsync(ct);
    }
}