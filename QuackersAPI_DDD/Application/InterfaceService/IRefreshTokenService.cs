using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Application.InterfaceService
{
    public interface IRefreshTokenService
    {
        Task<string> GenerateRefreshToken(Person person);
        Task<int?> ValidateRefreshToken(string refreshToken);
        Task<bool> RevokeRefreshToken(string refreshToken);
    }

}
