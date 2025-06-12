using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MongoDB.Driver.Linq;

namespace MarketToolsV3.PermissionStore.Infrastructure.Repositories;

public class MongoExtensionRepository : IExtensionRepository
{
    public Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class
    {
        return query.CountAsync(cancellationToken);
    }

    public Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class
    {
        return query.ToListAsync(cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class
    {
        return await query.FirstOrDefaultAsync(cancellationToken);
    }
}