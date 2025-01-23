using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Specifications
{
    public class GetRangeNotificationsSpecification : RangeBaseSpecification<Notification>
    {
        public Expression<Func<Notification, bool>>? Filter { get; set; }
    }
}
