using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Applications.Specifications;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Infrastructure.SpecificationHandlers
{
    internal class NotificationsSpecificationHandler(IMongoCollection<Notification> collection)
        : ISpecificationHandler<GetRangeNotificationsSpecification, Notification>
    {
        public Task<Notification> HandleAsync(GetRangeNotificationsSpecification specification)
        {
            throw new NotImplementedException();
        }
    }
}
