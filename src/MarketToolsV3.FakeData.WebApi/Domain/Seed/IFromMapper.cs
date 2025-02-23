namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IFromMapper<in T, out TResult>
    where T : IFromMap<TResult>
    {
        TResult Map(T value);
    }
}
