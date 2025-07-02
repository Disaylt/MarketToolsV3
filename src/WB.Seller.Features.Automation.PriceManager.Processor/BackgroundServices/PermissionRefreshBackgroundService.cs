using Microsoft.Extensions.DependencyInjection;
using WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract;

namespace WB.Seller.Features.Automation.PriceManager.Processor.BackgroundServices;

public class PermissionRefreshBackgroundService(IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var scope = serviceProvider.CreateAsyncScope();

        await scope
            .ServiceProvider
            .GetRequiredService<IExternalPermissionsService>()
            .RefreshPermissionsAsync();
    }
}