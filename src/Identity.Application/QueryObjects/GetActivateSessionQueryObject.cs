using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Domain.Seed;

namespace Identity.Application.QueryObjects
{
    public class GetActivateSessionQueryObject : IQueryObject<Session>
    {
        public required string IdentityId { get; set; }
        public int ExpireRefreshTokenHours { get; set; }
    }
}
