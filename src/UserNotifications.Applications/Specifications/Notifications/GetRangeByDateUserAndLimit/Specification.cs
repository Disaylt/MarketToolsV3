using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Enums;

namespace UserNotifications.Applications.Specifications.Notifications.GetRangeByDateUserAndLimit
{
    public class GetRangeForUsersNotificationSpecification(FilterData filterData)
        : RangeBaseSpecification<Notification>
    {
        public FilterData Filter { get; } = filterData;
        public Options Options { get; } = new();
    }

    public class Options
    {
        public int? Take { get; set; }
        public int? Skip { get; set; }
    }

    public class FilterData
    {
        public required string UserId { get; set; }
        public bool? IsRead { get; set; }
        public Category? Category { get; set; }
    }
}
