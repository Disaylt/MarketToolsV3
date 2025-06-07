using MarketToolsV3.PermissionStore.Application.Extensions;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Utilities.Abstract;
using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionsByFilterQueryHandler(
    IRepository<PermissionEntity> permissionsRepository,
    IExtensionRepository extensionRepository,
    IPermissionsUtility permissionsUtility)
    : IRequestHandler<GetPermissionsByFilterQuery, IEnumerable<PermissionViewDto>>
{
    public async Task<IEnumerable<PermissionViewDto>> Handle(GetPermissionsByFilterQuery request, CancellationToken cancellationToken)
    {
        IQueryable<PermissionViewDto> existsPermissionsQuery = permissionsRepository
            .AsQueryable()
            .WhereIf(string.IsNullOrEmpty(request.Module) == false, x=> x.Module == request.Module)
            .Select(x => new PermissionViewDto
            {
                Path = x.Path,
                ViewName = string.Empty
            });
        
        var permissions = await extensionRepository.ToListAsync(existsPermissionsQuery, cancellationToken);

        return permissions
            .Select(permission => permission with
            {
                ViewName = permissionsUtility.FindOrDefaultByPathView(permission.Path)
            });
    }
}