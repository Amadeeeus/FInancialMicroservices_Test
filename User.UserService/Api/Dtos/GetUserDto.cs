using Microsoft.AspNetCore.Razor.TagHelpers;

namespace User.UserService.Api.Dtos;

/// <summary>
/// Входная модель
/// </summary>
public class GetUserDto
{
    [HtmlAttributeName("userId")]
    public required Guid UserId { get; set; }
}