using QuackersAPI_DDD.Domain.Model;

namespace QuackersAPI_DDD.Infrastructure.InterfaceRepository
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken);
        Task<RefreshToken> FindByTokenAsync(string token);
        Task UpdateAsync(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }

}
