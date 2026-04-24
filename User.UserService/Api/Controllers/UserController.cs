using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace User.UserService.Api.Controllers;

/// <summary>
/// Контроллер авторизации/регистрации пользователя
/// </summary>
/// <param name="mediator">Библиотека реализации через SQRS</param>
/// <param name="logger">Стандартная библиотека логирования</param>
[ApiController]
public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
{
    public async Task<IActionResult> GetUserAsync([FromRoute]  string userId)
    {
       var result =  await mediator.Send(userId);
       return Ok(result);
    }

    public async Task<IActionResult> UpdateUserAsync([FromRoute] string userId)
    {
        var result = await mediator.Send(userId);
        
    }



}