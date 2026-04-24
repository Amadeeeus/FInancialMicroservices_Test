using MediatR;
using Microsoft.AspNetCore.Mvc;
using User.UserService.Application.Dtos;

namespace User.UserService.Api.Controllers;

/// <summary>
/// Контроллер авторизации/регистрации пользователя
/// </summary>
/// <param name="mediator">Библиотека реализации через SQRS</param>
/// <param name="logger">Стандартная библиотека логирования</param>
[ApiController]
[Route("api/user")]
public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
{
    /// <summary>
    /// Получение пользователя по ID
    /// </summary>
    /// <param name="id">id пользователя</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByAsync([FromRoute] GetUserByIdDto id)
    {
       var result =  await mediator.Send(id);
       return Ok(result);
    }

    /// <summary>
    /// Регистрация пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromQuery] CreateUserDto user)
    {
        var result = await mediator.Send(user);
        return Ok(result);
    }

    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <returns></returns>
    [HttpPost("update")]
    public async Task<IActionResult> UpdateUserAsync([FromQuery] CreateUserDto user)
    {
        var result = await mediator.Send(user);
        return Ok(result);
    }

    /// <summary>
    /// Вход пользователя
    /// </summary>
    /// <param name="user">Входная сущность пользователя</param>
    /// <returns></returns>
    [HttpPost("auth")]
    public async Task<IActionResult> AuthentificationUserAsync([FromQuery] CreateUserDto user)
    {
        var result = await mediator.Send(user);
        return Ok(result);
    }
    
    /// <summary>
    /// Выход пользователя
    /// </summary>
    /// <returns></returns>
    [HttpGet("logout")]
    public async Task<IActionResult> LogoutUserAsync()
    {
        return Ok();
    }
    


}