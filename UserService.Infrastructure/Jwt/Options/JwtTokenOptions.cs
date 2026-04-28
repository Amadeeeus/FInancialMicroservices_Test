namespace UserService.Infrastructure.Persistence.Jwt.Options;

/// <summary>
/// Опции конфигурации jwt токена
/// </summary>
public class JwtTokenOptions
{
    /// <summary>
    /// Издатель
    /// </summary>
    public string Issuer { get; set; } = null!;
    
    /// <summary>
    /// Получатель
    /// </summary>
    public string Audience { get; set; } = null!;
    
    /// <summary>
    /// Секретный ключ
    /// </summary>
    public string Secret { get; set; } = null!;
    
    /// <summary>
    /// Время действия access токена
    /// </summary>
    public int AccessExpiresIn { get; set; }
    
    /// <summary>
    /// Время действия refresh токена
    /// </summary>
    public int RefreshExpiresId { get; set; }
}