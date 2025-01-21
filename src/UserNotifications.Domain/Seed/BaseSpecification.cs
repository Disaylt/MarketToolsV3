using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Domain.Seed
{
    public abstract class BaseSpecification<TResult>
        where TResult : Entity
    {
    }

    public abstract class BaseSpecification
    {

    }
}
