using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Abstract
{
    public interface IQueryableRepository<out T> where T : Entity
    {
        IQueryable<T> AsQueryable();
    }
}
