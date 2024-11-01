using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Application.Services
{
    public interface IIdentityPersonService
    {
        Task<IdentityPerson> AddAsync(IdentityPerson identity, string password);
        Task<IdentityPerson?> FindByEmailAsync(string email);
        Task<bool> CheckPassword(IdentityPerson identity, string password);
    }
}
