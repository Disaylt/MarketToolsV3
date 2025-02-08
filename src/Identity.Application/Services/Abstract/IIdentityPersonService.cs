using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Application.Services.Abstract
{
    public interface IIdentityPersonService
    {
        Task<IdentityPerson> AddAsync(IdentityPerson identity, string password);
        Task<IdentityPerson?> FindByEmailAsync(string email);
        Task<IdentityPerson?> FindByIdAsync(string id);
        Task<IdentityPerson> FindByIdRequiredAsync(string id);
        Task<bool> CheckPassword(IdentityPerson identity, string password);
    }
}
