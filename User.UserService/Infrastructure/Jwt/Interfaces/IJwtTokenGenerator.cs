namespace User.UserService.Infrastructure.Jwt.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User.UserService.Domain.Models.User user);
    string GenerateRefreshToken();
}