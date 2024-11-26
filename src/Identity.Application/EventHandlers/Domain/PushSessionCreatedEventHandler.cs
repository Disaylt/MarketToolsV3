using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Events;
using IntegrationEvents.Contract.Identity;
using MassTransit;
using MediatR;

namespace Identity.Application.EventHandlers.Domain
{
    public class PushSessionCreatedEventHandler(IBus bus)
        : INotificationHandler<SessionCreated>
    {
        public async Task Handle(SessionCreated notification, CancellationToken cancellationToken)
        {
            SessionCreatedIntegrationEvent integrationMessage = new SessionCreatedIntegrationEvent
            {
                SessionId = notification.Session.Id
            };

            await bus.Publish(integrationMessage, cancellationToken);
        }
    }
}
