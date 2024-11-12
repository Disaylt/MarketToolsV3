using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Domain.Seed
{
    public class ServiceConfiguration
    {
        public string DatabaseConnection { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string NotificationsCollectionName { get; set; } = string.Empty;
    }
}
