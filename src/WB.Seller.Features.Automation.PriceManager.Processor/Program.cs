using WB.Seller.Features.Automation.PriceManager.Infrastructure;
using WB.Seller.Features.Automation.PriceManager.Processor.BackgroundServices;

var builder = Host.CreateApplicationBuilder(args);
builder.AddServiceDefaults();
builder.Services.AddInfrastructureServices();

await builder.AddConfigs();

builder.Services.AddHostedService<PermissionRefreshBackgroundService>();

var host = builder.Build();
host.Run();
