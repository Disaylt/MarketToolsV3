using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition
{
    internal class UpdateDefinitionFactory : IUpdateDefinitionFactory
    {
        public required Func<INotificationNewFieldsData, INotificationMongoUpdateDifinitionBuilder> NotificationUpdateBuilder { get; set; }
    }
}
