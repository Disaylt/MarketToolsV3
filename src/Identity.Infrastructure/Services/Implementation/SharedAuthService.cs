using Identity.Application.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services.Implementation
{
    public class SharedAuthService(
        [FromKeyedServices("shared-identity")] ICacheRepository sharedIdentityCacheRepository,
        IOptions<AuthConfig> authConfigOptions)
        : ISharedAuthService
    {

        public async Task AddToBlackListAsync(string id)
        {
            string key = $"blacklist-access-token-{id}";
            TimeSpan expire = TimeSpan.FromMinutes(authConfigOptions.Value.ExpireAccessTokenMinutes);

            await sharedIdentityCacheRepository.SetAsync(key, new object(), expire);
        }
    }
}
