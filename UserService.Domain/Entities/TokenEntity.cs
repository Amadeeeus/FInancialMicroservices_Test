namespace User.UserService.Domain.Models;

/// <summary>
/// Сущность хранения токена
/// </summary>
public class TokenEntity
{
    /// <summary>
    /// Id токена
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Refresh токен
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;

    /// <summary>
    /// Время хранения
    /// </summary>
    public DateTime Expires { get; set; }

    /// <summary>
    /// Время хранения
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Отозван ли
    /// </summary>
    public bool IsRevoked { get; set; }
    
    /// <summary>
    /// Время отзыва
    /// </summary>
    public DateTime? Revoked { get; set; }
}