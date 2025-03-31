using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegrationEvents.Contract.Identity;
using MassTransit;
using MediatR;
using WB.Seller.Companies.Application.Commands;

namespace WB.Seller.Companies.Processor.Consumers
{
    public class IdentityCreatedConsumer(IMediator mediator) : IConsumer<IdentityCreatedIntegrationEvent>
    {
        public async Task Consume(ConsumeContext<IdentityCreatedIntegrationEvent> context)
        {
            AddUserCommand command = new()
            {
                Login = context.Message.Login,
                UserId = context.Message.IdentityId
            };

            await mediator.Send(command);
        }
    }
}
