using User.UserService.Domain.Models;

namespace UserService.Infrastructure.Persistence.Jwt.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(UserEntity? userEntity);
    string GenerateRefreshToken();
}