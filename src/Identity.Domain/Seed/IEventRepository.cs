using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public interface IEventRepository
    {
        public IReadOnlyCollection<INotification> Notifications { get; }
        void RemoveNotification(INotification notification);
        void AddNotification(INotification notification);
        void ClearNotifications();
    }
}
