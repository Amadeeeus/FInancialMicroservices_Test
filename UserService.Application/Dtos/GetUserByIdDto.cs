namespace UserServiceApplication.Dtos;

/// <summary>
/// Входная модель для получения пользователя
/// </summary>
public class GetUserByIdDto
{
    
    public required Guid UserId { get; set; }
}