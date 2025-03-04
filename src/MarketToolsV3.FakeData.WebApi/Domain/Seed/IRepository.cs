namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> FindAsync(params object[] keys);
        Task<T> FindRequiredAsync(params object[] keys);
        IQueryable<T> AsQueryable();
        Task<List<TResult>> ToListAsync<TResult>(IQueryable<TResult>  query);
    }
}
