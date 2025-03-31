namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IFromMapper<in T, out TResult>
    where TResult : IFromMap<T>
    {
        TResult Map(T value);
    }
}
