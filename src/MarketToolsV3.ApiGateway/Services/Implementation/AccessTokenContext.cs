using MarketToolsV3.ApiGateway.Models.Tokens;
using MarketToolsV3.ApiGateway.Services.Interfaces;

namespace MarketToolsV3.ApiGateway.Services.Implementation
{
    public class AccessTokenContext : IAccessTokenContext
    {
        public AccessToken? Data { get; set; }
        public void Remove()
        {
            Data = null;
        }
    }
}
