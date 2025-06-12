using MarketToolsV3.PermissionStore.Domain.Entities;

namespace MarketToolsV3.PermissionStore.Domain.Seed;

public interface IExtensionRepository
{
    Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class;
    Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class;
    Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class;
}