namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IToMapper<in T, out TResult>
    where T : IToMap<TResult>
    {
        TResult Map(T value);
    }
}
