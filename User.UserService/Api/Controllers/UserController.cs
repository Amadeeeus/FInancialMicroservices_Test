using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.UserService.Application.Commands;
using User.UserService.Application.Dtos;
using User.UserService.Application.Queries;

namespace User.UserService.Api.Controllers;

/// <summary>
/// Контроллер авторизации/регистрации пользователя
/// </summary>
/// <param name="mediator">Библиотека реализации через SQRS</param>
/// <param name="logger">Стандартная библиотека логирования</param>
[ApiController]
[Route("api/user")]
public class UserController(IMediator mediator, ILogger<UserController> logger, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение пользователя по ID
    /// </summary>
    /// <param name="id">id пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByAsync([FromRoute] GetUserByIdDto id, CancellationToken ct)
    { 
        var query = mapper.Map<GetUserByIdDto, GetUserByIdQuery>(id); 
        var result =  await mediator.Send(query, ct); 
        return Ok(result);
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromQuery] CreateUserDto user, CancellationToken ct)
    {
        var command = mapper.Map<CreateUserDto, CreateUserCommand>(user);
        await mediator.Send(command, ct);
        return Created();
    }

    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserAsync([FromQuery] CreateUserDto user, CancellationToken ct)
    {
        var command = mapper.Map<CreateUserDto, UpdateUserCommand>(user);
        await mediator.Send(command, ct);
        return Ok();
    }

    /// <summary>
    /// Вход пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    [HttpPost("auth")]
    public async Task<IActionResult> AuthentificationUserAsync([FromQuery] AuthentificationUserDto user, CancellationToken ct)
    {
        var map = mapper.Map<AuthentificationUserDto, AuthentificationUserCommand>(user);
        var tokens = await mediator.Send(map, ct);

        //Запись рефреш в HttpOnly
        Response.Cookies.Append("Refresh", tokens?.RefreshToken!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires =  DateTimeOffset.UtcNow.AddDays(7)
        });

        return Ok(new { tokens?.AccessToken});
    }
    
    /// <summary>
    /// Выход пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUserAsync(CancellationToken ct)
    {
        var cookie = Request.Cookies["Refresh"];
        
        if (cookie is null)
        {
            return Unauthorized();
        }
        
        var command = mapper.Map<LogoutUserCommand>(cookie);
        
        // Запрос на удаление из БД
        await mediator.Send(command, ct);
        
        // Удаляем из кук
        Response.Cookies.Delete("Refresh");
        
        return Ok();
    }

    /// <summary>
    /// Перевыпуск токена
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshUserAsync(CancellationToken ct)
    {
        var cookie = Request.Cookies["Refresh"];
        
        if (cookie is null)
        {
            return Unauthorized();
        }

        var command = mapper.Map<RefreshTokenCommand>(cookie);
        
        var result = await mediator.Send(command, ct);
        
        Response.Cookies.Append("Refresh",result?.RefreshToken!, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        });
        
        return Ok(result?.AccessToken);
    }
}