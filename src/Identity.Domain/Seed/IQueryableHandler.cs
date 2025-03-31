namespace Identity.Domain.Seed;

public interface IQueryableHandler<in T, TResult>
{
    Task<IQueryable<TResult>> HandleAsync(IQueryable<T>  query);
}