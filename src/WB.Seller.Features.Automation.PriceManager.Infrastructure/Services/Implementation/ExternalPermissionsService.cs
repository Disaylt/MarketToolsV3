using Grpc.Core;
using Grpc.Net.Client;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;
using Proto.Contract.Common.PermissionStore;
using WB.Seller.Features.Automation.PriceManager.Application.Seed;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure.Services.Implementation;

public class ExternalPermissionsService
    : IAsyncDisposable, IDisposable
{
    private readonly GrpcChannel _grpcChannel;
    private readonly ModuleConfig _permissionsConfig;
    private readonly Permission.PermissionClient _permissionClient;

    public ExternalPermissionsService(
        IOptions<ServicesAddressesConfig> addressesOptions,
        GrpcChannelOptions grpcChannelOptions)
    {
        _permissionsConfig = addressesOptions
                        .Value
                        .Common
                        .Permissions;

        string address = _permissionsConfig
                         .Addresses
                         .GetOrDefaultRandomGrpcAddress()
                         ?? throw new NullReferenceException("Permissions grpc address not found.");

        _grpcChannel = GrpcChannel.ForAddress(address, grpcChannelOptions);
        _permissionClient = new Permission.PermissionClient(_grpcChannel);
    }

    public async Task RefreshPermissionsAsync()
    {
        RefreshRequest request = new RefreshRequest
        {
            Permissions = { PermissionsStorage.Collection },
            Module = _permissionsConfig.Name
        };

        await _permissionClient.RefreshAsync(request);
    }

    public ValueTask DisposeAsync()
    {
        Dispose();

        return ValueTask.CompletedTask;
    }

    public void Dispose()
    {
        _grpcChannel.Dispose();
    }
}