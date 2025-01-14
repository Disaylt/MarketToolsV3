using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;

namespace Identity.Application.Queries
{
    public class GetIdentityDetailsQueryHandler(IIdentityPersonService identityPersonService)
        : IRequestHandler<GetIdentityDetailsQuery, IdentityDetailsDto>
    {
        private const string DefaultValue = "Неизвестно";

        public async Task<IdentityDetailsDto> Handle(GetIdentityDetailsQuery request, CancellationToken cancellationToken)
        {
            IdentityPerson identity = await identityPersonService.FindByIdRequiredAsync(request.UserId);

            return new IdentityDetailsDto
            {
                Email = identity.Email ?? DefaultValue,
                Name = identity.UserName ?? DefaultValue,
                Id = identity.Id
            };
        }
    }
}
