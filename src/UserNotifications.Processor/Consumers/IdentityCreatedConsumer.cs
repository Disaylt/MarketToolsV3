using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract.Identity;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using MassTransit.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using UserNotifications.Applications.Commands;

namespace UserNotifications.Processor.Consumers
{
    public class IdentityCreatedConsumer(IMediator mediator, IOptions<ServicesAddressesConfig> servicesAddressesConfig) 
        : IConsumer<IdentityCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<IdentityCreatedIntegrationEvent> context)
        {
            CreateNotificationCommand command = new()
            {
                UserId = context.Message.IdentityId,
                Category = servicesAddressesConfig.Value.Identity.Name,
                Title = "Приветствие",
                Message = "Доброе пожаловать! Рады видеть вас. Совершенствуйте уровень управления вашего бизнеса. " +
                          "Анализируйте, собирайте статистику и будьте в курсе всех событий ваших компаний."
            };

            await mediator.Send(command);
        }
    }
}
