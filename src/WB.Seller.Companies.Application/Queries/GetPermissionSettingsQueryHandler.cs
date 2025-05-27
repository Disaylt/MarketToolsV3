using MarketToolsV3.ConfigurationManager.Models;
using MediatR;
using Microsoft.Extensions.Options;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Permissions;
using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Queries;

public class GetPermissionSettingsQueryHandler(
    IPermissionsExternalService permissionsExternalService,
    IOptions<ModulesInfoConfig> modulesInfoOptions,
    IQueryDataHandler<SearchPermissionsQueryData, IEnumerable<PermissionDto>> permissionsQueryDataHandler)
    : IRequestHandler<GetPermissionSettingsQuery, IEnumerable<PermissionSettingDto>>
{
    public async Task<IEnumerable<PermissionSettingDto>> Handle(GetPermissionSettingsQuery request, CancellationToken cancellationToken)
    {
        string module = modulesInfoOptions.Value.Marketplaces.Wb;
        var allPermissions = await permissionsExternalService.GetRangeByModuleAsync(module);

        var subscriberPermissionsQueryData = new SearchPermissionsQueryData
        {
            CompanyId = request.CompanyId,
            SubscriberId = request.SubscriberId
        };

        var typeAndSubscriberPermissionPairs = await permissionsQueryDataHandler
            .HandleAsync(subscriberPermissionsQueryData)
            .ContinueWith(task => task.Result.ToDictionary(p => p.Path), cancellationToken);

        List<PermissionSettingDto> permissionSettings = [];

        foreach (var permissionSettingDto in allPermissions)
        {

        }

        return [];
    }
}