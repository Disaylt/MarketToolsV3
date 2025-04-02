using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public interface IMapperFactory
    {
        IToMapper<T, TResult> CreateToMapper<T, TResult>() where T : IToMap<TResult>;
        IFromMapper<T, TResult> CreateFromMapper<T, TResult>() where TResult : IFromMap<T>;
    }
}
