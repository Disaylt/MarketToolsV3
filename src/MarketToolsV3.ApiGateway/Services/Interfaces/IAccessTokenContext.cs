using MarketToolsV3.ApiGateway.Models.Tokens;

namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface IAccessTokenContext
    {
        AccessToken? Data { get; set; }
        void Remove();
    }
}
