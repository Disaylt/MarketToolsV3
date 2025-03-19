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
    public class ServiceToAuthServiceTransferQueryableHandler : IQueryableHandler<Module, ModuleAuthInfoDto>
    {
        public Task<IQueryable<ModuleAuthInfoDto>> HandleAsync(IQueryable<Module> query)
        {
            var result = query
                .Select(x => new ModuleAuthInfoDto
                {
                    Type = x.Type,
                    Path = x.Path,
                    Id = x.ExternalId,
                    ClaimTypeAndValuePairs = x.Claims
                        .ToDictionary(c => c.Type, c => c.Value),
                    Roles = x.Roles.Select(r => r.Value).ToList()

                });

            return Task.FromResult(result);
        }
    }
}
