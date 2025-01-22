using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Specifications
{
    public class UpdateNotificationSpecification : BaseSpecification
    {
        public UpdateNotificationSpecificationFilter Filter { get; } = new UpdateNotificationSpecificationFilter();
        public UpdateNotificationSpecificationData Data { get; } = new UpdateNotificationSpecificationData();
    }

    public class UpdateNotificationSpecificationFilter
    {
        public string? UserId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public class UpdateNotificationSpecificationData
    {
        public bool? IsRead { get; set; }
    }
}
