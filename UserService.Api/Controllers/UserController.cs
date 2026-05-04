using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using UserService.Api.Helpers;
using UserServiceApplication.Commands;
using UserServiceApplication.Dtos;
using UserServiceApplication.Queries;

namespace UserService.Api.Controllers;

/// <summary>
/// Контроллер авторизации/регистрации пользователя
/// </summary>
/// <param name="mediator">Библиотека реализации через SQRS</param>
/// <param name="logger">Стандартная библиотека логирования</param>
[ApiController]
[Route("api/users")]
public class UserController(IMediator mediator, ILogger<UserController> logger, IMapper mapper) : ControllerBase
{
    /// <summary>
    /// Получение пользователя по ID
    /// </summary>
    /// <param name="id">id пользователя</param>
    /// <param name="userId">id пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns>404 Not Found, 200 Ok с пользователем по id</returns>
    [Authorize]
    [EnableRateLimiting("DefaultPolicy")]
    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId, CancellationToken ct)
    { 
        logger.LogInformation("GET /users/{UserId}", userId);
        
        var query = new GetUserByIdQuery(userId);
        
        logger.LogInformation("mapping{UserId}", query.UserId);
        var result =  await mediator.Send(query, ct);
        
        return Ok(result);
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns>201 Created</returns>
    [EnableRateLimiting("LoginPolicy")]
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserDto user, CancellationToken ct)
    {
        logger.LogInformation("POST /auth/register | Name: {Name}", user.Name);
        
        var command = mapper.Map<CreateUserDto, CreateUserCommand>(user);
        
        await mediator.Send(command, ct);
        
        logger.LogInformation("POST /auth/register completed | Name: {Name}", command.Name);
        
        return Created();
    }

    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns>200 Ok</returns>
    [Authorize]
    [EnableRateLimiting("DefaultPolicy")]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] CreateUserDto user, CancellationToken ct)
    {
        logger.LogInformation("POST /auth/update | Name: {Name}", user.Name);
        
        var command = mapper.Map<CreateUserDto, UpdateUserCommand>(user);
        await mediator.Send(command, ct);
        
        logger.LogInformation("POST /auth/update updated | Name: {Name}", command.Name);
        return Ok();
    }

    /// <summary>
    /// Вход пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns>200 OK, 401 Unautorized в случае неправильных данных </returns>
    [EnableRateLimiting("LoginPolicy")]
    [AllowAnonymous]
    [HttpPost("auth")]
    public async Task<IActionResult> AuthentificationUserAsync([FromBody] AuthentificationUserDto user, CancellationToken ct)
    {
        logger.LogInformation("POST /auth/login | Start authentification");
        
        var map = mapper.Map<AuthentificationUserDto, AuthentificationUserCommand>(user);
        var tokens = await mediator.Send(map, ct);

        if (tokens is null)
        {
            logger.LogWarning("POST /auth/login failed - invalid credentials");
            
            return Unauthorized();
        }

        //Запись рефреш в HttpOnly
        Response.Cookies.Append("Refresh", tokens?.RefreshToken!, CookieHelper.AddHttpOnlyCookie());

        return Ok(new { tokens?.AccessToken});
    }
    
    /// <summary>
    /// Выход пользователя
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutUserAsync(CancellationToken ct)
    {
        var cookie = Request.Cookies["Refresh"];
        
        logger.LogInformation("POST /auth/logout | Get Refresh Token");
        
        if (cookie is null)
        {
            logger.LogWarning("POST /auth/logout - cookie not found");
            
            return Unauthorized();
        }

        var command = new LogoutUserCommand
        {
            RefreshToken = cookie
        };
        
        // Запрос на удаление из БД
        await mediator.Send(command, ct);
        
        // Удаляем из кук
        Response.Cookies.Delete("Refresh");
        
        logger.LogInformation("POST /auth/logout completed");
        
        return Ok();
    }

    /// <summary>
    /// Перевыпуск токена
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshUserAsync(CancellationToken ct)
    {
        logger.LogInformation("POST /auth/refresh");
        
        var cookie = Request.Cookies["Refresh"];
        
        if (cookie is null)
        {
            logger.LogWarning("POST /auth/refresh - cookie not found");
            
            return Unauthorized();
        }

        var command = new RefreshTokenCommand()
        {
            RefreshToken = cookie
        };
        
        var result = await mediator.Send(command, ct);

        Response.Cookies.Append("Refresh", result?.RefreshToken!, CookieHelper.AddHttpOnlyCookie());
        
        logger.LogInformation("POST /auth/refresh completed");
        
        return Ok(result?.AccessToken);
    }
}