using MSAuth.Domain.Entities;

namespace MSAuth.Domain.Interfaces.Persistence.CachedRepositories
{
    public interface IRefreshTokenCachedRepository
    {
        Task<UserClaims?> GetUserClaimsByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default);
        Task SetAsync(string refreshToken, UserClaims userClaims, int expirationHours, CancellationToken cancellationToken = default);
    }
}
