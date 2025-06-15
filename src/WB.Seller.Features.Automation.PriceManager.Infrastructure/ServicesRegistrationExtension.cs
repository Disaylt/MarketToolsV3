using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Grpc.Net.Client;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure;

public static class ServicesRegistrationExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient("grpc");
        serviceCollection.AddSingleton(serviceProvider => new GrpcChannelOptions
        {
            HttpClient = serviceProvider
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("grpc"),
            DisposeHttpClient = false
        });

        return serviceCollection;
    }
}