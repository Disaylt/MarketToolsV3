using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Mappers.Abstract;
using Identity.Application.Models;
using Identity.Domain.Entities;

namespace Identity.Application.Mappers.Implementation
{
    internal class SessionDtoMapper : ISessionMapper<SessionDto>
    {
        public SessionDto Map(Session model)
        {
            return new SessionDto
            {
                CreateDate = model.Created,
                Updated = model.Updated,
                Id = model.Id,
                IsActive = model.IsActive,
                UserAgent = model.UserAgent,
                UserId = model.IdentityId
            };
        }
    }
}
