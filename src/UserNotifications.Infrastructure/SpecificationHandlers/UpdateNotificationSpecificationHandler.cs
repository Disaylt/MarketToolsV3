using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.UpdateDetails;
using UserNotifications.Infrastructure.Database;
using UserNotifications.Infrastructure.Services;
using UserNotifications.Infrastructure.Utilities;

namespace UserNotifications.Infrastructure.SpecificationHandlers
{
    internal class UpdateNotificationSpecificationHandler(IMongoCollection<Notification> collection,
        IClientSessionHandleContext clientSessionHandleContext,
        IMongoUpdateDifinitionService<INotificationUpdateDetails, Notification> mongoUpdateDifinitionService)
        : ISpecificationHandler<UpdateNotificationSpecification>
    {
        public async Task HandleAsync(UpdateNotificationSpecification specification)
        {
            var update = mongoUpdateDifinitionService.Create(specification.Data);

            var mongoFilter = MongoFilterUtility.CreateOrEmpty(specification.Filter);

            await collection.UpdateManyAsync(clientSessionHandleContext.Session, mongoFilter, update);
        }
    }
}
