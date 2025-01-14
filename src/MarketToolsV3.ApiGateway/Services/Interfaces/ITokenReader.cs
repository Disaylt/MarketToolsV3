using MarketToolsV3.ApiGateway.Models.Tokens;

namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface ITokenReader<out T> where T : BaseToken
    {
        T? ReadOrDefault(string token);
    }
}
