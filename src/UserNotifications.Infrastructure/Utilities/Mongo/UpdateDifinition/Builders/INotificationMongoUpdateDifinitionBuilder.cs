using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders
{
    internal interface INotificationMongoUpdateDifinitionBuilder : IMongoUpdateDifinitionBuilder<Notification>
    {
        INotificationMongoUpdateDifinitionBuilder UseIsRead();
    }
}
