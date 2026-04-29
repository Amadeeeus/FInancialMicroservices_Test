using FinanceService.Application.DTOs;
using MediatR;

namespace FinanceService.Application.Commands;

/// <summary>
/// Команда получения пользователя с любимой валютой
/// </summary>
public class GetUserWithFavouriteRateCommand : IRequest<GetUserWithFavouriteRateOutDto>
{
    /// <summary>
    /// Id пользователя
    /// </summary>
    public Guid UserId { get; set; }
}