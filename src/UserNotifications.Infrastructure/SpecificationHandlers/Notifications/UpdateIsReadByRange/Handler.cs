using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications.Notifications.UpdateIsReadByRange;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Services.Abstract;
using UserNotifications.Infrastructure.Utilities;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

namespace UserNotifications.Infrastructure.SpecificationHandlers.Notifications.UpdateIsReadByRange
{
    internal class UpdateIsReadByRangeFilterNotificationSpecififcationHandler(IMongoCollection<Notification> collection,
        IClientSessionHandleContext clientSessionHandleContext,
        IMongoUpdateDifinitionService<INotificationNewFieldsData, Notification> mongoUpdateDifinitionService)
        : ISpecificationHandler<UpdateIsReadByRangeFilterNotificationSpecififcation>
    {
        public async Task HandleAsync(UpdateIsReadByRangeFilterNotificationSpecififcation specification)
        {
            INotificationNewFieldsData newFieldsData = CreateNewFiledData(specification.Data);
            var update = mongoUpdateDifinitionService.Create(newFieldsData);

            var mongoFilter = Builders<Notification>.Filter
                .Where(x => x.UserId == specification.Filter.UserId
                    && x.IsRead == false
                    && x.Created >= specification.Filter.FromDate
                    && x.Created <= specification.Filter.ToDate);

            await collection.UpdateManyAsync(clientSessionHandleContext.Session, mongoFilter, update);
        }

        private INotificationNewFieldsData CreateNewFiledData(EntityData data)
        {
            return new NotificationNewFieldsData
            {
                IsRead = data.IsRead
            };
        }
    }
}
