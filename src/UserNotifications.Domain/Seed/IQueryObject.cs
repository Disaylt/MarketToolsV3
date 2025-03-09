using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Domain.Seed
{
    public interface IQueryObject<T> where T : Entity
    {
    }
}
