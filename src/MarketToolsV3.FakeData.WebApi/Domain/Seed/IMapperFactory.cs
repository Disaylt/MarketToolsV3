namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IMapperFactory
    {
        IToMapper<T, TResult> CreateToMapper<T, TResult>() where T : IToMap<TResult>;
        IFromMapper<T, TResult> CreateFromMapper<T, TResult>() where TResult : IFromMap<T>;
    }
}
