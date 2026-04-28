using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using User.UserService.Application.Dtos;
using User.UserService.Application.Queries;
using UserService.Infrastructure.Persistence;

namespace UserServiceApplication.Handlers;

public class GetUserByIdHandler(UserDbContext context, ILogger<GetUserByIdHandler> logger) : IRequestHandler<GetUserByIdQuery, UserDto>
{
    public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        return (await context.Users
            .AsNoTracking()
            .Where(x => x.Id == request.UserId)
            .Select(x => new UserDto(
                x.Id, 
                x.Name, 
                x.Password,
                x.Favourites))
            .FirstOrDefaultAsync(ct))!;
    }
}