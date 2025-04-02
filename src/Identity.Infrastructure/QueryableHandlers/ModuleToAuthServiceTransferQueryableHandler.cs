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
    public class ModuleToAuthServiceTransferQueryableHandler : IQueryableHandler<Module, ModuleAuthInfoDto>
    {
        public Task<IQueryable<ModuleAuthInfoDto>> HandleAsync(IQueryable<Module> query)
        {
            var result = query
                .Select(x => new ModuleAuthInfoDto
                {
                    Path = x.Path,
                    Id = x.ExternalId,

                    ClaimTypeAndValuePairs = x.Claims
                        .Select(moduleClaim=> new ModuleClaimDto
                        {
                            Type = moduleClaim.Type,
                            Value = moduleClaim.Value
                        })
                        .ToList(),

                    Roles = x.Roles
                        .Select(r => r.Value)
                        .ToList()
                });


            return Task.FromResult(result);
        }
    }
}
