namespace UserServiceApplication.Dtos;

/// <summary>
/// Входная модель для получения пользователя
/// </summary>
public record GetUserByIdDto
{
    
    public required Guid UserId { get; set; }
}