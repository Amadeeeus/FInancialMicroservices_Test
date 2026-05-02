namespace UserService.Api.Helpers;


/// <summary>
/// Хелпер куки
/// </summary>
public static class CookieHelper
{
    public static CookieOptions AddHttpOnlyCookie()
     =>   new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddDays(7)
        };
}