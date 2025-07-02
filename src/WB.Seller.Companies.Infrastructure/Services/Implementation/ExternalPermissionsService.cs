using Grpc.Net.Client;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;
using Proto.Contract.Common.PermissionStore;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure.Utilities.Abstract;

namespace WB.Seller.Companies.Infrastructure.Services.Implementation;

public class ExternalPermissionsService
    : IDisposable, IPermissionsExternalService
{
    private readonly GrpcChannel _grpcChannel;
    private readonly Permission.PermissionClient _permissionClient;
    private readonly ServicesAddressesConfig _servicesAddressesConfig;
    private readonly IPermissionMapperUtility _permissionMapperUtility;

    public ExternalPermissionsService(
        IOptions<ServicesAddressesConfig> addressesOptions,
        GrpcChannelOptions grpcChannelOptions,
        IPermissionMapperUtility permissionMapperUtility)
    {
        _servicesAddressesConfig = addressesOptions.Value;

        string address = _servicesAddressesConfig
                             .Common
                             .Permissions
                             .Addresses
                             .GetOrDefaultRandomGrpcAddress()
                         ?? throw new NullReferenceException("Permissions grpc address not found.");

        _grpcChannel = GrpcChannel.ForAddress(address, grpcChannelOptions);
        _permissionClient = new Permission.PermissionClient(_grpcChannel);
        _permissionMapperUtility = permissionMapperUtility;
    }

    public void Dispose()
    {
        _grpcChannel.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<PermissionSettingNodeDto>> GetPermissionsSettingTreeAsync(IEnumerable<PermissionDto> userPermissions)
    {
        PermissionTreeRequest request = new()
        {
            Module = GetWbSellerCompanyModuleName(),
            Permissions =
            {
                userPermissions
                    .Select(x => new PermissionSetting
                    {
                        Path = x.Path,
                        Status = (PermissionStatus)x.Status
                    })
            }
        };

        var permissionsTreeResponse = await _permissionClient.GetPermissionTreeAsync(request);

        return _permissionMapperUtility.MapPermissionSettingNodes(permissionsTreeResponse.PermissionsTree);
    }

    private string GetWbSellerCompanyModuleName()
    {
        return _servicesAddressesConfig
            .Wb
            .Seller
            .Companies
            .Name;
    }
}