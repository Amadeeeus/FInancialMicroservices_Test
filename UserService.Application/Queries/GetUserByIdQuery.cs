using MediatR;
using User.UserService.Application.Dtos;
using UserService.Domain.Entities;

namespace User.UserService.Application.Queries;

/// <summary>
/// Запрос пользователя из БД
/// </summary>
public record GetUserByIdQuery(Guid UserId) : IRequest<UserEntity>, IRequest<UserDto>;
