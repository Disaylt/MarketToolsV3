﻿using System;
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
    public class PushIdentityCreatedEventHandler(IBus bus)
        : INotificationHandler<IdentityCreated>
    {
        public async Task Handle(IdentityCreated notification, CancellationToken cancellationToken)
        {
            IdentityCreatedIntegrationEvent integrationMessage = new IdentityCreatedIntegrationEvent
            {
                IdentityId = notification.Identity.Id
            };

            await bus.Publish(integrationMessage, cancellationToken);
        }
    }
}
