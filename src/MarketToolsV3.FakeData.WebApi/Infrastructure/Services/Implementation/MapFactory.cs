using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation
{
    public class MapFactory(IServiceProvider serviceProvider)
        : IMapFactory
    {
        public IFromMapper<T, TResult> CreateFromMapper<T, TResult>() where T : IFromMap<TResult>
        {
            return serviceProvider.GetRequiredService<IFromMapper<T, TResult>>();
        }

        public IToMapper<T, TResult> CreateToMapper<T, TResult>() where T : IToMap<TResult>
        {
            return serviceProvider.GetRequiredService<IToMapper<T, TResult>>();
        }
    }
}
