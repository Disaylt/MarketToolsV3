using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Domain.Seed
{
    public interface IQueryableHandler<in T, TResult>
    {
        Task<IQueryable<TResult>> HandleAsync(IQueryable<T> query);
    }
}
