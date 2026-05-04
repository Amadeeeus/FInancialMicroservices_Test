using MediatR;
using UserServiceApplication.Dtos;

namespace UserServiceApplication.Queries;

/// <summary>
/// Запрос пользователя из БД
/// </summary>
public record GetUserByIdQuery(Guid UserId) : IRequest<GetUserByIdOutDto>;
