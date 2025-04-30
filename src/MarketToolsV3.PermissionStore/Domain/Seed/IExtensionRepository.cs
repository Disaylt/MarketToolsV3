namespace MarketToolsV3.PermissionStore.Domain.Seed;

public interface IExtensionRepository
{
    Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);
    Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken);
}