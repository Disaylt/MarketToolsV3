using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition
{
    internal interface IUpdateDefinitionFactory
    {
        Func<INotificationNewFieldsData, INotificationMongoUpdateDifinitionBuilder> NotificationUpdateBuilder { get; }
    }
}
