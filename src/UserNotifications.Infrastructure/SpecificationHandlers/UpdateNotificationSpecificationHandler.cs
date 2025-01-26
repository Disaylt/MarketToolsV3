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
using UserNotifications.Infrastructure.Services;
using UserNotifications.Infrastructure.Utilities;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

namespace UserNotifications.Infrastructure.SpecificationHandlers
{
    internal class UpdateNotificationSpecificationHandler(IMongoCollection<Notification> collection,
        IClientSessionHandleContext clientSessionHandleContext,
        IMongoUpdateDifinitionService<INotificationNewFieldsData, Notification> mongoUpdateDifinitionService)
        : ISpecificationHandler<UpdateIsReadByRangeFilterNotificationSpecififcation>
    {
        public async Task HandleAsync(UpdateIsReadByRangeFilterNotificationSpecififcation specification)
        {
            var update = mongoUpdateDifinitionService.Create(specification.Data);

            var mongoFilter = MongoFilterUtility.CreateOrEmpty(specification.Filter);

            await collection.UpdateManyAsync(clientSessionHandleContext.Session, mongoFilter, update);
        }
    }
}
