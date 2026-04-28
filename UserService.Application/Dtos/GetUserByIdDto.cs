using Microsoft.AspNetCore.Razor.TagHelpers;

namespace User.UserService.Application.Dtos;

/// <summary>
/// Входная модель для получения пользователя
/// </summary>
public class GetUserByIdDto
{
    [HtmlAttributeName("userId")]
    public required Guid UserId { get; set; }
}