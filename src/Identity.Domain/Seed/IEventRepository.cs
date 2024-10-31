using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    internal interface IEventRepository
    {
        public IReadOnlyCollection<INotification> Notifications { get; }
        Task PublishAllAsync();
        void RemoveNotification(INotification notification);
        void AddNotification(INotification notification);
        void ClearNotifications();
    }
}
