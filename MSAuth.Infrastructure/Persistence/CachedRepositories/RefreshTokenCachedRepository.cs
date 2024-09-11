using Microsoft.Extensions.Caching.Distributed;
using MSAuth.Domain.Interfaces.Persistence.CachedRepositories;
using Newtonsoft.Json;

namespace MSAuth.Infrastructure.Persistence.CachedRepositories
{
    public class RefreshTokenCachedRepository : IRefreshTokenCachedRepository
    {
        private readonly IDistributedCache _distributedCache;

        // TODO: error handling (not here, but where this repository is consumed)

        public RefreshTokenCachedRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<long?> GetUserIdByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            string key = $"refresh-{refreshToken}";

            string? userId = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (userId == null)
                return null;

            return JsonConvert.DeserializeObject<long>(userId);
        }

        public async Task SetAsync(string refreshToken, long userId, int expirationHours, CancellationToken cancellationToken = default)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationHours)
            };

            string key = $"refresh-{refreshToken}";

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(userId),
                cacheOptions,
                cancellationToken);
        }

        public async Task RemoveAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            string key = $"refresh-{refreshToken}";
            await _distributedCache.RemoveAsync(key, cancellationToken);
        } 

    }
}
