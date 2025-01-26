using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Applications.Specifications.Notifications.GetRangeByDateUserAndLimit
{
    public class GetRangeByDateUserAndLimitNotificationSpecification(FilterData filterData)
        : RangeBaseSpecification<Notification>
    {
        public FilterData Filter { get; } = filterData;
    }

    public class FilterData
    {
        public required string UserId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }
}
