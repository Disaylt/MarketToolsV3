using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract.Identity;
using MassTransit;
using MediatR;
using UserNotifications.Applications.Commands;
using UserNotifications.Domain.Enums;

namespace UserNotifications.Processor.Consumers
{
    public class IdentityCreatedConsumer(IMediator mediator) : IConsumer<IdentityCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<IdentityCreatedIntegrationEvent> context)
        {
            CreateNotificationCommand command = new()
            {
                UserId = context.Message.IdentityId,
                Category = Category.Identity,
                Title = "Приветствие",
                Message = "Доброе пожаловать! Рады видеть вас. Совершенствуйте уровень управления вашего бизнеса. " +
                          "Анализируйте, собирайте статистику и будьте в курсе всех событий ваших компаний."
            };

            await mediator.Send(command);
        }
    }
}
