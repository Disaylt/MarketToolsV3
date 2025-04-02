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
    public class ModulePermissionsService(
        IQueryObjectHandler<FindModuleQueryObject, Module> findServiceQueryObjectHandler,
        IQueryableHandler<Module, ModuleAuthInfoDto> serviceToServiceAuthQueryableHandler,
        IExtensionRepository extensionRepository)
    : IModulePermissionsService
    {
        public async Task<ModuleAuthInfoDto?> FindOrDefault(string? path, string? userId, int? id, CancellationToken cancellationToken)
        {
            if (path == null
                || id == null 
                || userId == null)
            {
                return null;
            }

            FindModuleQueryObject queryObject = new()
            {
                Path = path,
                UserId = userId,
                Id = id.Value,
            };

            var queryObjectResult = findServiceQueryObjectHandler.Create(queryObject);
            var serviceAuthInfoQuery = await serviceToServiceAuthQueryableHandler.HandleAsync(queryObjectResult);

            return await extensionRepository.FirstOrDefaultAsync(serviceAuthInfoQuery, cancellationToken);
        }
    }
}
