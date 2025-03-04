using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Implementation
{
    public class BaseRepository<T>(FakeDataDbContext context)
        : IRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<T?> FindAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys);
        }

        public async Task<T> FindRequiredAsync(params object[] keys)
        {
            return await _dbSet.FindAsync(keys)
                ?? throw new RootServiceException(HttpStatusCode.NotFound,
                    $"Entity ({typeof(T).Name} not found. Keys:{string.Join('|', keys)})");
        }

        public async Task<List<TResult>> ToListAsync<TResult>(IQueryable<TResult> query)
        {
            return await query.ToListAsync();
        }
    }
}
