using AutoMapper;
using FinanceService.Application.Commands;
using FinanceService.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FinanceService.Api.Controllers;

/// <summary>
/// Контроллер получения пользователя и его любимой валюты
/// </summary>
[ApiController]
[Route("api/v1/financial/[controller]")]
public class Controller(IMapper mapper, ILogger<Controller> logger, IMediator mediator) : ControllerBase
{
    [Authorize]
    [EnableRateLimiting("DefaultPolicy")]
    [HttpGet("favourite-rates")]
    public async Task<IActionResult> GetUserWithFavouriteRateAsync(GetUserByIdDto dto, CancellationToken cancellationToken)
    { 
        logger.LogInformation("GET /finance/rates | UserId: {UserId}", dto.UserId); 
        var command =  mapper.Map<GetUserByIdDto, GetUserWithFavouriteRateCommand>(dto); 
         
        var result = await mediator.Send(command, cancellationToken);

        if (result.FavouriteRates == null)
        {
            logger.LogWarning("GET /finance/rates - not found");
        }
        
        logger.LogInformation("GET /finance/rates completed | UserId: {UserId} Count: {Count}", result.Id, result.FavouriteRates!.Count);
        
       return Ok(result);
    }
}