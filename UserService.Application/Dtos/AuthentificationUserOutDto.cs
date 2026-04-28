namespace UserServiceApplication.Dtos;

/// <summary>
/// Возвращение обоих токенов
/// </summary>
public class AuthentificationUserOutDto
{
    /// <summary>
    /// Access
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;
    
    /// <summary>
    /// Refresh
    /// </summary>
    public string RefreshToken { get; set; }  = string.Empty;
}