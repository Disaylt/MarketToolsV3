namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public interface IMapFactory
    {
        IToMapper<T, TResult> CreateToMapper<T, TResult>() where T : IToMap<TResult>;
        IFromMapper<T, TResult> CreateFromMapper<T, TResult>() where T : IFromMap<TResult>;
    }
}
