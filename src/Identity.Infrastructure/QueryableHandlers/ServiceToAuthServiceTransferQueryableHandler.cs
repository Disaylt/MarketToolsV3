using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Domain.Entities;
using Identity.Domain.Seed;

namespace Identity.Infrastructure.QueryableHandlers
{
    public class ServiceToAuthServiceTransferQueryableHandler : IQueryableHandler<Service, ServiceAuthInfoDto>
    {
        public Task<IQueryable<ServiceAuthInfoDto>> HandleAsync(IQueryable<Service> query)
        {
            var result = query
                .Select(x => new ServiceAuthInfoDto
                {
                    ProviderType = x.ProviderType,
                    ProviderId = x.ProviderId,
                    ClaimTypeAndValuePairs = x.Claims
                        .ToDictionary(c => c.Type, c => c.Value)

                });

            return Task.FromResult(result);
        }
    }
}
