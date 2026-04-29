namespace FinanceService.Application.DTOs;

/// <summary>
/// Входной Dto для клиента
/// </summary>
public class GetUserByIdDto
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public required Guid UserId { get; set; }
}