using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;

namespace FinanceService.Infrastructure.Handlers;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public AuthHeaderHandler(IHttpContextAccessor accessor)
        => _httpContextAccessor = accessor;
    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken ct)
    {
        var token = _httpContextAccessor.HttpContext?
            .Request.Headers["Authorization"]
            .ToString()
            .Replace("Bearer ", "");
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        return base.SendAsync(request, ct);
    }
}