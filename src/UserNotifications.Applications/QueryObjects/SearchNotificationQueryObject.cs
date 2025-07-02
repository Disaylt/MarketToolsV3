using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.QueryObjects
{
    public class SearchNotificationQueryObject : IQueryObject<Notification>
    {
        public string? UserId { get; set; }
        public string? Category { get; set; }
        public bool? IsRead { get; set; }
    }
}
