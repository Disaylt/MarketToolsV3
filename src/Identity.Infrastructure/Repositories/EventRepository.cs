using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Infrastructure.Repositories
{
    public class EventRepository(IMediator mediator) 
        : IEventRepository
    {
        private readonly List<INotification> _notifications = [];
        public IReadOnlyCollection<INotification> Notifications => _notifications.AsReadOnly();

        public void AddNotification(INotification notification)
        {
            _notifications.Add(notification);
        }

        public void ClearNotifications()
        {
            _notifications.Clear();
        }

        public async Task PublishAllAsync(CancellationToken cancellationToken)
        {
            foreach (INotification notification in _notifications)
            {
                await mediator.Publish(notification, cancellationToken);
            }

            ClearNotifications();
        }

        public void RemoveNotification(INotification notification)
        {
            _notifications.Remove(notification);
        }
    }
}
