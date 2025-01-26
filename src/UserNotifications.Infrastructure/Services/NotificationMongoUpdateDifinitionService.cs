using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.UpdateDetails;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;

namespace UserNotifications.Infrastructure.Services
{
    internal class NotificationMongoUpdateDifinitionService(IUpdateDefinitionFactory updateDefinitionFactory)
        : IMongoUpdateDifinitionService<INotificationUpdateDetails, Notification>
    {
        public virtual UpdateDefinition<Notification> Create(INotificationUpdateDetails update)
        {
            return updateDefinitionFactory
                .NotificationUpdateBuilder
                .Invoke(update)
                .UseIsRead()
                .Build();
        }
    }
}
