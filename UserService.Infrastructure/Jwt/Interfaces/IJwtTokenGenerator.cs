using UserService.Domain.Entities;

namespace UserService.Infrastructure.Jwt.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(UserEntity? userEntity);
    string GenerateRefreshToken();
}