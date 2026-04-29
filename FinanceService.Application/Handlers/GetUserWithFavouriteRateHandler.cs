using FinanceService.Application.Commands;
using FinanceService.Application.Contracts;
using FinanceService.Application.DTOs;
using FinanceService.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FinanceService.Application.Handlers;

/// <summary>
/// Хендлер получения пользователя с любимыми курсами
/// </summary>
public class GetUserWithFavouriteRateHandler(IUserServiceClient client, ICurrencyDbContext context) : IRequestHandler<GetUserWithFavouriteRateCommand, GetUserWithFavouriteRateOutDto>
{
    public async Task<GetUserWithFavouriteRateOutDto> Handle(GetUserWithFavouriteRateCommand request, CancellationToken ct)
    {
        var favouriteRate = new List<FavouriteRate>();
        var user = await client.GetUserById(request.UserId, ct);
        
        var rates  = user.Favourites?
            .Split(',')
            .Select(x => 
                x.Trim())
            .ToList();
        
        
        foreach (var rate in rates!)
        {
            var favorite = await context.ExchangeRates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == rate, ct);
            
            favouriteRate.Add(new FavouriteRate
            {
                FavouriteRateId = favorite!.Id,
                Name = favorite.Name,
                Rate = rate
            });
        }

        return new GetUserWithFavouriteRateOutDto
        {
            Id = user.Id,
            Name = user.Name,
            FavouriteRates = favouriteRate
        };
    }
}