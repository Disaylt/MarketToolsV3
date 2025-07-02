using Grpc.Core;
using Grpc.Net.Client;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;
using Proto.Contract.Common.PermissionStore;
using WB.Seller.Features.Automation.PriceManager.Application.Seed;
using WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure.Services.Implementation;

public class ExternalPermissionsService
    : IDisposable, IExternalPermissionsService
{
    private readonly GrpcChannel _grpcChannel;
    private readonly Permission.PermissionClient _permissionClient;
    private readonly ServicesAddressesConfig _servicesAddressesConfig;
    private readonly IPermissionsService _permissionsService;

    public ExternalPermissionsService(
        IOptions<ServicesAddressesConfig> addressesOptions,
        GrpcChannelOptions grpcChannelOptions,
        IPermissionsService permissionsService)
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
        _permissionsService = permissionsService;
    }

    public async Task RefreshPermissionsAsync()
    {
        RefreshRequest request = new()
        {
            Creator = GetPermissionModuleName(),
            Permissions = { GetPermissionsInfo() }
        };

        await _permissionClient.RefreshAsync(request);
    }

    private IEnumerable<PermissionInfo> GetPermissionsInfo()
    {
        return _permissionsService
            .GetPermissions()
            .Select(x => new PermissionInfo
            {
                AvailableModules = { x.AvailableModules },
                Path = x.Path,
                RequireUse = x.RequireUse
            });
    }

    private string GetPermissionModuleName()
    {
        return _servicesAddressesConfig
            .Wb
            .Seller
            .Automation
            .PriceManager
            .Name;
    }

    public void Dispose()
    {
        _grpcChannel.Dispose();
        GC.SuppressFinalize(this);
    }
}