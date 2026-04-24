using MediatR;
using User.UserService.Application.Dtos;

namespace User.UserService.Application.Queries;

/// <summary>
/// Запрос пользователя из БД
/// </summary>
public record GetUserByIdQuery(Guid UserId) : IRequest<Domain.Models.User>, IRequest<UserDto>;
