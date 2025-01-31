using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Applications.Models
{
    public record NotificationDateFilter
    {
        public required DateTime FromDate { get; set; }
        public required DateTime ToDate { get; set; }
    }
}
