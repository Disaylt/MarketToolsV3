using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WB.Seller.Companies.Domain.Entities;

namespace WB.Seller.Companies.Domain.Events
{
    public class CompanyCreatedDomainEvent(CompanyEntity company) : INotification
    {
        public CompanyEntity Company { get; } = company;
    }
}
