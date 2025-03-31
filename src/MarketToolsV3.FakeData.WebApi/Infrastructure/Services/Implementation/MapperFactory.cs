using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class MapperFactory(IServiceProvider serviceProvider)
        : IMapperFactory
    {
        public IFromMapper<T, TResult> CreateFromMapper<T, TResult>() where TResult : IFromMap<T>
        {
            return serviceProvider.GetRequiredService<IFromMapper<T, TResult>>();
        }

        public IToMapper<T, TResult> CreateToMapper<T, TResult>() where T : IToMap<TResult>
        {
            return serviceProvider.GetRequiredService<IToMapper<T, TResult>>();
        }
    }
}
