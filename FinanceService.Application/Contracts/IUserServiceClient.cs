using FinanceService.Application.DTOs;
using Refit;

namespace FinanceService.Application.Contracts;

public interface IUserServiceClient
{
    [Get("/api/users/{userId}")]
    Task<ApiResponse<GetUserByIdOutDto>>GetUserById(Guid userId, CancellationToken ct);
}