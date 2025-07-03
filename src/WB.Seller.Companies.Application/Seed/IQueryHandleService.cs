namespace WB.Seller.Companies.Application.Seed;

public interface IQueryHandleService
{
    Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query);
    Task<T> FirstAsync<T>(IQueryable<T> query);
}