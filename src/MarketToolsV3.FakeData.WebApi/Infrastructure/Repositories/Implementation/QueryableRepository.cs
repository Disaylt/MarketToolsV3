using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Implementation
{
    public class QueryableRepository<T>(FakeDataDbContext context)
        : IQueryableRepository<T> where T : Entity
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();
        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }
    }
}
