﻿using IntegrationEvents.Contract.Identity;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserNotifications.Applications.Commands;

namespace UserNotifications.Processor.Consumers
{
    internal class SessionCreatedConsumer(IMediator mediator) : IConsumer<SessionCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<SessionCreatedIntegrationEvent> context)
        {
            CreateNotificationCommand command = new()
            {
                UserId = context.Message.UserId,
                Message = $"Выполнен вход с помощью устройства - {context.Message.UserAgent}"
            };

            await mediator.Send(command);
        }
    }
}