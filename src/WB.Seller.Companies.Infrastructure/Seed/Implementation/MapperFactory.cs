using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.Seed.Implementation
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
