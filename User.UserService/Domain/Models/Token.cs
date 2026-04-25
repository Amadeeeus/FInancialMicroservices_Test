namespace User.UserService.Domain.Models;

/// <summary>
/// Сущность хранения токена
/// </summary>
public class Token
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Refresh токен
    /// </summary>
    public string RefreshToken { get; set; } = null!;
    
    /// <summary>
    /// Время хранения
    /// </summary>
    public DateTime Expires { get; set; }
}