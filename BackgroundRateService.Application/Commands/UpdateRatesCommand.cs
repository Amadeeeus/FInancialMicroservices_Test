using MediatR;

namespace BackgroundRateService.Application.Commands;

/// <summary>
/// Команда обновления курса
/// </summary>
public record UpdateRatesCommand : IRequest;
