using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Grpc.Net.Client;
using Polly;
using WB.Seller.Features.Automation.PriceManager.Infrastructure.Services.Implementation;
using Microsoft.Extensions.Hosting;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager.Models;
using MarketToolsV3.ConfigurationManager;
using WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract;

namespace WB.Seller.Features.Automation.PriceManager.Infrastructure;

public static class ServicesRegistrationExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient("grpc")
            .AddStandardResilienceHandler(opt =>
            {
                opt.Retry.MaxRetryAttempts = 4;
                opt.Retry.Delay = TimeSpan.FromSeconds(2);
                opt.Retry.BackoffType = DelayBackoffType.Exponential;
            });

        serviceCollection.AddSingleton(serviceProvider => new GrpcChannelOptions
        {
            HttpClient = serviceProvider
                .GetRequiredService<IHttpClientFactory>()
                .CreateClient("grpc"),
            DisposeHttpClient = false
        });

        serviceCollection.AddScoped<IExternalPermissionsService, ExternalPermissionsService>();
        return serviceCollection;
    }

    public static async Task AddConfigs(this IHostApplicationBuilder hostApplicationBuilder)
    {
        ConfigurationServiceFactory configurationServiceFactory = new(hostApplicationBuilder.Configuration);

        ITypingConfigManager<ServicesAddressesConfig> addressesConfig = await configurationServiceFactory.CreateFromServicesAddressesAsync();
        addressesConfig.AddAsOptions(hostApplicationBuilder.Services);
    }
}