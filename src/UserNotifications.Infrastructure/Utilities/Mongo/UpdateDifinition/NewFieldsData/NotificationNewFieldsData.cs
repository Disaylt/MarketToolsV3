using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData
{
    internal class NotificationNewFieldsData : INotificationNewFieldsData
    {
        public bool? IsRead { get; set; }
    }
}
