using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public interface IToMapper<in T, out TResult>
        where T : IToMap<TResult>
    {
        TResult Map(T value);
    }
}
