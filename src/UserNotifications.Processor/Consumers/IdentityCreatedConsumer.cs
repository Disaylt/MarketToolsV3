using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract.Identity;
using MassTransit;
using MediatR;
using UserNotifications.Applications.Commands;

namespace UserNotifications.Processor.Consumers
{
    internal class IdentityCreatedConsumer(IMediator mediator) : IConsumer<IdentityCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<IdentityCreatedIntegrationEvent> context)
        {
            CreateNotificationCommand command = new()
            {
                UserId = context.Message.IdentityId,
                Message = "Вы успешно зарегистрированы в сервисе автоматизации и управлению маркетплейсов."
            };

            await mediator.Send(command);
        }
    }
}
