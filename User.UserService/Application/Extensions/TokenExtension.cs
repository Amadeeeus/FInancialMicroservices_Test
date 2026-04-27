using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using User.UserService.Domain.Models;
using User.UserService.Infrastructure.Persistence;

namespace User.UserService.Application.Extensions;

/// <summary>
/// Методы расширения для токенов
/// </summary>
public static class TokenExtension
{
    /// <summary>
    /// Хеширование
    /// </summary>
    /// <param name="token">Оригинальный токен</param>
    /// <returns>Хешированный токен</returns>
    public static string HashRefreshToken(this string token)
    {
        return Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
    }

    public static async Task<TokenEntity?> GetValidRefreshToken(this TokensDbContext context, string hash,CancellationToken ct)
    => await context
        .Tokens
        .FirstOrDefaultAsync(x=>x.RefreshToken == hash && !x.IsRevoked, ct);
}