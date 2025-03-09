using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Domain.Seed
{
    public interface IQueryObjectHandler<in TQueryObject, out TResult>
        where TQueryObject : IQueryObject<TResult>
        where TResult : Entity
    {
        IQueryable<TResult> Create(TQueryObject queryObject);
    }
}
