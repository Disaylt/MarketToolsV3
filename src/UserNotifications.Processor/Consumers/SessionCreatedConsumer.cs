using IntegrationEvents.Contract.Identity;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserNotifications.Applications.Commands;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;

namespace UserNotifications.Processor.Consumers
{
    internal class SessionCreatedConsumer(IMediator mediator, IOptions<ServicesAddressesConfig> servicesAddressesConfig) 
        : IConsumer<SessionCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<SessionCreatedIntegrationEvent> context)
        {
            CreateNotificationCommand command = new()
            {
                UserId = context.Message.UserId,
                Category = servicesAddressesConfig.Value.Identity.Name,
                Title = "Система безопасности",
                Message = $"Выполнен вход с устройства - {context.Message.UserAgent}"
            };

            await mediator.Send(command);
        }
    }
}
