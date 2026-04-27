using User.UserService.Domain.Models;

namespace User.UserService.Infrastructure.Jwt.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(UserEntity? userEntity);
    string GenerateRefreshToken();
}