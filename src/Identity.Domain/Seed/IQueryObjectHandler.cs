using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public interface IQueryObjectHandler<in TQueryObject, out TResult>
    where TQueryObject : IQueryObject<TResult> 
    where TResult : class
    {
        IQueryable<TResult> Create(TQueryObject  query);
    }
}
