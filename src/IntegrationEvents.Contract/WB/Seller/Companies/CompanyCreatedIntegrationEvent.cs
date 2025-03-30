using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract.WB.Seller.Companies
{
    public record CompanyCreatedIntegrationEvent : BaseIntegrationEvent
    {
        public required int CompanyId { get; init; }
        public required string UserId { get; init; }
        public string? Token { get; set; }
    }
}
