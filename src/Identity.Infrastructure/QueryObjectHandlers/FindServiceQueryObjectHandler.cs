using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.QueryObjects;
using Identity.Domain.Entities;
using Identity.Domain.Seed;

namespace Identity.Infrastructure.QueryObjectHandlers
{
    public class FindServiceQueryObjectHandler(IRepository<Service> serviceRepository)
        : IQueryObjectHandler<FindServiceQueryObject, Service>
    {
        public IQueryable<Service> Create(FindServiceQueryObject query)
        {
            return serviceRepository
                .AsQueryable()
                .Where(x => x.ProviderType == query.ProviderType && x.ProviderId == query.ProviderId);
        }
    }
}
