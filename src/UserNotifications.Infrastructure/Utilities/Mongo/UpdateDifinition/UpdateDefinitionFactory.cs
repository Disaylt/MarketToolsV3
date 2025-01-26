using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.UpdateDetails;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition
{
    internal class UpdateDefinitionFactory : IUpdateDefinitionFactory
    {
        public INotificationMongoUpdateDifinitionBuilder CreateNotificationUpdateBuilder(INotificationUpdateDetails notificationUpdateDetails)
        {
            return new NotificationMongoUpdateDifinitionBuilder(notificationUpdateDetails);
        }
    }
}
