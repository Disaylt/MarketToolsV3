namespace MarketToolsV3.ApiGateway.Services.Interfaces;

public interface IAccessTokenService
{
    Task<bool> IsValidAsync(string token);
}