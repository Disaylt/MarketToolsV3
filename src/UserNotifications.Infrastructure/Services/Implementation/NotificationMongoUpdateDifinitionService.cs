using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Infrastructure.Services.Abstract;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

namespace UserNotifications.Infrastructure.Services.Implementation
{
    internal class NotificationMongoUpdateDifinitionService(IUpdateDefinitionFactory updateDefinitionFactory)
        : IMongoUpdateDifinitionService<INotificationNewFieldsData, Notification>
    {
        public virtual UpdateDefinition<Notification> Create(INotificationNewFieldsData update)
        {
            return updateDefinitionFactory
                .NotificationUpdateBuilder
                .Invoke(update)
                .UseIsRead()
                .Build();
        }
    }
}
