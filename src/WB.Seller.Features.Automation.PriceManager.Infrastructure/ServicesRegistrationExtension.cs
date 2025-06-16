using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Grpc.Net.Client;
using Polly;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure;

public static class ServicesRegistrationExtension
{
    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient("grpc")
            .AddStandardResilienceHandler(opt =>
            {
                opt.Retry.MaxRetryAttempts = 3;
                opt.Retry.Delay = TimeSpan.FromSeconds(1);
                opt.Retry.BackoffType = DelayBackoffType.Exponential;
            });

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