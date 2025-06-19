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

    public ExternalPermissionsService(
        IOptions<ServicesAddressesConfig> addressesOptions,
        GrpcChannelOptions grpcChannelOptions)
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
    }

    public async Task RefreshPermissionsAsync()
    {
        RefreshRequest request = new()
        {
            Permissions = { PermissionsStorage.Collection },
            Module = GetPermissionModuleName(),
            ParentModules = { GetWbSellerCompanyModuleName() }
        };

        await _permissionClient.RefreshAsync(request);
    }

    private string GetWbSellerCompanyModuleName()
    {
        return _servicesAddressesConfig
            .Wb
            .Seller
            .Companies
            .Name;
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