using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;
using MediatR;

namespace Identity.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly List<INotification> _notifications = new();
        public IReadOnlyCollection<INotification> Notifications => _notifications.AsReadOnly();

        public void AddNotification(INotification notification)
        {
            _notifications.Add(notification);
        }

        public void ClearNotifications()
        {
            _notifications.Clear();
        }

        public void RemoveNotification(INotification notification)
        {
            _notifications.Remove(notification);
        }
    }
}
