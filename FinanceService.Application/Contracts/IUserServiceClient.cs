using FinanceService.Application.DTOs;
using Refit;

namespace FinanceService.Application.Contracts;

public interface IUserServiceClient
{
    [Get("/users/{id}")]
    Task<GetUserByIdOutDto>GetUserById(Guid id, CancellationToken ct);
}