using MediatR;
using UserService.Domain.Entities;
using UserServiceApplication.Dtos;

namespace UserServiceApplication.Queries;

/// <summary>
/// Запрос пользователя из БД
/// </summary>
public record GetUserByIdQuery(Guid UserId) : IRequest<UserEntity>, IRequest<GetUserByIdOutDto>;
