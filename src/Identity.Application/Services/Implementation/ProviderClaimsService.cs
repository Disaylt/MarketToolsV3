using Identity.Application.Models;
using Identity.Application.QueryObjects;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Seed;

namespace Identity.Application.Services.Implementation
{
    public class ProviderClaimsService(
        IQueryObjectHandler<FindServiceQueryObject, Service> findServiceQueryObjectHandler,
        IQueryableHandler<Service, ServiceAuthInfoDto> serviceToServiceAuthQueryableHandler,
        IExtensionRepository extensionRepository)
    : IProviderClaimsService
    {
        public async Task<ServiceAuthInfoDto?> FindOrDefault(int? providerType, int? providerId)
        {
            if (providerType == null || providerId == null)
            {
                return null;
            }

            FindServiceQueryObject queryObject = new()
            {
                ProviderType = providerType.Value,
                ProviderId = providerId.Value
            };

            var queryObjectResult = findServiceQueryObjectHandler.Create(queryObject);
            var serviceAuthInfoQuery = await serviceToServiceAuthQueryableHandler.HandleAsync(queryObjectResult);

            return await extensionRepository.FirstOrDefaultAsync(serviceAuthInfoQuery);
        }
    }
}
