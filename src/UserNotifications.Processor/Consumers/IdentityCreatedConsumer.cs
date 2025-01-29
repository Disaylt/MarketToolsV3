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
                Message = "Доброе пожаловать! Рады видеть вас на нашей платформе. Здесь вы сможете эффективно управлять своими компаниями на маркетплейсах."
            };

            await mediator.Send(command);
        }
    }
}
