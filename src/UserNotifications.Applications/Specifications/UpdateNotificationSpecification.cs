using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Models;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.UpdeteDetails;

namespace UserNotifications.Applications.Specifications
{
    public class UpdateNotificationSpecification : BaseSpecification
    {
        public Expression<Func<Notification, bool>>? Filter { get; set; }
        public UpdateNotificationSpecificationData Data { get; } = new UpdateNotificationSpecificationData();
    }

    public class UpdateNotificationSpecificationData : INotificationUpdateDetails
    {
        public bool? IsRead { get; set; }
    }
}
