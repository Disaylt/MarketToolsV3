using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Models;

namespace Identity.Application.Services.Abstract
{
    public interface IProviderClaimsService
    {
        Task<ServiceAuthInfoDto?> FindOrDefault(int? categoryId, int? providerId);
    }
}
