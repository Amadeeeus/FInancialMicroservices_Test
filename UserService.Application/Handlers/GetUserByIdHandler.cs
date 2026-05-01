using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Infrastructure.Persistence;
using UserServiceApplication.Dtos;
using UserServiceApplication.Queries;

namespace UserServiceApplication.Handlers;

public class GetUserByIdHandler(UserDbContext context, ILogger<GetUserByIdHandler> logger) : IRequestHandler<GetUserByIdQuery, GetUserByIdOutDto>
{
    public async Task<GetUserByIdOutDto> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        logger.LogInformation("Getting user | UserId: {UserId}", request.UserId);
        
        return (await context.Users
            .AsNoTracking()
            .Where(x => x.Id == request.UserId)
            .Select(x => new GetUserByIdOutDto(
                x.Id, 
                x.Name, 
                x.Favourites))
            .FirstOrDefaultAsync(ct))!;
    }
}