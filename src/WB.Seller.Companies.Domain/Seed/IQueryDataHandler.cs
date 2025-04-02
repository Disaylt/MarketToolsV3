using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public interface IQueryDataHandler<in TQueryData, TResponse>
    where TQueryData : IQueryData<TResponse>
    where TResponse : class
    {
        Task<TResponse> HandleAsync(TQueryData queryData);
    }
}
