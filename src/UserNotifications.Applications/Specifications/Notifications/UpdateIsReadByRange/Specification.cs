using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Specifications.Notifications.UpdateIsReadByRange
{
    public class UpdateIsReadFieldByRangeNotificationSpecification(
        FilterData filterData,
        EntityData entityData)
        : BaseSpecification
    {
        public FilterData Filter { get; } = filterData;
        public EntityData Data { get; } = entityData;
    }

    public class FilterData
    {
        public required string UserId { get; set; }
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }

    public class EntityData
    {
        public bool IsRead { get; set; }
    }
}
