using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public interface IQueryObjectAdapter<in TQueryObject, out TResult>
    where TQueryObject : IQueryObject
    where TResult : class
    {
        IQueryable<TResult> Create(TQueryObject  query);
    }
}
