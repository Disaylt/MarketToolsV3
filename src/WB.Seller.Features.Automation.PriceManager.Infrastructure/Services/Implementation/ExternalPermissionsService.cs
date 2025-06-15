using Grpc.Net.Client;
using MarketToolsV3.ConfigurationManager.Models;
using Microsoft.Extensions.Options;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure.Services.Implementation;

public class ExternalPermissionsService(
    IOptions<ServicesAddressesConfig> addressesOptions)
    : IAsyncDisposable, IDisposable
{
    
    public async Task RefreshPermissionsAsync()
    {
        GrpcChannelOptions channelOptions = new GrpcChannelOptions
        {

        }
    }

    public ValueTask DisposeAsync()
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}