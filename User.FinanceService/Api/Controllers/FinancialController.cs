using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace User.FinanceService.Api.Controllers;

/// <summary>
/// Контроллер получения пользователя с favourite курсом
/// </summary>
/// <param name="mediator">Библиотека реализации через SQRS</param>
/// <param name="logger">Стандартная библиотека логирования</param>
[ApiController]
public class FinancialController(IMediator mediator, ILogger<FinancialController> logger) : ControllerBase
{
    public async Task<IActionResult> GetUserWithCurrencyRateAsync([FromRoute] string userId)
    {
        var result = await mediator.Send(userId);

        return result is null ? throw new NullReferenceException() : Ok(result);
    }
}