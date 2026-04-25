using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.UserService.Infrastructure.Jwt.Interfaces;
using User.UserService.Infrastructure.Jwt.Options;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace User.UserService.Infrastructure.Jwt.Implementations;

/// <summary>
/// Генератор Jwt токенов
/// </summary>
/// <param name="options"></param>
public class JwtTokenGenerator(IOptions<JwtTokenOptions> options) : IJwtTokenGenerator
{
    private readonly JwtTokenOptions _options = options.Value;
    private readonly JwtSecurityTokenHandler _tokenHandler = new ();

    public string GenerateAccessToken(User.UserService.Domain.Models.User user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.Secret));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, user.Name),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_options.AccessExpiresIn),
            Issuer = _options.Issuer,
            Audience = _options.Audience,
            SigningCredentials = credentials
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);

        return _tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(randomBytes);
    }
}