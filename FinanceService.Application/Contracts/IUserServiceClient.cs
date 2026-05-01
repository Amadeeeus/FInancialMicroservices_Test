using FinanceService.Application.DTOs;
using Refit;

namespace FinanceService.Application.Contracts;

public interface IUserServiceClient
{
    [Get("/users/{id}")]
    Task<ApiResponse<GetUserByIdOutDto>>GetUserById(Guid id, CancellationToken ct);
}