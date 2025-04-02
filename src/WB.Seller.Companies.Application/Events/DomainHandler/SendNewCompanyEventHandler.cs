using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Domain.Events;

namespace WB.Seller.Companies.Application.Events.DomainHandler
{
    public class SendNewCompanyEventHandler : INotificationHandler<CompanyCreatedDomainEvent>
    {
        public Task Handle(CompanyCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
