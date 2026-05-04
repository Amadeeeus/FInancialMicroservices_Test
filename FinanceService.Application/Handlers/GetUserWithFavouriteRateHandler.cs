using System.Globalization;
using FinanceService.Application.Commands;
using FinanceService.Application.Contracts;
using FinanceService.Application.DTOs;
using FinanceService.Application.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FinanceService.Application.Handlers;

/// <summary>
/// Хендлер получения пользователя с любимыми курсами
/// </summary>
public class GetUserWithFavouriteRateHandler(IUserServiceClient client, ICurrencyDbContext context, ILogger<GetUserWithFavouriteRateHandler> logger) : IRequestHandler<GetUserWithFavouriteRateCommand, GetUserWithFavouriteRateOutDto>
{
    public async Task<GetUserWithFavouriteRateOutDto> Handle(GetUserWithFavouriteRateCommand request, CancellationToken ct)
    {
        logger.LogInformation("Getting favourite rates | UserId: {UserId}", request.UserId);
        
        var favouriteRate = new List<FavouriteRate>();
        var user = await client.GetUserById(request.UserId, ct);

        // if (user.Content is null)
        // {
        //     logger.LogWarning("User not found in UserService | UserId: {UserId}", request.UserId);
        //
        //     throw new Exception("User not found in UserService ");
        // }

        logger.LogCritical("{rates}",  user.Content.Favourites);
        
        var rates  = user.Content?.Favourites?
            .Split(',')
            .Select(x => 
                x.Trim())
            .ToList();
        
        logger.LogCritical("{rates}",  rates);
        
        foreach (var rate in rates!)
        {
            var favorite = await context.ExchangeRates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Name == rate, ct);

            if (favorite is null)
            {
                logger.LogWarning("Rates not found in ExchangeRates");
                continue;
            }

            favouriteRate.Add(new FavouriteRate
            {
                FavouriteRateId = favorite!.Id,
                Name = favorite.Name,
                Rate = favorite.Rate.ToString(CultureInfo.InvariantCulture)
            });
        }

        logger.LogInformation("Favourite rates retrieved | UserId: {UserId} Count: {Count}", request.UserId, favouriteRate.Count);
        
        return new GetUserWithFavouriteRateOutDto
        {
            Id = user.Content!.Id,
            Name = user.Content.Name,
            FavouriteRates = favouriteRate
        };
    }
}