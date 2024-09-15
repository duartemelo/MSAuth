using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using MSAuth.Domain.Entities;
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

        public async Task<UserClaims?> GetUserClaimsByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default)
        {
            string key = $"refresh-{refreshToken}";

            string? user = await _distributedCache.GetStringAsync(key, cancellationToken);

            if (string.IsNullOrEmpty(user))
                return null;

            return JsonConvert.DeserializeObject<UserClaims>(
                user, 
                new JsonSerializerSettings 
                {
                    ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                });
        }

        public async Task SetAsync(string refreshToken, UserClaims userClaims, int expirationHours, CancellationToken cancellationToken = default)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(expirationHours)
            };

            string key = $"refresh-{refreshToken}";

            await _distributedCache.SetStringAsync(
                key,
                JsonConvert.SerializeObject(userClaims),
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
