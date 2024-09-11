using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Persistence.CachedRepositories
{
    public interface IRefreshTokenCachedRepository
    {
        Task<long?> GetUserIdByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task SetAsync(string refreshToken, long userId, int expirationHours, CancellationToken cancellationToken = default);
        Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default);
    }
}
