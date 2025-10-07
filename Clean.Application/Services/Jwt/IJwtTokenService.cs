namespace Clean.Application.Services.Jwt;

public interface IJwtTokenService
{
    string CreateAccessToken(int userId, string userName, string role);

}