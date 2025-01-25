using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Mappers
{
    public interface INotificationMapper<out TResult> where TResult : IMap
    {
        TResult Map(Notification notification);
    }
}
