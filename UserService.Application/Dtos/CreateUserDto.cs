using Microsoft.AspNetCore.Razor.TagHelpers;

namespace User.UserService.Application.Dtos;

/// <summary>
/// Входная модель создания/обновления пользователя
/// </summary>
public class CreateUserDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [HtmlAttributeName("Name")]
    public required string Name { get; init; } 

    /// <summary>
    /// Пароль
    /// </summary>
    [HtmlAttributeName("password")]
    public required string Password { get; init; } 
    
    /// <summary>
    /// Интересные пользователю курсы
    /// </summary>
    [HtmlAttributeName("Favourites")]
    public string? Favourites { get; init; }
}