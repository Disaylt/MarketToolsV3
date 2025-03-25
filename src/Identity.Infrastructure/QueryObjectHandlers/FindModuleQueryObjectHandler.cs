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
    public class FindModuleQueryObjectHandler(IRepository<Module> serviceRepository)
        : IQueryObjectHandler<FindModuleQueryObject, Module>
    {
        public IQueryable<Module> Create(FindModuleQueryObject query)
        {
            return serviceRepository
                .AsQueryable()
                .Where(x => x.Type == query.Type 
                            && x.ExternalId == query.Id 
                            && x.Path == query.Path
                            && x.IdentityId == query.UserId);
        }
    }
}
