namespace MarketToolsV3.ApiGateway.Services.Interfaces
{
    public interface ICacheRepository<T> where T : class
    {
        Task<T?> GetAsync(string key);
        Task SetAsync(string key, T value, TimeSpan expire);
        Task DeleteAsync(string key, CancellationToken cancellationToken);
    }
}
