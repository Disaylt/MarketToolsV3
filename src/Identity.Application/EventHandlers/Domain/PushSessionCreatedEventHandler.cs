using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Events;
using IntegrationEvents.Contract.Identity;
using MarketToolsV3.IntegrationEventLogService.Services.Abstract;
using MassTransit;
using MediatR;

namespace Identity.Application.EventHandlers.Domain
{
    public class PushSessionCreatedEventHandler(IIntegrationEventLogService integrationEventLogService)
        : INotificationHandler<SessionCreated>
    {
        public async Task Handle(SessionCreated notification, CancellationToken cancellationToken)
        {
            SessionCreatedIntegrationEvent integrationMessage = new()
            {
                SessionId = notification.Session.Id,
                UserId = notification.Session.IdentityId,
                UserAgent = notification.Session.UserAgent
            };

            await integrationEventLogService.SaveEventAsync(integrationMessage, cancellationToken);
        }
    }
}
