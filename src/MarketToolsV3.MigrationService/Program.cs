using Identity.Domain.Constant;
using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager;
using MarketToolsV3.MigrationService.Registrations;
using Microsoft.Extensions.DependencyInjection;
using MarketToolsV3.MigrationService.Hosts;
using Microsoft.Extensions.Options;
using MarketToolsV3.MigrationService;
using MarketToolsV3.MigrationService.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IMigrationTaskService, MigrationTaskService>();

builder.AddServiceDefaults();
builder.AddIdentityContext();

builder.AddMigrationHostService<EfMigrationBackgroundService<IdentityDbContext>>();

builder.Services.AddHostedService<HostLifeBackgroundService>();

var host = builder.Build();

host.Run();
