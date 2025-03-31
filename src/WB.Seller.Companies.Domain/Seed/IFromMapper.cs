using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public interface IFromMapper<in T, out TResult>
        where TResult : IFromMap<T>
    {
        TResult Map(T value);
    }
}
