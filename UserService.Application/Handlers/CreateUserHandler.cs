using MediatR;
using User.UserService.Application.Commands;
using UserService.Infrastructure.Persistence.Persistence;

namespace UserServiceApplication.Handlers;

/// <summary>
/// Хендлер создания пользователя
/// </summary>
public class CreateUserHandler(UserDbContext context, ILogger<CreateUserHandler> logger) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken ct)
    {
        var crypted = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new User.UserService.Domain.Models.UserEntity(
            new Guid(),
            request.Name, 
            crypted,
            request.Favourites!
            );
        
        await context.Users.AddAsync(user, ct);
        
        await context.SaveChangesAsync(ct);
    }
}