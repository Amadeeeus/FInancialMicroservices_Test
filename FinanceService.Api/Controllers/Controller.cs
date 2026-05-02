using FinanceService.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FinanceService.Api.Controllers;

/// <summary>
/// Контроллер получения пользователя и его любимой валюты
/// </summary>
[ApiController]
[Route("api/v1/financial")]
public class Controller(ILogger<Controller> logger, IMediator mediator) : ControllerBase
{
    [Authorize]
    [EnableRateLimiting("DefaultPolicy")]
    [HttpGet("favourite-rates/{userId}")]
    public async Task<IActionResult> GetUserWithFavouriteRateAsync([FromRoute]Guid userId, CancellationToken cancellationToken)
    { 
        logger.LogInformation("GET /finance/rates | UserId: {UserId}", userId);
        var command = new GetUserWithFavouriteRateCommand()
        {
            UserId = userId
        };
         
        var result = await mediator.Send(command, cancellationToken);

        if (result.FavouriteRates == null)
        {
            logger.LogWarning("GET /finance/rates - not found");
        }
        
        logger.LogInformation("GET /finance/rates completed | UserId: {UserId} Count: {Count}", result.Id, result.FavouriteRates!.Count);
        
       return Ok(result);
    }
}