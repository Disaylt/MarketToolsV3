using MarketToolsV3.PermissionStore.Application.Extensions;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionsByFilterQueryHandler(
    IRepository<ModuleEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionsUtility permissionsUtility)
    : IRequestHandler<GetPermissionsByFilterQuery, IEnumerable<PermissionViewDto>>
{
    public async Task<IEnumerable<PermissionViewDto>> Handle(GetPermissionsByFilterQuery request, CancellationToken cancellationToken)
    {
        IQueryable<string> pathsQuery = permissionsRepository.BuildPathsQueryByParentModule(request.Module);
        var paths = await extensionRepository.ToListAsync(pathsQuery, cancellationToken);

        return permissionsUtility.MapFromPaths(paths);
    }
}