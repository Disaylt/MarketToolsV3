namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface ICacheRepository
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T value, TimeSpan expire) where T : class;
        Task DeleteAsync<T>(string key, CancellationToken cancellationToken) where T : class;
    }
}
